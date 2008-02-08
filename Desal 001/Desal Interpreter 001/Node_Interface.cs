using System.Collections.Generic;

class Node_Interface : INode_Expression {
	IList<INode_Expression> _inheritees;
	IList<Node_Callee> _callees;
	Node_ReferenceType _returnType;
	IList<Node_Property> _properties;
	IList<Node_Method> _methods;

	public Node_Interface(
	IList<INode_Expression> inheritees, IList<Node_Callee> callees,
	Node_ReferenceType returnType, IList<Node_Property> properties,
	IList<Node_Method> methods )
	{
		_inheritees = inheritees;
		_callees = callees;
		_returnType = returnType;
		_properties = properties;
		_methods = methods;
	}

	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
		//xxx return InterfaceWrapper.wrap( evaluateInterface(scope) );
	}
	
	public IInterface evaluateInterface(Scope scope) {
		IList<PropertyInfo> props = new List<PropertyInfo>();
		foreach( Node_Property prop in _properties ) {
			props.Add( prop.evaluatePropertyInfo(scope) );
		}
		IList<MethodInfo> meths = new List<MethodInfo>();
		foreach( Node_Method meth in _methods ) {
			meths.Add( meth.evaluateMethodInfo(scope) );
		}
		return new Interface( props, meths );
	}
	
	public void getInfo(out string name, out object children) {
		name = "interface";
		children = new object[]{ null };
	}
}