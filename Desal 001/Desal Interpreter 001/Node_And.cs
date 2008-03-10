using System.Collections.Generic;

class Node_And : INode_Expression {
	INode_Expression _first;
	INode_Expression _second;

	public Node_And( INode_Expression first, INode_Expression second ) {
		_first = first;
		_second = second;
	}
	
	public IValue execute(Scope scope) {		
		IValue first = _first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(false);
		
		IValue second = _second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}
	
	public string typeName {
		get { return "and"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _first, _second }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _first, _second ); }
	}
}
