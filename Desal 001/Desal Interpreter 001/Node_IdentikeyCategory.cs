using System.Collections.Generic;

class Node_IdentikeyCategory : INode {
	IdentikeyCategory _category;
	
	public Node_IdentikeyCategory(IdentikeyCategory category) {
		_category = category;
	}
	
	public IdentikeyCategory category {
		get { return _category; }
	}
	
	public void getInfo(out string name, out object children) {
		name = "identikey-category";
		children = _category;
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
}