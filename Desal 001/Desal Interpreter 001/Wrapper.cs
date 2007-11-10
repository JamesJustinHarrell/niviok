//xxx move other wrapping functions into this class

		/* xxx
		Add constructor to StringClass that takes a list of code points.
		This may make converting to builtin strings easier.
		
		interface of StringClass
			call()
			call(String)
			call(List<Int> codePoints)
		
		IList<IValue> codePointVals = ...
		IList<int> codePoints = new List<int>();
		foreach( IValue codePointVal in codePointVals ) {
			codePoints.Add( unwrapInteger(codePointVal) );
		}
		return Wrapper.wrapString( stringFromCodePoints(codePoints) );
		*/

using System.Xml;
using System.Collections.Generic;

static class Wrapper {
	public static IInterface Bool;
	public static IInterface Int;
	public static IInterface Rat;
	public static IInterface String;
	static IInterfaceImplementation<Client_Boolean> boolImpl;
	static IInterfaceImplementation<Client_Integer> intImpl;
	static IInterfaceImplementation<Client_Rational> ratImpl;
	static IInterfaceImplementation<Client_String> stringImpl;
	
	static Wrapper() {
		XmlDocument doc = new XmlDocument();
		
		doc.LoadXml(@"
			<wrapper xmlns='urn:desible1'>
				<interface xml:id='bool'>
				</interface>
				<interface xml:id='int'>
				</interface>
				<interface xml:id='rat'>
				</interface>
				<interface xml:id='string'>
				</interface>
			</wrapper>
		");
	
		Bool = DesibleParser.extractInterface(
			(XmlElement)doc.GetElementsByTagName("interface")[0] );
		Int = DesibleParser.extractInterface(
			(XmlElement)doc.GetElementsByTagName("interface")[1] );
		Rat = DesibleParser.extractInterface(
			(XmlElement)doc.GetElementsByTagName("interface")[2] );
		String = DesibleParser.extractInterface(
			(XmlElement)doc.GetElementsByTagName("interface")[3] );
	
		InterfaceImplementationBuilder<Client_String> stringBuilder =
			new InterfaceImplementationBuilder<Client_String>();

		stringBuilder.addPropertyGetter(
			new Identifier("length"),
			delegate(Client_String o) { return wrapInteger(o.length); } );

		stringBuilder.addValueMethod(
			new Identifier("concat"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("value"), String )
				},
				new ReferenceType( ReferenceCategory.VALUE, String ) ),
			delegate(Client_String o, Scope args) {
				return wrapCodePoints(
					o.concat(
						unwrapCodePoints(
							args.evaluateIdentifier(
								new Identifier("value"))) ));
			});
		
		stringBuilder.addVoidMethod(
			new Identifier("concat!"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("value"), String )
				},
				null),
			delegate(Client_String o, Scope args) {
				o.concat(
					unwrapCodePoints(
						args.evaluateIdentifier(
							new Identifier("value"))) );
			});

		stringBuilder.addValueMethod(
			new Identifier("substring"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("start"), Int )
				},
				new ReferenceType( ReferenceCategory.VALUE, String ) ),
			delegate(Client_String o, Scope args) {
				return wrapCodePoints(
						o.substring(
							unwrapInteger(
								args.evaluateLocalIdentifier(
									new Identifier("start"))) ));
			});

		stringBuilder.addValueMethod(
			new Identifier("substring"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("start"), Int ),
					new Parameter( new Identifier("limit"), Int )
				},
				new ReferenceType( ReferenceCategory.VALUE, String ) ),
			delegate(Client_String o, Scope args) {
				return wrapCodePoints(
						o.substring(
							unwrapInteger(
								args.evaluateLocalIdentifier(
									new Identifier("start"))),
							unwrapInteger(
								args.evaluateLocalIdentifier(
									new Identifier("limit"))) ));
			});
		
		stringImpl = stringBuilder.compile(String);
		
		InterfaceImplementationBuilder<Client_Integer> intBuilder =
			new InterfaceImplementationBuilder<Client_Integer>();
		
		intBuilder.addValueMethod(
			new Identifier("add"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("value"), Int )
				},
				new ReferenceType( ReferenceCategory.VALUE, Int ) ),
			delegate(Client_Integer o, Scope args) {
				return wrapInteger(
						o.add(
							unwrapInteger(
								args.evaluateLocalIdentifier(
									new Identifier("value"))) ));
			} );
			
		intImpl = intBuilder.compile(Int);
		
		InterfaceImplementationBuilder<Client_Boolean> boolBuilder =
			new InterfaceImplementationBuilder<Client_Boolean>();
		
		boolBuilder.addValueMethod(
			new Identifier("equals?"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("value"), Bool )
				},
				new ReferenceType( ReferenceCategory.VALUE, Bool ) ),
			delegate(Client_Boolean o, Scope args) {
				return wrapBoolean(
					o.equals(
						unwrapBoolean(
							args.evaluateLocalIdentifier(
								new Identifier("value"))) ));
			} );
		
		boolImpl = boolBuilder.compile(Bool);
	}

	public static IValue wrapBoolean(bool val) {
		return new Value<Client_Boolean>(
			new Object<Client_Boolean>( new Client_Boolean(val) ),
			boolImpl );
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
		return ((Value<Client_Boolean>)val).state.val;
	}
	
	public static bool isBuiltinBoolean(IValue val) {
		return (val is Value<Client_Boolean>);
	}

	public static IValue wrapInteger(long val) {
		return new Value<Client_Integer>(
			new Object<Client_Integer>( new Client_Integer(val) ),
			intImpl );
	}
	
	public static long unwrapInteger(IValue val) {
		if( val is Value<Client_Integer> )
			return ((Value<Client_Integer>)val).state.val;
		throw new Error_Unimplemented("only builtin integers currently supported");
	}
	
	public static bool isBuiltinInteger(IValue val) {
		return (val is Value<Client_Integer>);
	}

	public static IValue wrapCodePoints(IList<uint> codePoints) {
		return new Value<Client_String>(
			new Object<Client_String>( new Client_String(codePoints) ),
			stringImpl );
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
}