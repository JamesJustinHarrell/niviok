//function defined by client code

using System.Collections.Generic;

class Function_Client : IFunction {
	IList<ParameterImpl> _parameters;
	NullableType _returnType;
	INode_Expression _body;
	Scope _scope; //should have already been trimmed for closure

	public Function_Client(
	IList<ParameterImpl> parameters, NullableType returnType,
	INode_Expression body, Scope scope )
	{
		_parameters = parameters;
		_returnType = returnType;
		_body = body;
		_scope = scope;
	}
	
	public IInterface face {
		get {
			IList<ParameterInfo> paramInfos = new List<ParameterInfo>();
			foreach( ParameterImpl p in _parameters ) {
				paramInfos.Add(
					new ParameterInfo(
						p.direction,
						p.nullableType,
						p.name,
						p.defaultValue != null));
			}
			return FunctionInterface.getFuncFace(
				new Callee(paramInfos, _returnType));
		}
	}
	
	public IWorker call(IList<Argument> arguments) {
		return Executor.execute(
			_body,
			G.setupArguments(_parameters, arguments, _scope));
	}	
}