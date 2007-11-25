class Node_Integer : INode_Expression {
	Bridge _bridge;
	long _int;

	public Node_Integer(Bridge bridge, long integer) {
		_bridge = bridge;
		_int = integer;
	}
	
	public IValue evaluate(Scope scope) {
		return Bridge.wrapInteger(_int);
	}
	
	public void execute(Scope scope) {
		_bridge.warning("executing an integer node has no effect");
	}
	
	public void getInfo(out string name, out object objs) {
		name = "integer";
		objs = _int;
	}
}