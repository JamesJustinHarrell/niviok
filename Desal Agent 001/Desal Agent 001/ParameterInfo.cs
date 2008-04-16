class ParameterInfo {
	Direction _direction;
	NullableType _nullableType;
	Identifier _name;
	bool _hasDefaultValue;
	
	public ParameterInfo(
	Direction direction, NullableType nullableType,
	Identifier name, bool hasDefaultValue )
	{
		_direction = direction;
		_nullableType = nullableType;
		_name = name;
		_hasDefaultValue = hasDefaultValue;
	}
	
	public Direction direction {
		get { return _direction; }
	}
	
	public NullableType nullableType {
		get { return _nullableType; }
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public bool hasDefaultValue {
		get { return _hasDefaultValue; }
	}
}
