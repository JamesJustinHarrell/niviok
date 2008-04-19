//determines the identikey dependencies of nodes
//i.e. the names of identikeys in the outer scope referenced by this node
//the set returned should never be null (though may be empty set)

using System;
using System.Collections;
using System.Collections.Generic;
using Reflection = System.Reflection;

static class Depends {
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
	
	//block
	//xxx may want to remove references to global identikeys (optimization)
	public static HashSet<Identifier> depends(Node_Block node) {
		HashSet<Identifier> idents = unionChildDepends(node);
		foreach( INode_Expression member in node.members ) {
			if( member is INode_Declaration ) {
				//xxx shouldn't remove references to function identikeys
				idents.Remove( (member as INode_Declaration).name.value );
			}
		}
		return idents;
	}

	//declare-assign
	public static HashSet<Identifier> depends(Node_DeclareAssign node) {
		HashSet<Identifier> idents = collectDepends(node.identikeyType, node.value);
		if( node.identikeyType.identikeyCategory.value != IdentikeyCategory.FUNCTION )
			idents.Remove( node.name.value );
		return idents;
	}
	
	//declare-first
	public static HashSet<Identifier> depends(Node_DeclareFirst node) {
		HashSet<Identifier> idents = collectDepends(node.identikeyType, node.value);
		if( node.identikeyType.identikeyCategory.value != IdentikeyCategory.FUNCTION )
			idents.Remove( node.name.value );
		return idents;
	}
	
	//extract-member
	public static HashSet<Identifier> depends(Node_ExtractMember node) {
		return depends(node.source);
	}
	
	//for-range
	public static HashSet<Identifier> depends(Node_ForRange node) {
		HashSet<Identifier> idents = depends(node.action);
		idents.Remove(node.name.value);
		idents.UnionWith(depends(node.start));
		idents.UnionWith(depends(node.limit));
		return idents;
	}
	
	//function
	public static HashSet<Identifier> depends(Node_Function node) {
		HashSet<Identifier> idents = depends(node.body);
		foreach( Node_ParameterImpl param in node.parameterImpls )
			idents.Remove( param.name.value );
		foreach( Node_ParameterImpl param in node.parameterImpls )
			idents.UnionWith( depends(param) );
		if( node.returnInfo != null )
			idents.UnionWith( depends(node.returnInfo) );
		return idents;
	}
	
	//identifier
	public static HashSet<Identifier> depends(Node_Identifier node) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		idents.Add(node.value);
		return idents;
	}
	
	//method
	public static HashSet<Identifier> depends(Node_Method node) {
		return depends(node.@interface);
	}
	
	//parameter-impl
	public static HashSet<Identifier> depends(Node_ParameterImpl node) {
		return collectDepends(node.defaultValue, node.nullableType);
	}
	
	//parameter-info
	public static HashSet<Identifier> depends(Node_ParameterInfo node) {
		return depends(node.nullableType);
	}
	
	//plane
	public static HashSet<Identifier> depends(Node_Plane node) {
		HashSet<Identifier> idents = collectDepends(node.declareFirsts);
		foreach( Node_DeclareFirst decl in node.declareFirsts )
			idents.Remove( decl.name.value ); //xxx only if not function
		return idents;
	}
	
	//property
	public static HashSet<Identifier> depends(Node_Property node) {
		return depends(node.nullableType);
	}
	
	//any node
	public static HashSet<Identifier> depends(INode node) {
		//xxx System.Console.WriteLine("in depends(INode)");
		Type classType = typeof(Depends);
		Type nodeType = ((Object)node).GetType();
		Reflection.MethodInfo meth = classType.GetMethod(
			"depends", new Type[]{nodeType});

		//xxx System.Console.WriteLine(meth.GetParameters()[0].ParameterType);
		//System.Console.WriteLine(typeof(INode));
		if( meth.GetParameters()[0].ParameterType == typeof(INode) )
			return unionChildDepends(node);
		
		try {
			return (HashSet<Identifier>)meth.Invoke(null, new object[]{node});
		}
		catch(Reflection.TargetInvocationException e) {
			if( e.InnerException is ClientException )
				throw e.InnerException;
			throw e;
		}
	}
}
