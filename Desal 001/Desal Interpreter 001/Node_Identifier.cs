class Node_Identifier : TerminalNode<Identifier, Node_Identifier>, INode_Expression {
	public Node_Identifier(Identifier value) : base(value) {}

	public IValue execute(Scope scope) {
		return scope.evaluateIdentifier(this.value);
	}
	
	public string typeName {
		get { return "identifier"; }
	}

	public override HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = new HashSet<Identifier>();
			idents.Add(this.value);
			return idents;
		}
	}
}