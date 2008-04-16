//function defined by native code

using System.Collections.Generic;

class Function_Native : IFunction {
	public delegate IWorker FunctionType(Scope scope);

	FunctionType _function;
	IList<ParameterImpl> _parameters;
	NullableType _returnType;
	Scope _scope;
	
	public Function_Native(
	IList<ParameterImpl> parameters, NullableType returnType,
	FunctionType function, Scope scope )
	{
		_function = function;
		_parameters = parameters;
		_returnType = returnType;
		_scope = scope;
	}
	
	public IList<ParameterImpl> parameters {
		get { return _parameters; }
	}
	
	public NullableType returnType {
		get { return _returnType; }
	}

	public IInterface face {
		get {
			IList<ParameterInfo> pinfos = new List<ParameterInfo>();
			foreach( ParameterImpl p in _parameters ) {
				pinfos.Add(
					new ParameterInfo(
						p.direction,
						p.nullableType,
						p.name,
						p.defaultValue != null ));
			}
			return FunctionInterface.getFuncFace(
				new CalleeInfo(pinfos, _returnType));
		}
	}

	public IWorker call(IList<Argument> arguments) {
		Scope innerScope = G.setupArguments(_parameters, arguments, _scope);		
		return _function(innerScope);
	}
}