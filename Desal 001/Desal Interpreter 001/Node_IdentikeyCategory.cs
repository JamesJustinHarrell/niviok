using System.Collections.Generic;

class Node_IdentikeyCategory : INode {
	IdentikeyCategory _category;
	
	public Node_IdentikeyCategory(IdentikeyCategory category) {
		_category = category;
	}
	
	public IdentikeyCategory category {
		get { return _category; }
	}
	
	public string typeName {
		get { return "identikey-category"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
	
	public override string ToString() {
		return _category.ToString();
	}
}