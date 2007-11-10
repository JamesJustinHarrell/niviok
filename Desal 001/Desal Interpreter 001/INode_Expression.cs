interface INode_Expression : INode_Statement {
	IValue evaluate(Scope scope);
}