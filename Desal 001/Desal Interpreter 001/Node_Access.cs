using System.Collections.Generic;

class Node_Access : INode {
	Access _access;

	public Node_Access(Access access) {
		_access = access;
	}
	
	public Access access {
		get { return _access; }
	}
	
	public string typeName {
		get { return "access"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
	
	public override string ToString () {
		return _access.ToString();
	}
}
