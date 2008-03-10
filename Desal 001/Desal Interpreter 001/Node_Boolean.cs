using System.Collections.Generic;

class Node_Boolean : INode {
	bool _val;
	
	public Node_Boolean(bool val) {
		_val = val;
	}
	
	public bool val {
		get { return _val; }
	}
	
	public string typeName {
		get { return "boolean"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
	
	public override string ToString() {
		return (_val ? "true" : "false");
	}
}