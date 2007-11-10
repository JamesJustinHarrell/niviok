class Node_GetProperty : INode_Expression {
	INode_Expression _value;
	Node_Identifier _propertyName;

	public Node_GetProperty(INode_Expression val, Node_Identifier propertyName) {
		_value = val;
		_propertyName = propertyName;
	}
	
	public void execute(Scope scope) {
		throw new System.Exception("does nothing");
	}
	
	public IValue evaluate(Scope scope) {
		return _value.evaluate(scope).getProperty(_propertyName.identifier);
	}
	
	public void getInfo(out string name, out object children) {
		name = "get-property";
		children = new object[]{ _value, _propertyName };
	}
}