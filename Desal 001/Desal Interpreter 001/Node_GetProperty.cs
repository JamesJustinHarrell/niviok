class Node_GetProperty : INode_Expression {
	INode_Expression _value;
	Node_Identifier _propertyName;

	public Node_GetProperty(INode_Expression val, Node_Identifier propertyName) {
		_value = val;
		_propertyName = propertyName;
	}
	
	public void execute(ref Scope scope) {
		throw new System.Exception("does nothing");
	}
	
	public IValue evaluate(ref Scope scope) {
		return _value.evaluate(ref scope).getProperty(_propertyName.identifier);
	}
	
	public void getInfo(out string name, out object children) {
		name = "get-property";
		children = new object[]{ _value, _propertyName };
	}
}