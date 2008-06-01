//implements the "nativeObject" property of the IWorker interface

class Worker_NativeObject {
	object _nativeObject;
	
	public object nativeObject {
		get { return _nativeObject; }
		set { _nativeObject = value; }
	}
}
