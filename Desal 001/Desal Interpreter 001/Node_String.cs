//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates

class Node_String : TerminalNode<string, Node_String>, INode_Expression {
	public Node_String(string value) :base(value) {}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapString(this.value);
	}
	
	public string typeName {
		get { return "string"; }
	}
}