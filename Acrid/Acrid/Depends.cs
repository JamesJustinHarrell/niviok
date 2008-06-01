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
	
	//compound
	//xxx may want to remove references to global identikeys (optimization)
	public static HashSet<Identifier> depends(Node_Compound node) {
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
	
	//expose
	public static HashSet<Identifier> depends(Node_Expose node) {
		IEnumerator<Node_Identifier> en = node.identifiers.GetEnumerator();
		if( en.MoveNext() ) {
			HashSet<Identifier> s = new HashSet<Identifier>();
			s.Add(en.Current.value);
			return s;
		}
		return new HashSet<Identifier>();
	}
	
	//extract-member
	public static HashSet<Identifier> depends(Node_ExtractMember node) {
		return depends(node.source);
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
	
	//limit-old
	//xxx nowhere near finished
	public static HashSet<Identifier> depends(Node_LimitOld node) {
		HashSet<Identifier> idents = collectDepends(node.hidables);
		foreach( Node_Hidable child in node.hidables ) {
			if( child.declaration is Node_DeclareFirst ) {
				Node_DeclareFirst df = (Node_DeclareFirst)child.declaration;
				if( df.identikeyType.identikeyCategory.value == IdentikeyCategory.CONSTANT )
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
		return depends(node.limitOld);
	}
	
	//parameter-impl
	public static HashSet<Identifier> depends(Node_ParameterImpl node) {
		return collectDepends(node.defaultValue, node.nullableType);
	}
	
	//parameter-info
	public static HashSet<Identifier> depends(Node_ParameterInfo node) {
		return depends(node.nullableType);
	}
	
	//property
	public static HashSet<Identifier> depends(Node_Property node) {
		return depends(node.nullableType);
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
		Type classType = typeof(Depends);
		Type nodeType = ((Object)node).GetType();
		Reflection.MethodInfo meth = classType.GetMethod(
			"depends", new Type[]{nodeType});
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
}
