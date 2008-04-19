//temporary solution untill proper bindings can be created
/*
using System.Collections.Generic;

class NativeValue<T> : IWorker {
	T _obj;
	IInterfaceImplementation<T> _impl;
	
	public NativeValue(T obj, IInterfaceImplementation<T> impl) {
		_obj = obj;
		_impl = impl;
	}

	public IInterface activeInterface {
		get { throw new NotImplementedException(); }
	}
	public long objectID {
		get { throw new NotImplementedException(); }
	}
	public IWorker cast(IInterface aInterface) {
		throw new NotImplementedException();
	}
	public IWorker call(IList<Argument> arguments) {
		throw new NotImplementedException();
	}
	public IWorker getProperty(Identifier name) {
		throw new NotImplementedException();
	}
	public void setProperty(Identifier propName, IWorker aValue) {
		throw new NotImplementedException();
	}
	public IWorker callMethod(Identifier name, IList<Argument> arguments) {
		return _impl.callMethod(_obj, name, IList<Argument>);
	}
	public IWorker extractMember(Identifier name) {
		IInterface face = _impl.@interface;
		if( face.properties.ContainsKey(name) )
			return _impl.getProperty(_obj, this, name);
		if( face.methods.ContainsKey(name) )
			return new BoundMethod<T>(_obj, _impl, name);
		throw new ClientException(
			String.Format("no member with name: '{0}'", name.ToString()));
	}
	
	public T obj {
		get { return _obj; }
	}
}
*/