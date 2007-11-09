class Node_Bind : INode_Expression {
	Node_Identifier _identNode;
	INode_Expression _valueNode;

	public Node_Bind(Node_Identifier identNode, INode_Expression valueNode) {
		_identNode = identNode;
		_valueNode = valueNode;
	}
	
	public IValue evaluate(ref Scope scope) {
		IValue val = _valueNode.evaluate(ref scope);
		scope.bind( _identNode.identifier, val );
		return val;
	}
	
	public void execute(ref Scope scope) {
		evaluate(ref scope);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "bind";
		objs = new object[]{ _identNode, _valueNode };
	}
}