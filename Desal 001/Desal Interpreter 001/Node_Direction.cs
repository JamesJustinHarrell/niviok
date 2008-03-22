class Node_Direction : TerminalNode<Direction, Node_Direction>, INode {
	public Node_Direction(Direction value) :base(value) {}

	public string typeName {
		get { return "direction"; }
	}
}
