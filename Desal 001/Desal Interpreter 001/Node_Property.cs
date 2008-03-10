using System.Collections.Generic;

class Node_Property : INode {
	Node_Identifier _name;
	Node_NullableType _type;
	Node_Access _access;

	public Node_Property(
	Node_Identifier name, Node_NullableType type, Node_Access access) {
		_name = name;
		_type = type;
		_access = access;
	}
	
	public Identifier name {
		get { return _name.identifier; }
	}
	
	public NullableType evaluateType(Scope scope) {
		return _type.evaluateType(scope);
	}
	
	public Access access {
		get { return _access.access; }
	}
	
	public PropertyInfo evaluatePropertyInfo(Scope scope) {
		return new PropertyInfo(
			_name.identifier,
			_type.evaluateType(scope),
			_access.access );
	}
	
	public string typeName {
		get { return "property"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _name, _type, _access }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _type, _access ); }
	}
}