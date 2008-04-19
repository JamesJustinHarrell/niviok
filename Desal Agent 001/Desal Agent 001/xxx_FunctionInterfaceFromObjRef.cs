using System.Collections.Generic;

/* xxx remove
user code cannot implement FuncInterface

class FunctionInterfaceFromObjRef : IFunctionInterface {
	public static IFunctionInterface wrap(IWorker objRef) {
		if( objRef.object_ is FunctionInterfaceWrapper )
			return (objRef.object_ as FunctionInterfaceWrapper).unwrap();
		else
			return new FunctionInterfaceFromObjRef(objRef);
	}
	
	IWorker _objRef;

	FunctionInterfaceFromObjRef(IWorker objRef) {
		_objRef = objRef;
	}

	public IWorker unwrap() {
		return _objRef;
	}

	public IList<Parameter> parameters {
		get { throw new NotImplementedException(); }
	}
	
	public IInterface returnType {
		get { throw new NotImplementedException(); }
	}
}
*/