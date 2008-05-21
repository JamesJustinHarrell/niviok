/*
A bridge between native code and a Desal bundle node.

xxx Should this merge with Node_Bundle or be renamed to "Bundle"?

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
	static IWorker _faceBool;
	static IWorker _faceInt;
	static IWorker _faceInterface;
	static IWorker _faceObject;
	static IWorker _faceRat;
	static IWorker _faceString;
	static IWorker _faceGenerator;
	static Scope _universalScope;

	//to fake inner functions in the static constructor
	delegate IInterface InnerFunc(string name);

	static Bridge() {		
		string assemblyPath = Reflection.Assembly.GetCallingAssembly().Location;
		string assemblyDirectory = assemblyPath.Substring(
			0, assemblyPath.LastIndexOf("/"));
		string stdLibPath = assemblyDirectory + "/std.desexp";
		
		Bridge bridge = new Bridge();
		_universalScope = new Scope(bridge);
		Node_Bundle bundle = Desexp.DesexpParser.parseFile(bridge, stdLibPath);
		Node_Plane plane = bundle.planes[0];
		
		//reserve identikeys
		foreach( Node_DeclareFirst df in plane.declareFirsts )
			_universalScope.reserveDeclareFirst(
				df.name.value,
				df.identikeyType.identikeyCategory.value,
				null);
		
		//set nullable-type of identikeys
		foreach( Node_DeclareFirst df in plane.declareFirsts )
			_universalScope.setType(
				df.name.value,
				Evaluator.evaluate(df.identikeyType.nullableType, _universalScope));

		//get identikeys
		_faceBool = _universalScope.evaluateIdentifier(new Identifier("Bool"));
		_faceInt = _universalScope.evaluateIdentifier(new Identifier("Int"));
		_faceInterface = _universalScope.evaluateIdentifier(new Identifier("Interface"));
		_faceObject = _universalScope.evaluateIdentifier(new Identifier("Object"));
		_faceRat = _universalScope.evaluateIdentifier(new Identifier("Rat"));
		_faceString = _universalScope.evaluateIdentifier(new Identifier("String"));
		_faceGenerator = _universalScope.evaluateIdentifier(new Identifier("Generator"));

		//assign to identikeys
		foreach( Node_DeclareFirst df in plane.declareFirsts ) {
			Executor.execute(df, _universalScope);
		}
		
		//add func println(dyn value)
		IList<ParameterImpl> printParameters = new ParameterImpl[] {
			new ParameterImpl(
				Direction.IN, NullableType.dyn,
				new Identifier("text"), null)
		};
		IFunction printFunction = new Function_Native(
			printParameters, null, printFunctionNative, _universalScope);
		IWorker wrappedPrintFunction = Client_Function.wrap(printFunction);
		
		_universalScope.declareAssign(
			new Identifier("println"),
			IdentikeyCategory.CONSTANT,
			new NullableType(printFunction.face, false),
			wrappedPrintFunction);
			
		_universalScope.declareAssign(
			new Identifier("true"),
			IdentikeyCategory.CONSTANT,
			new NullableType(Bridge.faceBool, false),
			Bridge.wrapBoolean(true));
		
		_universalScope.declareAssign(
			new Identifier("false"),
			IdentikeyCategory.CONSTANT,
			new NullableType(Bridge.faceBool, false),
			Bridge.wrapBoolean(false));
	}
	
	//func println( Stringable text )
	static IWorker printFunctionNative(Scope args) {	
		Bridge bridge = args.bridge;
		IWorker arg = args.evaluateLocalIdentifier( new Identifier("text") );
		
		/* xxx
		should use String breeder of argument
		bridge.println(
			Bridge.unwrapString(
				arg.breed(Bridge.faceString) ))
		But I haven't yet implemented breeders.
		*/
		
		if( arg is Null )
			bridge.println( "null" );
		else if( arg.face == Bridge.faceString )
			bridge.println( Bridge.unwrapString(arg) );
		else if( arg.face == Bridge.faceInt )
			bridge.println( Bridge.unwrapInteger(arg).ToString() );
		else if( arg.face == Bridge.faceBool )
			bridge.println( Bridge.unwrapBoolean(arg).ToString() );
		else if( arg.face == Bridge.faceRat )
			bridge.println( Bridge.unwrapRational(arg).ToString() );
		else if( arg.face == Bridge.faceObject )
			bridge.println( "object" );
		else
			throw new NotImplementedException(
				"unknown type cannot be converted to string " +
				"because breeders aren't implemented yet");
		
		return new Null();
	}

	public static IWorker faceBool {
		get { return _faceBool; }
	}
	public static IWorker faceInt {
		get { return _faceInt; }
	}
	public static IWorker faceInterface {
		get { return _faceInterface; }
	}
	public static IWorker faceObject {
		get { return _faceObject; }
	}
	public static IWorker faceRat {
		get { return _faceRat; }
	}
	public static IWorker faceString {
		get { return _faceString; }
	}
	public static IWorker faceGenerator {
		get { return _faceGenerator; }
	}

	public static Scope universalScope {
		get { return _universalScope; }
	}

	public static IWorker wrapBoolean(bool val) {
		return Client_Boolean.wrap(val);
	}
	public static bool unwrapBoolean(IWorker worker) {
		return (bool)worker.owner.native;
	}

	public static IWorker wrapCodePoints(IList<uint> codePoints) {
		return Client_String.wrap(codePoints);
	}
	public static IList<uint> unwrapCodePoints(IWorker worker) {
		return (IList<uint>)worker.owner.native;
	}

	public static IWorker wrapFunction(IFunction val) {
		return Client_Function.wrap(val);
	}
	public static IFunction unwrapFunction(IWorker worker) {
		return (IFunction)worker.owner.native;
	}

	public static IWorker wrapInteger(long val) {
		return Client_Integer.wrap(val);
	}
	public static long unwrapInteger(IWorker worker) {
		return (long)worker.owner.native;
	}

	public static IWorker wrapInterface(IInterface val) {
		return Client_Interface.wrap(val);
	}
	public static IInterface unwrapInterface(IWorker worker) {
		return (IInterface)worker.owner.native;
	}

	public static IWorker wrapRational(double val) {
		return Client_Rational.wrap(val);
	}
	public static double unwrapRational(IWorker worker) {
		return (double)worker.owner.native;
	}

	public static IWorker wrapString(string val) {
		return wrapCodePoints(StringUtil.codePointsFromString(val));
	}
	public static string unwrapString(IWorker val) {
		return StringUtil.stringFromCodePoints(unwrapCodePoints(val));
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