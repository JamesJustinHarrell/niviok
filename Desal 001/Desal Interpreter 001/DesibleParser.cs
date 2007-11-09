using System.Collections.Generic;
using System.Xml;

class DesibleParser {
	string desible1NS = "urn:desible1";
	XmlNamespaceManager nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> handledElements;
	int _unhandledWarnLevel = 2;
	
	public int unhandledWarnLevel {
		get { return _unhandledWarnLevel; }
		set {
			if(value == 0 || value == 1 || value == 2 )
				_unhandledWarnLevel = value;
			else
				throw new System.Exception("bad value");
		}
	}
	
	public Node_Global parsePath(string path) {
		XmlDocument doc = new XmlDocument();
		doc.Load(path);
		return parse(doc);
	}
	
	public Node_Global parseMarkup(string markup) {
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(markup);
		return parse(doc);
	}

	public Node_Global parse(XmlDocument doc) {
		nsManager = new XmlNamespaceManager(doc.NameTable);
		nsManager.AddNamespace("desible1", desible1NS);
		handledElements = new List<XmlElement>();
		//xxx check XML with Relax NG
		setLabels(doc.DocumentElement);
		Node_Global global = parseGlobal(doc.DocumentElement);
		warnAboutUnhandled(doc.DocumentElement);
		return global;
	}

	void setLabels(XmlElement element) {
		if( ! element.HasAttribute("label") )
			element.SetAttribute("label", element.LocalName);
		foreach( XmlElement child in selectChildren(element) ) {
			setLabels(child);
		}
	}
	
	void checkElement(XmlElement element, string expectedTagName) {
		if( element.LocalName != expectedTagName )
			throw new Error_TagName(element.LocalName);
		if( element.NamespaceURI != desible1NS )
			throw new System.Exception(
				"attempted to handle an element in namespace '" + element.NamespaceURI + "'");
		handledElements.Add(element);
	}
	
	void warnAboutUnhandled(XmlElement element) {
		if( handledElements.Contains(element) ) {
			//warn about unhandled elements in the Desible 1 namespace 
			if( _unhandledWarnLevel == 1 ) {
				foreach( XmlElement child in selectChildren(element) )
					warnAboutUnhandled(child);
			}
			//warn about all unhandled elemnets
			else if( _unhandledWarnLevel == 2 ) {
				foreach( XmlElement child in element.SelectNodes("*") )
					warnAboutUnhandled(child);
			}
		}
		else
			System.Console.WriteLine(
				"WARNING: unhandled element with tag name '{0}' in namespace '{1}'",
				element.LocalName, element.NamespaceURI );
	}
	
	XmlElement selectFirst(XmlElement element, string label) {
		XmlNode node = element.SelectSingleNode(
			"desible1:*[@label='" + label + "']", nsManager);
		if( node == null )
			throw new System.Exception(
				"element with tag name '" +
				element.LocalName +
				"' did not contain element with label '" +
				label +
				"'" );
		return (XmlElement)node;
	}
	
	XmlElement trySelectFirst(XmlElement element, string label) {
		return (XmlElement)element.SelectSingleNode(
			"desible1:*[@label='" + label + "']", nsManager);
	}
	
	XmlNodeList selectAll(XmlElement element, string label) {
		return element.SelectNodes(
			"desible1:*[@label='" + label + "']", nsManager);
	}
	
	XmlNodeList selectChildren(XmlElement element) {
		return element.SelectNodes("desible1:*", nsManager);
	}
	
	delegate T ParseFunc<T>(XmlElement element);
	
	IList<T> parseAll<T>( XmlElement element, string label, ParseFunc<T> func ) {
		IList<T> rv = new List<T>();
		foreach( XmlElement child in selectAll(element, label) ) {
			rv.Add( func(child) );
		}
		return rv;
	}

	T tryParse<T>( XmlElement element, string label, ParseFunc<T> func ) {
		XmlElement child = trySelectFirst(element, label);
		if( child == null )
			return default(T); //null - google CS0403 for info
		return func(child);
	}

	INode_Expression parseExpression(XmlElement element) {
		switch( element.LocalName ) {
			case "bind":
				return parseBind(element);
			case "class":
				return parseClass(element);
			case "declaration-bind":
				return parseDeclarationBind(element);
			case "function":
				return parseFunction(element);
			case "function-call":
				return parseFunctionCall(element);
			case "get-property":
				return parseGetProperty(element);
			case "identifier":
				return parseIdentifier(element);
			case "integer":
				return parseInteger(element);
			case "method-call":
				return parseMethodCall(element);
			case "string":
				return parseString(element);
			default:
				throw new System.Exception(
					"element with tag name '" +
					element.LocalName +
					"' and label '" +
					element.GetAttribute("label") + "' is not an expression" );
		}
	}
	
