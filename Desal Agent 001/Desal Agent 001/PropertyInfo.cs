//corresponds to the property node
class PropertyInfo {
	Identifier _name;
	NullableType _type;
	Access _access;
	//xxx default values

	public PropertyInfo(Identifier name, NullableType type, Access access) {
		_name = name;
		_type = type;
		_access = access;
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public NullableType type {
		get { return _type; }
	}
	
	public Access access {
		get { return _access; }
	}
}
