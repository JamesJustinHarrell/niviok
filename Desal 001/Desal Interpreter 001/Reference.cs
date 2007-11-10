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
		_value = new NullValue(type.face);
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
		if( _type.category == ReferenceCategory.DYN || val.activeInterface == _type.face )
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
	ReferenceCategory _category;
	IInterface _face;
	
	public ReferenceType(ReferenceCategory cat, IInterface face) {
		if( cat == ReferenceCategory.DYN && face != null )
			throw new System.Exception("dyn ref associated with an interface");
		_category = cat;
		_face = face;
	}

	public ReferenceCategory category {
		get { return _category; }
	}
	
	public IInterface face {
		get { return _face; }
	}
}