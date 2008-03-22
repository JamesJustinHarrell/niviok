using System.Collections.Generic;

/*
executes and "evaluates" nodes
execution is defined by the Desal Semantics specification
evaluation is for nodes that cannot be executed,
	but contain children that can be executed
*/
static class Interpreter {
	//parameter
	public static Parameter evaluate(Node_Parameter node, Scope scope) {
		return new Parameter(
			evaluate(node.nullableType, scope),
			node.name.value,
		    node.hasDefaultValue.value,
		    ( node.defaultValue == null ?
		    	null :
		    	node.defaultValue.execute(scope) ));
	}
	
	//nullable-type
	public static NullableType evaluate(Node_NullableType node, Scope scope) {
		IInterface iface = ( node.@interface == null ?
			null :
			InterfaceFromValue.wrap(node.@interface.execute(scope)) );
		return new NullableType(iface, node.nullable.value);
	}
	
	//property
	public static PropertyInfo evaluate(Node_Property node, Scope scope) {
		return new PropertyInfo(
			node.name.value,
			evaluate(node.nullableType, scope),
			node.access.value );
	}
	
	//method
	public static MethodInfo evaluate(Node_Method node, Scope scope) {
		return new MethodInfo(
			node.name.value, null );
			//xxx InterfaceFromValue.wrap(node.@interface.execute(scope)) );
	}

	//and
	public static IValue execute(Node_And node, Scope scope) {
		IValue first = node.first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(false);
		
		IValue second = node.second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}

	//array
	public static IValue execute(Node_Array node, Scope scope) {
		throw new Error_Unimplemented();
	}

	//assign
	public static IValue execute(Node_Assign node, Scope scope) {
		IValue val = node.value.execute(scope);
		scope.assign( node.name.value, val );
		return val;
	}

	//block
	public static IValue execute(Node_Block node, Scope scope) {
		Scope innerScope = new Scope(scope);
		
		//reserve identikeys, so the closures will include
		//the identikeys from other declare-first nodes
		foreach( INode_Expression member in node.members )
			if( member is Node_DeclareFirst )
				scope.reserveDeclareFirst( (member as Node_DeclareFirst).name.value );

		IValue rv = new NullValue();
		foreach( INode_Expression expr in node.members ) {
			rv = expr.execute(innerScope);
		}
		return rv;
	}

	//break
	public static IValue execute(Node_Break node, Scope scope) {
		throw new Error_Unimplemented();
	}

	//breed
	public static IValue execute(Node_Breed node, Scope scope) {
		throw new Error_Unimplemented();
	}

	//call
	public static IValue execute(Node_Call node, Scope scope) {
		IList<IValue> evaledArgs = new List<IValue>();
		foreach( Node_Argument argument in node.argument ) {
			evaledArgs.Add( argument.value.execute(scope) );
		}
	
		Arguments args = new Arguments(
			evaledArgs,
			new Dictionary<Identifier, IValue>() );
		IValue func = node.value.execute(scope);
	
		try {
			return func.call(args);
		}
		catch(ClientException e) {
			e.pushFunc(
				( node.value is Node_Identifier ?
					node.value.ToString() :
					"(anonymous)" ));
			throw e;
		}
	}
	
