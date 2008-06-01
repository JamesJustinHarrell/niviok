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
	public string source;
	public ParseError(string message, string aSource)
		: base(message) { source = aSource; }
	public ParseError(string message, string aSource, Exception innerException)
		: base(message, innerException) { source = aSource; }
}

//break node
class ClientBreak : Exception {}

//continue node
class ClientContinue : Exception {}

//return node
class ClientReturn : Exception {
	IWorker _worker;

	public ClientReturn() {}
	
	public ClientReturn(IWorker worker) {
		_worker = worker;
	}
	
	public IWorker worker {
		get { return _worker; }
	}
}

//exceptions in client code
//may be thrown by throw nodes, but not necesarrily
class ClientException : Exception {
	public IWorker thrown; //the value of the throw node
	public Stack<string> stackTrace;
	
	public ClientException(string message)
	:base(message)
	{
		try {
			//this will fail if the client exception
			//arose from the Bridge static constructor
			this.thrown = Bridge.toClientString(message);
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
		this.thrown = Bridge.toClientString(message);
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
			
			/* xxx enable when Breeders are implemented
			//if can breed string
			if( G.canBreed(thrown.face, Bridge.std_String) ) {
				message += Bridge.toNativeString(
					thrown.breed(Bridge.std_String)) + "\n\n";
			}			
			*/
			try{
				message += Bridge.toNativeString(thrown) + "\n\n";
			}catch(Exception e){}
			
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

//xxx how the hell am I supposed to implement this?
class ClientYield : Exception {}

