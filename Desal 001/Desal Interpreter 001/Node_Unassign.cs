//NOTE: may be removed in favor of just assigning null

using System.Collections.Generic;

class Node_Unassign : INode_Expression {
	Node_Identifier _name;

	public Node_Unassign( Node_Identifier name ) {
		_name = name;
	}
	
	public IValue execute(Scope scope) {
		IValue val = _name.execute(scope);
		scope.assign(_name.identifier, new NullValue());
		return val;
	}
	
	public string typeName {
		get { return "unassign"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _name }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return _name.identikeyDependencies; }
	}
}

