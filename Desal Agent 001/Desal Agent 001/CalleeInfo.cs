using System.Collections.Generic;

//corresponds to the callee node
class CalleeInfo {
	IList<ParameterInfo> _parameters;
	NullableType _returnType;
	
	public CalleeInfo(IList<ParameterInfo> parameters, NullableType returnType) {
		_parameters = parameters;
		_returnType = returnType;
	}

	IList<ParameterInfo> parameters {
		get { return _parameters; }
	}
	
	NullableType returnType {
		get { return _returnType; }
	}
}