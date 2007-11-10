class Node_Identifier : INode_Expression {
	Identifier _identifier;

	public Node_Identifier(string identifier) {
		_identifier = new Identifier(identifier);
	}
	
	public Identifier identifier {
		get { return _identifier; }
	}
	
	public IValue evaluate(Scope scope) {
		return scope.evaluateIdentifier(_identifier);
	}
	
	public void execute(Scope scope) {
		//xxx create better warning system
		System.Console.WriteLine("WARNING: executing an identifier node has no effect");
	}
	
	public void getInfo(out string name, out object objs) {
		name = "identifier";
		objs = _identifier.str;
	}
}