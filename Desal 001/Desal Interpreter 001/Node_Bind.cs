class Node_Bind : INode_Expression {
	Node_Identifier _identNode;
	INode_Expression _valueNode;

	public Node_Bind(Node_Identifier identNode, INode_Expression valueNode) {
		_identNode = identNode;
		_valueNode = valueNode;
	}
	
	public IValue evaluate(Scope scope) {
		IValue val = _valueNode.evaluate(scope);
		scope.bind( _identNode.identifier, val );
		return val;
	}
	
	public void execute(Scope scope) {
		evaluate(scope);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "bind";
		objs = new object[]{ _identNode, _valueNode };
	}
}