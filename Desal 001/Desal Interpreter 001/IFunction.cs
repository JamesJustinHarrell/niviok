//DEPRECATED - a "function" is now just a callable value
//internal - exposed by calling a value

using System.Collections.Generic;

interface IFunction {
	IList<Parameter> parameters { get; }
	ReferenceType returnType { get; }
	void executeCall(Arguments arguments);
	IValue evaluateCall(Arguments arguments);
}