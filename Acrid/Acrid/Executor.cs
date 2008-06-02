//executes statement and expression nodes

using System;
using System.Collections.Generic;
using System.Threading; //for yield node

static partial class Executor {

	//----- STATEMENTS

	//xxx remove? -- execution of declare-first is done by limit-old executor
	//declare-first
	public static void execute(Node_DeclareFirst node, Scope scope) {
		//xxx
		scope.bridge.printlnWarning("declare-first implemented incorrectly");
		scope.declareAssign(
			node.name.value,
			node.identikeyType.identikeyCategory.value,
			Evaluator.evaluate(node.identikeyType.nullableType, scope),
			executeAny(node.value, scope));
	}

	//expose
	public static void execute(Node_Expose node, Scope scope) {
		IList<Identifier> idents = new List<Identifier>();
		foreach( Node_Identifier ident in node.identifiers )
			idents.Add(ident.value);
		scope.expose(idents);
	}

	//hidable
	public static void execute(Node_Hidable node, Scope scope) {
		//set on scope
		executeISN(node.declaration, scope);
		
		//set on scope.parent if hidden=false
		//xxx
		scope.bridge.printlnWarning("hidable node implemented incorrectly");
		if( node.hidden.value == false && (node.declaration is Node_DeclareFirst) ) {
			Node_DeclareFirst df = (node.declaration as Node_DeclareFirst);
			scope.parent.declareAssign(
				df.name.value,
				df.identikeyType.identikeyCategory.value,
				NullableType.dyn,
				scope.evaluateLocalIdentifier(df.name.value));
		}
	}

	//xxx remove? -- execution is handled by limit-old executor
	//identikey-special-new
	public static void executeISN(INode_IdentikeySpecialNew node, Scope scope) {
		if( node is Node_DeclareFirst )
			execute( node as Node_DeclareFirst, scope );
		else if( node is Node_LimitOld )
			execute( node as Node_LimitOld, scope );
		else if( node is Node_Namespace )
			execute( node as Node_Namespace, scope );
		else
			throw new ApplicationException(
				String.Format(
					"unknown type of identikey-special-new node: {0}",
					node));
	}

	//identikey-special-old
	public static void executeISO(INode_IdentikeySpecialOld node, Scope scope) {
		if( node is Node_Expose )
			execute( node as Node_Expose, scope );
		else if( node is Node_Using )
			execute( node as Node_Using, scope );
		else
			throw new ApplicationException(
				String.Format(
					"unknown type of identikey-special-old node: {0}",
					node));
	}

	//import
	public static void execute(Node_Import node, Scope scope) {
		//xxx
		scope.bridge.printlnWarning("import node not implemented");
	}

	//limit-old
	public static void execute(Node_LimitOld node, Scope outerScope) {
		Scope innerScope = new Scope(outerScope);
		
		foreach( INode_IdentikeySpecialOld iso in node.declarations )
			executeISO(iso, innerScope);

		//reserve identikeys
		foreach( Node_Hidable h in node.hidables ) {
			if( ! (h.declaration is Node_DeclareFirst) ) {
				Console.WriteLine(
					String.Format(
						"this type of declaration not yet implemented: {0}",
						h.declaration));
				continue;
			}
			Node_DeclareFirst df = h.declaration as Node_DeclareFirst;
			innerScope.reserveDeclareFirst(
				df.name.value, df.identikeyType.identikeyCategory.value);
			if( h.hidden.value == false )
				outerScope.reserveDeclareFirst(
					df.name.value, df.identikeyType.identikeyCategory.value);
		}

		//set nullable-type of reserved identikeys
		foreach( Node_Hidable h in node.hidables ) {
			if( ! (h.declaration is Node_DeclareFirst) ) {
				Console.WriteLine(
					String.Format(
						"this type of declaration not yet implemented: {0}",
						h.declaration));
				continue;
			}
			Node_DeclareFirst df = h.declaration as Node_DeclareFirst;
			NullableType nt = Evaluator.evaluate(df.identikeyType.nullableType, innerScope);
			innerScope.setType(df.name.value, nt);
			if( h.hidden.value == false )
				outerScope.setType(df.name.value, nt);
		}
		
		//set value of reserved identikeys
		foreach( Node_Hidable h in node.hidables ) {
			if( ! (h.declaration is Node_DeclareFirst) ) {
				Console.WriteLine(
					String.Format(
						"this type of declaration not yet implemented: {0}",
						h.declaration));
				continue;
			}
			Node_DeclareFirst df = h.declaration as Node_DeclareFirst;
			IWorker val = Executor.executeAny(df.value, innerScope);
			innerScope.declareFirst(df.name.value, val);
			if( h.hidden.value == false )
				outerScope.declareFirst(df.name.value, val);
		}
	}

