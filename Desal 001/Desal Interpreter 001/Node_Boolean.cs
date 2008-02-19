using System.Collections.Generic;

class Node_Boolean : INode {
	bool _val;
	
	public Node_Boolean(bool val) {
		_val = val;
	}
	
	public bool val {
		get { return _val; }
	}
	
	public void getInfo(out string name, out object children) {
		name = "boolean";
		children = _val;
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
}