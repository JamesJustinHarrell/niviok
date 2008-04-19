using System;
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
	
	//breeder
	public static Breeder evaluate(Node_Breeder node, Scope scope) {
		return new Breeder(
			Executor.execute(node.@interface, scope));
	}
	
	//callee
	public static Callee evaluate(Node_Callee node, Scope scope) {
		IList<ParameterInfo> parameters = new List<ParameterInfo>();
		foreach( Node_ParameterInfo child in node.parameterInfos )
			parameters.Add(evaluate(child, scope));
		return new Callee(
			parameters,
			evaluate(node.returnInfo, scope));
	}
	
	//interface
	public static IInterface evaluate(Node_Interface node, Scope scope) {
		IList<IWorker> inheritees = new List<IWorker>();
		IList<Callee> callees = new List<Callee>();
		IList<Breeder> breeders = new List<Breeder>();
		IList<Property> props = new List<Property>();
		IList<Method> meths = new List<Method>();

		foreach( INode_Expression inherNode in node.inheritees )
			inheritees.Add(Executor.execute(inherNode, scope));

		foreach( INode_InterfaceMember member in node.members ) {
			if( member is Node_Callee )
				callees.Add(evaluate(member as Node_Callee, scope));
			if( member is Node_Breeder )
				breeders.Add(evaluate(member as Node_Breeder, scope));
			if( member is Node_Property )
				props.Add(evaluate(member as Node_Property, scope));
			if( member is Node_Method )
				meths.Add(evaluate(member as Node_Method, scope));
		}

		return new Interface(
			inheritees, callees, breeders, props, meths );
	}
	
	//method
	public static Method evaluate(Node_Method node, Scope scope) {
		return new Method(
			node.name.value,
			Bridge.unwrapInterface(Executor.execute(node.@interface, scope)) );
	}
	
	//nullable-type
	public static NullableType evaluate(Node_NullableType node, Scope scope) {
		IWorker face = ( node.@interface == null ?
			null :
			Executor.execute(node.@interface, scope) );
		return new NullableType(face, node.nullable.value);
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
	public static Property evaluate(Node_Property node, Scope scope) {
		return new Property(
			node.name.value,
			node.writable.value,
			evaluate(node.nullableType, scope) );
	}
	
	//worker
	public static IWorker evaluate(Node_Worker node, Scope scope, IObject owner) {
		IList<IWorker> children = new List<IWorker>();
		foreach( Node_Worker child in node.childs )
			children.Add(evaluate(child, scope, owner));
	
		WorkerBuilder builder = new WorkerBuilder(
			Executor.execute(node.face, scope),
			owner,
			children );
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
