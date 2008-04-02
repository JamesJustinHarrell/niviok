using System.Collections.Generic;

/* xxx remove
user code cannot implement FuncInterface

class FunctionInterfaceFromObjRef : IFunctionInterface {
	public static IFunctionInterface wrap(IValue objRef) {
		if( objRef.object_ is FunctionInterfaceWrapper )
			return (objRef.object_ as FunctionInterfaceWrapper).unwrap();
		else
			return new FunctionInterfaceFromObjRef(objRef);
	}
	
	IValue _objRef;

	FunctionInterfaceFromObjRef(IValue objRef) {
		_objRef = objRef;
	}

	public IValue unwrap() {
		return _objRef;
	}

	public IList<Parameter> parameters {
		get { throw new Error_Unimplemented(); }
	}
	
	public IInterface returnType {
		get { throw new Error_Unimplemented(); }
	}
}
*/