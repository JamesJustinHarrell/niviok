using System;
using System.Collections.Generic;
using Reflection = System.Reflection;

//executes expression nodes, as defined by the Desal Semantics specification
static class Executor {
	//and
	public static IValue execute(Node_And node, Scope scope) {
		IValue first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(false);
		
		IValue second = execute(node.second, scope);
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
		IValue val = execute(node.value, scope);
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
			rv = execute(expr, innerScope);
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
	
	//bundle
	public static IValue execute(Node_Bundle node, Scope scope) {
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts )
				scope.reserveDeclareFirst( decl.name.value );
		}
		
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts )
				Executor.execute(decl, scope);
		}
		
		IValue rv;
		try {
			IValue val = scope.evaluateIdentifier( new Identifier("main") );
			rv = val.call(
				new Arguments(
					new IValue[]{},
					new Dictionary<Identifier, IValue>() ));
		}
		catch(ClientException e) {
			e.pushFunc("main (called while executing Node_Bundle)");
			throw e;
		}

		if( rv is NullValue )
			return Bridge.wrapInteger(0);
		else
			return rv;
	}

	//call
	public static IValue execute(Node_Call node, Scope scope) {
		IList<IValue> evaledArgs = new List<IValue>();
		foreach( Node_Argument argument in node.arguments ) {
			evaledArgs.Add( execute(argument.value, scope) );
		}
	
		Arguments args = new Arguments(
			evaledArgs,
			new Dictionary<Identifier, IValue>() );
		IValue func = execute(node.value, scope);
	
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
		IValue val = execute(node.value, scope);
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
		IValue val = execute(node.value, scope);
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
		IValue source = execute(node.source, scope);
		Identifier memberName = node.memberName.value;
		return source.extractMember(memberName);
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
		IValue start = execute(node.start, scope);
		long current = Bridge.unwrapInteger(start);
		IValue limit = execute(node.limit, scope);
		while( current < Bridge.unwrapInteger(limit) ) {
			Scope innerScope = new Scope(scope);
			innerScope.declareAssign(
				node.name.value, Bridge.wrapInteger(current) );
			execute(node.action, innerScope);
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
		foreach( Node_Parameter paramNode in node.parameters ) {
			evaledParams.Add( Evaluator.evaluate(paramNode, scope) );
		}
		
		NullableType returnType = ( node.returnInfo == null ?
			null :
			Evaluator.evaluate(node.returnInfo, scope) );
		
		IFunction function = new Client_Function(
			evaledParams, returnType, node.body,
			scope.createClosure(Depends.depends(node)) );
		
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
	
	//identifier
	public static IValue execute(Node_Identifier node, Scope scope) {
		return scope.evaluateIdentifier(node.value);
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
	
	//interface
	public static IValue execute(Node_Interface node, Scope scope) {
		return Bridge.wrapInterface( Evaluator.evaluate(node, scope) );
	}
	
	//integer
	public static IValue execute(Node_Integer node, Scope scope) {
		return Bridge.wrapInteger(node.value);
	}
	
	//labeled
	public static IValue execute(Node_Labeled node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//loop
	public static IValue execute(Node_Loop node, Scope scope) {
		for(;;)
			execute(node.block, scope);
		return new NullValue();
	}
	
	//nand
	public static IValue execute(Node_Nand node, Scope scope) {
		IValue first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(true);
		
		IValue second = execute(node.second, scope);
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
		IValue first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(false);
		
		IValue second = execute(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}
	
	//or
	public static IValue execute(Node_Or node, Scope scope) {
		IValue first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(true);
		
		IValue second = execute(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	//possibility
	public static IValue execute(Node_Possibility node, Scope scope) {
		IValue testVal = execute(node.test, scope);

		if( testVal.activeInterface != Bridge.Bool )
			throw new ClientException("test must be a Bool");
		
		return ( Bridge.unwrapBoolean(testVal) ?
			execute(node.result, scope) :
			new NullValue() );
	}
	
	//rational
	public static IValue execute(Node_Rational node, Scope scope) {
		return Bridge.wrapRational(node.value);
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
		IValue val = execute(node.value, scope);
		execute(node.source, scope).setProperty(
			node.propertyName.value, val );
		return val;
	}
	
	//string
	public static IValue execute(Node_String node, Scope scope) {
		return Bridge.wrapString(node.value);
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
		IValue val = execute(node.identifier, scope);
		scope.assign(node.identifier.value, new NullValue());
		return val;
	}
	
	//while
	public static IValue execute(Node_While node, Scope scope) {
		while( Bridge.unwrapBoolean(execute(node.test, scope)) )
			execute(node.block, scope);
		return new NullValue();
	}
	
	//xnor
	public static IValue execute(Node_Xnor node, Scope scope) {
		IValue first = execute(node.first, scope);
		IValue second = execute(node.second, scope);

		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) == Bridge.unwrapBoolean(second) );
	}
	
	//xor
	public static IValue execute(Node_Xor node, Scope scope) {
		IValue first = execute(node.first, scope);
		IValue second = execute(node.second, scope);
		
		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) != Bridge.unwrapBoolean(second) );
	}
	
	//yield
	public static IValue execute(Node_Yield node, Scope scope) {
		throw new Error_Unimplemented();
	}
	
	//any expression node
	public static IValue execute(INode_Expression node, Scope scope) {
		Type classType = typeof(Executor);
		Type nodeType = ((Object)node).GetType();
		Reflection.MethodInfo meth = classType.GetMethod(
			"execute",
			new Type[]{ nodeType, typeof(Scope) });
		if( meth == null ||
		meth.GetParameters()[0].ParameterType == typeof(INode_Expression) )
			throw new Exception(
				String.Format("can't execute node of type {0}", node.typeName));
		try {
			return (IValue)meth.Invoke(null, new object[]{ node, scope });
		}
		catch(Reflection.TargetInvocationException e) {
			if( e.InnerException is ClientException )
				throw e.InnerException;
			throw e;
		}
	}
}