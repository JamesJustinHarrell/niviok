interface IInterfaceImplementation<T> {
	IInterface @interface { get; }
	IInterfaceImplementation<T> cast(IInterface @interface);
	bool sameClass(T state);
	IValue call(T state, Arguments arguments);
	IValue getProperty(T state, IValue @this, Identifier name);
	void setProperty(T state, IValue @this, Identifier name, IValue @value);
	IValue callMethod(T state, Identifier name, Arguments arguments);
}