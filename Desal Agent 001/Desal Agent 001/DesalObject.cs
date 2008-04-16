//named DesalObject instead of Object to avoid conflicts with C#

using System;

class DesalObject : IObject {
	IWorker _rootWorker;
	object _native;
	
	public DesalObject(){}
	
	public DesalObject(object native) {
		_native = native;
	}
	
	public IWorker rootWorker {
		get {
			if( _rootWorker == null )
				throw new Exception("object was never setup");
			return _rootWorker;
		}
		set {
			if( _rootWorker != null )
				throw new Exception("object was already setup");
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