	INode_Statement parseStatement(XmlElement element) {
		switch( element.GetAttribute("label") ) {
			case "block" :
				return parseBlock(element);
			case "conditional-block" :
				return parseConditionalBlock(element);
			case "for-range" :
				return parseForRange(element);
			default:
				return parseExpression(element);
		}
	}
	
	INode_DeclarationAny parseDeclarationAny(XmlElement element) {
		switch( element.GetAttribute("label") ) {
			case "declaration" :
				return parseDeclaration(element);
			case "declaration-bind" :
				return parseDeclarationBind(element);
			case "declaration-pervasive" :
				return parseDeclarationPervasive(element);
			case "declaration-const-empty" :
				return parseDeclarationConstEmpty(element);
			default:
				throw new System.Exception();
		}
	}
	
	Node_Access parseAccess(XmlElement element) {
		checkElement(element, "access");
		return new Node_Access(
			(Access)System.Enum.Parse(
				typeof(Access), element.InnerText, true ) );
	}
	
	Node_Bind parseBind(XmlElement element) {
		checkElement(element, "bind");
		return new Node_Bind(
			parseIdentifier(selectFirst(element, "identifier")),
			parseExpression(selectFirst(element, "value")) );
	}
	
	Node_Bool parseBool(XmlElement element) {
		checkElement(element, "bool");
		return new Node_Bool(
			System.Boolean.Parse( element.InnerText ) );
	}

	Node_Block parseBlock(XmlElement element) {
		checkElement(element, "block");
		
		//note the lack of labels
		IList<INode_Statement> statements = new List<INode_Statement>();		
		foreach( XmlElement child in selectChildren(element) ) {
			statements.Add( parseStatement(child) );
		}
		
		return new Node_Block(statements);
	}
	
	Node_Callee parseCallee(XmlElement element) {
		return new Node_Callee(
			parseAll<Node_Parameter>(element, "parameter", parseParameter) );
	}

	Node_Class parseClass(XmlElement element) {
		checkElement(element, "class");

		IList<Node_DeclarationClass> staticDeclarations =
			parseAll<Node_DeclarationClass>(
				element, "static-declaration", parseDeclarationClass);
		
		Node_Block staticConstructor =
			parseBlock(selectFirst(element, "static-constructor"));
		
		IList<Node_Function> staticCallees =
			parseAll<Node_Function>(
				element, "static-callee", parseFunction);
		
		IList<Node_ClassProperty> staticProperties =
			parseAll<Node_ClassProperty>(
				element, "static-property", parseClassProperty);
		
		IList<Node_Function> instanceConstructors =
			parseAll<Node_Function>(
				element, "instance-constructor", parseFunction);
		
		IList<INode_DeclarationAny> instanceDeclarations =
			parseAll<INode_DeclarationAny>(
				element, "instance-declaration", parseDeclarationAny);

		IList<Node_InterfaceImplementation> interfaceImplementations =
			parseAll<Node_InterfaceImplementation>(
				element, "interface-implementation", parseInterfaceImplementation);
		
		return new Node_Class(
			staticDeclarations,
			staticConstructor,
			staticCallees,
			staticProperties,
			instanceConstructors,
			instanceDeclarations,
			interfaceImplementations );
	}
	
	Node_ClassProperty parseClassProperty(XmlElement element) {
		throw new Error_Unimplemented();
	}
	
	Node_ConditionalBlock parseConditionalBlock(XmlElement element) {
		checkElement(element, "conditional-block");		
		return new Node_ConditionalBlock(
			parseExpression(selectFirst(element, "test")),
			parseBlock(selectFirst(element, "action")) );
	}
	
	Node_Declaration parseDeclaration(XmlElement element) {
		throw new Error_Unimplemented();
	}
	
	Node_DeclarationClass parseDeclarationClass(XmlElement element) {
		throw new Error_Unimplemented();
	}
	
	Node_DeclarationBind parseDeclarationBind(XmlElement element) {
		checkElement(element, "declaration-bind");
		return new Node_DeclarationBind(
			parseIdentifier(selectFirst(element, "name")),
			parseReferenceType(selectFirst(element, "type")),
			tryParse<Node_Bool>(element, "constant", parseBool),
			parseExpression(selectFirst(element, "value")) );
	}
	
	Node_DeclarationPervasive parseDeclarationPervasive(XmlElement element) {
		checkElement(element, "declaration-pervasive");
		return new Node_DeclarationPervasive(
			parseIdentifier(selectFirst(element, "identifier")),
			parseReferenceType(selectFirst(element, "reference-type")),
			parseExpression(selectFirst(element, "value")) );
	}
	
