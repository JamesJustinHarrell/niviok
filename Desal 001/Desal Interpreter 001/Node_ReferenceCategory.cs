//rationale for existance: the getInfo() method

class Node_ReferenceCategory : INode {
	ReferenceCategory _category;

	public Node_ReferenceCategory(ReferenceCategory category) {
		_category = category;
	}
	
	public ReferenceCategory category {
		get { return _category; }
	}

	public void getInfo(out string name, out object objs) {
		name = "reference-category";
		objs = _category;
	}
}