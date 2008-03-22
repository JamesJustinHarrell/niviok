//determines the identikey dependencies of nodes

using System.Collections;
using System.Collections.Generic;

static class Depends {
	//xxx unused
	static HashSet<Identifier> collectFromList( ICollection args ) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		foreach( object o in args ) {
			if( o == null )
				continue;
			if( o is INode )
				idents.UnionWith( (o as INode).identikeyDependencies );
			else if( o is ICollection )
				idents.UnionWith( collectFromList(o as ICollection) );
			else
				throw new System.Exception("unknown object type: " + o.ToString());
		}
		return idents;
	}
	
	static HashSet<Identifier> collectFromNodeList( ICollection<INode> args ) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		foreach( INode node in args ) {
			if( node != null )
				idents.UnionWith( node.identikeyDependencies );
		}
		return idents;
	}
	
	//block
	//xxx may want to remove references to global identikeys (optimization)
	public static HashSet<Identifier> depends(Node_Block node) {
		HashSet<Identifier> idents = depends(node as INode);
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
		HashSet<Identifier> idents = G.depends( node.identikeyType, node.value );
		if( node.identikeyType.identikeyCategory.value != IdentikeyCategory.FUNCTION )
			idents.Remove( node.name.value );
		return idents;
	}
	
	//declare-first
	public static HashSet<Identifier> depends(Node_DeclareFirst node) {
		HashSet<Identifier> idents = G.depends( node.identikeyType, node.value );
		if( node.identikeyType.identikeyCategory.value != IdentikeyCategory.FUNCTION )
			idents.Remove( node.name.value );
		return idents;
	}
	
	//extract-member
	public static HashSet<Identifier> depends(Node_ExtractMember node) {
		return node.source.identikeyDependencies;
	}
	
	//for-range
	public static HashSet<Identifier> depends(Node_ForRange node) {
		HashSet<Identifier> idents = node.action.identikeyDependencies;
		idents.Remove(node.name.value);
		idents.UnionWith(node.start.identikeyDependencies);
		idents.UnionWith(node.limit.identikeyDependencies);
		return idents;
	}
	
	//function
	public static HashSet<Identifier> depends(Node_Function node) {
		HashSet<Identifier> idents = node.body.identikeyDependencies;
		foreach( Node_Parameter param in node.parameter )
			idents.Remove( param.name.value );
		foreach( Node_Parameter param in node.parameter )
			idents.UnionWith( param.identikeyDependencies );
		if( node.returnInfo != null )
			idents.UnionWith( node.returnInfo.identikeyDependencies );
		return idents;
	}
	
	//method
	public static HashSet<Identifier> depends(Node_Method node) {
		return node.@interface.identikeyDependencies;
	}
	
	//parameter
	public static HashSet<Identifier> depends(Node_Parameter node) {
		return collectFromNodeList(new INode[]{ node.defaultValue, node.nullableType });
	}
	
	//property
	public static HashSet<Identifier> depends(Node_Property node) {
		return collectFromNodeList(new INode[]{ node.access, node.nullableType });
	}
	
	//node
	public static HashSet<Identifier> depends(INode node) {
		return collectFromNodeList(node.childNodes);
	}
}
