//makes a Desal function 

/*
using System.Collections.Generic;


static class FunctionWrapper {
	public static IWorker wrap(IFunction function) {
		IInterface face = function.face;
		DesalObject obj = new DesalObject();
		NativeWorkerBuilder builder = new NativeWorkerBuilder(obj);
		builder.addCallee(function);
		return builder.compile(face);
	}
	
	IObject owner {get;}
	IList<IWorker> children {get;}
	IInterface face {get;}
	IWorker breed(IInterface face);
	IWorker call(IList<Argument> arguments);
	IWorker extractMember(Identifier name);
	void setProperty(Identifier propName, IWorker worker);
}


class FW_Value : IWorker {
	FW_InterfaceImplementation _interfaceImpl;

	public FW_Value(FW_InterfaceImplementation faceImpl) {
		_interfaceImpl = faceImpl;
	}

	public IInterface activeInterface {
		get { throw new NotImplementedException(); }
	}

	public IWorker cast(IInterface aInterface) {
		throw new NotImplementedException();
	}
	
	public IWorker call(IList<Argument> arguments) {
		return _interfaceImpl.evaluateCall(IList<Argument>);
	}
	
	public IWorker extractMember(Identifier name) {
		throw new NotImplementedException();
	}

	public void setProperty(Identifier propName, IWorker aValue) {
		throw new NotImplementedException();
	}
}

class FW_InterfaceImplementation {
	IFunction _func;
	
	public FW_InterfaceImplementation(IFunction func) {
		_func = func;
	}
	
	public IWorker evaluateCall(IList<Argument> args) {
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
		return class_.instantiate( new IWorker[]{} );
	}
}

/* xxx remove

	public static IWorker wrap(IFunction function) {
		if( function is FunctionFromObjRef )
			return ((FunctionFromObjRef)function).unwrap();
		else
		/*
			return new IWorker(
				new FunctionWrapper(function),
				Objects.getFunctionInterface(function) );
			* /
			throw new NotImplementedException();
	}

	IFunction _function;
	IFunctionInterface _functionInterface;
	
	FunctionWrapper(IFunction function) {
		_function = function;
		_functionInterface = Objects.getFunctionInterface(_function);
	}
	
	
	
	public Class class_ {
		get { throw new NotImplementedException(); }
	}
	
	public IFunction unwrap() {
		return _function;
	}
	
	public long ID {
		get { throw new NotImplementedException(); }
	}
	
	public bool implements(IInterface interface_) {
		if( interface_ == Objects.Object )
			return true;
		if( interface_ is IFunctionInterface &&
		 ((IFunctionInterface)interface_) == _functionInterface )
			return true;
		return false;
	}
	
	public IWorker readProperty(IInterface interface_, Identifier ident) {
		throw new NotImplementedException();
	}
	
	public void writeProperty(IInterface interface_, Identifier ident, IWorker objRef) {
		throw new NotImplementedException();
	}
};
*/