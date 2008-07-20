//determines the identikey dependencies of nodes
//i.e. the names of identikeys in the outer scope referenced by this node
//the set returned should never be null (though may be empty set)

using System;
using System.Collections;
using System.Collections.Generic;
using Reflection = System.Reflection;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public static class Depends {
	//unions dependencies of nodes extracted from params
	static HashSet<Identifier> collectDepends( params object[] args ) {
		return unionDepends(G.collect<INode>(args));
	}

	//unions dependencies of nodes in collection
	static HashSet<Identifier> unionDepends(ICollection<INode> args) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		foreach( INode node in args ) {
			if( node != null )
				idents.UnionWith( Depends.depends(node) );
		}
		return idents;
	}
	
	//unions dependencies of child nodes
	static HashSet<Identifier> unionChildDepends(INode node) {
		return ( node.childNodes == null ?
		        new HashSet<Identifier>() :
		        unionDepends(node.childNodes) );
	}
	
	//compound
	//xxx may want to remove references to global identikeys (optimization)
	public static HashSet<Identifier> depends(Node_Compound node) {
		HashSet<Identifier> idents = unionChildDepends(node);
		foreach( INode_Expression member in node.members ) {
			if( member is Node_DeclareAssign )
				idents.Remove( (member as Node_DeclareAssign).name.value );
			else if( member is Node_DeclareEmpty )
				idents.Remove( (member as Node_DeclareEmpty).name.value );
		}
		return idents;
	}

	//declare-assign
	public static HashSet<Identifier> depends(Node_DeclareAssign node) {
		HashSet<Identifier> idents = collectDepends(node.type, node.value);
		idents.Remove( node.name.value );
		return idents;
	}
	
	//declare-first
	public static HashSet<Identifier> depends(Node_DeclareFirst node) {
		HashSet<Identifier> idents = collectDepends(node.type, node.value);
		if( ! node.overload.value )
			idents.Remove(node.name.value);
		return idents;
	}
	
	//extract-member
	public static HashSet<Identifier> depends(Node_ExtractMember node) {
		return depends(node.source);
	}
	
	//function
	public static HashSet<Identifier> depends(Node_Function node) {
		HashSet<Identifier> idents = depends(node.body);
		foreach( Node_ParameterImpl param in node.parameterImpls )
			idents.Remove(param.name.value);
		foreach( Node_ParameterImpl param in node.parameterImpls )
			idents.UnionWith( depends(param) );
		idents.UnionWith( depends(node.returnType) );
		return idents;
	}
	
	//identifier
	public static HashSet<Identifier> depends(Node_Identifier node) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		idents.Add(node.value);
		return idents;
	}
	
	//sieve
	//xxx temporary
	public static HashSet<Identifier> depends(Node_Sieve node) {
		HashSet<Identifier> idents = collectDepends(node.hidables);
		foreach( Node_Hidable child in node.hidables ) {
			if( child.declaration is Node_DeclareFirst ) {
				Node_DeclareFirst df = (Node_DeclareFirst)child.declaration;
				if( ! df.overload.value )
					idents.Remove(df.name.value);
			}
		}
		return idents;
	}
	
	//method
	public static HashSet<Identifier> depends(Node_Method node) {
		return depends(node.@interface);
	}
	
	//module
	//identikeys that must be exposed by import and/or expose nodes
	public static HashSet<Identifier> depends(Node_Module node) {
		return depends(node.sieve);
	}
	
	//parameter-impl
	public static HashSet<Identifier> depends(Node_ParameterImpl node) {
		return collectDepends(node.defaultValue, node.type);
	}
	
	//parameter-info
	public static HashSet<Identifier> depends(Node_ParameterInfo node) {
		return depends(node.type);
	}
	
	//property
	public static HashSet<Identifier> depends(Node_Property node) {
		return depends(node.type);
	}
	
	//used by the function below
	static HashSet<Identifier> dependsDefault(INode node) {
		string name = node.typeName;
		if( name == "import" )
			return new HashSet<Identifier>();
	
		return unionChildDepends(node);
	}
	
	//any node
	public static HashSet<Identifier> depends(INode node) {
		System.Type classType = typeof(Depends);
		System.Type nodeType = ((Object)node).GetType();
		Reflection.MethodInfo meth = classType.GetMethod(
			"depends", new System.Type[]{nodeType});
		//if unable to find a more specific function
		if( meth.GetParameters()[0].ParameterType == typeof(INode) )
			return dependsDefault(node);
		try {
			return (HashSet<Identifier>)meth.Invoke(null, new object[]{node});
		}
		catch(Reflection.TargetInvocationException e) {
			if( e.InnerException is ClientException )
				throw e.InnerException;
			throw e;
		}
	}
	
	//function
	public static DependsResults dependsSplit(Node_Function node) {
		//xxx temporary
		DependsResults source = new DependsResults(null,null);
		foreach( Node_ParameterImpl pi in node.parameterImpls )
			source.executeDepends.UnionWith(depends(pi));
		source.executeDepends.UnionWith(depends(node.returnType));
		source.finishDepends.UnionWith(depends(node.body));
		return source;
	}
	
	//interface
	public static DependsResults dependsSplit(Node_Interface node) {
		DependsResults results = new DependsResults(null,null);
		foreach( INode_Expression inher in node.inheritees )
			results.tryFinish(inher);
		foreach( Node_StatusedMember sm in node.members )
			results.UnionWith(dependsSplitAny(sm.member));
		return results;
	}
	//breeder
	public static DependsResults dependsSplit(Node_Breeder node) {
		DependsResults results = new DependsResults(null,null);
		if( node.type != null )
			results.tryFinish(node.type);
		return results;
	}
	//callee
	public static DependsResults dependsSplit(Node_Callee node) {
		DependsResults results = new DependsResults(null,null);
		foreach( Node_ParameterInfo pi in node.parameterInfos )
			results.tryFinish(pi.type);
		results.tryFinish(node.returnType);
		return results;
	}
	//method
	public static DependsResults dependsSplit(Node_Method node) {
		return dependsSplitAny(node.@interface);
	}
	//property
	public static DependsResults dependsSplit(Node_Property node) {
		DependsResults results = new DependsResults(null,null);
		results.tryFinish(node.type);
		return results;
	}
	
	//split between execute depends and finish depends
	public static DependsResults dependsSplitAny(INode node) {
		return (
			node is Node_Breeder ?
				dependsSplit(node as Node_Breeder) :
			node is Node_Callee ?
				dependsSplit(node as Node_Callee) :
			node is Node_Function ?
				dependsSplit(node as Node_Function) :
			node is Node_Interface ?
				dependsSplit(node as Node_Interface) :
			node is Node_Method ?
				dependsSplit(node as Node_Method) :
			node is Node_Property ?
				dependsSplit(node as Node_Property) :
			new DependsResults(
				depends(node), new HashSet<Identifier>() ));
	}
}

} //public
