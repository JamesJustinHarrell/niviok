/*
A bridge between native code and a Desal bundle node.

xxx Should this merge with Node_Module or be renamed to "Bundle"?

security note:
This class defines static objects that are shared across Bundles.
Be careful to not let one Bundle visibly affect the objects that appear to other Bundles.
*/

using System;
using Reflection = System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;

class Bridge {
	//for internal types, like integers
	//should only actually be used by those types when very bad errors occur
	public static Scope debugScope;
	
	static Namespace _std;

	//to fake inner functions in the static constructor
	delegate IInterface InnerFunc(string name);

	static Bridge() {
		debugScope = new Scope(new Bridge());
	
		string assemblyPath = Reflection.Assembly.GetCallingAssembly().Location;
		string assemblyDirectory = assemblyPath.Substring(
			0, assemblyPath.LastIndexOf("/"));
		string stdLibPath = assemblyDirectory + "/std.desexp";
		
		Bridge bridge = new Bridge();
		Node_Module module = Desexp.DesexpParser.parseFile(bridge, stdLibPath);
		Scope scope = new Scope(bridge);
		_std = new Namespace(scope);
		
		scope.declareAssign(
			new Identifier("Interface"),
			IdentikeyCategory.CONSTANT,
			NullableType.dyn,
			std_Interface.worker);
			
		scope.declareAssign(
			new Identifier("Object"),
			IdentikeyCategory.CONSTANT,
			NullableType.dyn,
			std_Object.worker);
			
		Executor.execute(module, scope);
		
		//add func println(dyn value)
		IList<ParameterImpl> printParameters = new ParameterImpl[] {
			new ParameterImpl(
				Direction.IN, NullableType.dyn,
				new Identifier("text"), null)
		};
		IFunction printFunction = new Function_Native(
			printParameters, null, printFunctionNative, scope);
		IWorker wrappedPrintFunction = Client_Function.wrap(printFunction);
		
		scope.declareAssign(
			new Identifier("println"),
			IdentikeyCategory.CONSTANT,
			new NullableType(printFunction.face, false),
			wrappedPrintFunction);
			
		scope.declareAssign(
			new Identifier("true"),
			IdentikeyCategory.CONSTANT,
			new NullableType(Bridge.std_Bool, false),
			toClientBoolean(true));
		
		scope.declareAssign(
			new Identifier("false"),
			IdentikeyCategory.CONSTANT,
			new NullableType(Bridge.std_Bool, false),
			toClientBoolean(false));
	}
	
	//func println( Stringable text )
	static IWorker printFunctionNative(Scope args) {	
		Bridge bridge = args.bridge;
		IWorker arg = args.evaluateLocalIdentifier( new Identifier("text") );
		if( arg is Null )
			bridge.println( "null" );
		else
			bridge.println( Bridge.toNativeString(arg) );
		return new Null();
	}
	
	public static Namespace std {
		get { return _std; }
	}
	
	public static IInterface std_Bool {
		get {
			return toNativeInterface(
				_std.evalWorkerIdent(new Identifier("Bool")));
		}
	}
	public static IInterface std_Int {
		get {
			return toNativeInterface(
				_std.evalWorkerIdent(new Identifier("Int")));
		}
	}
	public static IInterface std_Interface {
		get { return Interface_Interface.instance; }
	}
	public static IInterface std_Object {
		get { return Interface_Object.instance; }
	}
	public static IInterface std_Rat {
		get {
			return toNativeInterface(
				_std.evalWorkerIdent(new Identifier("Rat")));
		}
	}
	public static IInterface std_String {
		get {
			return toNativeInterface(
				_std.evalWorkerIdent(new Identifier("String")));
		}
	}
	//xxx replace Generator interface with Iterable/Iterator generic interfaces
	public static IInterface std_Generator {
		get {
			return toNativeInterface(
				_std.evalWorkerIdent(new Identifier("Generator")));
		}
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
		return Client_Boolean.wrap(val);
	}
	public static bool toNativeBoolean(IWorker worker) {
		return (bool)G.castDown(worker, std_Bool).nativeObject;
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
		worker = G.castDown(worker, std_Interface);
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
		return Client_String.wrap(StringUtil.codePointsFromString(val));
	}
	public static string toNativeString(IWorker val) {
		//xxx doesn't work with client implementations of String
		return StringUtil.stringFromCodePoints(
			Client_String.unwrap(val.breed(std_String)));
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