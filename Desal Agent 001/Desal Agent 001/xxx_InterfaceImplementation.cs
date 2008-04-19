using System.Collections.Generic;

/*
class InterfaceImplementation<T> {
	Class _class;
	IInterface _interface;
	
	//xxx repeated - IFunction also defines parameters
	//xxx Parameter includes unnecesarry information, like the identifier
	IFunction _callee;
	IDictionary<Identifier, IFunction> _getters;
	IDictionary<Identifier, IFunction> _setters;
	IDictionary<Identifier, IFunction> _methods;
	
	public InterfaceImplementation(
	Class class_,
	IInterface interface_,
	IDictionary< IList<Parameter>, IFunction> callees,
	IDictionary<Identifier, IFunction> getters,
	IDictionary<Identifier, IFunction> setters,
	IDictionary<Identifier, IFunction> methods ) {
		_class = class_;
		_interface = interface_;
		_callees = callees;
		_getters = getters;
		_setters = setters;
		_methods = methods;
	}
	
	public Class class_ {
		get { return _class; }
	}
	
	public IInterface interface_ {
		get { return _interface; }
	}
	
	/* xxx remove
	public InterfaceImplementation adopt(Class class_) {
		return new InterfaceImplementation(
			class_, _interface, _callees, _getters, _setters, _methods );
	}
	* /
	
	public void executeCall(Object obj, IList<IWorker> args) {
		if( _callees.Count == 0 )
			throw new System.Exception("interface implementations has no callees");
		//xxx
		System.Console.WriteLine("WARNING: interimp.executeCall always uses first function");
		foreach( IFunction func in _callees.Values ) {
			func.executeCall(args);
			break;
		}
	}
	
	public IWorker evaluateCall(Object obj, IList<IWorker> args) {
		if( _callees.Count == 0 )
			throw new System.Exception("interface implementation has no callees");
		//xxx
		System.Console.WriteLine("WARNING: interimp.evaluateCall always uses first function");
		foreach( IFunction func in _callees.Values ) {
			return func.evaluateCall(args);
		}
		//xxx
		throw new System.Exception("shouldn't happen");
	}
	
	public IWorker getProperty(
	Object obj, Identifier name) {
		throw new NotImplementedException();
	}
	
	public void setProperty(
	Object obj, Identifier name, IWorker val) {
		throw new NotImplementedException();
	}
	
	public void executeMethod(
	Object obj, Identifier name, IList<IWorker> args) {
		throw new NotImplementedException();
	}
	
	public IWorker evaluateMethod(
	Object obj, Identifier name, IList<IWorker> args) {
		throw new NotImplementedException();
	}
}

//xxx poltergeist - exists only for conveenyence
class InterfaceImplementationInfo {
	IInterface _interface;
	public IDictionary< IList<Parameter>, IFunction> callees;
	public IDictionary<Identifier, IFunction> getters;
	public IDictionary<Identifier, IFunction> setters;
	public IDictionary<Identifier, IFunction> methods;
	
	public InterfaceImplementationInfo(IInterface aInterface) {
		_interface = aInterface;
		callees = new Dictionary< IList<Parameter>, IFunction>();
		getters = new Dictionary<Identifier, IFunction>();
		setters = new Dictionary<Identifier, IFunction>();
		methods = new Dictionary<Identifier, IFunction>();
	}
	
	public IInterface interface_ {
		get { return _interface; }
	}
}
*/