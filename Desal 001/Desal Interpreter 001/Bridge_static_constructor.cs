using System.Xml;
using System.Collections.Generic;

partial class Bridge {
	static Bridge() {

		XmlDocument doc = new XmlDocument();
		
		doc.LoadXml(@"
			<wrapper xmlns='urn:desible1'>
				<interface xml:id='bool'>
				</interface>
				
				<interface xml:id='int'>
					<method>
						<identifier label='name'>add</identifier>
						<function-interface label='interface'>
							<parameter>
								<identifier label='name'>value</identifier>
								<nullable-type>
									<boolean label='nullable'>false</boolean>
									<identifier label='interface'>Int</identifier>
								</nullable-type>
								<boolean label='has-default-value'>false</boolean>
							</parameter>
							<nullable-type label='return-type'>
								<boolean label='nullable'>false</boolean>
								<identifier label='interface'>Int</identifier>
							</nullable-type>
						</function-interface>
					</method>
				</interface>
				
				<interface xml:id='rat'>
				</interface>
				
				<interface xml:id='string'>
					<property>
						<identifier label='name'>length</identifier>
						<nullable-type>
							<boolean label='nullable'>false</boolean>
						</nullable-type>
						<access>get</access>
					</property>
					<method>
						<identifier label='name'>concat</identifier>
						<function-interface label='interface'>
							<parameter>
								<identifier label='name'>value</identifier>
								<nullable-type>
									<boolean label='nullable'>false</boolean>
									<identifier label='interface'>String</identifier>
								</nullable-type>
								<boolean label='has-default-value'>false</boolean>
							</parameter>
							<nullable-type label='return-type'>
								<boolean label='nullable'>false</boolean>
								<identifier label='interface'>String</identifier>
							</nullable-type>
						</function-interface>
					</method>
				</interface>
				
				<interface xml:id='interface'/>
			</wrapper>
		");
	
		//xxx bridge parameter is null
		_boolFace = DesibleParser.createNativeInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[0] );
		_intFace = DesibleParser.createNativeInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[1] );
		_ratFace = DesibleParser.createNativeInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[2] );
		_stringFace = DesibleParser.createNativeInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[3] );
		_interfaceFace = DesibleParser.createNativeInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[4] );
	
		InterfaceImplementationBuilder<Client_String> stringBuilder =
			new InterfaceImplementationBuilder<Client_String>();

		stringBuilder.addPropertyGetter(
			new Identifier("length"),
			delegate(Client_String o) { return wrapInteger(o.length); } );

		stringBuilder.addMethod(
			new Identifier("concat"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter(
						new NullableType(String, false),
						new Identifier("value") )
				},
				new NullableType(String, false) ),
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
					new Parameter(
						new NullableType(String, false),
						new Identifier("value") )
				},
				null),
			delegate(Client_String o, Scope args) {
				o.concat(
					unwrapCodePoints(
						args.evaluateIdentifier(
							new Identifier("value"))) );
			});

		stringBuilder.addMethod(
			new Identifier("substring"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter(
						new NullableType(Int, false),
						new Identifier("start") )
				},
				new NullableType( String, false ) ),
			delegate(Client_String o, Scope args) {
				return wrapCodePoints(
						o.substring(
							unwrapInteger(
								args.evaluateLocalIdentifier(
									new Identifier("start"))) ));
			});

		stringBuilder.addMethod(
			new Identifier("substring"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter(
						new NullableType(Int, false),
						new Identifier("start") ),
					new Parameter(
						new NullableType(Int, false),
						new Identifier("limit") )
				},
				new NullableType( String, false ) ),
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
		
		_stringImpl = stringBuilder.compile(String);
		
		InterfaceImplementationBuilder<Client_Integer> intBuilder =
			new InterfaceImplementationBuilder<Client_Integer>();
		
		intBuilder.addMethod(
			new Identifier("add"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter(
						new NullableType(Int, false),
						new Identifier("value") )
				},
				new NullableType( Int, false ) ),
			delegate(Client_Integer o, Scope args) {
				return wrapInteger(
						o.add(
							unwrapInteger(
								args.evaluateLocalIdentifier(
									new Identifier("value"))) ));
			} );
			
		_intImpl = intBuilder.compile(Int);
		
		InterfaceImplementationBuilder<Client_Rational> ratBuilder =
			new InterfaceImplementationBuilder<Client_Rational>();
			
		_ratImpl = ratBuilder.compile(Rat);
		
		InterfaceImplementationBuilder<Client_Boolean> boolBuilder =
			new InterfaceImplementationBuilder<Client_Boolean>();
		
		boolBuilder.addMethod(
			new Identifier("equals?"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter(
						new NullableType(Bool, false),
						new Identifier("value") )
				},
				new NullableType( Bool, false ) ),
			delegate(Client_Boolean o, Scope args) {
				return wrapBoolean(
					o.equals(
						unwrapBoolean(
							args.evaluateLocalIdentifier(
								new Identifier("value"))) ));
			} );
		
		_boolImpl = boolBuilder.compile(Bool);


		InterfaceImplementationBuilder<Client_Interface> faceBuilder =
			new InterfaceImplementationBuilder<Client_Interface>();
		
		faceBuilder.addMethod(
			new Identifier("equals?"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter(
						new NullableType(Interface, false),
						new Identifier("value") )
				},
				new NullableType( Interface, false ) ),
			delegate(Client_Interface o, Scope args) {
				return wrapBoolean(
					o.equals(
						unwrapInterface(
							args.evaluateLocalIdentifier(
								new Identifier("value"))) ));
			} );
		
		_interfaceImpl = faceBuilder.compile(Interface);
		
	} //end of static Bridge()
} //end of partial class Bridge
