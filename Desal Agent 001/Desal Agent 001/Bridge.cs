/*
A bridge between native code and a Desal bundle node.

xxx Should this merge with Node_Bundle or be renamed to "Bundle"?

security note:
This class defines static objects that are shared across Bundles.
Be careful to not let one Bundle visibly affect the objects that appear to other Bundles.
*/

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Diagnostics;

class Bridge {
	static Scope _universalScope;
	static IWorker _faceBool;
	static IWorker _faceInt;
	static IWorker _faceInterface;
	static IWorker _faceObject;
	static IWorker _faceRat;
	static IWorker _faceString;

	//to fake inner functions in the static constructor
	delegate IInterface InnerFunc(string name);

	static Bridge() {		
		XmlDocument doc = new XmlDocument();
		string assemblyPath = Assembly.GetCallingAssembly().Location;
		string assemblyDirectory = assemblyPath.Substring(
			0, assemblyPath.LastIndexOf("/"));
		doc.Load(assemblyDirectory + "/interfaces.desible");
		
		Bridge bridge = new Bridge();
		_universalScope = new Scope(bridge);
		DesibleParser dp = new DesibleParser(bridge, doc);
		Node_Block block = dp.parse<Node_Block>(doc.DocumentElement);
		
		//reserve identikeys
		foreach( Node_DeclareFirst df in block.members )
			_universalScope.reserveDeclareFirst(
				df.name.value,
				df.identikeyType.identikeyCategory.value,
				null);
		
		//set nullable-type of identikeys
		foreach( Node_DeclareFirst df in block.members )
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

		//assign to identikeys
		foreach( Node_DeclareFirst df in block.members ) {
			Executor.execute(df, _universalScope);
		}
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