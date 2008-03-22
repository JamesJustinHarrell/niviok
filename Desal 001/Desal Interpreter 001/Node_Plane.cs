using System.Collections.Generic;

class Node_Plane : INode {
	IList<Node_DeclareFirst> _binds;

	public Node_Plane(IList<Node_DeclareFirst> binds) {
		_binds = binds;
	}	
	
	public IList<Node_DeclareFirst> binds {
		get { return _binds; }
	}
	
	//xxx remove
	//all decl-first children of all planes must be executed together, not plane by plane
	public void xxx_execute(Scope scope) {
		//reserve identikeys, so the closures will include
		//the identikeys from other declare-first nodes
		foreach( Node_DeclareFirst decl in _binds )
			scope.reserveDeclareFirst( decl.name.value );
	
		foreach( Node_DeclareFirst decl in _binds )
			decl.execute(scope);
	}
	
	public string typeName {
		get { return "plane"; }
	}
	
	public ICollection<INode> childNodes {
		get { return G.collect<INode>(_binds); }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}