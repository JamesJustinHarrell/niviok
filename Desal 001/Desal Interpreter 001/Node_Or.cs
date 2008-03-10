using System.Collections.Generic;

class Node_Or : INode_Expression {
	INode_Expression _first;
	INode_Expression _second;

	public Node_Or( INode_Expression first, INode_Expression second ) {
		_first = first;
		_second = second;
	}
	
	public IValue execute(Scope scope) {		
		IValue first = _first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(true);
		
		IValue second = _second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	public string typeName {
		get { return "or"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _first, _second }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _first, _second ); }
	}
}
