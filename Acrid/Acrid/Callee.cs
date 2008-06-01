//corresponds to Desal "callee" nodes

using System.Collections.Generic;

class Callee {
	IList<ParameterInfo> _parameters;
	NullableType _returnType;
	
	public Callee(IList<ParameterInfo> parameters, NullableType returnType) {
		_parameters = parameters;
		_returnType = returnType;
	}

	public IList<ParameterInfo> parameters {
		get { return _parameters; }
	}
	
	public NullableType returnType {
		get { return _returnType; }
	}
}
