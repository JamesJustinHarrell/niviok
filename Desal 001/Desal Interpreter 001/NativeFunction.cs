//DEPRECATED because IFunction is deprecated
//function defined by native code

using System.Collections.Generic;

class NativeFunction : IFunction {
	/*xxx enable
	public delegate void FunctionStatement(Scope scope);
	public delegate IValue FunctionExpression(Scope scope);
	*/
	public delegate void FunctionType(Bridge bridge, Scope scope);

	Bridge _bridge;
	FunctionType _function;
	IList<Parameter> _parameters;
	ReferenceType _returnType;
	Scope _scope;
	
	public NativeFunction(
	Bridge bridge,
	FunctionType function, IList<Parameter> parameters,
	ReferenceType returnType, Scope scope ) {
		_bridge = bridge;
		_function = function;
		_parameters = parameters;
		_returnType = returnType;
		_scope = scope;
	}
	
	public IList<Parameter> parameters {
		get { return _parameters; }
	}
	
	public ReferenceType returnType {
		get { return _returnType; }
	}
	
	public void executeCall(Arguments arguments) {
		Scope innerScope = arguments.setup(_parameters, _scope);		
		_function(_bridge, innerScope);
	}
	
	public IValue evaluateCall(Arguments arguments) {
		throw new System.Exception("native functions cannot yet return values");
	
		/*
		Scope functionScope = arguments.setup(_scope);

		if( _function is FunctionExpression )
			return ((FunctionExpression)_function)(functionScope, arguments);
			
		throw new Exception("function is not an expression");
		*/
	}
}