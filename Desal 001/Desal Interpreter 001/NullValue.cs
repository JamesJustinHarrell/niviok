class NullValue : IValue {
	IInterface _iface;
	
	public NullValue()
	: this(null)
	{}
	
	public NullValue(IInterface iface) {
		_iface = iface;
	}
	
	public IInterface activeInterface {
		get { return _iface; }
	}
	
	public long objectID {
		get { throw new Error_Unimplemented(); }
	}
	
	public IValue cast(IInterface aInterface) {
		//xxx allow casting down to inherited interfaces
		throw new Error_Unimplemented();
	}

	public IValue call(Arguments arguments) {
		throw new ClientException("null");
	}
	
	public IValue getProperty(Identifier name) {
		throw new ClientException("null");
	}
	
	public void setProperty(Identifier propName, IValue aValue) {
		throw new ClientException("null");
	}
	
	public IValue callMethod(Identifier name, Arguments arguments) {
		throw new ClientException("null");
	}
}