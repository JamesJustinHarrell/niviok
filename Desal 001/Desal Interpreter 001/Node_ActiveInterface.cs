class Node_ActiveInterface : INode_Expression {
	INode_Expression _val;

	public Node_ActiveInterface(INode_Expression val) {
		_val = val;
	}
	
	public IValue execute(Scope scope) {
		return _val.execute(scope).activeInterface.value;
	}
	
	public void getInfo(out string name, out object children) {
		name = "active-interface";
		children = _val;
	}
}