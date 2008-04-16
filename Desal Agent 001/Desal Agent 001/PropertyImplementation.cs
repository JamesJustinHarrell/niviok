class PropertyImplementation {
	public delegate IWorker ReadFunc();
	public delegate void WriteFunc(IWorker objRef);
	public ReadFunc read;
	public WriteFunc write;
};