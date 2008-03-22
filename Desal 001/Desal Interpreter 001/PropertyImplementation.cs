class PropertyImplementation {
	public delegate IValue ReadFunc();
	public delegate void WriteFunc(IValue objRef);
	public ReadFunc read;
	public WriteFunc write;
};