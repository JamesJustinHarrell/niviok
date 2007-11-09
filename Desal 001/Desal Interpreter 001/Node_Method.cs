class Node_Method : INode {
	Node_Identifier _name;
	INode_Expression _iface;

	public Node_Method(Node_Identifier name, INode_Expression iface) {
		_name = name;
		_iface = iface;
	}
	
	public MethodInfo evaluateMethodInfo(ref Scope scope) {
		return new MethodInfo(
			_name.identifier,
			null ); //InterfaceFromValue.wrap( _iface.evaluate(ref scope) ) );
	}
	
	public void getInfo(out string name, out object children) {
		name = "method";
		children = new object[]{ _name, _iface };
	}
}