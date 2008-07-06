//corresponds to Desal "parameter-info" nodes

class ParameterInfo {
	Direction _direction;
	IType _type;
	Identifier _name;
	bool _hasDefaultValue;
	
	public ParameterInfo(
	Direction direction, IType type,
	Identifier name, bool hasDefaultValue )
	{
		_direction = direction;
		_type = type;
		_name = name;
		_hasDefaultValue = hasDefaultValue;
	}
	
	public Direction direction {
		get { return _direction; }
	}
	
	public IType type {
		get { return _type; }
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public bool hasDefaultValue {
		get { return _hasDefaultValue; }
	}
}