	//module
	//called by Evaluator.evaluate(Node_Module, Bridge)
	//the std namespace should have already been added
	public static void execute(Node_Module node, Scope scope) {
		//xxx check types of main, library_initialize, and library_dispose

		foreach( Node_Import imp in node.imports )
			execute(imp, scope);
		
		execute(node.limitOld, scope);
	}

	//module
	public static int executeProgram(Node_Module node, Bridge bridge) {
		//evaluator should handle importing the std library
		Scope scope = Evaluator.evaluate(node, bridge);
		IWorker result;
		try {
			IWorker main = scope.evaluateIdentifier(new Identifier("main"));
			result = main.call(new Argument[]{});
		}
		catch(ClientException e) {
			bridge.printlnError("uncaught exception: " + e.clientMessage);
			return 1;
		}
		catch(ClientReturn e) {
			throw new ClientException("unhandled return", e);
		}
		//xxx catch other Client* exception types
		//xxx what should happen if result code is out of range?
		if( result != null && !(result is Null) )
			return (int)Bridge.toNativeInteger(result);
		return 0;
	}
	
	//namespace
	public static void execute(Node_Namespace node, Scope scope) {
		//xxx
		scope.bridge.printlnWarning("namespace node not implemented");
	}
	
	//using
	public static void execute(Node_Using node, Scope scope) {
		//xxx
		scope.bridge.printlnWarning("using node not implemented");
	}
	
	
	//----- EXPRESSIONS
	//every method returns IWorker

