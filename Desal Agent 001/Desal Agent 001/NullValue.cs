using System;

class NullValue : IValue {
	IInterface _face;
	
	public NullValue() :this(null) {}
	
	public NullValue(IInterface face) {
		_face = face;
	}
	
	//xxx should this throw if _face is null?
	public IInterface activeInterface {
		get { return _face; }
	}
	
	//xxx what should happen with untyped nulls?
	public IValue cast(IInterface face) {
		if( G.inheritsOrIs(_face, face) )
			return new NullValue(face);
		throw new ClientException(
			"the associated interface of this null value " +
			"does not inherit from the requested interface");
	}

	public IValue call(Arguments arguments) {
		throw new ClientException("attempted to call a null value");
	}
	
	public IValue extractMember(Identifier memberName) {
		throw new ClientException(
			String.Format(
				"attempted to extract member '{0}' from a null value",
				memberName));
	}
	
	public void setProperty(Identifier propName, IValue aValue) {
		throw new ClientException(
			String.Format(
				"attempted to set property '{0}' of a null value",
				propName));
	}
}