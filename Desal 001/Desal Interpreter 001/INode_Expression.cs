interface INode_Expression : INode {
	IValue execute(Scope scope);
}