/* * /
static class InterfaceWrapper {
	public static IWorker wrap(IInterface aInterface) {
		//interface implementation info
		InterfaceImplementationInfo info =
			new InterfaceImplementationInfo(Objects.Interface);
		//xxx
		Console.WriteLine("WARNING: InterfaceWrapper: properties and methods unimplemented"); 
		
		//xxx create internal interface implementation that exposes the IInterface C# object
		
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
	public static IWorker wrap(IInterface interface_) {
		if( interface_ is InterfaceFromObjRef )
			return ((InterfaceFromObjRef)interface_).unwrap();
		else
			return new IWorker(
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
		get { throw new NotImplementedException(); }
	}
	
	public bool implements(IInterface interface_) {
		Console.WriteLine("WARNING: InterfaceWrapper.implements is unimplemented");
		return true;
	}
	
	public IWorker readProperty(IInterface interface_, Identifier ident) {
		throw new NotImplementedException();
	}
	
	public void writeProperty(IInterface interface_, Identifier ident, IWorker objRef) {
		throw new NotImplementedException();
	}
}
*/