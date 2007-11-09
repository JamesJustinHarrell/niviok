class Node_Access : INode {
	Access _access;

	public Node_Access(Access access) {
		_access = access;
	}
	
	public Access access {
		get { return _access; }
	}
	
	public void getInfo(out string name, out object children) {
		name = "access";
		children = _access;
	}
}