class Node_Access : TerminalNode<Access, Node_Access>, INode {
	public Node_Access(Access value) :base(value) {}
	public Node_Access(string str) :base(G.parseEnum<Access>(str)) {}

	public string typeName {
		get { return "access"; }
	}
}
