class Node_DeclareEmpty : INode_DeclarationAny {
	Node_Identifier _name;

	public Node_DeclareEmpty( Node_Identifier name ) {
		_name = name;
	}

	public IValue execute(Scope scope) {
		scope.declareEmpty(_name.identifier);
		return new NullValue();
	}
	
	public void getInfo(out string name, out object children) {
		name = "declare-empty";
		children = new object[]{ _name };
	}
}