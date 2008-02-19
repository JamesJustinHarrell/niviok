using System.Collections.Generic;

class Node_Plane : INode {
	IList<Node_DeclareFirst> _binds;
	Scope _scope;

	public Node_Plane(IList<Node_DeclareFirst> binds) {
		_binds = binds;
		_scope = new Scope();
	}	
	
	//xxx
	public void xxx_execute(Scope scope) {
		//reserve identikeys, so the closures will include
		//the identikeys from other declare-first nodes
		foreach( Node_DeclareFirst decl in _binds )
			scope.reserveDeclareFirst( decl.name );
	
		foreach( Node_DeclareFirst decl in _binds )
			decl.execute(scope);
	}
	
	public void getInfo(out string name, out object children) {
		name = "plane";
		children = _binds;
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = Help.getIdentRefs( _binds );
			foreach( Node_DeclareFirst decl in _binds )
				idents.Remove( decl.name ); //xxx only if not function
			return idents;
		}
	}
}