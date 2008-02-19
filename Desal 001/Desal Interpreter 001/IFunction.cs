//DEPRECATED - a "function" is now just a callable value
//internal - exposed by calling a value

using System.Collections.Generic;

interface IFunction {
	IList<Parameter> parameters { get; }
	NullableType returnType { get; }
	IValue call(Arguments arguments);
}