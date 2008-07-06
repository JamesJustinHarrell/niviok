//corresponds to Desal "callee" nodes

using System.Collections.Generic;

class Callee {
	IList<ParameterInfo> _parameters;
	IType _returnType;
	
	public Callee(IList<ParameterInfo> parameters, IType returnType) {
		_parameters = parameters;
		_returnType = returnType;
	}

	public IList<ParameterInfo> parameters {
		get { return _parameters; }
	}
	
	public IType returnType {
		get { return _returnType; }
	}
}
