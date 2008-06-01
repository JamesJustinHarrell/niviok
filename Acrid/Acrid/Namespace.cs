using System;
using System.Diagnostics;

class Namespace {
	Scope _scope;

	public Namespace(Scope scope) {
		Debug.Assert(scope.parent == null);
		_scope = scope;
	}

	public Namespace evalNamespaceIdent(Identifier ident) {
		Console.Error.WriteLine("returning null in evaluateNamespaceIdentifier");
		return null;
	}
	
	public IWorker evalWorkerIdent(Identifier ident) {
		if( _scope.isReserved(ident) )
			throw new ApplicationException(
				"declare-first identikey hasn't been assigned to yet");
		return _scope.evaluateLocalIdentifier(ident);
	}
	
	public Scope scope {
		get { return _scope; }
	}
}
