//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates

class Node_String : INode_Expression {
	string _str;

	public Node_String(string str) {
		_str = str;
	}
	
	public IValue evaluate(Scope scope) {
		return Wrapper.wrapString(_str);
	}
	
	public void execute(Scope scope) {
		//xxx create better warning system
		System.Console.WriteLine("WARNING: executing a string node has no effect");
	}
	
	public void getInfo(out string name, out object objs) {
		name = "string";
		objs = _str;
	}
}