	Node_DeclarationConstEmpty parseDeclarationConstEmpty(XmlElement element) {
		throw new Error_Unimplemented();
	}
	
	Node_ForRange parseForRange(XmlElement element) {
		checkElement(element, "for-range");		
		return new Node_ForRange(
			parseIdentifier(selectFirst(element, "identifier")),
			parseExpression(selectFirst(element, "start")),
			parseExpression(selectFirst(element, "limit")),
			parseBlock(selectFirst(element, "block")) );
	}
	
	Node_Function parseFunction(XmlElement element) {
		checkElement(element, "function");
		return new Node_Function(
			parseAll<Node_Parameter>(element, "parameter", parseParameter),
			tryParse<Node_ReferenceType>(element, "return-type", parseReferenceType),
			parseBlock(selectFirst(element, "block")) );
	}
	
	Node_FunctionCall parseFunctionCall(XmlElement element) {
		checkElement(element, "function-call");
		return new Node_FunctionCall(
			parseExpression(selectFirst(element, "function")),
			parseAll<INode_Expression>(element, "argument", parseExpression) );
	}
	
	Node_GetProperty parseGetProperty(XmlElement element) {
		checkElement(element, "get-property");
		return new Node_GetProperty(
			parseExpression(selectFirst(element, "value")),
			parseIdentifier(selectFirst(element, "property-name")) );
	}
	
	Node_Global parseGlobal(XmlElement element) {
		checkElement(element, "global");
		
		//note the lack of labels
		IList<Node_DeclarationPervasive> binds = new List<Node_DeclarationPervasive>();
		foreach( XmlElement child in selectChildren(element) ) {
			binds.Add( parseDeclarationPervasive(child) );
		}
	
		return new Node_Global(binds);
	}

	Node_Identifier parseIdentifier(XmlElement element) {
		checkElement(element, "identifier");
		return new Node_Identifier(element.InnerText);
	}
	
	Node_Integer parseInteger(XmlElement element) {
		checkElement(element, "integer");
		return new Node_Integer( System.Int64.Parse(element.InnerText) );
	}
	
	public Node_Interface parseInterface(XmlElement element) {
		return new Node_Interface(
			parseAll<INode_Expression>(element, "inheritee", parseExpression),
			parseAll<Node_Callee>(element, "callee", parseCallee),
			tryParse<Node_ReferenceType>(element, "return-type", parseReferenceType),
			parseAll<Node_Property>(element, "property", parseProperty),
			parseAll<Node_Method>(element, "method", parseMethod) );
	}
	
	Node_InterfaceImplementation parseInterfaceImplementation(XmlElement element) {
		throw new Error_Unimplemented();
	}
	
	Node_Method parseMethod(XmlElement element) {
		checkElement(element, "method");
		return new Node_Method(
			parseIdentifier(selectFirst(element, "name")),
			parseExpression(selectFirst(element, "interface")) );
	}
	
	Node_MethodCall parseMethodCall(XmlElement element) {
		checkElement(element, "method-call");
		return new Node_MethodCall(
			parseExpression(selectFirst(element, "value")),
			parseIdentifier(selectFirst(element, "method-name")),
			parseAll<INode_Expression>(element, "argument", parseExpression) );
			//xxx parseAll<Node_LabeledArgument>(element, ...)
	}
	
	Node_Parameter parseParameter(XmlElement element) {
		checkElement(element, "parameter");
		return new Node_Parameter(
			parseIdentifier(selectFirst(element, "name")),
			null, //xxx parseInterface(selectFirst(element, "interface")),
			parseExpression(selectFirst(element, "default-value")) );
	}

	Node_Property parseProperty(XmlElement element) {
		checkElement(element, "property");
		return new Node_Property(
			parseIdentifier(selectFirst(element, "name")),
			parseReferenceType(selectFirst(element, "type")),
			parseAccess(selectFirst(element, "access"))	);
	}
	
	Node_String parseString(XmlElement element) {
		checkElement(element, "string");		
		return new Node_String(element.InnerText);
	}
	
	Node_ReferenceCategory parseReferenceCategory(XmlElement element) {
		checkElement(element, "reference-category");
		return new Node_ReferenceCategory(
			(ReferenceCategory)System.Enum.Parse(
				typeof(ReferenceCategory), element.InnerText, true ) );
	}
	
	Node_ReferenceType parseReferenceType(XmlElement element) {
		checkElement(element, "reference-type");
		return new Node_ReferenceType(
			parseReferenceCategory(selectFirst(element, "reference-category")),
			tryParse<INode_Expression>(element, "interface", parseExpression) );			
	}
}
