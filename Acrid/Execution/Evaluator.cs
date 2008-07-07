/*
Some nodes cannot be executed,
but contain children that can be executed.
For these, there are corresponding native types
that are similar to the nodes, but contain the values produced
from executing these nodes, instead of child expression nodes.
*/

using System;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public static class Evaluator {
	//argument
	public static Argument evaluate(Node_Argument node, IScope scope) {
		return new Argument(
			node.parameterName == null ? null : node.parameterName.value,
			Executor.executeAny(node.value, scope));
	}
	
	//breeder
	public static Breeder evaluate(Node_Breeder node, IScope scope) {
		return new Breeder(ReservedType.tryWrap(node.type, scope));
	}
	
	//callee
	public static Callee evaluate(Node_Callee node, IScope scope) {
		IList<ParameterInfo> parameters = new List<ParameterInfo>();
		foreach( Node_ParameterInfo child in node.parameterInfos )
			parameters.Add(evaluate(child, scope));
		return new Callee(
			parameters,
			ReservedType.tryWrap(node.returnType, scope));
	}
	
	//interface
	public static IInterface evaluate(Node_Interface node, IScope scope) {
		IList<IInterface> inheritees = new List<IInterface>();
		IList<Callee> callees = new List<Callee>();
		IList<Breeder> breeders = new List<Breeder>();
		IList<Property> props = new List<Property>();
		IList<Method> meths = new List<Method>();

		foreach( INode_Expression inherNode in node.inheritees )
			inheritees.Add(
				Bridge.toNativeInterface(
					Executor.executeAny(inherNode, scope)));

		foreach( Node_StatusedMember sm in node.members ) {
			//xxx member status (sm.memberStatus) is ignored
			INode_InterfaceMember member = sm.member;
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
	public static Method evaluate(Node_Method node, IScope scope) {
		return new Method(
			node.name.value,
			Bridge.toNativeInterface(Executor.executeAny(node.@interface, scope)) );
	}

	//parameter-impl
	public static ParameterImpl evaluate(Node_ParameterImpl node, IScope scope) {
		return new ParameterImpl(
			node.direction.value,
			evaluateType(node.type, scope),
			node.name.value,
		    ( node.defaultValue == null ?
		    	null :
		    	Executor.executeAny(node.defaultValue, scope) ));
	}

	//parameter-info
	public static ParameterInfo evaluate(Node_ParameterInfo node, IScope scope) {
		return new ParameterInfo(
			node.direction.value,
			ReservedType.tryWrap(node.type, scope),
			node.name.value,
		    node.hasDefaultValue.value);
	}
	
	//property
	public static Property evaluate(Node_Property node, IScope scope) {
		return new Property(
			node.name.value,
			node.writable.value,
			evaluateType(node.type, scope) );
	}
	
	//type
	public static IType evaluateType(INode_Expression node, IScope scope) {
		return new NType(Executor.executeAny(node, scope));
	}
	
	//worker
	public static IWorker evaluate(Node_Worker node, IScope scope, IObject owner) {
		IList<IWorker> children = new List<IWorker>();
		foreach( Node_Worker child in node.childWorkers )
			children.Add(evaluate(child, scope, owner));
	
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.toNativeInterface(Executor.executeAny(node.face, scope)),
			owner,
			children );
		foreach( Node_MemberImplementation nmi in node.memberImplementations ) {
			Node_MemberType type = nmi.memberType;
			if( type.value == MemberType.PROPERTY_GETTER )
				builder.addPropertyGetter(
					nmi.name.value, Executor.executeAny(nmi.function, scope));
			if( type.value == MemberType.PROPERTY_SETTER )
				builder.addPropertySetter(
					nmi.name.value,
					Executor.executeAny(nmi.function, scope));
		}
		return builder.compile();
	}
}

} //namespace
