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
		try {
			val.call(
				new Arguments(
					new IValue[]{},
					new Dictionary<Identifier, IValue>() ));
		}
		catch(ClientException e) {
			e.pushFunc("main (in native class old_Node_Bundle)");
			throw e;
		}
		
		//xxx if main returns something, evaluateCall and return the result
		return 0;
	}
	
	public string typeName {
		get { return "bundle"; }
	}
	
	public ICollection<INode> childNodes {
		get { return G.collect<INode>(_planes); }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}