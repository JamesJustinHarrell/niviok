class Parameter {
	Identifier _name;
	ReferenceType _type;
	IValue _defaultValue;
	
	//equivalent to having default parameters on the real constructor
	public Parameter(Identifier name, IInterface face)
		: this(name, ReferenceCategory.VALUE, face, null) {}
	public Parameter(Identifier name, IInterface face, IValue defaultValue)
		: this(name, ReferenceCategory.VALUE, face, defaultValue) {}
	public Parameter(Identifier name, ReferenceCategory cat)
		: this(name, cat, null, null) {}
	public Parameter(Identifier name, ReferenceCategory cat, IValue defaultValue)
		: this(name, cat, null, defaultValue) {}
	
	public Parameter(
	Identifier name, ReferenceCategory cat,
	IInterface face, IValue defaultValue) {
		if( name == null )
			throw new System.Exception("name can't be null");
		if( cat == ReferenceCategory.DYN && face != null )
			throw new System.Exception("dyn parameters are associated with an interface");
		if( cat != ReferenceCategory.DYN &&
		cat != ReferenceCategory.FUNCTION &&
		cat != ReferenceCategory.VALUE )
			throw new System.Exception("bad reference type");
		_name = name;
		_type = new ReferenceType(cat, face);
		_defaultValue = defaultValue;
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public ReferenceType type {
		get { return _type; }
	}
	
	public IValue defaultValue {
		get { return _defaultValue; }
	}
}