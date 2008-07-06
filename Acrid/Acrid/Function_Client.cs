//function defined using a Desal "function" node

using System.Collections.Generic;

class Function_Client : IFunction {
	IList<ParameterImpl> _parameters;
	NType _returnType;
	INode_Expression _body;
	IScope _scope; //should have already been trimmed for closure

	public Function_Client(
	IList<ParameterImpl> parameters, NType returnType,
	INode_Expression body, IScope scope )
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
						p.type,
						p.name,
						p.defaultValue != null));
			}
			return FunctionInterface.getFuncFace(
				new Callee(paramInfos, _returnType));
		}
	}
	
	public IWorker call(IList<Argument> arguments) {
		IScope argScope = G.setupArguments(_parameters, arguments, _scope);
	
		try {
			return Executor.executeAny(_body, argScope);
		}
		catch(ClientReturn ret) {
			if( ret.worker == null ) {
				return new Null();
			}
			else {
				return ret.worker;
			}
		}
	}	
}
