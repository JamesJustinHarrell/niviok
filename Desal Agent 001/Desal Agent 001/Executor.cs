using System;
using System.Collections.Generic;
using System.Threading; //for yield node

//executes expression nodes, as defined by the Desal Semantics specification
static partial class Executor {
	//and
	public static IWorker execute(Node_And node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(false);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == false )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}

	//assign
	public static IWorker execute(Node_Assign node, Scope scope) {
		IWorker val = executeAny(node.value, scope);
		scope.assign( node.name.value, val );
		return val;
	}

	//break
	public static IWorker execute(Node_Break node, Scope scope) {
		throw new NotImplementedException();
	}

	//breed
	public static IWorker execute(Node_Breed node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//bundle as standalone
	public static IWorker execute(Node_Bundle node, Bridge bridge) {
		Scope globalScope = new Scope(bridge);
		globalScope.declareNamespace(new Identifier("std"), Bridge.universalScope);
		
		//xxx import nodes
		
		//expose nodes (xxx support using nodes)
		foreach( INode_ScopeAlteration alt in node.alts ) {
			if( alt is Node_Expose ) {
				IList<Identifier> idents = new List<Identifier>();
				foreach( Node_Identifier ident in (alt as Node_Expose).identifiers )
					idents.Add(ident.value);
				globalScope.expose(idents);
			}
		}

		//reserve decl-first identikeys
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts ) {
				globalScope.reserveDeclareFirst(
					decl.name.value,
					decl.identikeyType.identikeyCategory.value,
					null);
			}
		}
		
		//set nullable-type of identikeys
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts ) {
				globalScope.setType(
					decl.name.value,
					Evaluator.evaluate(
						decl.identikeyType.nullableType,
						globalScope));
			}
		}

		//assign decl-first identikeys
		foreach( Node_Plane plane in node.planes ) {
			foreach( Node_DeclareFirst decl in plane.declareFirsts )
				Executor.execute(decl, globalScope);
		}

		//call main
		IWorker rv;
		try {
			Node_Call call = new Node_Call(
				new Node_Identifier(new Identifier("main")),
				new Node_Argument[]{});
			rv = execute(call, globalScope);
		}
		catch(ClientReturn e) {
			throw new ClientException("unhandled return", e);
		}

		if( rv == null )
			return Bridge.wrapInteger(0);
		else {
			//xxx ensure rv is an integer
			return rv;
		}
	}

	//call
	public static IWorker execute(Node_Call node, Scope scope) {
		IWorker func = executeAny(node.receiver, scope);
		
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
			executeAny(node.source, scope),
			Evaluator.evaluate(node.nullableType, scope));
	}

	//compound
	public static IWorker execute(Node_Compound node, Scope scope) {
		Scope innerScope = new Scope(scope);
		
		//reserve identikeys
		foreach( INode_Expression member in node.members )
			if( member is Node_DeclareFirst ) {
				Node_DeclareFirst df = (Node_DeclareFirst)member;
				innerScope.reserveDeclareFirst(
					df.name.value,
					df.identikeyType.identikeyCategory.value,
					null);
			}

		//set nullable-type of identikeys
		foreach( INode_Expression member in node.members )
			if( member is Node_DeclareFirst ) {
				Node_DeclareFirst df = (Node_DeclareFirst)member;
				innerScope.setType(
					df.name.value,
					Evaluator.evaluate(df.identikeyType.nullableType, innerScope));
			}

		IWorker rv = new Null();
		foreach( INode_Expression expr in node.members ) {
			rv = executeAny(expr, innerScope);
		}

		return rv;
	}

	//conditional
	public static IWorker execute(Node_Conditional node, Scope scope) {
		IWorker testVal = executeAny(node.test, scope);

		if( testVal.face != Bridge.faceBool )
			throw new ClientException("test must be a Bool");
		
		return (
			Bridge.unwrapBoolean(testVal) ? executeAny(node.result, scope) :
			node.@else != null ? executeAny(node.@else, scope) :
			new Null() );
	}
	
	//conditional-loop
	public static IWorker execute(Node_ConditionalLoop node, Scope scope) {
		while( Bridge.unwrapBoolean(executeAny(node.test, scope)) )
			executeAny(node.body, scope);
		return new Null();
	}
	
	//continue
	public static IWorker execute(Node_Continue node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//curry
	public static IWorker execute(Node_Curry node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//declare-assign
	public static IWorker execute(Node_DeclareAssign node, Scope scope) {
		IWorker val = executeAny(node.value, scope);
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
		IWorker val = executeAny(node.value, scope);
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
	
	//enum
	public static IWorker execute(Node_Enum node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//enumerator-loop
	public static IWorker execute(Node_EnumeratorLoop node, Scope scope) {
		int rCount = node.receivers.Count;
		if( rCount > 1 )
			throw new NotImplementedException();

		IWorker producer = executeAny(node.container, scope);
		
		while( node.test == null || Bridge.unwrapBoolean(executeAny(node.test, scope)) ) {
			Scope innerScope = new Scope(scope);
			IWorker yielded;
			try {
				yielded = producer
					.extractMember(new Identifier("yield"))
					.call(new Argument[]{});
			}
			catch(ClientException e) {
				//xxx use typing and interfaces instead of values
				if( e.thrown.face == Bridge.faceString &&
				Bridge.unwrapString(e.thrown) == "generator exhausted" )
					return new Null();
				else
					throw;
			}
			if( rCount == 1 ) {
				IEnumerator<Node_Receiver> en = node.receivers.GetEnumerator();
				en.MoveNext();
				Node_Receiver nr = en.Current;
				innerScope.declareAssign(
					nr.name.value,
					IdentikeyCategory.CONSTANT,
					NullableType.dyn_nullable,
					yielded);
			}
			executeAny(node.body, innerScope);
		}
		
		return new Null();
	}
	
	//extract-member
	public static IWorker execute(Node_ExtractMember node, Scope scope) {
		IWorker source = executeAny(node.source, scope);
		Identifier memberName = node.memberName.value;
		return source.extractMember(memberName);
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
		return Client_Generator.wrap(node.body, scope);
	}
	
	//generic-function
	public static IWorker execute(Node_GenericFunction node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//generic-interface
	public static IWorker execute(Node_GenericInterface node, Scope scope) {
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
	
	//nand
	public static IWorker execute(Node_Nand node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == false )
			return Bridge.wrapBoolean(true);
		
		IWorker second = executeAny(node.second, scope);
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
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(false);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(false);
		
		return Bridge.wrapBoolean(true);
	}
	
	//null
	public static IWorker execute(Node_Null node, Scope scope) {
		if( node.@interface == null )
			return new Null();
		return new Null(executeAny(node.@interface, scope));
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
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(first) == true )
			return Bridge.wrapBoolean(true);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.unwrapBoolean(second) == true )
			return Bridge.wrapBoolean(true);
		
		return Bridge.wrapBoolean(false);
	}
	
	//rational
	public static IWorker execute(Node_Rational node, Scope scope) {
		return Bridge.wrapRational(node.value);
	}
	
	//return
	public static IWorker execute(Node_Return node, Scope scope) {
		throw new ClientReturn(executeAny(node.value, scope));
	}
	
	//select
	public static IWorker execute(Node_Select node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//set-property
	public static IWorker execute(Node_SetProperty node, Scope scope) {
		IWorker val = executeAny(node.value, scope);
		executeAny(node.source, scope).setProperty(
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
	
	//unconditional-loop
	public static IWorker execute(Node_UnconditionalLoop node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//xnor
	public static IWorker execute(Node_Xnor node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		IWorker second = executeAny(node.second, scope);

		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) == Bridge.unwrapBoolean(second) );
	}
	
	//xor
	public static IWorker execute(Node_Xor node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		IWorker second = executeAny(node.second, scope);
		
		return Bridge.wrapBoolean(
			Bridge.unwrapBoolean(first) != Bridge.unwrapBoolean(second) );
	}
	
	//yield
	//highly coupled with Client_Generator
	public static IWorker execute(Node_Yield node, Scope scope) {
		IWorker yieldValue = executeAny(node.value, scope);
		#if GEN_DEBUG
		Console.WriteLine("fixin to yield " + Bridge.unwrapInteger(yieldValue));
		Console.Out.Flush();
		#endif
		scope.yieldValue = yieldValue;
		
		//xxx for each call to Interrupt, Mono throws ThreadInterruptedException twice
		//the while loop is a workaround which would otherwise not be required
		while( scope.yieldValue != null ) {
			try {
				Thread.Sleep(Timeout.Infinite);
			}
			catch(ThreadInterruptedException e) {
				#if GEN_DEBUG
				Console.WriteLine("generator awoken");
				Console.Out.Flush();
				#endif
			}
		}
		
		return new Null(); //note: in future, may add something like Python's "send" method
	}
}