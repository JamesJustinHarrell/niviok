using System.Collections.Generic;

class Node_ForRange : INode_Expression {
	Node_Identifier _name;
	INode_Expression _start;
	INode_Expression _limit;
	Node_Block _body;

	public Node_ForRange(
	Node_Identifier name, INode_Expression start,
	INode_Expression limit, Node_Block body) {
		_name = name;
		_start = start;
		_limit = limit;
		_body = body;
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
			_body.execute(innerScope);
			current++;
		}
		return new NullValue();
	}
	
	public string typeName {
		get { return "for-range"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _name, _start, _limit, _body }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = G.depends( _start, _limit, _body );
			idents.Remove( _name.identifier );
			return idents;
		}
	}
}