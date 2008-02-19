using System.Collections.Generic;
using WeakRef = System.WeakReference;

class FunctionInterface : IFunctionInterface {
	// params -> { RefType -> FunctionInterface }
	static IDictionary<
		IList<Parameter>,
		IDictionary<NullableType, WeakRef> > _funcFaces;
	
	static FunctionInterface() {
		_funcFaces = new Dictionary<
			IList<Parameter>,
			IDictionary<NullableType, WeakRef> >();
	}
	
	public static IFunctionInterface getFuncFace(
	IList<Parameter> parameters, NullableType returnType) {
		if( returnType == null ) { //xxx void functions
			returnType = NullableType.dyn;
		}
	
		IDictionary<NullableType, WeakRef> dict;
	
		if( _funcFaces.ContainsKey(parameters) ) {
			dict = _funcFaces[parameters];
		}
		else {
			dict = new Dictionary<NullableType, WeakRef>();
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
	NullableType _returnType;

	FunctionInterface(IList<Parameter> parameters, NullableType returnType) {
		_params = parameters;
		_returnType = returnType;
	}
	
	public IList<Parameter> parameters {
		get { return _params; }
	}

	public NullableType returnType {
		get { return _returnType; }
	}
	
	public IList<IInterface> inheritees {
		get { return null; }
	}

	public IDictionary<Identifier, PropertyInfo> properties {
		get { throw new Error_Unimplemented(); }
	}

	public IDictionary<Identifier, IList<MethodInfo>> methods {
		get { throw new Error_Unimplemented(); }
	}
	
	public IValue value {
		get { throw new Error_Unimplemented(); }
	}
}