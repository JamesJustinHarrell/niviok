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

	public void getInfo(out string name, out object objs) {
		name = "nullable-type";
		objs = new object[]{ _nodeInterface, _nodeNullable };
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return Help.getIdentRefs(_nodeInterface); }
	}
}