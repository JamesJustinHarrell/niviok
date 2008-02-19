using System.Collections.Generic;

class Node_IdentikeyType : INode {
	Node_IdentikeyCategory _category;
	Node_NullableType _type;
	Node_Boolean _constant;

	public Node_IdentikeyType(
	Node_IdentikeyCategory category, Node_NullableType type, Node_Boolean constant) {
		_category = category;
		_type = type;
		_constant = constant;
	}
	
	public IdentikeyCategory category {
		get { return _category.category; }
	}
	
	public void getInfo(out string name, out object children) {
		name = "identikey-type";
		children = new object[]{ _category, _type, _constant };
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return _type.identikeyDependencies; }
	}
}