interface IInterfaceImplementation<T> {
	IInterface @interface { get; }
	IInterfaceImplementation<T> cast(IInterface @interface);
	bool sameClass(T state);
	void executeCall(T state, Arguments arguments);
	IValue evaluateCall(T state, Arguments arguments);
	IValue getProperty(T state, IValue @this, Identifier name);
	void setProperty(T state, IValue @this, Identifier name, IValue @value);
	void executeMethod(T state, Identifier name, Arguments arguments);
	IValue evaluateMethod(T state, Identifier name, Arguments arguments);
}