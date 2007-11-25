//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates

class Node_String : INode_Expression {
	Bridge _bridge;
	string _str;

	public Node_String(Bridge bridge, string str) {
		_bridge = bridge;
		_str = str;
	}
	
	public IValue evaluate(Scope scope) {
		return Bridge.wrapString(_str);
	}
	
	public void execute(Scope scope) {
		_bridge.warning("executing a string node has no effect");
	}
	
	public void getInfo(out string name, out object objs) {
		name = "string";
		objs = _str;
	}
}