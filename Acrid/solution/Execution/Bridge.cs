//A bridge between native code and a Niviok module.
//xxx Should this be merged with Node_Module?
//xxx Should this be renamed to "Module"?

using System;
using Reflection = System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;
using System.IO;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public class Bridge {
	static IWorker _std;
	static ISieve _stdSieve;
	
	//xxx temporary
	static NType _any;
	static NType _nullable_any;

	//to fake inner functions in the static constructor
	delegate IInterface InnerFunc(string name);

	static Bridge() {
		//xxx temporary
		_any = new NType();
		_nullable_any = new NType();
	
		string assemblyPath = Reflection.Assembly.GetCallingAssembly().Location;
		string assemblyDirectory = assemblyPath.Substring(
			0, assemblyPath.LastIndexOf("/"));
		string stdLibPath = assemblyDirectory + "/std.toy";
		
		Node_Module module;
		
		try {
			module = Acrid.Toy.ToyParser.parseFile(stdLibPath);
		}
		catch(ParseError e) {
			throw new TypeInitializationException(
				String.Format(
					"Error while parsing standard library:\n{0}\nlocation: {1}",
					e.Message, e.source),
				e);
		}
		
		buildStandardLibrary(module);
	}
	
	static void buildStandardLibrary(Node_Module node) {
		IScope scope = new Scope(null, new ScopeAllowance(false,false));
		ScopeQueue sq = new ScopeQueue();
		Sieve sieve = Executor.executeGetSieve(node.sieve, sq, scope);
		_stdSieve = sieve;

		GE.declareAssign(
			new Identifier("Interface"),
			ScidentreCategory.CONSTANT,
			new NType(),
			stdn_Interface.worker,
			sieve.visible );
		GE.declareAssign(
			new Identifier("Object"),
			ScidentreCategory.CONSTANT,
			new NType(),
			stdn_Object.worker,
			sieve.visible );
		GE.declareAssign(
			new Identifier("any"),
			ScidentreCategory.CONSTANT,
			new NType(),
			stdn_Object.worker, //xxx temporary - won't work when type checking is enabled
			sieve.visible );
		
		sq.executeAll();
		
		GE.declareAssign(
			new Identifier("true"),
			ScidentreCategory.CONSTANT,
			new NType(stdn_Bool),
			Client_Boolean.wrap(true),
			sieve.visible );
		GE.declareAssign(
			new Identifier("false"),
			ScidentreCategory.CONSTANT,
			new NType(stdn_Bool),
			Client_Boolean.wrap(false),
			sieve.visible );
		
		_std = LibraryWrapper.wrap(_stdSieve);
	}

	public static IWorker buildStdioLib(
	TextReader inStream, TextWriter outStream, TextWriter logStream ) {
		Sieve sieve = new Sieve(null);
		
		//println function
		IScidentre ws = sieve.reserveScidentre(
			true, new Identifier("println"), ScidentreCategory.OVERLOAD);
		ws.type = new NType();
		ws.assign(
			toClientFunction(
				new Function_Native(
					new ParameterImpl[] {
						new ParameterImpl(
							Direction.IN,
							stdn_any,
							new Identifier("text"),
							null)
					},
					null,
					delegate(IScope args) {
						IWorker arg = GE.evalIdent(args, "text");
						outStream.WriteLine( arg is Null ? "null" : Bridge.toNativeString(arg) );
						return new Null();
					},
					null)));
		
		//get_exit_status function
		IScidentre ws2 = sieve.reserveScidentre(
			true, new Identifier("get exit status"), ScidentreCategory.OVERLOAD);
		ws2.type = new NType();
		ws2.assign(
			toClientFunction(
				new Function_Native(
					new ParameterImpl[] {},
					null,
					delegate(IScope args) {
						return toClientInteger(0);
					},
					null)));
		
		return LibraryWrapper.wrap(sieve);
	}

	public static IWorker tryImport(string scheme, string body) {
		//xxx
		Console.WriteLine("returning null in Bridge.tryImport");
		return null;
	}

	public static IWorker std {
		get { return _std; }
	}
	
	public static NType stdn_any {
		get { return _any; } //xxx temporary
	}
	public static NType stdn_Nullable_any {
		get { return _nullable_any; } //xxx temporary
	}
	public static IInterface stdn_Bool {
		get { return toNativeInterface(GE.evalIdent(_stdSieve, "Bool")); }
	}
	public static IInterface stdn_Int {
		get { return toNativeInterface(GE.evalIdent(_stdSieve, "Bool")); }
	}
	public static IInterface stdn_Interface {
		get { return Interface_Interface.instance; }
	}
	public static IInterface stdn_Object {
		get { return Interface_Object.instance; }
	}
	public static IInterface stdn_Rat {
		get { return toNativeInterface(GE.evalIdent(_stdSieve, "Rat")); }
	}
	public static IInterface stdn_String {
		get { return toNativeInterface(GE.evalIdent(_stdSieve, "String")); }
	}
	//xxx replace Generator interface with Iterable/Iterator generic interfaces
	public static IInterface stdn_Generator {
		get { return toNativeInterface(GE.evalIdent(_stdSieve, "Generator")); }
	}

	//e.g. Foo -> Breeder<Foo>
	public static IInterface getBreederFace(IInterface face) {
		throw new NotImplementedException();
		/* xxx
			return _std
			.evalWorkerIdent(new Identifier("Breeder"))
			.extract(new Identifier("instantiate"))
			.call(new Argument[]{ toClientInterface(face) });
		*/
	}

	public static IWorker toClientBoolean(bool val) {
		return GE.extractMember(_std, val ? "true" : "false");
	}
	public static bool toNativeBoolean(IWorker worker) {
		//xxx how to handle client implementations of the Bool interface?
		return (bool)GE.castDown(worker, stdn_Bool).nativeObject;
	}

	public static IWorker toClientFunction(IFunction val) {
		return Client_Function.wrap(val);
	}
	public static IFunction toNativeFunction(IWorker worker) {
		return (IFunction)worker.nativeObject;
	}

	public static IWorker toClientInteger(long val) {
		return Client_Integer.wrap(val);
	}
	public static long toNativeInteger(IWorker worker) {
		return (long)worker.nativeObject;
	}

	//note: instead of Bridge.toClientInterface(face), use face.worker
	
	public static IInterface toNativeInterface(IWorker worker) {
		worker = GE.castDown(worker, stdn_Interface);
		if( worker.nativeObject == null )
			worker.nativeObject = InterfaceFromValue.wrap(worker);
		return (IInterface)worker.nativeObject;
	}

	public static IWorker toClientRational(double val) {
		return Client_Rational.wrap(val);
	}
	public static double toNativeRational(IWorker worker) {
		return (double)worker.nativeObject;
	}

	public static IWorker toClientString(string val) {
		return Client_String.wrap(StringUtil.toCodePoints(val));
	}
	public static string toNativeString(IWorker val) {
		//xxx doesn't work with client implementations of String
		return StringUtil.stringFromCodePoints(
			Client_String.unwrap(val.breed(stdn_String)));
	}
	
	public System.IO.TextWriter stdout {
		get { return Console.Out; }
	}
	
	public System.IO.TextWriter stderr {
		get { return Console.Error; }
	}
	
	public System.IO.TextReader stdin {
		get { return Console.In; }
	}
	
	public void println(string text) {
		stdout.WriteLine(text);
	}
	
	public void println() {
		println("");
	}
	
	public void print(string text) {
		stdout.Write(text);
	}
	
	public void printlnWarning(string message) {
		Console.Error.WriteLine("Warning: " + message);
	}
	
	public void printlnError(string message) {
		Console.Error.WriteLine("Error: " + message);
	}
}

} //namespace
