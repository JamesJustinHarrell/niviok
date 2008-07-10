//executes statement and expression nodes

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading; //for yield node
using System.IO;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public static partial class Executor {

	//----- STATEMENTS

	public static int executeProgramModule(
	Node_Module node, TextReader inStream, TextWriter outStream, TextWriter logStream ) {
		try {
			IScope scope = new Scope(null, new ScopeAllowance(false,false));
			
			//bind libraries
			scope.bindNamespace(new Identifier("std"), Bridge.std);
			IDerefable stdio = Bridge.buildStdioLib(inStream, outStream, logStream);
			scope.bindNamespace(new Identifier("stdio"), stdio);
			try {
				foreach(Node_Import i in node.imports)
					execute(i, scope);
			}
			catch(LibraryFailure e) {
				outStream.WriteLine(
					"Failed to load library. Program will not execute.");
				return 1;
			}
			
			//execute global declarations
			ScopeQueue sq = new ScopeQueue();
			IDerefable sieve = executeGetSieve(node.sieve, sq, scope);
			try {
				sq.executeAll();
			}
			catch(ClientException e) {
				outStream.WriteLine("Global declaration threw an exception: " + e);
				return 1;
			}
			
			//get main function
			IWorker main;
			try {
				main = GE.evalIdent(sieve, "main");
			}
			catch(UnknownScidentre e) {
				outStream.WriteLine("no main function");
				return 1;
			}
		
			//call main function
			try {
				main.call(new Argument[]{});
			}
			catch(ClientException e) {
				outStream.WriteLine("uncaught exception: " + e.clientMessage);
				return 1;
			}

			IWorker exitStatus = GE.evalIdent(stdio, "get exit status").call(new Argument[]{});
			return (int)Bridge.toNativeInteger(exitStatus);
		}
		catch(Exception e) {
			outStream.WriteLine(
				"An error occurred in Acrid. Client program terminated.");
			throw;
		}
	}

	public static IDerefable executeLibraryModule(
	Node_Module node, TextReader inStream, TextWriter outStream, TextWriter logStream ) {
		IScope scope = new Scope(null, new ScopeAllowance(false,false));
		
		//bind libraries
		scope.bindNamespace(new Identifier("std"), Bridge.std);
		try {
			foreach(Node_Import i in node.imports)
				execute(i, scope);
		}
		catch(LibraryFailure e) {
			throw new LibraryFailure(
				"couldn't load library due to failure to load child library", e);
		}
		
		//execute global declarations
		IDerefable sieve;
		try {
			ScopeQueue sq = new ScopeQueue();
			sieve = executeGetSieve(node.sieve, sq, scope);
			sq.executeAll();
		}
		catch(ClientException e) {
			throw new LibraryFailure(
				"couldn't load library because it throw an exception", e);
		}
		
		return new Library(sieve);
	}

	public static void execute( Node_Expose node, IScope scope ) {
		IList<Identifier> idents = GE.extractIdents(node.identifiers);
		scope.expose( new NamespaceReference(new IdentifierSequence(idents), scope) );
	}

	public static void execute( Node_Import node, IScope s ) {
		foreach(Node_ImportAttempt at in node.importAttempts) {
			IDerefable lib = Bridge.tryImport(at.scheme.value, at.body.value);
			if(lib != null) {
				s.bindNamespace(node.alias.value, lib);
				return;
			}
		}
		throw new LibraryFailure("couldn't load library with attempts ...");
	}

	public static void execute( Node_Using node, IScope scope ) {
		IList<Identifier> idents = GE.extractIdents(node.targets);
		Identifier name;
		if(node.name == null)
			name = G.last(idents);
		else
			name = node.name.value;
		scope.bindNamespace( name, new NamespaceReference(new IdentifierSequence(idents), scope) );
	}

	public static void execute( Node_Sieve node, ScopeQueue sq, IScope scope ) {
		scope.expose( executeGetSieve(node, sq, scope) );
	}

	public static Sieve executeGetSieve( Node_Sieve node, ScopeQueue sq, IScope scope ) {
		Sieve sieve = new Sieve(scope);
		foreach( Node_Expose e in node.exposes )
			execute(e, sieve.hidden);
		foreach( Node_Using u in node.usings )
			execute(u, sieve.hidden);
		foreach( Node_Hidable hidable in node.hidables )
			execute(hidable, sq, sieve);
		return sieve;
	}

	public static void execute( Node_Hidable node, ScopeQueue sq, Sieve sieve ) {
		execute( node.declaration, sq, node.hidden.value ? sieve.hidden : sieve.visible );
	}

	public static void execute( Node_Namespace node, ScopeQueue sq, IScope scope ) {
		scope.bindNamespace( node.name.value,
			executeGetSieve( node.sieve, sq,
				new NamespaceScope(scope, node.name.value)));
	}

	public static void execute( Node_DeclareFirst node, ScopeQueue sq, IScope scope ) {
		//the ScopeQueue takes care of finding dependencies
		sq.add(
			scope.reserveWoScidentre(
				node.name.value,
				node.overload.value ? WoScidentreCategory.OVERLOAD : WoScidentreCategory.CONSTANT),
			node.type,
			node.value,
			scope );
	}
	
	public static void execute( INode_StatementDeclaration decl, ScopeQueue sq, IScope scope ) {
		switch(decl.typeName) {
		case "declare-first" :
			execute(decl as Node_DeclareFirst, sq, scope);
			break;
		case "namespace" :
			execute(decl as Node_Namespace, sq, scope);
			break;
		case "sieve" :
			execute(decl as Node_Sieve, sq, scope);
			break;
		default :
			throw new Exception();
		}
	}	

	
	//----- EXPRESSIONS
	//every method returns IWorker

	//and
	public static IWorker execute(Node_And node, IScope scope) {
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
	public static IWorker execute(Node_Assign node, IScope scope) {
		IWorker val = executeAny(node.value, scope);
		scope.assign( node.name.value, val );
		return val;
	}

	//breed
	public static IWorker execute(Node_Breed node, IScope scope) {
		throw new NotImplementedException();
	}

	//call
	public static IWorker execute(Node_Call node, IScope scope) {
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
	public static IWorker execute(Node_Caller node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//compound
	public static IWorker execute(Node_Compound node, IScope parentScope) {
		IScope scope = new Scope(parentScope, null);
		
		foreach(Node_Expose exp in node.exposes)
			execute(exp, scope);

		foreach(Node_Using us in node.usings)
			execute(us, scope);
		
		WoScidentreReserver.reserve(node, scope);

		ScopeQueue sq = new ScopeQueue();
		foreach(INode_StatementDeclaration decl in node.declarations)
			execute(decl, sq, scope);
		sq.executeAll();
		
		IWorker rv = new Null();
		foreach(INode_Expression member in node.members)
			rv = executeAny(member, scope);
		return rv;
	}

	//conditional
	public static IWorker execute(Node_Conditional node, IScope scope) {
		IWorker testVal = executeAny(node.test, scope);

		if( testVal.face != Bridge.stdn_Bool )
			throw new ClientException("test must be a Bool");
		
		return (
			Bridge.toNativeBoolean(testVal) ? executeAny(node.result, scope) :
			node.@else != null ? executeAny(node.@else, scope) :
			new Null() );
	}
	
	//curry
	public static IWorker execute(Node_Curry node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//declare-assign
	public static IWorker execute(Node_DeclareAssign node, IScope scope) {
		IWorker val = executeAny(node.value, scope);
		scope.activateWoScidentre(
			node.name.value,
			new NType(executeAny(node.type, scope)),
			val);
		return val;
	}

	//declare-empty
	public static IWorker execute(Node_DeclareEmpty node, IScope scope) {
		scope.activateWoScidentre(
			node.name.value,
			new NType(executeAny(node.type, scope)),
			new Null());
		return new Null();
	}
	
	//dictionary
	public static IWorker execute(Node_Dictionary node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//enum
	public static IWorker execute(Node_Enum node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//extract-member
	public static IWorker execute(Node_ExtractMember node, IScope scope) {
		IWorker source = executeAny(node.source, scope);
		Identifier memberName = node.memberName.value;
		return source.extractMember(memberName);
	}
	
	//function
	public static IWorker execute(Node_Function node, IScope scope) {
		IList<ParameterImpl> parameters = new List<ParameterImpl>();
		foreach( Node_ParameterImpl parameter in node.parameterImpls )
			parameters.Add(Evaluator.evaluate(parameter, scope));

		NType returnType = new NType(executeAny(node.returnType, scope));
		
		return Client_Function.wrap(
			new Function_Client(
				parameters, returnType, node.body, scope));
	}
	
	//function-interface
	public static IWorker execute(Node_FunctionInterface node, IScope scope) {
		IList<ParameterInfo> parameters = new List<ParameterInfo>();
		foreach( Node_ParameterInfo pinfo in node.parameterInfos )
			parameters.Add(Evaluator.evaluate(pinfo, scope));
		return FunctionInterface.getFuncFace(
				new Callee(
					parameters,
					new NType(executeAny(node.returnType, scope)))).worker;
	}
	
	//generator
	public static IWorker execute(Node_Generator node, IScope scope) {
		return Client_Generator.wrap(node.body, scope);
	}
	
	//generic-function
	public static IWorker execute(Node_GenericFunction node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//generic-interface
	public static IWorker execute(Node_GenericInterface node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//identifier
	public static IWorker execute(Node_Identifier node, IScope scope) {
		try {
			return GE.evalIdent(scope, node.value);
		}
		catch(UnassignedDeclareFirst e) {
			//xxx temporary
			throw new ClientException(
				"identifier '" + e.ident + "' refers to a scidentre which was " +
				"created by a declare-first node and hasn't been assigned to yet",
				node.nodeSource);
		}
	}
	
	//instantiate-generic
	public static IWorker execute(Node_InstantiateGeneric node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//interface
	public static IWorker execute(Node_Interface node, IScope scope) {
		return Evaluator.evaluate(node, scope).worker;
	}
	
	//integer
	public static IWorker execute(Node_Integer node, IScope scope) {
		return Bridge.toClientInteger(node.value);
	}
	
	//nand
	public static IWorker execute(Node_Nand node, IScope scope) {
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
	
	//namespaced-wo-scidentre
	public static IWorker execute(Node_NamespacedWoScidentre node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//nor
	public static IWorker execute(Node_Nor node, IScope scope) {
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
	
	//object
	public static IWorker execute(Node_Object node, IScope scope) {
		NObject obj = new NObject();
		IList<IWorker> subroots = new List<IWorker>();
		foreach( Node_Worker workerNode in node.workers )
			subroots.Add( Evaluator.evaluate(workerNode, scope, obj) );
		obj.rootWorker = GE.combineWorkers(subroots);
		return obj.rootWorker;
	}
	
	//or
	public static IWorker execute(Node_Or node, IScope scope) {
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
	public static IWorker execute(Node_Rational node, IScope scope) {
		return Bridge.toClientRational(node.value);
	}
	
	//remit
	public static IWorker execute(Node_Remit node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//select
	public static IWorker execute(Node_Select node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//set-property
	public static IWorker execute(Node_SetProperty node, IScope scope) {
		IWorker val = executeAny(node.value, scope);
		executeAny(node.source, scope).setProperty(
			node.propertyName.value, val );
		return val;
	}
	
	//string
	public static IWorker execute(Node_String node, IScope scope) {
		return Bridge.toClientString(node.value);
	}
	
	//throw
	public static IWorker execute(Node_Throw node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//try-catch
	//xxx copy code over from spec -- this here is outdated
	public static IWorker execute(Node_TryCatch node, IScope scope) {
		IScope innerScope = new Scope(scope, null);
		IWorker rv = new Null();
		bool exceptionOccurred = false;
		try {
			rv = executeAny(node.@try, innerScope);
		}
		catch(ClientException exception) {
			exceptionOccurred = true;
			IInterface thrownFace = exception.thrown.face;
			foreach( Node_Catcher eh in node.catchers ) {
				IInterface catchFace = Bridge.toNativeInterface(
					executeAny(eh.type, innerScope));
				if( GE.inheritsOrIs(thrownFace, catchFace) ) {
					IScope handlerScope = new Scope(innerScope, null);
					if( eh.name != null )
						GE.declareAssign(
							eh.name.value,
							WoScidentreCategory.CONSTANT,
							new NType(),
							exception.thrown,
							handlerScope);
					rv = Executor.executeAny(eh.result, handlerScope);
					break;
				}
			}
		}
		finally {
			if( exceptionOccurred == false && node.onSuccess != null )
				rv = executeAny(node.onSuccess , innerScope);
			if( node.@finally != null )
				executeAny(node.@finally, innerScope);
		}
		return rv;
	}
	
	//type-select
	public static IWorker execute(Node_TypeSelect node, IScope scope) {
		throw new NotImplementedException();
	}
	
	//xnor
	public static IWorker execute(Node_Xnor node, IScope scope) {
		IWorker first = executeAny(node.first, scope);
		IWorker second = executeAny(node.second, scope);

		return Bridge.toClientBoolean(
			Bridge.toNativeBoolean(first) == Bridge.toNativeBoolean(second) );
	}
	
	//xor
	public static IWorker execute(Node_Xor node, IScope scope) {
		IWorker first = executeAny(node.first, scope);
		IWorker second = executeAny(node.second, scope);
		
		return Bridge.toClientBoolean(
			Bridge.toNativeBoolean(first) != Bridge.toNativeBoolean(second) );
	}
	
	//yield
	//highly coupled with Client_Generator
	public static IWorker execute(Node_Yield node, IScope scope) {
		IWorker yieldValue = executeAny(node.value, scope);
		#if GEN_DEBUG
		Console.WriteLine("fixin to yield " + Bridge.toNativeInteger(yieldValue));
		Console.Out.Flush();
		#endif
		
		Client_Generator.setYieldValue(Thread.CurrentThread, yieldValue);
		
		//xxx for each call to Interrupt, Mono throws ThreadInterruptedException twice
		//the while loop is a workaround which would otherwise not be required
		while( ! Client_Generator.hasNullYieldValue(Thread.CurrentThread) ) {
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

} //namespace
