using System.Collections.Generic;

class Node_Assign : INode_Expression {
	Node_Identifier _name;
	INode_Expression _value;

	public Node_Assign( Node_Identifier name, INode_Expression val ) {
		_name = name;
		_value = val;
	}

	public IValue execute(Scope scope) {
		IValue val = _value.execute(scope);
		scope.assign( _name.identifier, val );
		return val;
	}
	
	public string typeName {
		get { return "assign"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _name, _value }; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _name, _value ); }
	}
}