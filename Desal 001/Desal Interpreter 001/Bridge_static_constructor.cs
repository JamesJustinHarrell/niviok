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
				</interface>
				
				<interface xml:id='rat'>
				</interface>
				
				<interface xml:id='string'/>
				
				<!--
				<interface xml:id='string'>
					<method>
						<identifier label='name'>concat!</identifier>
						<function-interface label='interface'>
							<parameter>
								<identifier label='name'>value</identifier>
								<reference-type label='type'>
									<reference-category>value</reference-category>
									<identifier label='interface'>String</identifier>
								</reference-type>
							</parameter>
							<reference-type label='return-type'>
								<reference-category>value</reference-category>
								<identifier label='interface'>String</identifier>
							</reference-type>
						</function-interface>
					</method>
				</interface>
				-->
				
				<interface xml:id='interface'/>
			</wrapper>
		");
	
		//xxx bridge parameter is null
		_boolFace = DesibleParser.extractInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[0] );
		_intFace = DesibleParser.extractInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[1] );
		_ratFace = DesibleParser.extractInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[2] );
		_stringFace = DesibleParser.extractInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[3] );
		_interfaceFace = DesibleParser.extractInterface( null,
			(XmlElement)doc.GetElementsByTagName("interface")[4] );
	
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
		
		_stringImpl = stringBuilder.compile(String);
		
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
			
		_intImpl = intBuilder.compile(Int);
		
		InterfaceImplementationBuilder<Client_Rational> ratBuilder =
			new InterfaceImplementationBuilder<Client_Rational>();
			
		_ratImpl = ratBuilder.compile(Rat);
		
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
		
		_boolImpl = boolBuilder.compile(Bool);


		InterfaceImplementationBuilder<Client_Interface> faceBuilder =
			new InterfaceImplementationBuilder<Client_Interface>();
		
		faceBuilder.addValueMethod(
			new Identifier("equals?"),
			FunctionInterface.getFuncFace(
				new Parameter[]{
					new Parameter( new Identifier("value"), Interface )
				},
				new ReferenceType( ReferenceCategory.VALUE, Interface ) ),
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
