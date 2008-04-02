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
	NullableType _returnType;
	Scope _scope;
	
	public NativeFunction(
	Bridge bridge,
	FunctionType function, IList<Parameter> parameters,
	NullableType returnType, Scope scope ) {
		_bridge = bridge;
		_function = function;
		_parameters = parameters;
		_returnType = returnType;
		_scope = scope;
	}
	
	public IList<Parameter> parameters {
		get { return _parameters; }
	}
	
	public NullableType returnType {
		get { return _returnType; }
	}

	public IValue call(Arguments arguments) {
		Scope innerScope = arguments.setup(_parameters, _scope);		
		_function(_bridge, innerScope);
		
		//xxx
		return new NullValue();
	
		/*
		Scope functionScope = arguments.setup(_scope);

		if( _function is FunctionExpression )
			return ((FunctionExpression)_function)(functionScope, arguments);
			
		throw new Exception("function is not an expression");
		*/
	}
}