class Node_Access : TerminalNode<Access, Node_Access>, INode {
	public Node_Access(Access value) :base(value) {}

	public string typeName {
		get { return "access"; }
	}
}
