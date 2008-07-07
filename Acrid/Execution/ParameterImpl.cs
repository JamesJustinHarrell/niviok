//corresponds to "parameter-impl" nodes

/* xxx
the default value expression is supposed
to be evaluated for every call to the function
*/

using Acrid.NodeTypes;

namespace Acrid.Execution {

public class ParameterImpl {
	Direction _direction;
	IType _type;
	Identifier _name;
	IWorker _defaultValue;
	
	public ParameterImpl(
	Direction direction, IType type,
	Identifier name, IWorker defaultValue )
	{
		_direction = direction;
		_type = type;
		_name = name;
		_defaultValue = defaultValue;
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
	
	public IWorker defaultValue {
		get { return _defaultValue; }
	}
}

} //namespace
