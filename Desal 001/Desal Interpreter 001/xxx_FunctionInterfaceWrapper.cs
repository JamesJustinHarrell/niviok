//makes an IFunctionInterface appear like a Desal object
//xxx can InterfaceWrapper do this instead?

/* xxx remove
function interfaces are not objects (anymore)

class FunctionInterfaceWrapper : IObject {
	public static IValue wrap(IFunctionInterface functionInterface) {
		if( functionInterface is FunctionFromObjRef )
			return (functionInterface as FunctionFromObjRef).unwrap();
		else
			return new IValue(
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
		get { throw new Error_Unimplemented(); }
	}
	
	public bool implements(IInterface interface_) {
		return ( interface_ == Objects.Object || interface_ == Objects.FuncInterface );
	}
	
	public IValue readProperty(IInterface interface_, Identifier ident) {
		throw new Error_Unimplemented();
	}
	
	public void writeProperty(IInterface interface_, Identifier ident, IValue objRef) {
		throw new Error_Unimplemented();
	}
}
*/