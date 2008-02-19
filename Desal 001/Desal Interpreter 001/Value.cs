using System.Collections.Generic;

class Value<T> : IValue {
	IObject<T> _object;
	IInterfaceImplementation<T> _faceimpl;
	
	public Value( IObject<T> @object, IInterfaceImplementation<T> faceimpl ) {
		if( @object == null || faceimpl == null )
			throw new System.Exception("null arguments");
		/* xxx enable
		if( ! @object.sameClass(faceimpl) )
			throw new System.Exception("object and interface implementation not associated with same class");
		*/
		_object = @object;
		_faceimpl = faceimpl;
	}
	
	//not a part of IValue
	public T state {
		get { return _object.state; }
	}
	
	public IInterface activeInterface {
		get { return _faceimpl.@interface; }
	}
	
	public IValue cast(IInterface @interface) {
		return new Value<T>( _object, _faceimpl.cast(@interface) );
	}
	
	public IValue call( Arguments arguments ) {
		try {
			return _faceimpl.call(_object.state, arguments);
		}
		catch(ClientException e) {
			e.pushFunc( "unknown (in native class Value)" );
			throw e;
		}
	}
	
	public IValue extractNamedMember(Identifier name) {
		IInterface face = _faceimpl.@interface;
		if( face.properties.ContainsKey(name) )
			return _faceimpl.getProperty(_object.state, this, name);
		if( face.methods.ContainsKey(name) )
			return new BoundMethod<T>(_object.state, _faceimpl, name);
		throw new ClientException(
			System.String.Format("no member with name: '{0}'", name.str));
	}
	
	public IValue getProperty(Identifier name) {
		return _faceimpl.getProperty(_object.state, this, name);
	}

	public void setProperty(Identifier propName, IValue value_) {
		_faceimpl.setProperty(_object.state, this, propName, value_);
	}
	
	public IValue callMethod( Identifier name, Arguments arguments ) {
		try {
			return _faceimpl.callMethod(_object.state, name, arguments);
		}
		catch(ClientException e) {
			e.pushFunc( "." + name.str + " (in native class Value)" );
			throw e;
		}
	}
}