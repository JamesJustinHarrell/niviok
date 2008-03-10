using System.Collections.Generic;

class Node_NullableType : INode {
	INode_Expression _nodeInterface;
	Node_Boolean _nodeNullable;

	public Node_NullableType(
	INode_Expression nodeInterface, Node_Boolean nodeNullable) {
		_nodeInterface = nodeInterface;
		_nodeNullable = nodeNullable;
	}

	public NullableType evaluateType(Scope scope) {
		IInterface iface = ( _nodeInterface == null ? null :
			InterfaceFromValue.wrap(_nodeInterface.execute(scope)) );
		return new NullableType( iface, _nodeNullable.val );
	}
	
	public string typeName {
		get { return "nullable-type"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _nodeInterface, _nodeNullable }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			return ( _nodeInterface == null ?
				new HashSet<Identifier>() :
				_nodeInterface.identikeyDependencies );
		}
	}
}