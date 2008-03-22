using System.Collections.Generic;

class Node_Interface : INode_Expression {
	IList<INode_Expression> _inheritees;
	IList<Node_Callee> _callees;
	Node_NullableType _returnType;
	IList<Node_Property> _properties;
	IList<Node_Method> _methods;

	public Node_Interface(
	IList<INode_Expression> inheritees, IList<Node_Callee> callees,
	Node_NullableType returnType, IList<Node_Property> properties,
	IList<Node_Method> methods )
	{
		_inheritees = inheritees;
		_callees = callees;
		_returnType = returnType;
		_properties = properties;
		_methods = methods;
	}
	
	public IInterface evaluateInterface(Scope scope) {
		IList<PropertyInfo> props = new List<PropertyInfo>();
		foreach( Node_Property prop in _properties ) {
			props.Add(Interpreter.evaluate(prop, scope));
		}
		IList<MethodInfo> meths = new List<MethodInfo>();
		foreach( Node_Method meth in _methods ) {
			meths.Add(Interpreter.evaluate(meth, scope));
		}
		return new Interface( props, meths );
	}

	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
		//xxx return InterfaceWrapper.wrap( evaluateInterface(scope) );
	}
	
	public string typeName {
		get { return "interface"; }
	}
	
	public ICollection<INode> childNodes {
		get { throw new Error_Unimplemented(); }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}