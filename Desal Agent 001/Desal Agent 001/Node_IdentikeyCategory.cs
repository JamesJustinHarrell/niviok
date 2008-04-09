class Node_IdentikeyCategory : TerminalNode<IdentikeyCategory, Node_IdentikeyCategory>, INode {
	public Node_IdentikeyCategory(IdentikeyCategory value) :base(value) {}
	public Node_IdentikeyCategory(string str) :base(G.parseEnum<IdentikeyCategory>(str)) {}
	
	public string typeName {
		get { return "identikey-category"; }
	}
}