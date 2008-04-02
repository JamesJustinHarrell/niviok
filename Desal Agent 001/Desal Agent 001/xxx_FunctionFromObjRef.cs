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

	IValue _objRef;

	FunctionFromObjRef(IValue objRef) {
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
	
	public void executeCall(IList<IValue> arguments) {
		throw new Error_Unimplemented();
	}
	
	public IValue evaluateCall(IList<IValue> arguments) {
		throw new Error_Unimplemented();
	}
}

*/