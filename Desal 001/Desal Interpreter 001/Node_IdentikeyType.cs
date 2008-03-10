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
	
	public string typeName {
		get { return "identikey-type"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _category, _type, _constant }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return _type.identikeyDependencies; }
	}
}