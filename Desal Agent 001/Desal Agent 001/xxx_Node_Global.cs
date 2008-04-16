/*
using System.Collections.Generic;

class old_Node_Global : INode {
	IList<Node_DeclarationPervasive> _binds;
	Scope _scope;

	public old_Node_Global(IList<Node_DeclarationPervasive> binds) {
		_binds = binds;
	}		
	
	public void setup(Scope scope) {
		if( _scope != null )
			throw new System.Exception("scope already set");
		_scope = scope;
	}
	
	public int run() {
		if( _scope == null )
			throw new System.Exception("scope not set");
	
		foreach( INode_Statement statement in _binds ) {
			statement.execute(_scope);
		}
	
		IWorker val = _scope.evaluateIdentifier( new Identifier("main") );
		val.executeCall( new IList<Argument>(new IWorker[]{}, new Dictionary<Identifier, IWorker>()) );
		
		//xxx if main returns something, evaluateCall and return the result
		return 0;
	}
	
	public void getInfo(out string name, out object children) {
		name = "global";
		children = _binds;
	}
}
*/