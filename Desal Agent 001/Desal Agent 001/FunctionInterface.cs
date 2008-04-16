using System;
using System.Collections.Generic;

class FunctionInterface : IInterface {
	static IDictionary<CalleeInfo, WeakReference> _funcFaces;
	
	static FunctionInterface() {
		_funcFaces = new Dictionary<CalleeInfo, WeakReference>();
	}
	
	public static IInterface getFuncFace(CalleeInfo info) {			
		if( _funcFaces.ContainsKey(info) ) {
			WeakReference wr = _funcFaces[info];
			IInterface face = (IInterface)wr.Target;
			if( wr.IsAlive )
				return face;
			_funcFaces.Remove(info);
		}
		
		IInterface rv = new FunctionInterface(info);
		_funcFaces.Add(info, new System.WeakReference(rv));
		return rv;
	}
	
	CalleeInfo _info;

	FunctionInterface(CalleeInfo info) {
		_info = info;
	}
	
	public IList<IInterface> inheritees {
		get { return new IInterface[]{ Bridge.faceObject }; }
	}
	
	public IList<CalleeInfo> callees {
		get { return new CalleeInfo[]{ _info }; }
	}

	public IDictionary<Identifier, PropertyInfo> properties {
		get { throw new Error_Unimplemented(); }
	}

	public IDictionary<Identifier, IList<MethodInfo>> methods {
		get { throw new Error_Unimplemented(); }
	}
}
