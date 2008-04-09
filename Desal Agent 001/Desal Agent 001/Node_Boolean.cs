class Node_Boolean : TerminalNode<bool, Node_Boolean>, INode {
	public Node_Boolean(bool value) :base(value) {}
	public Node_Boolean(string str) :base(System.Boolean.Parse(str)) {}
	
	public string typeName {
		get { return "boolean"; }
	}
}