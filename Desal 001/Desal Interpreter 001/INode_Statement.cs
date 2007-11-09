interface INode_Statement : INode {
	void execute(ref Scope scope);
}