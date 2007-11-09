class Reference {
	ReferenceType _type;
	bool _constant;
	IValue _value;
	
	public Reference( ReferenceType type, bool constant, IValue val ) {
		_type = type;
		_constant = constant;
		_value = val;
	}

	public Reference( ReferenceType type, IValue val ) {
		_type = type;
		_constant = false;
		_value = val;
	}

	public Reference( ReferenceType type ) {
		_type = type;
		_constant = false;
		_value = new NullValue(type.iface);
	}
	
	public ReferenceType type {
		get { return _type; }
	}
	
	public bool constant {
		get { return _constant; }
	}
	
	public IValue @value {
		get { return _value; }
	}
	
	public void setValue(IValue val) {
		if( _type.category == ReferenceCategory.DYN || val.activeInterface == _type.iface )
			_value = val;
		throw new Error_ArgumentInterface();
	}
}

//xxx move

enum ReferenceCategory {
	VALUE, FUNCTION, DYN
}

enum IdentifierCategory {
	VALUE, FUNCTION, DYN, NAMESPACE, ALIAS
}

class ReferenceType {
	public ReferenceCategory category;
	public IInterface iface;
	
	public ReferenceType(ReferenceCategory cat, IInterface @interface) {
		category = cat;
		iface = @interface;
	}
}