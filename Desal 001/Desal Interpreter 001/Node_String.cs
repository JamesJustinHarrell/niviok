//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates

using System.Collections.Generic;

class Node_String : INode_Expression {
	Bridge _bridge;
	string _str;

	public Node_String(Bridge bridge, string str) {
		_bridge = bridge;
		_str = str;
	}
	
	public IValue execute(Scope scope) {
		return Bridge.wrapString(_str);
	}

	public void getInfo(out string name, out object objs) {
		name = "string";
		objs = _str;
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return new HashSet<Identifier>(); }
	}
}