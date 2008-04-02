//xxx should this be merged into Interface?

using System.Collections.Generic;

interface IFunctionInterface : IInterface {
	IList<Parameter> parameters { get; }
	NullableType returnType { get; }
}