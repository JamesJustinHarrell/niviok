using System.Collections.Generic;

class Node_Global : INode {
	IList<Node_DeclarationPervasive> _binds;
	Scope _scope;

	public Node_Global(IList<Node_DeclarationPervasive> binds) {
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
			statement.execute(ref _scope);
		}
	
		IValue val = _scope.evaluateIdentifier( new Identifier("main") );
		val.executeCall( new Arguments(new IValue[]{}, new Dictionary<Identifier, IValue>()) );
		
		//xxx if main returns something, evaluateCall and return the result
		return 0;
	}
	
	public void getInfo(out string name, out object children) {
		name = "global";
		children = _binds;
	}
}