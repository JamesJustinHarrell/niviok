using System;
using System.Collections.Generic;

class FunctionInterface : IInterface {
	static IDictionary<Callee, WeakReference> _funcFaces;
	
	static FunctionInterface() {
		_funcFaces = new Dictionary<Callee, WeakReference>();
	}
	
	public static IInterface getFuncFace(Callee info) {			
		if( _funcFaces.ContainsKey(info) ) {
			WeakReference wr = _funcFaces[info];
			IInterface face = (IInterface)wr.Target;
			if( wr.IsAlive )
				return face;
			_funcFaces.Remove(info);
		}
		
		IInterface rv = new FunctionInterface(info);
		_funcFaces.Add(info, new WeakReference(rv));
		return rv;
	}
	
	Callee _info;

	FunctionInterface(Callee info) {
		_info = info;
	}
	
	public IList<IWorker> inheritees {
		get { return new IWorker[]{ Bridge.faceObject }; }
	}
	
	public IList<Callee> callees {
		get { return new Callee[]{ _info }; }
	}
	
	public IList<Breeder> breeders {
		get { return new Breeder[]{}; }
	}

	public IDictionary<Identifier, Property> properties {
		get { throw new NotImplementedException(); }
	}

	public IDictionary<Identifier, IList<Method>> methods {
		get { return new Dictionary<Identifier, IList<Method>>(); }
	}
}
