using System.Collections.Generic;

class Node_Block : INode_Statement {
	IList<INode_Statement> _statements;

	public Node_Block(IList<INode_Statement> statements) {
		_statements = statements;
	}	
	
	public void execute(ref Scope scope) {
		Scope innerScope = new Scope(ref scope);
		foreach( INode_Statement statement in _statements ) {
			statement.execute(ref innerScope);
		}
	}

	public void getInfo(out string name, out object objs) {
		name = "block";
		objs = _statements;
	}
}