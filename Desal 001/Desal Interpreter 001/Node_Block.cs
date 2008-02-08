using System.Collections.Generic;

class Node_Block : INode_Expression {
	IList<INode_Expression> _members;

	public Node_Block(IList<INode_Expression> members) {
		_members = members;
	}	
	
	public IValue execute(Scope scope) {
		Scope innerScope = new Scope(scope);
		IValue rv = new NullValue();
		foreach( INode_Expression expr in _members ) {
			rv = expr.execute(innerScope);
		}
		return rv;
	}

	public void getInfo(out string name, out object objs) {
		name = "block";
		objs = _members;
	}
}