using System;
using System.Collections.Generic;

class Null : IWorker {
	IInterface _face;
	
	public Null() {}
	
	public Null(IInterface face) {
		_face = face;
	}
	
	//xxx throw or return null?
	public IObject owner {
		get { return null; }
	}
	
	//xxx throw, return null, or return empty list?
	public IList<IWorker> children {
		get { return null; }
	}
	
	public IInterface face {
		get { return _face; }
	}
	
	public IWorker breed(IInterface face) {
		throw new ClientException("requested null to breed");
	}
	
	public IWorker call(IList<Argument> arguments) {
		throw new ClientException("attempted to call a null value");
	}
	
	public IWorker extractMember(Identifier name) {
		throw new ClientException(
			String.Format(
				"attempted to extract member '{0}' from a null value",
				name));
	}
	
	public void setProperty(Identifier propName, IWorker worker) {
		throw new ClientException(
			String.Format(
				"attempted to set property '{0}' of a null value",
				propName));
	}
}