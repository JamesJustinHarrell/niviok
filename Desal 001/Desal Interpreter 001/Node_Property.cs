class Node_Property : INode {
	Node_Identifier _name;
	Node_ReferenceType _type;
	Node_Access _access;

	public Node_Property(Node_Identifier name, Node_ReferenceType type, Node_Access access) {
		_name = name;
		_type = type;
		_access = access;
	}
	
	public Identifier name {
		get { return _name.identifier; }
	}
	
	public ReferenceType evaluateType(Scope scope) {
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
	
	public void getInfo(out string name, out object children) {
		name = "property";
		children = new object[]{ _name, _type, _access };
	}
}