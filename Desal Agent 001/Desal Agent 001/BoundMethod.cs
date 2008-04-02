/*
a method that's bound to the object it's attached to, appearing like a free function
e.g. something.doSomething evaluates to a BoundMethod

XXX This approach is completetly wrong.
I need to build an interface implementation and an object,
and then combine them to form a value.
Idealy, there would only be one implementation of the IValue interface.
*/

class BoundMethod<T> : IValue {
	T _state;
	IInterfaceImplementation<T> _faceimpl;
	Identifier _name;

	public BoundMethod( T state, IInterfaceImplementation<T> faceimpl, Identifier name ) {
		_state = state;
		_faceimpl = faceimpl;
		_name = name;
	}
	
	public IInterface activeInterface {
		/* xxx
		need to combine all interfaces at:
		_faceimpl.@interface.methods[_name]
		And maybe the interfaces of methods from inheritees, depending on how it gets speced out. 
		*/
	
		get { throw new Error_Unimplemented(); }
	}
	
	public IValue cast(IInterface aInterface) {
		throw new Error_Unimplemented();
	}
	
	public IValue call(Arguments arguments) {
		return _faceimpl.callMethod(_state, _name, arguments);
	}
		
	public IValue extractMember(Identifier name) {
		throw new Error_Unimplemented();
	}
	
	public void setProperty(Identifier propName, IValue aValue) {
		throw new Error_Unimplemented();
	}
}