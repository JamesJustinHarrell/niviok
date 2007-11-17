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
	
	public long objectID {
		get { return _object.ID; }
	}
	
	public IValue cast(IInterface @interface) {
		return new Value<T>( _object, _faceimpl.cast(@interface) );
	}
	
	public void executeCall( Arguments arguments ) {
		try {
			_faceimpl.executeCall( _object.state, arguments );
		}
		catch(ClientException e) {
			e.pushFunc( "unknown" );
			throw e;
		}
	}
	
	public IValue evaluateCall( Arguments arguments ) {
		try {
			return _faceimpl.evaluateCall(_object.state, arguments);
		}
		catch(ClientException e) {
			e.pushFunc( "unknown" );
			throw e;
		}
	}
	
	public IValue getProperty(Identifier name) {
		return _faceimpl.getProperty(_object.state, this, name);
	}

	public void setProperty(Identifier propName, IValue value_) {
		_faceimpl.setProperty(_object.state, this, propName, value_);
	}
	
	public void executeMethod( Identifier name, Arguments arguments ) {
		try {
			_faceimpl.executeMethod(_object.state, name, arguments);
		}
		catch(ClientException e) {
			e.pushFunc( "." + name.str );
			throw e;
		}
	}
	
	public IValue evaluateMethod( Identifier name, Arguments arguments ) {
		try {
			return _faceimpl.evaluateMethod(_object.state, name, arguments);
		}
		catch(ClientException e) {
			e.pushFunc( "." + name.str );
			throw e;
		}
	}
}