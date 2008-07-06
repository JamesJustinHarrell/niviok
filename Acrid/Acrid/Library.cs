using System;

class Library : IDerefable {
	IDerefable _derefable;

	public Library(IDerefable derefable) {
		_derefable = derefable;
		callIfFound("library_initialize");
	}

	~Library() {
		callIfFound("library_dispose");
	}
	
	void callIfFound(string name) {
		IWorker worker = null;
		try {
			worker = G.evalIdent(_derefable, new Identifier(name));
		}
		catch(Exception e){}
		if(worker != null)
			worker.call(new Argument[]{});
		
	/* xxx
		IWoScidentre ws = G.tryEvalIdent(_derefable, new Identifier(name));
		if(ws != null)
			ws.call(new Arguments[]{});
	*/
	}
	
	public DerefResults deref(IdentifierSequence idents) {
		string name = G.first(idents).ToString();
		if(name == "library_initialize" || name == "library_dispose" || name == "main")
			return new DerefResults(null, null);
		else
			return _derefable.deref(idents);
	}

	public HashSet<IWoScidentre> findEmptyWoScidentres(IdentifierSequence idents) {
		return new HashSet<IWoScidentre>();
	}
}
