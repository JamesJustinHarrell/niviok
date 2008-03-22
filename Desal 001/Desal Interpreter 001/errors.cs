/*
ClientStaticError - e.g. invalid syntax
ClientException - e.g. client code executes a throw statement
Error_* - errors in implementation
*/

using System;
using System.Collections.Generic;

class ClientException : ApplicationException {
	public IValue e;
	public Stack<string> stackTrace;
	
	public ClientException(string message)
	:base(message)
	{
		this.e = Bridge.wrapString(message);
		stackTrace = new Stack<string>();
	}
	
	public ClientException(IValue e) {
		this.e = e;
		stackTrace = new Stack<string>();
	}
	
	public void pushFunc(string name) {
		stackTrace.Push(name);
	}
	
	public string clientMessage {
		get {
			string message = "";
			if( e.activeInterface == Bridge.String ) {
				message += Bridge.unwrapString(e) + "\n\n";
			}
			message += "Stack trace:\n";
			foreach( string s in stackTrace ) {
				message += s + "\n";
			}
			message += "\nImplementation stack trace:\n";
			message += this.ToString();
			return message;
		}
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