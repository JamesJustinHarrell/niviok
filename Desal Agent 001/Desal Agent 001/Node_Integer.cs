//xxx use BigInt
class Node_Integer : TerminalNode<long, Node_Integer>, INode_Expression {
	public Node_Integer(long value) :base(value) {}
	public Node_Integer(string str) :base(System.Int64.Parse(str)) {}

	public string typeName {
		get { return "integer"; }
	}
}