using System.Collections.Generic;

class Node_Xnor : INode_Expression {
	INode_Expression _first;
	INode_Expression _second;

	public Node_Xnor( INode_Expression first, INode_Expression second ) {
		_first = first;
		_second = second;
	}
	
	public IValue execute(Scope scope) {		
		IValue first = _first.execute(scope);
		IValue second = _second.execute(scope);

		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) == Bridge.unwrapBoolean(second) );
	}
	
	public string typeName {
		get { return "xnor"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _first, _second }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _first, _second ); }
	}
}
