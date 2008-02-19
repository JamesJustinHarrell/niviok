using System.Collections.Generic;

class Node_Nor : INode_Expression {
	INode_Expression _first;
	INode_Expression _second;

	public Node_Nor( INode_Expression first, INode_Expression second ) {
		_first = first;
		_second = second;
	}
	
	public IValue execute(Scope scope) {		
		IValue first = _first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(false);
		
		IValue second = _second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "nor";
		objs = new object[]{ _first, _second };
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return Help.getIdentRefs( _first, _second ); }
	}
}