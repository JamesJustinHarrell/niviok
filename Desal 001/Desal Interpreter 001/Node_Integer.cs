class Node_Integer : INode_Expression {
	long _int;

	public Node_Integer(long integer) {
		_int = integer;
	}
	
	public IValue evaluate(Scope scope) {
		return Wrapper.wrapInteger(_int);
	}
	
	public void execute(Scope scope) {
		//xxx create better warning system
		System.Console.WriteLine("WARNING: executing an integer node has no effect");
	}
	
	public void getInfo(out string name, out object objs) {
		name = "integer";
		objs = _int;
	}
}