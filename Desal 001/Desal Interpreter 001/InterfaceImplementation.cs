using System.Collections.Generic;

abstract class IfaceImplDelegates<T> {
	public delegate IValue PropGetter(T o);
	public delegate void PropSetter(T o, IValue val);
	public delegate void VoidFunction(T o, ref Scope args);
	public delegate IValue ValueFunction(T o, ref Scope args);
}

class InterfaceImplementation<T> : IfaceImplDelegates<T>, IInterfaceImplementation<T> {
	IDictionary<IFunctionInterface, VoidFunction> _voidCallees;
	IDictionary<IFunctionInterface, ValueFunction> _valueCallees;
	IDictionary<Identifier, PropGetter> _propGetters;
	IDictionary<Identifier, PropSetter> _propSetters;
	IDictionary<Identifier, IDictionary<IFunctionInterface, VoidFunction> > _voidMethods;
	IDictionary<Identifier, IDictionary<IFunctionInterface, ValueFunction> > _valueMethods;
	IInterface _interface;
	
	//have IfaceImpl and IfaceImplBuilder inherit from common base
	public InterfaceImplementation(
		IDictionary<IFunctionInterface, VoidFunction> voidCallees,
		IDictionary<IFunctionInterface, ValueFunction> valueCallees,
		IDictionary<Identifier, PropGetter> propGetters,
		IDictionary<Identifier, PropSetter> propSetters,
		IDictionary<Identifier, IDictionary<IFunctionInterface, VoidFunction> > voidMethods,
		IDictionary<Identifier, IDictionary<IFunctionInterface, ValueFunction> > valueMethods,
		IInterface face
	) {
		_voidCallees = voidCallees;
		_valueCallees = valueCallees;
		_propGetters = propGetters;
		_propSetters = propSetters;
		_voidMethods = voidMethods;
		_valueMethods = valueMethods;
		_interface = face;
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
		get { return _interface; }
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
		//xxx can also execute value methods
		KeyValuePair<IFunctionInterface, VoidFunction> pair =
			getFunction(_voidMethods[name], arguments);
		Scope scope = arguments.setup(pair.Key.parameters);
		pair.Value(obj, ref scope);
	}
	
	public IValue evaluateMethod(T obj, Identifier name, Arguments arguments) {
		KeyValuePair<IFunctionInterface, ValueFunction> pair =
			getFunction(_valueMethods[name], arguments);
		Scope scope = arguments.setup(pair.Key.parameters);
		return pair.Value(obj, ref scope);
	}
}