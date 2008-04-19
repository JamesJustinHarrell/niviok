//a Desal object
//named DesalObject instead of Object to avoid naming conflicts

using System;

class DesalObject : IObject {
	//workers reference their owner object, creating a cycle
	//so this will be null when a DesalObject is created
	IWorker _rootWorker;
	
	//the native value which this object is wrapping
	//for client-implemented objects, it's null
	object _native; 
	
	public DesalObject(){}
	
	public DesalObject(object native) {
		_native = native;
	}
	
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
	
	public object native {
		get { return _native; }
	}
}
