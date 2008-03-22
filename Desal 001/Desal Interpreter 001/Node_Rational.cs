//xxx use BigNum library
class Node_Rational : TerminalNode<double, Node_Rational>, INode_Expression {
	public Node_Rational(double value) :base(value) {}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapRational(this.value);
	}
	
	public string typeName {
		get { return "rational"; }
	}
}