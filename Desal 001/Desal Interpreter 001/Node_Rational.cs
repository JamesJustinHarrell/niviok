using System.Collections.Generic;

class Node_Rational : INode_Expression {
	double _rat;

	public Node_Rational(double rat) {
		_rat = rat;
	}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapRational(_rat);
	}
	
	public string typeName {
		get { return "rational"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
	
	public override string ToString() {
		return _rat.ToString();
	}
}