//xxx isn't this bad?
//makes a client object that implements a Function interface look like an IFunction

/* xxx

using System.Collections.Generic;

class FunctionFromObjRef : IFunction {
	public static IFunction wrap(IIValue objRef) {
		if( objRef.object_ is FunctionWrapper )
			return (objRef.object_ as FunctionWrapper).unwrap();
		else
			return new FunctionFromObjRef(objRef);
	}

	IWorker _objRef;

	FunctionFromObjRef(IWorker objRef) {
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
	
	public void executeCall(IList<IWorker> IList<Argument>) {
		throw new NotImplementedException();
	}
	
	public IWorker evaluateCall(IList<IWorker> IList<Argument>) {
		throw new NotImplementedException();
	}
}

*/