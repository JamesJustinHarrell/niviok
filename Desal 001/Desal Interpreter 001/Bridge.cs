/*
A bridge between native code and a Desal bundle node.

xxx Should this merge with Node_Bundle or be renamed to "Bundle"?

security note:
This class defines static objects that are shared across Bundles.
Be careful to not let one Bundle visibly affect the objects that appear to other Bundles.
*/

using System.Xml;
using System.Collections.Generic;

partial class Bridge {
	static IInterface _boolFace;
	static IInterface _intFace;
	static IInterface _ratFace;
	static IInterface _stringFace;
	static IInterface _interfaceFace;
	static IInterfaceImplementation<Client_Boolean> _boolImpl;
	static IInterfaceImplementation<Client_Integer> _intImpl;
	static IInterfaceImplementation<Client_Rational> _ratImpl;
	static IInterfaceImplementation<Client_String> _stringImpl;
	static IInterfaceImplementation<Client_Interface> _interfaceImpl;

	public static IInterface Bool {
		get { return _boolFace; }
	}
	
	public static IInterface Int {
		get { return _intFace; }
	}
	
	public static IInterface Rat {
		get { return _ratFace; }
	}
	
	public static IInterface String {
		get { return _stringFace; }
	}
	
	public static IInterface Interface {
		get { return _interfaceFace; }
	}
	
	public static void checkNull(IList<object> objects) {
		for( int i = 0; i < objects.Count; i++ ) {
			if( objects[i] == null )
				throw new System.ApplicationException(
					System.String.Format("index {0} is null", i));
		}
	}

	public static IValue wrapBoolean(bool val) {
		return new Value<Client_Boolean>(
			new Object<Client_Boolean>( new Client_Boolean(val) ),
			_boolImpl );
	}
	
	public static bool unwrapBoolean(IValue val) {
		if( val is Value<Client_Boolean> == false ) {
			/* xxx enable
			val = val.evaluateMethod( new Identifier("toBuiltin"), null );
			if( val is Value<Client_Boolean> == false )
				throw new ClientException( createToBuiltinFailure() );
			*/
			throw new Error_Unimplemented();
		}
		return ((Value<Client_Boolean>)val).state.value;
	}
	
	public static bool isBuiltinBoolean(IValue val) {
		return (val is Value<Client_Boolean>);
	}

	public static IValue wrapInteger(long val) {
		return new Value<Client_Integer>(
			new Object<Client_Integer>( new Client_Integer(val) ),
			_intImpl );
	}
	
	public static long unwrapInteger(IValue val) {
		if( val is Value<Client_Integer> )
			return ((Value<Client_Integer>)val).state.value;
		throw new Error_Unimplemented("only builtin integers currently supported");
	}
	
	public static bool isBuiltinInteger(IValue val) {
		return (val is Value<Client_Integer>);
	}

	public static IValue wrapRational(double val) {
		return new Value<Client_Rational>(
			new Object<Client_Rational>( new Client_Rational(val) ),
			_ratImpl );
	}
	
	public static double unwrapRational(IValue val) {
		if( val is Value<Client_Rational> )
			return ((Value<Client_Rational>)val).state.value;
		throw new Error_Unimplemented("only builtin integers currently supported");
	}
	
	public static bool isBuiltinRational(IValue val) {
		return (val is Value<Client_Rational>);
	}
	
	public static IValue wrapInterface(IInterface face) {
		return new Value<Client_Interface>(
			new Object<Client_Interface>( new Client_Interface(face) ),
			_interfaceImpl );
	}
	
	public static IInterface unwrapInterface(IValue value) {		
		if( value is Value<Client_Interface> )
			return ((Value<Client_Interface>)value).state.value;
		return InterfaceFromValue.wrap(value);
	}

	public static IValue wrapCodePoints(IList<uint> codePoints) {
		return new Value<Client_String>(
			new Object<Client_String>( new Client_String(codePoints) ),
			_stringImpl );
	}

	public static IList<uint> unwrapCodePoints(IValue val) {
		if( val is Value<Client_String> == false) {
			/* xxx enable
			val = val.evaluateMethod( new Identifier("toBuiltin"), null );
			if( val is Value<Client_String> == false )
				throw new ClientException( createToBuiltinFailure() );
			*/
			throw new Error_Unimplemented("only builtin strings supported for now");
		}
		
		return ((Value<Client_String>)val).state.codePoints;
	}

	public static IValue wrapString(string val) {
		return wrapCodePoints( StringUtil.codePointsFromString(val) );
	}
	
	public static string unwrapString(IValue val) {		
		return StringUtil.stringFromCodePoints(
			unwrapCodePoints(val) );
	}
	
	public static bool isBuiltinString(IValue val) {
		return (val is Value<Client_String>);
	}
	
	public void output(string message) {
		System.Console.Out.WriteLine(message);
	}
	
	public void warning(string message) {
		System.Console.Error.WriteLine("WARNING: " + message);
	}
	
	public void error(string message) {
		System.Console.Error.WriteLine("ERROR: " + message);
	}
}