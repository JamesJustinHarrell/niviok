//function defined by native code

using System;
using System.Collections.Generic;
using System.Diagnostics;

class Function_Native : IFunction {
	public delegate IWorker FunctionType(IScope scope);

	FunctionType _function;
	IList<ParameterImpl> _parameters;
	NType _returnType;
	IScope _scope;
	
	public Function_Native(
	IList<ParameterImpl> parameters, NType returnType,
	FunctionType function, IScope scope )
	{
		_function = function;
		_parameters = parameters;
		_returnType = returnType;
		_scope = scope;
		
		if( _scope == null )
			_scope = new Scope(null, new ScopeAllowance(false,false));
	}
	
	public IList<ParameterImpl> parameters {
		get { return _parameters; }
	}
	
	public NType returnType {
		get { return _returnType; }
	}

	public IInterface face {
		get {
			IList<ParameterInfo> pinfos = new List<ParameterInfo>();
			foreach( ParameterImpl p in _parameters ) {
				pinfos.Add(
					new ParameterInfo(
						p.direction,
						p.type,
						p.name,
						p.defaultValue != null ));
			}
			return FunctionInterface.getFuncFace(
				new Callee(pinfos, _returnType));
		}
	}

	public IWorker call(IList<Argument> arguments) {
		IScope innerScope = G.setupArguments(_parameters, arguments, _scope);		
		return _function(innerScope);
	}
}