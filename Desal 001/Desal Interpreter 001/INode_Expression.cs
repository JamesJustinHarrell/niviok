//a Desal node that can be executed

interface INode_Expression : INode {
	IValue execute(Scope scope);
}
