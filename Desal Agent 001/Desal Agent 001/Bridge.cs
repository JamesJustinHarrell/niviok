/*
A bridge between native code and a Desal bundle node.

xxx Should this merge with Node_Bundle or be renamed to "Bundle"?

security note:
This class defines static objects that are shared across Bundles.
Be careful to not let one Bundle visibly affect the objects that appear to other Bundles.
*/

using System;
using System.Xml;
using System.Collections.Generic;

partial class Bridge {
	public static IInterface faceObject;
	public static IInterface faceBool;
	public static IInterface faceInt;
	public static IInterface faceRat;
	public static IInterface faceString;
	public static IInterface faceInterface;

	public static IWorker wrapBoolean(bool val) {
		return Client_Boolean.wrap(val);
	}
	
	public static bool unwrapBoolean(IWorker worker) {
		return (bool)worker.owner.native;
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

	public static IWorker wrapRational(double val) {
		return Client_Rational.wrap(val);
	}
	
	public static double unwrapRational(IWorker worker) {
		return (double)worker.owner.native;
	}

	public static IWorker wrapInterface(IInterface face) {
		return Client_Interface.wrap(face);
	}
	
	public static IInterface unwrapInterface(IWorker worker) {
		return (IInterface)worker.owner.native;
	}

	public static IWorker wrapCodePoints(IList<uint> codePoints) {
		return Client_String.wrap(codePoints);
	}

	public static IList<uint> unwrapCodePoints(IWorker worker) {
		return (IList<uint>)worker.owner.native;
	}

	public static IWorker wrapString(string val) {
		return wrapCodePoints( StringUtil.codePointsFromString(val) );
	}
	
	public static string unwrapString(IWorker val) {
		return StringUtil.stringFromCodePoints(
			unwrapCodePoints(val) );
	}
	
	public System.IO.TextWriter stdout {
		get { return System.Console.Out; }
	}
	
	public System.IO.TextWriter stderr {
		get { return System.Console.Error; }
	}
	
	public System.IO.TextReader stdin {
		get { return System.Console.In; }
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
	
	//xxx remove
	public void output(string message) {
		System.Console.Out.WriteLine(message);
	}
	
	//xxx rename?
	public void warning(string message) {
		System.Console.Error.WriteLine("WARNING: " + message);
	}
	
	//xxx rename?
	public void error(string message) {
		System.Console.Error.WriteLine("ERROR: " + message);
	}
}