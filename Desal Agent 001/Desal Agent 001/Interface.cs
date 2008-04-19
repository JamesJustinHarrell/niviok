/*
a Desal interface
implemented natively, or by client code with an interface node
interfaces implemented manually by client code use InterfaceFromValue
*/

using System.Collections.Generic;

class Interface : IInterface {
	IList<IWorker> _inheritees;
	IList<Callee> _callees;
	IList<Breeder> _breeders;
	IDictionary<Identifier, Property> _properties;
	IDictionary<Identifier, IList<Method>> _methods;
	IWorker _value;

	public Interface(
	IList<IWorker> inheritees,
	IList<Callee> callees,
	IList<Breeder> breeders,
	IList<Property> properties,
	IList<Method> methods ) {
		_inheritees = inheritees;
		_callees = callees;
		_breeders = breeders;
		
		_properties = new Dictionary<Identifier, Property>();
		foreach( Property info in properties )
			_properties.Add( info.name, info );
		
		_methods = new Dictionary<Identifier, IList<Method>>();
		foreach( Method info in methods ) {
			if( ! _methods.ContainsKey(info.name) )
				_methods.Add( info.name, new List<Method>() );
			_methods[info.name].Add(info);
		}
	}
	
	public IList<IWorker> inheritees {
		get { return _inheritees; }
	}
	
	public IList<Callee> callees {
		get { return _callees; }
	}
	
	public IList<Breeder> breeders {
		get { return _breeders; }
	}
	
	public IDictionary<Identifier, Property> properties {
		get { return _properties; }
	}
	
	public IDictionary<Identifier, IList<Method>> methods {
		get { return _methods; }
	}
	
	//note: This is different from just using Bridge.wrapInterface()
	//because the wrapper values of this interface need to all
	//be associated with the same object
	//xxx this isn't used because it isn't present on the IInterface interface
	public IWorker value {
		get {
			if( _value == null )
				_value = Bridge.wrapInterface(this);
			return _value;
		}
	}
}
