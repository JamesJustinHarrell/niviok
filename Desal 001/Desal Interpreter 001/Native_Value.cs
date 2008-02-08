//temporary solution untill proper bindings can be created

using System.Collections.Generic;

class NativeValue<T> : IValue {
	T _obj;
	IInterfaceImplementation<T> _impl;
	
	public NativeValue(T obj, IInterfaceImplementation<T> impl) {
		_obj = obj;
		_impl = impl;
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
		throw new Error_Unimplemented();
	}
	public IValue getProperty(Identifier name) {
		throw new Error_Unimplemented();
	}
	public void setProperty(Identifier propName, IValue aValue) {
		throw new Error_Unimplemented();
	}
	public IValue callMethod(Identifier name, Arguments arguments) {
		return _impl.evaluateMethod(_obj, name, arguments);
	}
	
	public T obj {
		get { return _obj; }
	}
}
