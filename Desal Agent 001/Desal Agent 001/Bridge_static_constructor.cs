using System;
using System.Xml;
using System.Collections.Generic;

partial class Bridge {
	static Bridge() {

		XmlDocument doc = new XmlDocument();
		
		doc.LoadXml(@"
			<wrapper xmlns='urn:desible1'>
				<interface xml:id='object'/>
			
				<interface xml:id='bool'>
				</interface>
				
				<interface xml:id='int'>
					<method label='member'>
						<identifier label='name'>add</identifier>
						<function-interface label='interface'>
							<parameter-info>
								<identifier label='name'>value</identifier>
								<nullable-type>
									<boolean label='nullable'>false</boolean>
									<identifier label='interface'>Int</identifier>
								</nullable-type>
								<boolean label='has-default-value'>false</boolean>
								<direction>in</direction>
							</parameter-info>
							<nullable-type label='return-info'>
								<boolean label='nullable'>false</boolean>
								<identifier label='interface'>Int</identifier>
							</nullable-type>
						</function-interface>
					</method>
				</interface>
				
				<interface xml:id='rat'>
				</interface>
				
				<interface xml:id='string'>
					<property label='member'>
						<identifier label='name'>length</identifier>
						<nullable-type>
							<boolean label='nullable'>false</boolean>
						</nullable-type>
						<boolean label='writable'>false</boolean>
					</property>
					<method label='member'>
						<identifier label='name'>concat</identifier>
						<function-interface label='interface'>
							<parameter-info>
								<identifier label='name'>value</identifier>
								<nullable-type>
									<boolean label='nullable'>false</boolean>
									<identifier label='interface'>String</identifier>
								</nullable-type>
								<boolean label='has-default-value'>false</boolean>
								<direction>in</direction>
							</parameter-info>
							<nullable-type label='return-info'>
								<boolean label='nullable'>false</boolean>
								<identifier label='interface'>String</identifier>
							</nullable-type>
						</function-interface>
					</method>
				</interface>
				
				<interface xml:id='interface'/>
			</wrapper>
		");
		
		Bridge bridge = new Bridge();
		XmlNamespaceManager nsMan = new XmlNamespaceManager(doc.NameTable);

		//note: for unknown reasons, all XPath expressions that specify a tag name fail
		//e.g. "./*" succeeds, but "./wrapper" fails

		//xxx bridge parameter should not be null
		faceObject = DesibleParser.createNativeInterface( bridge,
			(XmlElement)doc.SelectSingleNode("//*[@xml:id='object']", nsMan));
		faceBool = DesibleParser.createNativeInterface( bridge,
			(XmlElement)doc.SelectSingleNode("//*[@xml:id='bool']", nsMan));
		faceInt = DesibleParser.createNativeInterface( bridge,
			(XmlElement)doc.SelectSingleNode("//*[@xml:id='int']", nsMan));
		faceRat = DesibleParser.createNativeInterface( bridge,
			(XmlElement)doc.SelectSingleNode("//*[@xml:id='rat']", nsMan));
		faceString = DesibleParser.createNativeInterface( bridge,
			(XmlElement)doc.SelectSingleNode("//*[@xml:id='string']", nsMan));
		faceInterface = DesibleParser.createNativeInterface( bridge,
			(XmlElement)doc.SelectSingleNode("//*[@xml:id='interface']", nsMan));

	} //end of static Bridge()
} //end of partial class Bridge
