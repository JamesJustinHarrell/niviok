using System.Collections.Generic;
using WeakRef = System.WeakReference;

class FunctionInterface : IFunctionInterface {
	// params -> { RefType -> FunctionInterface }
	static IDictionary<
		IList<Parameter>,
		IDictionary<ReferenceType, WeakRef> > _funcFaces;
	
	static FunctionInterface() {
		_funcFaces = new Dictionary<
			IList<Parameter>,
			IDictionary<ReferenceType, WeakRef> >();
	}
	
	public static IFunctionInterface getFuncFace(
	IList<Parameter> parameters, ReferenceType returnType) {
		if( returnType == null ) { //xxx void functions
			returnType = new ReferenceType(ReferenceCategory.DYN, null);
		}
	
		IDictionary<ReferenceType, WeakRef> dict;
	
		if( _funcFaces.ContainsKey(parameters) ) {
			dict = _funcFaces[parameters];
		}
		else {
			dict = new Dictionary<ReferenceType, WeakRef>();
			_funcFaces.Add(	parameters, dict );
		}
		
		if( dict.ContainsKey(returnType) ) {
			if( dict[returnType].IsAlive )
				return (IFunctionInterface)dict[returnType].Target;
			dict.Remove(returnType);
		}
		
		IFunctionInterface rv = new FunctionInterface(parameters, returnType);
		dict.Add(returnType, new System.WeakReference(rv));
		return rv;
	}
	
	IList<Parameter> _params;
	ReferenceType _returnType;

	FunctionInterface(IList<Parameter> parameters, ReferenceType returnType) {
		_params = parameters;
		_returnType = returnType;
	}
	
	public IList<Parameter> parameters {
		get { return _params; }
	}

	public ReferenceType returnType {
		get { return _returnType; }
	}
	
	public IList<IInterface> inheritees {
		get { return null; }
	}

	public IList<PropertyInfo> properties {
		get { throw new Error_Unimplemented(); }
	}

	public IList<MethodInfo> methods {
		get { throw new Error_Unimplemented(); }
	}
	
	public IValue value {
		get { throw new Error_Unimplemented(); }
	}
}