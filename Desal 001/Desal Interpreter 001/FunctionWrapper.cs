//makes a Desal function 

using System.Collections.Generic;


static class FunctionWrapper {
	public static IValue wrap(IFunction function) {
		FW_InterfaceImplementation faceImpl = new FW_InterfaceImplementation(function);
		FW_Value val = new FW_Value(faceImpl);
		return val;
	}
}


class FW_Value : IValue {
	FW_InterfaceImplementation _interfaceImpl;

	public FW_Value(FW_InterfaceImplementation faceImpl) {
		_interfaceImpl = faceImpl;
	}

	public IInterface activeInterface {
		get { throw new Error_Unimplemented(); }
	}
	public long objectID {
		get { throw new Error_Unimplemented(); }
	}
	public IValue cast(IInterface aInterface) {
		throw new Error_Unimplemented();
	}
	public IValue call(Arguments arguments) {
		return _interfaceImpl.evaluateCall(arguments);
	}
	public IValue getProperty(Identifier name) {
		throw new Error_Unimplemented();
	}
	public void setProperty(Identifier propName, IValue aValue) {
		throw new Error_Unimplemented();
	}
	public IValue callMethod(Identifier name, Arguments arguments) {
		throw new Error_Unimplemented();
	}
}

class FW_InterfaceImplementation {
	IFunction _func;
	
	public FW_InterfaceImplementation(IFunction func) {
		_func = func;
	}
	
	public IValue evaluateCall(Arguments args) {
		return _func.call(args);
	}
}



/*
		//interfaces of parameters
		IList<IInterface> paramTypes = new List<IInterface>();
		foreach( Parameter param in function.parameters )
			paramTypes.Add( param.type );
		
		//interface implementation info
		InterfaceImplementationInfo info = new InterfaceImplementationInfo(
			Objects.getFunctionInterface(
				paramTypes, function.returnType ) );
		info.callees.Add( function.parameters, function );
		
		//class
		Class class_ = new Class(
			new ClassMember[]{},
			new InterfaceImplementationInfo[]{ info },
			info,
			null );

		//instantiation of class
		return class_.instantiate( new IValue[]{} );
	}
}

/* xxx remove

	public static IValue wrap(IFunction function) {
		if( function is FunctionFromObjRef )
			return ((FunctionFromObjRef)function).unwrap();
		else
		/*
			return new IValue(
				new FunctionWrapper(function),
				Objects.getFunctionInterface(function) );
			* /
			throw new Error_Unimplemented();
	}

	IFunction _function;
	IFunctionInterface _functionInterface;
	
	FunctionWrapper(IFunction function) {
		_function = function;
		_functionInterface = Objects.getFunctionInterface(_function);
	}
	
	
	
	public Class class_ {
		get { throw new Error_Unimplemented(); }
	}
	
	public IFunction unwrap() {
		return _function;
	}
	
	public long ID {
		get { throw new Error_Unimplemented(); }
	}
	
	public bool implements(IInterface interface_) {
		if( interface_ == Objects.Object )
			return true;
		if( interface_ is IFunctionInterface &&
		 ((IFunctionInterface)interface_) == _functionInterface )
			return true;
		return false;
	}
	
	public IValue readProperty(IInterface interface_, Identifier ident) {
		throw new Error_Unimplemented();
	}
	
	public void writeProperty(IInterface interface_, Identifier ident, IValue objRef) {
		throw new Error_Unimplemented();
	}
};
*/