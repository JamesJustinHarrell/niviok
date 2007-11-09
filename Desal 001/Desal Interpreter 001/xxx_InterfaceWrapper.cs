/*
static class InterfaceWrapper {
	public static IValue wrap(IInterface aInterface) {
		//interface implementation info
		InterfaceImplementationInfo info =
			new InterfaceImplementationInfo(Objects.Interface);
		//xxx
		System.Console.WriteLine("WARNING: InterfaceWrapper: properties and methods unimplemented"); 
		
		//xxx create internal interface implementation that exposes the IInterface C# object
		
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
	public static IValue wrap(IInterface interface_) {
		if( interface_ is InterfaceFromObjRef )
			return ((InterfaceFromObjRef)interface_).unwrap();
		else
			return new IValue(
				Objects.Interface, //xxx send correct interface
				new InterfaceWrapper(interface_) );
	}

	IInterface _interface;

	InterfaceWrapper(IInterface interface_) {
		_interface = interface_;
	}
	
	public IInterface unwrap() {
		return _interface;
	}

	public long ID {
		get { throw new Error_Unimplemented(); }
	}
	
	public bool implements(IInterface interface_) {
		System.Console.WriteLine("WARNING: InterfaceWrapper.implements is unimplemented");
		return true;
	}
	
	public IValue readProperty(IInterface interface_, Identifier ident) {
		throw new Error_Unimplemented();
	}
	
	public void writeProperty(IInterface interface_, Identifier ident, IValue objRef) {
		throw new Error_Unimplemented();
	}
}
*/