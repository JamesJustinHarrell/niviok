using System;
using System.Collections.Generic;

//throw instead of return null to find errors faster

class Null : Worker_NativeObject, IWorker {	
	public Null() {}
	
	public IObject owner {
		get { throw new ClientException("null has no owner"); }
	}
	
	public IList<IWorker> childWorkers {
		get { throw new ClientException("null has no children"); }
	}
	
	public IInterface face {
		get { throw new ClientException("null has no face"); }
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