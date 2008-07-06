//corresponds to "property" nodes

class Property {
	Identifier _name;
	bool _writable;
	IType _type;

	public Property(Identifier name, bool writable, IType type) {
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
	
	public IType type {
		get { return _type; }
	}
}
