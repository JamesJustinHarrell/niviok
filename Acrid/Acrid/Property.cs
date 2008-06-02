//corresponds to "property" nodes

class Property {
	Identifier _name;
	bool _writable;
	NullableType _type;

	public Property(Identifier name, bool writable, NullableType type) {
		_name = name;
		_writable = writable;
		_type = type;
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public bool writable {
		get { return _writable; }
	}
	
	public NullableType type {
		get { return _type; }
	}
}
