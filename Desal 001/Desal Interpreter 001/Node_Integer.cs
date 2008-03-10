using System.Collections.Generic;

class Node_Integer : INode_Expression {
	long _int;

	public Node_Integer(long integer) {
		_int = integer;
	}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapInteger(_int);
	}
	
	public string typeName {
		get { return "integer"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
	
	public override string ToString() {
		return _int.ToString();
	}
}