using System;
using System.Collections.Generic;
using Reflection = System.Reflection;

//executes expression nodes, as defined by the Desal Semantics specification
static partial class Executor {
	//and
	public static IWorker execute(Node_And node, Scope scope) {
		IWorker first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(false);
		
		IWorker second = execute(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}

	//array
	public static IWorker execute(Node_Array node, Scope scope) {
		throw new NotImplementedException();
	}

	//assign
	public static IWorker execute(Node_Assign node, Scope scope) {
		IWorker val = execute(node.value, scope);
		scope.assign( node.name.value, val );
		return val;
	}

	//block
	public static IWorker execute(Node_Block node, Scope scope) {
		Scope innerScope = new Scope(scope);
		
		//reserve identikeys
		foreach( INode_Expression member in node.members )
			if( member is Node_DeclareFirst ) {
				Node_DeclareFirst df = (Node_DeclareFirst)member;
				scope.reserveDeclareFirst(
					df.name.value,
					df.identikeyType.identikeyCategory.value,
					null);
			}

		//set nullable-type of identikeys
		foreach( INode_Expression member in node.members )
			if( member is Node_DeclareFirst ) {
				Node_DeclareFirst df = (Node_DeclareFirst)member;
				scope.setType(
					df.name.value,
					Evaluator.evaluate(df.identikeyType.nullableType, scope));
			}

		IWorker rv = new Null();
		foreach( INode_Expression expr in node.members ) {
			rv = execute(expr, innerScope);
		}

		return rv;
	}

	//break
	public static IWorker execute(Node_Break node, Scope scope) {
		throw new NotImplementedException();
	}

	//breed
	public static IWorker execute(Node_Breed node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//bundle
	public static IWorker execute(Node_Bundle node, Scope scope) {
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts ) {
				scope.reserveDeclareFirst(
					decl.name.value,
					decl.identikeyType.identikeyCategory.value,
					Evaluator.evaluate(decl.identikeyType.nullableType, scope));
			}
		}
		
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts )
				Executor.execute(decl, scope);
		}
		
		IWorker rv;
		try {
			IWorker val = scope.evaluateIdentifier( new Identifier("main") );
			rv = val.call(new Argument[]{});
		}
		catch(ClientException e) {
			e.pushFunc("main (called while executing Node_Bundle)");
			throw e;
		}

		if( rv is Null )
			return Bridge.wrapInteger(0);
		else
			return rv;
	}

	//call
	public static IWorker execute(Node_Call node, Scope scope) {
		IWorker func = execute(node.receiver, scope);
		
		IList<Argument> args = new List<Argument>();
		foreach( Node_Argument argument in node.arguments )
			args.Add(Evaluator.evaluate(argument, scope));
	
		try {
			return func.call(args);
		}
		catch(ClientException e) {
			e.pushFunc(
				( node.receiver is Node_Identifier ?
					node.receiver.ToString() :
					"(anonymous)" ));
			throw e;
		}
	}
	
	//caller
	public static IWorker execute(Node_Caller node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//cast
	public static IWorker execute(Node_Cast node, Scope scope) {
		return G.cast(
			execute(node.source, scope),
			Evaluator.evaluate(node.nullableType, scope));
	}
	
	//chain
	public static IWorker execute(Node_Chain node, Scope scope) {
		throw new NotImplementedException();
	}

	//conditional
	public static IWorker execute(Node_Conditional node, Scope scope) {
		foreach( Node_Possibility p in node.possibilitys )
			if( Bridge.unwrapBoolean(execute(p.test, scope)) )
				return execute(p.result, scope);
		if( node.@else != null )
			return execute(node.@else, scope);
		return new Null();			
	}
	
	//curry
	public static IWorker execute(Node_Curry node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//declare-assign
	public static IWorker execute(Node_DeclareAssign node, Scope scope) {
		IWorker val = execute(node.value, scope);
		scope.declareAssign(
			node.name.value,
			node.identikeyType.identikeyCategory.value,
			Evaluator.evaluate(node.identikeyType.nullableType, scope),
			val);
		return val;
	}

	//declare-empty
	public static IWorker execute(Node_DeclareEmpty node, Scope scope) {
		scope.declareEmpty(
			node.name.value,
			node.identikeyType.identikeyCategory.value,
			Evaluator.evaluate(node.identikeyType.nullableType, scope));
		return scope.evaluateIdentifier(node.name.value);
	}

	//xxx all declare-first assginments should happen beforehand, separately
	//xxx then normal execution only returns the value that has already been assigned
	//declare-first
	public static IWorker execute(Node_DeclareFirst node, Scope scope) {
		IWorker val = execute(node.value, scope);
		scope.declareFirst(
			node.name.value,
			node.identikeyType.identikeyCategory.value,
			Evaluator.evaluate(node.identikeyType.nullableType, scope),
			val);
		return val;
	}
	
	//dictionary
	public static IWorker execute(Node_Dictionary node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//do-while
	public static IWorker execute(Node_DoWhile node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//do-times
	public static IWorker execute(Node_DoTimes node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//enum
	public static IWorker execute(Node_Enum node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//extract-member
	public static IWorker execute(Node_ExtractMember node, Scope scope) {
		IWorker source = execute(node.source, scope);
		Identifier memberName = node.memberName.value;
		return source.extractMember(memberName);
	}
	
	//for-key
	public static IWorker execute(Node_ForKey node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//for-manual
	public static IWorker execute(Node_ForManual node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//for-pair
	public static IWorker execute(Node_ForPair node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//for-range
	public static IWorker execute(Node_ForRange node, Scope scope) {
		IWorker start = execute(node.start, scope);
		long current = Bridge.unwrapInteger(start);
		IWorker limit = execute(node.limit, scope);
		while( current < Bridge.unwrapInteger(limit) ) {
			Scope innerScope = new Scope(scope);
			innerScope.declareAssign(
				node.name.value,
				IdentikeyCategory.VARIABLE,
				new NullableType(Bridge.faceInt, false),
				Bridge.wrapInteger(current));
			execute(node.action, innerScope);
			current++;
		}
		return new Null();
	}
	
	//for-value
	public static IWorker execute(Node_ForValue node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//function
	public static IWorker execute(Node_Function node, Scope scope) {
		IList<ParameterImpl> parameters = new List<ParameterImpl>();
		foreach( Node_ParameterImpl parameter in node.parameterImpls )
			parameters.Add(Evaluator.evaluate(parameter, scope));

		NullableType returnType = ( node.returnInfo == null ?
			null :
			Evaluator.evaluate(node.returnInfo, scope) );
		
		return Client_Function.wrap(
			new Function_Client(
				parameters, returnType, node.body, scope));
	}
	
	//function-interface
	public static IWorker execute(Node_FunctionInterface node, Scope scope) {
		IList<ParameterInfo> parameters = new List<ParameterInfo>();
		foreach( Node_ParameterInfo pinfo in node.parameterInfos )
			parameters.Add(Evaluator.evaluate(pinfo, scope));
		return Client_Interface.wrap(
			FunctionInterface.getFuncFace(
				new Callee(
					parameters,
					Evaluator.evaluate(node.returnInfo, scope))));
	}
	
	//generator
	public static IWorker execute(Node_Generator node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//generic-function
	public static IWorker execute(Node_GenericFunction node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//identifier
	public static IWorker execute(Node_Identifier node, Scope scope) {
		return scope.evaluateIdentifier(node.value);
	}
	
	//ignore
	public static IWorker execute(Node_Ignore node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//implements
	public static IWorker execute(Node_Implements node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//instantiate-generic
	public static IWorker execute(Node_InstantiateGeneric node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//interface
	public static IWorker execute(Node_Interface node, Scope scope) {
		return Bridge.wrapInterface(Evaluator.evaluate(node, scope));
	}
	
	//integer
	public static IWorker execute(Node_Integer node, Scope scope) {
		return Bridge.wrapInteger(node.value);
	}
	
	//labeled
	public static IWorker execute(Node_Labeled node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//loop
	public static IWorker execute(Node_Loop node, Scope scope) {
		for(;;)
			execute(node.block, scope);
		return new Null();
	}
	
	//nand
	public static IWorker execute(Node_Nand node, Scope scope) {
		IWorker first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(true);
		
		IWorker second = execute(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	//namespaced-value-identikey
	public static IWorker execute(Node_NamespacedValueIdentikey node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//nor
	public static IWorker execute(Node_Nor node, Scope scope) {
		IWorker first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(false);
		
		IWorker second = execute(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}
	
	//object
	public static IWorker execute(Node_Object node, Scope scope) {
		DesalObject obj = new DesalObject();
		IList<IWorker> subroots = new List<IWorker>();
		foreach( Node_Worker workerNode in node.workers )
			subroots.Add( Evaluator.evaluate(workerNode, scope, obj) );
		obj.rootWorker = G.combineWorkers(subroots);
		return obj.rootWorker;
	}
	
	//or
	public static IWorker execute(Node_Or node, Scope scope) {
		IWorker first = execute(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(true);
		
		IWorker second = execute(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	//possibility
	public static IWorker execute(Node_Possibility node, Scope scope) {
		IWorker testVal = execute(node.test, scope);

		if( testVal.face != Bridge.faceBool )
			throw new ClientException("test must be a Bool");
		
		return ( Bridge.unwrapBoolean(testVal) ?
			execute(node.result, scope) :
			new Null() );
	}
	
	//rational
	public static IWorker execute(Node_Rational node, Scope scope) {
		return Bridge.wrapRational(node.value);
	}
	
	//return
	public static IWorker execute(Node_Return node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//select
	public static IWorker execute(Node_Select node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//set-property
	public static IWorker execute(Node_SetProperty node, Scope scope) {
		IWorker val = execute(node.value, scope);
		execute(node.source, scope).setProperty(
			node.propertyName.value, val );
		return val;
	}
	
	//string
	public static IWorker execute(Node_String node, Scope scope) {
		return Bridge.wrapString(node.value);
	}
	
	//throw
	public static IWorker execute(Node_Throw node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//try-catch
	public static IWorker execute(Node_TryCatch node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//while
	public static IWorker execute(Node_While node, Scope scope) {
		while( Bridge.unwrapBoolean(execute(node.test, scope)) )
			execute(node.block, scope);
		return new Null();
	}
	
	//xnor
	public static IWorker execute(Node_Xnor node, Scope scope) {
		IWorker first = execute(node.first, scope);
		IWorker second = execute(node.second, scope);

		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) == Bridge.unwrapBoolean(second) );
	}
	
	//xor
	public static IWorker execute(Node_Xor node, Scope scope) {
		IWorker first = execute(node.first, scope);
		IWorker second = execute(node.second, scope);
		
		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) != Bridge.unwrapBoolean(second) );
	}
	
	//yield
	public static IWorker execute(Node_Yield node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//any expression node
	public static IWorker execute(INode_Expression node, Scope scope) {
		Reflection.MethodInfo meth = typeof(Executor)
			.GetMethod("execute", new Type[]{node.GetType(), typeof(Scope)});
		
		if( meth.GetParameters()[0].ParameterType == typeof(INode_Expression) )
			throw new ApplicationException(String.Format(
				"can't execute node of type '{0}'",
				node.GetType()));
		
		try {
			return (IWorker)meth.Invoke(null, new object[]{node, scope});
		}
		catch(Reflection.TargetInvocationException e) {
			if( e.InnerException is ClientException )
				throw e.InnerException;
			throw e;
		}
	}
}