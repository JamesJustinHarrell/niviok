class Node_DeclarationConstEmpty : INode_DeclarationAny {
	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
	}
	
	public void getInfo(out string name, out object children) {
		throw new Error_Unimplemented();
	}
}