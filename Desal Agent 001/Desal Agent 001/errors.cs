//all thrown and caught exceptions should ApplicationException or one of these types

using System;
using System.Collections.Generic;

//errors caused by the user, but not in client code
//e.g. invalid program arguments
class UserError : Exception {
	public UserError()
		:base() {}
	public UserError(string message)
		:base(message) {}
	public UserError(string message, Exception innerException)
		:base(message, innerException) {}
}

class ParseError : Exception {
	public ParseError()
		:base() {}
	public ParseError(string message)
		:base(message) {}
	public ParseError(string message, Exception innerException)
		:base(message, innerException) {}
}

//exceptions in client code
class ClientException : Exception {
	public IWorker thrown; //the value of the throw node
	public Stack<string> stackTrace;
	
	public ClientException(string message)
	:base(message)
	{
		try {
			//this will fail if the client exception
			//arose from the Bridge static constructor
			this.thrown = Bridge.wrapString(message);
		}
		catch(Exception e) {
			Console.Error.WriteLine(message);
			throw new ApplicationException(
				"unable to create ClientException before Bridge has been setup");
		}
		stackTrace = new Stack<string>();
	}
	
	public ClientException(string message, Exception innerException)
	:base(message, innerException)
	{
		this.thrown = Bridge.wrapString(message);
		stackTrace = new Stack<string>();
	}
	
	public ClientException(IWorker thrown) {
		this.thrown = thrown;
		stackTrace = new Stack<string>();
	}
	
	public void pushFunc(string name) {
		stackTrace.Push(name);
	}
	
	public string clientMessage {
		get {
			string message = "";
			//xxx use String breeder
			if( thrown.face == Bridge.faceString ) {
				message += Bridge.unwrapString(thrown) + "\n\n";
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
