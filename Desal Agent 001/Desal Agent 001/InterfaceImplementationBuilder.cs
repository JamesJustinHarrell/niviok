using System.Collections.Generic;

class InterfaceImplementationBuilder<T> : FaceImplBase<T> {

	public InterfaceImplementationBuilder() {
		_callees = new Dictionary<IFunctionInterface, Function>();
		_propGetters = new Dictionary<Identifier, PropGetter>();
		_propSetters = new Dictionary<Identifier, PropSetter>();
		_methods = new Dictionary<Identifier, IDictionary<IFunctionInterface, Function> >();
	}
	
	public void addPropertyGetter(Identifier ident, PropGetter getter) {
		_propGetters.Add(ident, getter);
	}

	public void addPropertySetter(Identifier ident, PropSetter setter) {
		_propSetters.Add(ident, setter);
	}
	
	public void addVoidMethod(Identifier ident, IFunctionInterface face, VoidFunction func) {
		addMethod(
			ident, face,
			delegate(T state, Scope args) {
				func(state, args);
				return new NullValue(null);
			});
	}
	
	public void addMethod(Identifier ident, IFunctionInterface face, Function func) {
		if( ! _methods.ContainsKey(ident) )
			_methods.Add( ident, new Dictionary<IFunctionInterface, Function>() );
		_methods[ident].Add(face, func);
	}
	
	public IInterfaceImplementation<T> compile(IInterface face) {
		return new InterfaceImplementation<T>( this, face );
	}
}