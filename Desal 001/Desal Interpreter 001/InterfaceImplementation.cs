using System.Collections.Generic;

abstract class FaceImplBase<T> {
	public delegate IValue PropGetter(T o);
	public delegate void PropSetter(T o, IValue val);
	public delegate void VoidFunction(T o, Scope args);
	public delegate IValue ValueFunction(T o, Scope args);

	protected IDictionary<IFunctionInterface, VoidFunction> _voidCallees;
	protected IDictionary<IFunctionInterface, ValueFunction> _valueCallees;
	protected IDictionary<Identifier, PropGetter> _propGetters;
	protected IDictionary<Identifier, PropSetter> _propSetters;
	protected IDictionary<Identifier, IDictionary<IFunctionInterface, VoidFunction> > _voidMethods;
	protected IDictionary<Identifier, IDictionary<IFunctionInterface, ValueFunction> > _valueMethods;
	
	public FaceImplBase() {}
	
	public FaceImplBase(FaceImplBase<T> data) {
		_voidCallees = data._voidCallees;
		_valueCallees = data._valueCallees;
		_propGetters = data._propGetters;
		_voidMethods = data._voidMethods;
		_valueMethods = data._valueMethods;
	}
}

class InterfaceImplementation<T> : FaceImplBase<T>, IInterfaceImplementation<T> {
	IInterface _face;
	
	public InterfaceImplementation( FaceImplBase<T> members, IInterface face )
	: base(members)
	{
		_face = face;
		
		//xxx check inheritees
		
		foreach( MethodInfo meth in _face.methods ) {
			if(
				(! _voidMethods.ContainsKey(meth.name) ||
				! _voidMethods[meth.name].ContainsKey(meth.iface) ) &&
				(! _valueMethods.ContainsKey(meth.name) ||
				! _valueMethods[meth.name].ContainsKey(meth.iface) )
			) {
				throw new System.Exception(
					"method with name '" + meth.name + "' not implemented");
			}
		}
		
		/* xxx enable
		//ensure FaceImpl doesn't implement anything not defined in Face
		foreach( Identifier ident in _voidMethods.Keys ) {
			foreach( IFunctionInterface faceImpl in _voidMethods[ident].Keys ) {
				bool found = false;
				foreach( MethodInfo meth in _face.methods ) {
					if( meth.name == ident && meth.iface == faceImpl ) {
						found = true;
						break;
					}
				}
				if( ! found )
					throw new System.Exception(
						"implemented method '" + ident.str + "' not defined");
			}
		}
		*/
		
		//xxx checking
	}

	//xxx does this belong here?
	bool isMatch(IFunctionInterface face, Arguments args) {
		//xxx vararg
		if( args.count != face.parameters.Count ) {
			return false;
		}
		//xxx types
		return true;
	}

	KeyValuePair<IFunctionInterface, TFunc>
	getFunction<TFunc>(IDictionary<IFunctionInterface, TFunc> funcs, Arguments args) {
		IList< KeyValuePair<IFunctionInterface, TFunc> > matches =
			new List< KeyValuePair<IFunctionInterface, TFunc> >();
		foreach( KeyValuePair<IFunctionInterface, TFunc> pair in funcs ) {
			if( isMatch(pair.Key, args) ) {
				matches.Add(pair);
			}
		}
		if( matches.Count == 0 )
			throw new System.Exception("no matches found");
		if( matches.Count > 1 )
			throw new System.Exception("multiple matches");
		return matches[0];
	}

	public IInterface @interface {
		get { return _face; }
	}
	
	public IInterfaceImplementation<T> cast(IInterface @interface) {
		throw new Error_Unimplemented();
	}
	
	public bool sameClass(T state) {
		throw new Error_Unimplemented();
	}
	
	public void executeCall(T state, Arguments arguments) {
		throw new Error_Unimplemented();
	}
	
	public IValue evaluateCall(T state, Arguments arguments) {
		throw new Error_Unimplemented();
	}
	
	public IValue getProperty(T state, IValue @this, Identifier name) {
		return _propGetters[name](state);
	}
	
	public void setProperty(T state, IValue @this, Identifier name, IValue @value) {
		throw new Error_Unimplemented();
	}

	public void executeMethod(T obj, Identifier name, Arguments arguments) {
		//xxx should alse be able to execute value methods
		KeyValuePair<IFunctionInterface, VoidFunction> pair =
			getFunction(_voidMethods[name], arguments);
		Scope scope = arguments.setup(pair.Key.parameters);
		pair.Value(obj, scope);
	}
	
	public IValue evaluateMethod(T obj, Identifier name, Arguments arguments) {
		KeyValuePair<IFunctionInterface, ValueFunction> pair =
			getFunction(_valueMethods[name], arguments);
		Scope scope = arguments.setup(pair.Key.parameters);
		return pair.Value(obj, scope);
	}
}