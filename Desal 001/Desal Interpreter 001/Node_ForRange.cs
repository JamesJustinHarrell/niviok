class Node_ForRange : INode_Statement {
	Node_Identifier _ident;
	INode_Expression _start;
	INode_Expression _limit;
	//xxx bool _inclusive;
	Node_Block _block;

	public Node_ForRange(
	Node_Identifier ident, INode_Expression start,
	INode_Expression limit, Node_Block block) {
		_ident = ident;
		_start = start;
		_limit = limit;
		_block = block;
	}
	
	public void execute(ref Scope scope) {		
		IValue start = _start.evaluate(ref scope);
		long current = Wrapper.unwrapInteger(start);
		IValue limit = _limit.evaluate(ref scope);
		while( current < Wrapper.unwrapInteger(limit) ) {
			Scope innerScope = new Scope(ref scope);
			innerScope.declareBind(
				_ident.identifier,
				new ReferenceType( ReferenceCategory.VALUE, null),
				true,
				Wrapper.wrapInteger(current) );
			_block.execute(ref innerScope);
			current++;
		}
	}
	
	public void getInfo(out string name, out object objs) {
		name = "for-range";
		objs = new object[]{ _ident, _start, _limit, /* xxx _inclusive,*/ _block };
	}
}