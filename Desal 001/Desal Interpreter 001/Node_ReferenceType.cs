class Node_ReferenceType : INode {
	Node_ReferenceCategory _nodeCategory;
	INode_Expression _nodeInterface;

	public Node_ReferenceType(
	Node_ReferenceCategory nodeCategory, INode_Expression nodeInterface) {
		_nodeCategory = nodeCategory;
		_nodeInterface = nodeInterface;
	}
	
	public ReferenceCategory category {
		get { return _nodeCategory.category; }
	}
	
	public ReferenceType evaluateType(Scope scope) {
		IValue ifaceValue = null;
		if( _nodeInterface != null )
			ifaceValue = _nodeInterface.evaluate(scope);

		IInterface iface = null;
		if( ifaceValue != null )
			iface = InterfaceFromValue.wrap(ifaceValue);
		
		return new ReferenceType( _nodeCategory.category, iface );
	}

	public void getInfo(out string name, out object objs) {
		name = "reference-type";
		objs = new object[]{ _nodeCategory, _nodeInterface };
	}
}