	//caller
	public static IValue execute(Node_Caller node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//cast
	public static IValue execute(Node_Cast node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//chain
	public static IValue execute(Node_Chain node, Scope scope) {
		throw new Error_Unimplemented();
	}

	//conditional
	public static IValue execute(Node_Conditional node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//curry
	public static IValue execute(Node_Curry node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//declare-assign
	public static IValue execute(Node_DeclareAssign node, Scope scope) {
		IValue val = node.value.execute(scope);
		scope.declareAssign(node.name.value, val);
		return val;
	}

	//declare-const-empty
	public static IValue execute(Node_DeclareConstEmpty node, Scope scope) {
		throw new Error_Unimplemented();
	}

	//declare-empty
	public static IValue execute(Node_DeclareEmpty node, Scope scope) {
		scope.declareEmpty(node.name.value);
		return new NullValue();
	}

	//declare-first
	public static IValue execute(Node_DeclareFirst node, Scope scope) {
		IValue val = node.value.execute(scope);
		scope.declareFirst(node.name.value, val);
		return val;
	}
	
	//dictionary
	public static IValue execute(Node_Dictionary node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//do-while
	public static IValue execute(Node_DoWhile node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//do-times
	public static IValue execute(Node_DoTimes node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//enum
	public static IValue execute(Node_Enum node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//extract-member
	public static IValue execute(Node_ExtractMember node, Scope scope) {
		IValue source = node.source.execute(scope);
		Identifier memberName = node.memberName.value;
		return source.extractNamedMember(memberName);
	}
	
	//for-key
	public static IValue execute(Node_ForKey node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//for-manual
	public static IValue execute(Node_ForManual node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//for-pair
	public static IValue execute(Node_ForPair node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//for-range
	public static IValue execute(Node_ForRange node, Scope scope) {
		IValue start = node.start.execute(scope);
		long current = Bridge.unwrapInteger(start);
		IValue limit = node.limit.execute(scope);
		while( current < Bridge.unwrapInteger(limit) ) {
			Scope innerScope = new Scope(scope);
			innerScope.declareAssign(
				node.name.value, Bridge.wrapInteger(current) );
			node.action.execute(innerScope);
			current++;
		}
		return new NullValue();
	}
	
	//for-value
	public static IValue execute(Node_ForValue node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//function
	public static IValue execute(Node_Function node, Scope scope) {
		//evaluate parameters
		IList<Parameter> evaledParams = new List<Parameter>();
		foreach( Node_Parameter paramNode in node.parameter ) {
			evaledParams.Add( evaluate(paramNode, scope) );
		}
		
		NullableType returnType = ( node.returnInfo == null ?
			null :
			evaluate(node.returnInfo, scope) );
		
		IFunction function = new Client_Function(
			evaledParams, returnType, node.body,
			scope.createClosure(node.identikeyDependencies) );
		
		return FunctionWrapper.wrap(function);
	}
	
	//function-interface
	public static IValue execute(Node_FunctionInterface node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//generator
	public static IValue execute(Node_Generator node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//generic-function
	public static IValue execute(Node_GenericFunction node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//ignore
	public static IValue execute(Node_Ignore node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//implements
	public static IValue execute(Node_Implements node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//instantiate-generic
	public static IValue execute(Node_InstantiateGeneric node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//labeled
	public static IValue execute(Node_Labeled node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//loop
	public static IValue execute(Node_Loop node, Scope scope) {
		for(;;)
			node.block.execute(scope);
		return new NullValue();
	}
	
	//nand
	public static IValue execute(Node_Nand node, Scope scope) {
		IValue first = node.first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(true);
		
		IValue second = node.second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	//namespaced-value-identikey
	public static IValue execute(Node_NamespacedValueIdentikey node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//nor
	public static IValue execute(Node_Nor node, Scope scope) {
		IValue first = node.first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(false);
		
		IValue second = node.second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}
	
	//or
	public static IValue execute(Node_Or node, Scope scope) {
		IValue first = node.first.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(true);
		
		IValue second = node.second.execute(scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	//possibility
	public static IValue execute(Node_Possibility node, Scope scope) {
		IValue testVal = node.test.execute(scope);

		if( testVal.activeInterface != Bridge.Bool )
			throw new ClientException("test must be a Bool");
		
		return ( Bridge.unwrapBoolean(testVal) ?
			node.result.execute(scope) :
			new NullValue() );
	}
	
	//return
	public static IValue execute(Node_Return node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//select
	public static IValue execute(Node_Select node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//set-property
	public static IValue execute(Node_SetProperty node, Scope scope) {
		IValue val = node.value.execute(scope);
		node.source.execute(scope).setProperty(
			node.propertyName.value, val );
		return val;
	}
	
	//throw
	public static IValue execute(Node_Throw node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//try-catch
	public static IValue execute(Node_TryCatch node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//unassign
	public static IValue execute(Node_Unassign node, Scope scope) {
		IValue val = node.identifier.execute(scope);
		scope.assign(node.identifier.value, new NullValue());
		return val;
	}
	
	//while
	public static IValue execute(Node_While node, Scope scope) {
		while( Bridge.unwrapBoolean(node.test.execute(scope)) )
			node.block.execute(scope);
		return new NullValue();
	}
	
	//xnor
	public static IValue execute(Node_Xnor node, Scope scope) {
		IValue first = node.first.execute(scope);
		IValue second = node.second.execute(scope);

		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) == Bridge.unwrapBoolean(second) );
	}
	
	//xor
	public static IValue execute(Node_Xor node, Scope scope) {
		IValue first = node.first.execute(scope);
		IValue second = node.second.execute(scope);
		
		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) != Bridge.unwrapBoolean(second) );
	}
	
	//yield
	public static IValue execute(Node_Yield node, Scope scope) {
		throw new Error_Unimplemented();
	}
}