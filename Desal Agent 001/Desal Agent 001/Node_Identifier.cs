class Node_Identifier : TerminalNode<Identifier, Node_Identifier>, INode_Expression {
	public Node_Identifier(Identifier value) : base(value) {}
	
	public string typeName {
		get { return "identifier"; }
	}
}