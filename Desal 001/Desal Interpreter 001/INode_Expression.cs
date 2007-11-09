interface INode_Expression : INode_Statement {
	IValue evaluate(ref Scope scope);
}