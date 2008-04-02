//xxx use BigInt
class Node_Integer : TerminalNode<long, Node_Integer>, INode_Expression {
	public Node_Integer(long value) :base(value) {}

	public string typeName {
		get { return "integer"; }
	}
}