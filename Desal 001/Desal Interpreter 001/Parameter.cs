//xxx the default value expression is supposed to be evaluated for every call to the function
class Parameter {
	NullableType _nullableType;
	Identifier _name;
	bool _hasDefaultValue;
	IValue _defaultValue;
	
	public Parameter(
	/* xxx direction */
	NullableType nullableType, Identifier name )
		: this(nullableType, name, false, null) {}
	
	public Parameter(
	/* xxx direction */
	NullableType nullableType, Identifier name,
	bool hasDefaultValue, IValue defaultValue) {
		/* xxx direction */
		_nullableType = nullableType;
		_name = name;
		_hasDefaultValue = hasDefaultValue;
		_defaultValue = defaultValue;
	}
	
	public NullableType nullableType {
		get { return _nullableType; }
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public IValue defaultValue {
		get { return _defaultValue; }
	}
}