class Node_Bool : INode {
	bool _val;
	
	public Node_Bool(bool val) {
		_val = val;
	}
	
	public bool val {
		get { return _val; }
	}
	
	public void getInfo(out string name, out object children) {
		name = "bool";
		children = _val;
	}
}