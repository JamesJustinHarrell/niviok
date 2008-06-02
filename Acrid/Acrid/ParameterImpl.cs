//corresponds to "parameter-impl" nodes

/* xxx
the default value expression is supposed
to be evaluated for every call to the function
*/
class ParameterImpl {
	Direction _direction;
	NullableType _nullableType;
	Identifier _name;
	IWorker _defaultValue;
	
	public ParameterImpl(
	Direction direction, NullableType nullableType,
	Identifier name, IWorker defaultValue )
	{
		_direction = direction;
		_nullableType = nullableType;
		_name = name;
		_defaultValue = defaultValue;
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
	
	public IWorker defaultValue {
		get { return _defaultValue; }
	}
}
