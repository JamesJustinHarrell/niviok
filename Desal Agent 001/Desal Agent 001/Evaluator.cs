using System.Collections.Generic;

/*
Some nodes cannot be executed,
but contain children that can be executed.
For these, there are corresponding native types
that are similar to the nodes, but contain the values produced
from executing these nodes, instead of child expression nodes.
*/
static class Evaluator {
	//argument
	public static Argument evaluate(Node_Argument node, Scope scope) {
		return new Argument(
			node.parameterName == null ? null : node.parameterName.value,
			Executor.execute(node.value, scope));
	}
	
	//callee
	public static CalleeInfo evaluate(Node_Callee node, Scope scope) {
		IList<ParameterInfo> parameters = new List<ParameterInfo>();
		foreach( Node_ParameterInfo child in node.parameterInfos )
			parameters.Add(evaluate(child, scope));
		return new CalleeInfo(
			parameters,
			evaluate(node.returnInfo, scope));
	}
	
	//interface
	public static IInterface evaluate(Node_Interface node, Scope scope) {
		IList<IInterface> inheritees = new List<IInterface>();
		IList<CalleeInfo> callees = new List<CalleeInfo>();
		IList<PropertyInfo> props = new List<PropertyInfo>();
		IList<MethodInfo> meths = new List<MethodInfo>();
		foreach( Node_Interface inherNode in node.inheritees )
			inheritees.Add(evaluate(inherNode, scope));
		foreach( INode_InterfaceMember member in node.members ) {
			if( member is Node_Callee )
				callees.Add(evaluate(member as Node_Callee, scope));
			if( member is Node_Property )
				props.Add(evaluate(member as Node_Property, scope));
			if( member is Node_Method )
				meths.Add(evaluate(member as Node_Method, scope));
		}
		return new Interface(
			inheritees, callees, props, meths );
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
			Bridge.unwrapInterface(Executor.execute(node.@interface, scope)) );
		return new NullableType(iface, node.nullable.value);
	}

	//parameter-impl
	public static ParameterImpl evaluate(Node_ParameterImpl node, Scope scope) {
		return new ParameterImpl(
			node.direction.value,
			evaluate(node.nullableType, scope),
			node.name.value,
		    ( node.defaultValue == null ?
		    	null :
		    	Executor.execute(node.defaultValue, scope) ));
	}

	//parameter-info
	public static ParameterInfo evaluate(Node_ParameterInfo node, Scope scope) {
		return new ParameterInfo(
			node.direction.value,
			evaluate(node.nullableType, scope),
			node.name.value,
		    node.hasDefaultValue.value);
	}
	
	//property
	public static PropertyInfo evaluate(Node_Property node, Scope scope) {
		return new PropertyInfo(
			node.name.value,
			evaluate(node.nullableType, scope),
			Access.GET /* xxx hardcoded */ );
	}
	
	//worker
	public static IWorker evaluate(Node_Worker node, Scope scope, IObject owner) {
		IList<IWorker> children = new List<IWorker>();
		foreach( Node_Worker child in node.childs )
			children.Add(evaluate(child, scope, owner));
	
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.unwrapInterface(
				Executor.execute(node.face, scope)),
			owner, children );
		foreach( Node_MemberImplementation nmi in node.memberImplementations ) {
			Node_MemberIdentification id = nmi.memberIdentification;
			if( id.memberType.value == MemberType.PROPERTY_GETTER )
				builder.addPropertyGetter(
					id.name.value, Executor.execute(nmi.function, scope));
			if( id.memberType.value == MemberType.PROPERTY_SETTER )
				builder.addPropertySetter(
					id.name.value,
					Executor.execute(nmi.function, scope));
		}
		return builder.compile();
	}
}
