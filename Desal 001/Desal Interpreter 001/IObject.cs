interface IObject<T> {
	long ID { get; }
	bool sameClass(IInterfaceImplementation<T> faceimpl);
	T state { get; }
}