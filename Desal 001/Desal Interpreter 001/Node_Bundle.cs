using System.Collections.Generic;

class Node_Bundle : INode {
	IList<Node_Plane> _planes;
	Scope _scope;

	public Node_Bundle(IList<Node_Plane> planes) {
		_planes = planes;
	}		
	
	public void setup(Scope scope) {
		if( _scope != null )
			throw new System.Exception("scope already set");
		_scope = scope;
	}
	
	public int run() {
		if( _scope == null )
			throw new System.Exception("scope not set");
	
		foreach( Node_Plane plane in _planes ) {
			plane.xxx_execute(_scope);
		}
		
		IValue val = _scope.evaluateIdentifier( new Identifier("main") );
		val.executeCall(
			new Arguments(
				new IValue[]{},
				new Dictionary<Identifier, IValue>() ));
		
		//xxx if main returns something, evaluateCall and return the result
		return 0;
	}
	
	public void getInfo(out string name, out object children) {
		name = "bundle";
		children = _planes;
	}
}