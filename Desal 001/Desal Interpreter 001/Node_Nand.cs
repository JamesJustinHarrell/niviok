using System.Collections.Generic;

class Node_Nand : INode_Expression {
	INode_Expression _first;
	INode_Expression _second;

	public Node_Nand( INode_Expression first, INode_Expression second ) {
		_first = first;
		_second = second;
	}
	
	public IValue execute(Scope scope) {		
		IValue first = _first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(true);
		
		IValue second = _second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "nand";
		objs = new object[]{ _first, _second };
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return Help.getIdentRefs( _first, _second ); }
	}
}