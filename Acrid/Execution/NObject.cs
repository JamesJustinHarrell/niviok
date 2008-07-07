//a Niviok object
//named "NObject" instead of "Object" to avoid naming conflicts

using System;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class NObject : IObject {
	//workers reference their owner object, creating a cycle
	//so this will be null when a NiviokObject is created
	IWorker _rootWorker;
	
	public NObject(){}
	
	public IWorker rootWorker {
		get {
			if( _rootWorker == null )
				throw new ApplicationException("object was never setup");
			return _rootWorker;
		}
		set {
			if( _rootWorker != null )
				throw new ApplicationException("object was already setup");
			_rootWorker = value;
		}
	}
	
	public bool sameObject(IObject obj) {
		return this == obj;
	}
}

} //namespace
