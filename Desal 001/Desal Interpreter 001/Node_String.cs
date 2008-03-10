//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates

using System.Collections.Generic;

class Node_String : INode_Expression {
	string _str;

	public Node_String(string str) {
		_str = str;
	}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapString(_str);
	}
	
	public string typeName {
		get { return "string"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
	
	public override string ToString() {
		return _str;
	}
}