using System.Collections.Generic;

/*
Some nodes cannot be executed,
but contain children that can be executed.
For these, there are corresponding native types
that are similar to the nodes, but contain the values produced
from executing these nodes, instead of child expression nodes.
*/
static class Evaluator {
	//interface
	public static IInterface evaluate(Node_Interface node, Scope scope) {
		IList<PropertyInfo> props = new List<PropertyInfo>();
		foreach( Node_Property prop in node.propertys ) {
			props.Add(Evaluator.evaluate(prop, scope));
		}
		IList<MethodInfo> meths = new List<MethodInfo>();
		foreach( Node_Method meth in node.methods ) {
			meths.Add(Evaluator.evaluate(meth, scope));
		}
		return new Interface( props, meths );
	}
	
	//method
	public static MethodInfo evaluate(Node_Method node, Scope scope) {
		return new MethodInfo(
			node.name.value, null );
			//xxx InterfaceFromValue.wrap(node.@interface.execute(scope)) );
	}
	
	//nullable-type
	public static NullableType evaluate(Node_NullableType node, Scope scope) {
		IInterface iface = ( node.@interface == null ?
			null :
			InterfaceFromValue.wrap(Executor.execute(node.@interface, scope)) );
		return new NullableType(iface, node.nullable.value);
	}

	//parameter
	public static Parameter evaluate(Node_Parameter node, Scope scope) {
		return new Parameter(
			evaluate(node.nullableType, scope),
			node.name.value,
		    node.hasDefaultValue.value,
		    ( node.defaultValue == null ?
		    	null :
		    	Executor.execute(node.defaultValue, scope) ));
	}
	
	//property
	public static PropertyInfo evaluate(Node_Property node, Scope scope) {
		return new PropertyInfo(
			node.name.value,
			evaluate(node.nullableType, scope),
			node.access.value );
	}
}
