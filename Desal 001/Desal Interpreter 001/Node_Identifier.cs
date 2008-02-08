class Node_Identifier : INode_Expression {
	Bridge _bridge;
	Identifier _identifier;

	public Node_Identifier(Bridge bridge, string identifier) {
		_bridge = bridge;
		_identifier = new Identifier(identifier);
	}
	
	public Identifier identifier {
		get { return _identifier; }
	}
	
	public IValue execute(Scope scope) {
		return scope.evaluateIdentifier(_identifier);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "identifier";
		objs = _identifier.str;
	}
}