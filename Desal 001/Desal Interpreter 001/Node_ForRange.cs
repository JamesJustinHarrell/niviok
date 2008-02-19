using System.Collections.Generic;

class Node_ForRange : INode_Expression {
	Node_Identifier _name;
	INode_Expression _start;
	INode_Expression _limit;
	//xxx bool _inclusive;
	Node_Block _block; //xxx rename to body

	public Node_ForRange(
	Node_Identifier name, INode_Expression start,
	INode_Expression limit, Node_Block block) {
		_name = name;
		_start = start;
		_limit = limit;
		_block = block;
	}
	
	public IValue execute(Scope scope) {		
		IValue start = _start.execute(scope);
		long current = Bridge.unwrapInteger(start);
		IValue limit = _limit.execute(scope);
		while( current < Bridge.unwrapInteger(limit) ) {
			Scope innerScope = new Scope(scope);
			innerScope.declareAssign(
				_name.identifier,
				//xxx new NullableType( ReferenceCategory.VALUE, null),
				//xxx true,
				Bridge.wrapInteger(current) );
			_block.execute(innerScope);
			current++;
		}
		return new NullValue();
	}
	
	public void getInfo(out string name, out object objs) {
		name = "for-range";
		objs = new object[]{ _name, _start, _limit, /* xxx _inclusive,*/ _block };
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents =
				Help.getIdentRefs( _start, _limit, _block );
			idents.Remove( _name.identifier );
			return idents;
		}
	}
}