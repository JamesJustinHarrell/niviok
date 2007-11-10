//xxx too many constructors

class Parameter {
	Identifier _name;
	ReferenceType _type;
	IValue _defaultValue;
	bool _nullable;
	
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
	IInterface face, IValue defaultValue)
		: this(name, new ReferenceType(cat, face), defaultValue, false) {}
	
	public Parameter(
	Identifier name, ReferenceType type,
	IValue defaultValue, bool nullable) {
		if( name == null )
			throw new System.Exception("name can't be null");
		_name = name;
		_type = type;
		_defaultValue = defaultValue;
		_nullable = nullable;
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