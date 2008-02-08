class Node_DeclareFirst : INode_DeclarationAny {
	Node_Identifier _identNode;
	Node_ReferenceType _typeNode;
	INode_Expression _valueNode;
	
	public Node_DeclareFirst(
	Node_Identifier identNode, Node_ReferenceType typeNode, INode_Expression valueNode) {
		_identNode = identNode;
		_typeNode = typeNode;
		_valueNode = valueNode;
	}
	
	public IValue execute(Scope scope) {
		IValue val = _valueNode.execute(scope);
		scope.declareFirst(
			_identNode.identifier,
			//xxx _typeNode.evaluateType(scope),
			val );
		return val;
	}

	public void getInfo(out string name, out object objs) {
		name = "declaration-pervasive";
		objs = new object[]{ _identNode, _typeNode, _valueNode };
	}
}
