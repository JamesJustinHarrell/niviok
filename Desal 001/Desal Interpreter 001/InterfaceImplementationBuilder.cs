using System.Collections.Generic;

class InterfaceImplementationBuilder<T> : IfaceImplDelegates<T> {
	
	IDictionary<IFunctionInterface, VoidFunction> _voidCallees;
	IDictionary<IFunctionInterface, ValueFunction> _valueCallees;
	IDictionary<Identifier, PropGetter> _propGetters;
	IDictionary<Identifier, PropSetter> _propSetters;
	IDictionary<Identifier, IDictionary<IFunctionInterface, VoidFunction> > _voidMethods;
	IDictionary<Identifier, IDictionary<IFunctionInterface, ValueFunction> > _valueMethods;

	public InterfaceImplementationBuilder() {
		_voidCallees = new Dictionary<IFunctionInterface, VoidFunction>();
		_valueCallees = new Dictionary<IFunctionInterface, ValueFunction>();
		_propGetters = new Dictionary<Identifier, PropGetter>();
		_propSetters = new Dictionary<Identifier, PropSetter>();
		_voidMethods = new Dictionary<Identifier, IDictionary<IFunctionInterface, VoidFunction> >();
		_valueMethods = new Dictionary<Identifier, IDictionary<IFunctionInterface, ValueFunction> >();
	}
	
	public void addPropertyGetter(Identifier ident, PropGetter getter) {
		_propGetters.Add(ident, getter);
	}

	public void addPropertySetter(Identifier ident, PropSetter setter) {
		_propSetters.Add(ident, setter);
	}
	
	public void addVoidMethod(Identifier ident, IFunctionInterface face, VoidFunction func) {
		if( ! _voidMethods.ContainsKey(ident) )
			_voidMethods.Add( ident, new Dictionary<IFunctionInterface, VoidFunction>() );
		_voidMethods[ident].Add(face, func);
	}
	
	public void addValueMethod(Identifier ident, IFunctionInterface face, ValueFunction func) {
		if( ! _valueMethods.ContainsKey(ident) )
			_valueMethods.Add( ident, new Dictionary<IFunctionInterface, ValueFunction>() );
		_valueMethods[ident].Add(face, func);
	}
	
	//xxx should have parameters and do checking
	public IInterfaceImplementation<T> compile(IInterface face) {
		return new InterfaceImplementation<T>(
			_voidCallees, _valueCallees,
			_propGetters, _propSetters,
			_voidMethods, _valueMethods,
			face );
	}
}