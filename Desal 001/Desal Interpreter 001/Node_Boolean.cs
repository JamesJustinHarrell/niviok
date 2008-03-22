class Node_Boolean : TerminalNode<bool, Node_Boolean>, INode {
	public Node_Boolean(bool value) :base(value) {}
	
	public string typeName {
		get { return "boolean"; }
	}

	public override string ToString() {
		return base.ToString().ToLower();
	}
}