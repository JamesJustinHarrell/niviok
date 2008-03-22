using System.Collections.Generic;

class Node_Plane : INode {
	IList<Node_DeclareFirst> _binds;

	public Node_Plane(IList<Node_DeclareFirst> binds) {
		_binds = binds;
	}	
	
	//xxx
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
		get {
			HashSet<Identifier> idents = G.depends( _binds );
			foreach( Node_DeclareFirst decl in _binds )
				idents.Remove( decl.name.value ); //xxx only if not function
			return idents;
		}
	}
}