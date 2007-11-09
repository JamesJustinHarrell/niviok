class Node_Parameter : INode {
	public Node_Identifier name;
	public INode_Expression interface_;
	public INode_Expression defaultValue;

	public Node_Parameter(
	Node_Identifier aName, INode_Expression aInterface,
	INode_Expression aDefaultValue) {
		name = aName;
		interface_ = aInterface;
		defaultValue = aDefaultValue;
	}
	
	public void getInfo(out string name, out object objs) {
		name = "parameter";
		objs = new object[]{ name, interface_, defaultValue };
	}
}