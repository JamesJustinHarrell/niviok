using System.Collections.Generic;

class Node_Plane : INode {
	IList<Node_DeclarationPervasive> _binds;
	Scope _scope;

	public Node_Plane(IList<Node_DeclarationPervasive> binds) {
		_binds = binds;
		_scope = new Scope();
	}	
	
	//xxx
	public void xxx_execute(Scope scope) {
		foreach( Node_DeclarationPervasive decl in _binds ) {
			decl.execute(scope);
		}
	}
	
	public void getInfo(out string name, out object children) {
		name = "plane";
		children = _binds;
	}
}