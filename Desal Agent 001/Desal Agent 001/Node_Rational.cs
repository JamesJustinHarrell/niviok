//xxx use BigNum library
class Node_Rational : TerminalNode<double, Node_Rational>, INode_Expression {
	public Node_Rational(double value) :base(value) {}
	public Node_Rational(string str) :base(System.Double.Parse(str)) {}
	
	public string typeName {
		get { return "rational"; }
	}
}