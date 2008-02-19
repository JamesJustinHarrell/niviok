using System.Collections.Generic;

class Node_Rational : INode_Expression {
	Bridge _bridge;
	double _rat;

	public Node_Rational(Bridge bridge, double rat) {
		_bridge = bridge;
		_rat = rat;
	}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapRational(_rat);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "rational";
		objs = _rat;
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
}