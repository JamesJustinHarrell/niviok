/*
a Desal interface
implemented natively or by client code with an interface node
interfaces implemented manually by client code use InterfaceFromValue
remember this is not a node
*/

using System.Collections.Generic;

class Interface : IInterface {
	IList<IInterface> _inheritees;
	IList<PropertyInfo> _properties;
	IList<MethodInfo> _methods;
	IValue _value;
	
	public Interface(IList<PropertyInfo> properties, IList<MethodInfo> methods) {
		_inheritees = new List<IInterface>();
		_properties = properties;
		_methods = methods;
	}
	
	public Interface(
	IList<IInterface> inheritees, IList<PropertyInfo> properties, IList<MethodInfo> methods) {
		_inheritees = inheritees;
		_properties = properties;
		_methods = methods;
	}
	
	public IList<IInterface> inheritees {
		get { return _inheritees; }
	}
	
	public IList<PropertyInfo> properties {
		get { return _properties; }
	}
	
	public IList<MethodInfo> methods {
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
