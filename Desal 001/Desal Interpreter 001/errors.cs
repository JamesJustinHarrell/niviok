/*
ClientStaticError - e.g. invalid syntax
ClientException - e.g. client code executes a throw statement
Error_* - errors in implementation
*/

using System;

class ClientException : ApplicationException {
	public IValue e;
	
	public ClientException(string message)
	:base(message)
	{
		this.e = Wrapper.wrapString(message);
	}
	
	public ClientException(IValue e) {
		this.e = e;
	}
}

class Error_ArgumentCount : ApplicationException {
	public Error_ArgumentCount(int number) : base(number.ToString()) {}
	public Error_ArgumentCount(string message) : base(message) {}
}

class Error_ArgumentInterface : ApplicationException {
}

class Error_Unimplemented : ApplicationException {
	public Error_Unimplemented() {}
	public Error_Unimplemented(string message) : base(message) {}
}