//makes an IFunctionInterface appear like a Desal object
//xxx can InterfaceWrapper do this instead?

/* xxx remove
function interfaces are not objects (anymore)

class FunctionInterfaceWrapper : IObject {
	public static IWorker wrap(IFunctionInterface functionInterface) {
		if( functionInterface is FunctionFromObjRef )
			return (functionInterface as FunctionFromObjRef).unwrap();
		else
			return new IWorker(
				Objects.Interface, //xxx send correct interface
				new FunctionInterfaceWrapper(functionInterface) );
	}

	IFunctionInterface _functionInterface;

	FunctionInterfaceWrapper(IFunctionInterface functionInterface) {
		_functionInterface = functionInterface;
	}
	
	public IFunctionInterface unwrap() {
		return _functionInterface;
	}
	
	public long ID {
		get { throw new NotImplementedException(); }
	}
	
	public bool implements(IInterface interface_) {
		return ( interface_ == Objects.Object || interface_ == Objects.FuncInterface );
	}
	
	public IWorker readProperty(IInterface interface_, Identifier ident) {
		throw new NotImplementedException();
	}
	
	public void writeProperty(IInterface interface_, Identifier ident, IWorker objRef) {
		throw new NotImplementedException();
	}
}
*/