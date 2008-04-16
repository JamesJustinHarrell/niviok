using System.Collections.Generic;

/*
class Value<T> : IWorker {
	IObject<T> _object;
	IInterfaceImplementation<T> _faceimpl;
	
	public Value( IObject<T> @object, IInterfaceImplementation<T> faceimpl ) {
		if( @object == null || faceimpl == null )
			throw new System.Exception("null IList<Argument>");
		/* xxx enable
		if( ! @object.sameClass(faceimpl) )
			throw new System.Exception("object and interface implementation not associated with same class");
		* /
		_object = @object;
		_faceimpl = faceimpl;
	}
	
	//not a part of IWorker
	public T state {
		get { return _object.state; }
	}
	
	public IInterface activeInterface {
		get { return _faceimpl.@interface; }
	}
	
	public IWorker cast(IInterface @interface) {
		return new Value<T>( _object, _faceimpl.cast(@interface) );
	}
	
	public IWorker call( IList<Argument> arguments ) {
		try {
			return _faceimpl.call(_object.state, IList<Argument>);
		}
		catch(ClientException e) {
			e.pushFunc( "unknown (in native class Value)" );
			throw e;
		}
	}
	
	public IWorker extractMember(Identifier name) {
		IInterface face = _faceimpl.@interface;
		if( face.properties.ContainsKey(name) )
			return _faceimpl.getProperty(_object.state, this, name);
		if( face.methods.ContainsKey(name) )
			return new BoundMethod<T>(_object.state, _faceimpl, name);
		throw new ClientException(
			System.String.Format("no member with name: '{0}'", name.ToString()));
	}
	
	public IWorker getProperty(Identifier name) {
		return _faceimpl.getProperty(_object.state, this, name);
	}

	public void setProperty(Identifier propName, IWorker value_) {
		_faceimpl.setProperty(_object.state, this, propName, value_);
	}
	
	public IWorker callMethod( Identifier name, IList<Argument> arguments ) {
		try {
			return _faceimpl.callMethod(_object.state, name, IList<Argument>);
		}
		catch(ClientException e) {
			e.pushFunc( "." + name.ToString() + " (in native class Value)" );
			throw e;
		}
	}
}
*/