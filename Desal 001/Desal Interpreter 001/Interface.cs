/*
a Desal interface
implemented natively or by client code with an interface node
interfaces implemented manually by client code use InterfaceFromValue
remember this is not a node
*/

using System.Collections.Generic;

class Interface : IInterface {
	IList<IInterface> _inheritees;
	IDictionary<Identifier, PropertyInfo> _properties;
	IDictionary<Identifier, IList<MethodInfo> > _methods;
	IValue _value;
	
	public Interface(IList<PropertyInfo> properties, IList<MethodInfo> methods)
		: this( new List<IInterface>(), properties, methods ) {}
	
	public Interface(
	IList<IInterface> inheritees, IList<PropertyInfo> properties, IList<MethodInfo> methods) {
		_inheritees = inheritees;
		
		_properties = new Dictionary<Identifier, PropertyInfo>();
		foreach( PropertyInfo info in properties )
			_properties.Add( info.name, info );
		
		_methods = new Dictionary<Identifier, IList<MethodInfo>>();
		foreach( MethodInfo info in methods ) {
			if( ! _methods.ContainsKey(info.name) )
				_methods.Add( info.name, new List<MethodInfo>() );
			_methods[info.name].Add(info);
		}
	}
	
	public IList<IInterface> inheritees {
		get { return _inheritees; }
	}
	
	public IDictionary<Identifier, PropertyInfo> properties {
		get { return _properties; }
	}
	
	public IDictionary<Identifier, IList<MethodInfo> > methods {
		get { return _methods; }
	}
	
	public IValue value {
		get {
			if( _value == null )
				_value = Bridge.wrapInterface(this);
			return _value;
		}
	}
}
