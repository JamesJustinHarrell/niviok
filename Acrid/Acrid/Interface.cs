/*
a Niviok interface
implemented natively, or by client code with an interface node
interfaces implemented manually by client code use InterfaceFromValue
*/

using System.Collections.Generic;

class Interface : IInterface {
	IList<IInterface> _inheritees;
	IList<Callee> _callees;
	IList<Breeder> _breeders;
	IDictionary<Identifier, Property> _properties;
	IDictionary<Identifier, IList<Method>> _methods;
	IWorker _worker;

	public Interface(
	IList<IInterface> inheritees,
	IList<Callee> callees,
	IList<Breeder> breeders,
	IList<Property> properties,
	IList<Method> methods ) {
		_inheritees = inheritees;
		_callees = callees;
		_breeders = breeders;
		
		if( _inheritees.Count == 0 )
			_inheritees.Add(Bridge.std_Object);
		
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
	
	public IList<IInterface> inheritees {
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
	
	public IWorker worker {
		get {
			if( _worker == null )
				_worker = Client_Interface.wrap(this);
			return _worker;
		}
	}
}
