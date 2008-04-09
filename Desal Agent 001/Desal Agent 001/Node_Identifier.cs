class Node_Identifier : TerminalNode<Identifier, Node_Identifier>, INode_Expression {
	public Node_Identifier(Identifier value) : base(value) {}
	public Node_Identifier(string str) :base(new Identifier(str)) {}
	
	public string typeName {
		get { return "identifier"; }
	}
}