	//and
	public static IWorker execute(Node_And node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(first) == false )
			return Bridge.toClientBoolean(false);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(second) == false )
			return Bridge.toClientBoolean(false);
		
		return Bridge.toClientBoolean(true);
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
		foreach( INode_IdentikeySpecialOld iso in node.oldDeclarations )
			executeISO(iso, innerScope);
		//xxx declare-first children of ISN's should all be executed together
		foreach( INode_IdentikeySpecialNew isn in node.newDeclarations )
			executeISN(isn, innerScope);
		IWorker rv = new Null();
		foreach( INode_Expression expr in node.members ) {
			rv = executeAny(expr, innerScope);
		}
		return rv;
	}

	//conditional
	public static IWorker execute(Node_Conditional node, Scope scope) {
		IWorker testVal = executeAny(node.test, scope);

		if( testVal.face != Bridge.std_Bool )
			throw new ClientException("test must be a Bool");
		
		return (
			Bridge.toNativeBoolean(testVal) ? executeAny(node.result, scope) :
			node.@else != null ? executeAny(node.@else, scope) :
			new Null() );
	}
	
	//conditional-loop
	public static IWorker execute(Node_ConditionalLoop node, Scope scope) {
		while( Bridge.toNativeBoolean(executeAny(node.test, scope)) )
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
		
		while( node.test == null || Bridge.toNativeBoolean(executeAny(node.test, scope)) ) {
			Scope innerScope = new Scope(scope);
			IWorker yielded;
			try {
				yielded = producer
					.extractMember(new Identifier("yield"))
					.call(new Argument[]{});
			}
			catch(ClientException e) {
				//xxx use typing and interfaces instead of values
				if( e.thrown.face == Bridge.std_String &&
				Bridge.toNativeString(e.thrown) == "generator exhausted" )
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
		return FunctionInterface.getFuncFace(
				new Callee(
					parameters,
					Evaluator.evaluate(node.returnInfo, scope))).worker;
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
		return Evaluator.evaluate(node, scope).worker;
	}
	
	//integer
	public static IWorker execute(Node_Integer node, Scope scope) {
		return Bridge.toClientInteger(node.value);
	}
	
	//labeled
	public static IWorker execute(Node_Labeled node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//nand
	public static IWorker execute(Node_Nand node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(first) == false )
			return Bridge.toClientBoolean(true);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(second) == false )
			return Bridge.toClientBoolean(true);
		
		return Bridge.toClientBoolean(false);
	}
	
	//namespaced-value-identikey
	public static IWorker execute(Node_NamespacedValueIdentikey node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//nor
	public static IWorker execute(Node_Nor node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(first) == true )
			return Bridge.toClientBoolean(false);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(second) == true )
			return Bridge.toClientBoolean(false);
		
		return Bridge.toClientBoolean(true);
	}
	
	//null
	public static IWorker execute(Node_Null node, Scope scope) {
		if( node.@interface == null )
			return new Null();
		return new Null(
			Bridge.toNativeInterface(
				executeAny(node.@interface, scope)));
	}
	
	//object
	public static IWorker execute(Node_Object node, Scope scope) {
		NiviokObject obj = new NiviokObject();
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
		if( Bridge.toNativeBoolean(first) == true )
			return Bridge.toClientBoolean(true);
		
		IWorker second = executeAny(node.second, scope);
		//xxx downcast
		if( Bridge.toNativeBoolean(second) == true )
			return Bridge.toClientBoolean(true);
		
		return Bridge.toClientBoolean(false);
	}
	
	//rational
	public static IWorker execute(Node_Rational node, Scope scope) {
		return Bridge.toClientRational(node.value);
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
		return Bridge.toClientString(node.value);
	}
	
	//throw
	public static IWorker execute(Node_Throw node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//try-catch
	//xxx it's not clear how this node produces -- should the product of finally be used?
	public static IWorker execute(Node_TryCatch node, Scope scope) {
		Scope innerScope = new Scope(scope);
		IWorker rv = new Null();
		bool exceptionOccurred = false;
		try {
			rv = executeAny(node.@try, innerScope);
		}
		catch(ClientException exception) {
			exceptionOccurred = true;
			IInterface thrownFace = exception.thrown.face;
			foreach( Node_ExceptionHandler eh in node.exceptionHandlers ) {
				IInterface catchFace = Bridge.toNativeInterface(
					executeAny(eh.@interface, innerScope));
				if( G.inheritsOrIs(thrownFace, catchFace) ) {
					Scope handlerScope = new Scope(innerScope);
					if( eh.name != null )
						handlerScope.declareAssign(
							eh.name.value,
							IdentikeyCategory.CONSTANT,
							NullableType.dyn,
							exception.thrown);
					rv = Executor.executeAny(eh.result, handlerScope);
					if( eh.@catch.value )
						break;
					else
						throw exception;
				}
			}
		}
		finally {
			if( exceptionOccurred == false && node.@else != null )
				rv = executeAny(node.@else, innerScope);
			if( node.@finally != null )
				executeAny(node.@finally, innerScope);
		}
		return rv;
	}
	
	//unconditional-loop
	public static IWorker execute(Node_UnconditionalLoop node, Scope scope) {
		throw new NotImplementedException();
	}
	
	//xnor
	public static IWorker execute(Node_Xnor node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		IWorker second = executeAny(node.second, scope);

		return Bridge.toClientBoolean(
			Bridge.toNativeBoolean(first) == Bridge.toNativeBoolean(second) );
	}
	
	//xor
	public static IWorker execute(Node_Xor node, Scope scope) {
		IWorker first = executeAny(node.first, scope);
		IWorker second = executeAny(node.second, scope);
		
		return Bridge.toClientBoolean(
			Bridge.toNativeBoolean(first) != Bridge.toNativeBoolean(second) );
	}
	
	//yield
	//highly coupled with Client_Generator
	public static IWorker execute(Node_Yield node, Scope scope) {
		IWorker yieldValue = executeAny(node.value, scope);
		#if GEN_DEBUG
		Console.WriteLine("fixin to yield " + Bridge.toNativeInteger(yieldValue));
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