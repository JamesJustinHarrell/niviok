using System;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class LibraryFailure : Exception {
	public LibraryFailure(string message)
		:base(message) {}

	public LibraryFailure(string message, Exception inner)
		:base(message,inner) {}
}

class UnknownScidentre : Exception {}

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
			Console.WriteLine("ClientException exception: " + e);
			Console.Error.WriteLine("ClientException message: " + message);
			throw new ApplicationException(
				"Unable to create ClientException because " +
				"Bridge hasn't been setup. Message: " + message);
		}
		stackTrace = new Stack<string>();
	}
	
	public ClientException(string message, string location)
	:base(message)
	{
		try {
			//this will fail if the client exception
			//arose from the Bridge static constructor
			this.thrown = Bridge.toClientString(message);
		}
		catch(Exception e) {
			Console.WriteLine("ClientException exception: " + e);
			Console.Error.WriteLine("ClientException message: " + message);
			throw new ApplicationException(
				"Unable to create ClientException because " +
				"Bridge hasn't been setup. Message: " + message);
		}
		stackTrace = new Stack<string>();
		stackTrace.Push(location);
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
			if( G.canBreed(thrown.face, Bridge.stdn_String) ) {
				message += Bridge.toNativeString(
					thrown.breed(Bridge.stdn_String)) + "\n\n";
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

class UnassignedDeclareFirst : Exception {
	Identifier _ident;
	public UnassignedDeclareFirst(Identifier ident) {
		_ident = ident;
	}
	public Identifier ident {
		get { return _ident; }
	}
}

class NoCorrespondingNamespaceScidentre : Exception {
	Identifier _ident;
	public NoCorrespondingNamespaceScidentre(Identifier ident) {
		_ident = ident;
	}
	public Identifier ident {
		get { return _ident; }
	}
}

} //namespace
