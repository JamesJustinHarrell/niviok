class Node_Direction : TerminalNode<Direction, Node_Direction>, INode {
	public Node_Direction(Direction value) :base(value) {}
	public Node_Direction(string str) :base(G.parseEnum<Direction>(str)) {}

	public string typeName {
		get { return "direction"; }
	}
}
