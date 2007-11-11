using System.Collections.Generic;
using System.Xml;

/*
xxx
rename parse functions to e.g.
parseOpt ?
parseOne
parseMult +
parseMultOpt *
*/

/*
xxx

map
	class type -> object

cast object as ParseFunction<class-type>, then call

parseFoo is replaced with
map.Add( Node_Foo, delegate(XmlElement){...} )
*/

/* xxx - diff between parse tree funcs
Node_Foo
"foo"
"label", Node_Bar, amount
*/

class DesibleParser {
	public static IInterface extractInterface(XmlElement element) {
		DesibleParser parser = new DesibleParser();
		parser.setupParser(element.OwnerDocument);
		parser.unhandledWarnLevel = 0;
		//xxx set labels?
		Scope scope = new Scope();
		return parser.parseInterface(element).evaluateInterface(scope);
	}

	string desible1NS = "urn:desible1";
	XmlNamespaceManager nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> handledElements;
	int _unhandledWarnLevel = 2; //2 outputs lots, 0 outputs none
	
	class UnexpectedElementError : System.ApplicationException {
		public UnexpectedElementError(string expectedTag, string actualTag)
		: base( "expected tag name '" + expectedTag + "' but found '" + actualTag + "'" ) {}
	}
	
	public int unhandledWarnLevel {
		get { return _unhandledWarnLevel; }
		set {
			if(value == 0 || value == 1 || value == 2 )
				_unhandledWarnLevel = value;
			else
				throw new System.Exception("bad value");
		}
	}
	
	public Node_Bundle parsePath(string path) {
		XmlDocument doc = new XmlDocument();
		doc.Load(path);
		return parse(doc);
	}
	
	public Node_Bundle parseMarkup(string markup) {
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(markup);
		return parse(doc);
	}

	public Node_Bundle parse(XmlDocument doc) {
		setupParser(doc);
		//xxx check XML with Relax NG
		setLabels(doc.DocumentElement);
		Node_Bundle bundle = parseBundle(doc.DocumentElement);
		warnAboutUnhandled(doc.DocumentElement);
		return bundle;
	}
	
	void setupParser(XmlDocument doc) {
		nsManager = new XmlNamespaceManager(doc.NameTable);
		nsManager.AddNamespace("desible1", desible1NS);
		handledElements = new List<XmlElement>();
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
			throw new UnexpectedElementError(expectedTagName, element.LocalName);
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

	T parseFirst<T>( XmlElement element, string label, ParseFunc<T> func ) {
		XmlElement child = selectFirst(element, label);
		return func(child);
	}

	T tryParse<T>( XmlElement element, string label, ParseFunc<T> func ) {
		XmlElement child = trySelectFirst(element, label);
		if( child == null )
			return default(T); //null - google CS0403 for info
		return func(child);
	}
	
	IList<T> parseAll<T>( XmlElement element, string label, ParseFunc<T> func ) {
		IList<T> rv = new List<T>();
		foreach( XmlElement child in selectAll(element, label) ) {
			rv.Add( func(child) );
		}
		return rv;
	}
	
	//note the lack of labels
	IList<T> parseChildren<T>( XmlElement element, ParseFunc<T> func ) {	
		IList<T> rv = new List<T>();		
		foreach( XmlElement child in selectChildren(element) ) {
			rv.Add( func(child) );
		}
		return rv;
	}


	//----- node supertypes

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
	
	
	//----- terminal node types
	
	Node_Access parseAccess(XmlElement element) {
		checkElement(element, "access");
		return new Node_Access(
			(Access)System.Enum.Parse(
				typeof(Access), element.InnerText, true ) );
	}
	
	Node_Bool parseBool(XmlElement element) {
		checkElement(element, "bool");
		return new Node_Bool(
			System.Boolean.Parse( element.InnerText ) );
	}

	Node_Identifier parseIdentifier(XmlElement element) {
		checkElement(element, "identifier");
		return new Node_Identifier(element.InnerText);
	}
	
	Node_Integer parseInteger(XmlElement element) {
		checkElement(element, "integer");
		//xxx bignum
		return new Node_Integer( System.Int64.Parse(element.InnerText) );
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
	
	
	//----- composite node types
	
	Node_Bind parseBind(XmlElement element) {
		checkElement(element, "bind");
		return new Node_Bind(
			parseFirst<Node_Identifier>(element, "identifier", parseIdentifier),
			parseFirst<INode_Expression>(element, "value", parseExpression));
	}

	Node_Block parseBlock(XmlElement element) {
		checkElement(element, "block");
		return new Node_Block(
			parseChildren<INode_Statement>(element, parseStatement));
	}

	Node_Bundle parseBundle(XmlElement element) {
		checkElement(element, "bundle");
		IList<Node_Plane> inlinePlanes =
			parseAll<Node_Plane>(element, "inline-plane", parseInlinePlane);
		/* xxx
		IList<Node_Plane> otherPlanes =
			parseAll<Node_Plane>(element, "plane-reference", parsePlaneReference);
		IList<Node_Plane> allPlanes = inlinePlanes.Concat(otherPlanes);
		*/
		return new Node_Bundle(inlinePlanes);
	}
	
	Node_Callee parseCallee(XmlElement element) {
		checkElement(element, "callee");
		return new Node_Callee(
			parseAll<Node_Parameter>(element, "parameter", parseParameter) );
	}

	Node_Class parseClass(XmlElement element) {
		checkElement(element, "class");
		return new Node_Class(
			parseAll<Node_DeclarationClass>(element, "static-declaration", parseDeclarationClass),
			tryParse<Node_Block>(element, "static-constructor", parseBlock),
			parseAll<Node_Function>(element, "static-callee", parseFunction),
			parseAll<Node_ClassProperty>(element, "static-property", parseClassProperty),
			parseAll<Node_Function>(element, "instance-constructor", parseFunction),
			parseAll<INode_DeclarationAny>(element, "instance-declaration", parseDeclarationAny),
			parseAll<Node_InterfaceImplementation>(element, "interface-implementation", parseInterfaceImplementation));
	}
	
	Node_ClassProperty parseClassProperty(XmlElement element) {
		checkElement(element, "class-property");
		throw new Error_Unimplemented();
	}
	
	Node_ConditionalBlock parseConditionalBlock(XmlElement element) {
		checkElement(element, "conditional-block");		
		return new Node_ConditionalBlock(
			parseExpression(selectFirst(element, "test")),
			parseFirst<Node_Block>(element, "action", parseBlock));
	}
	
	Node_Declaration parseDeclaration(XmlElement element) {
		checkElement(element, "declaration");
		throw new Error_Unimplemented();
	}
	
	Node_DeclarationClass parseDeclarationClass(XmlElement element) {
		checkElement(element, "declaration-class");
		throw new Error_Unimplemented();
	}
	
	Node_DeclarationBind parseDeclarationBind(XmlElement element) {
		checkElement(element, "declaration-bind");
		return new Node_DeclarationBind(
			parseFirst<Node_Identifier>(element, "name", parseIdentifier),
			parseFirst<Node_ReferenceType>(element, "type", parseReferenceType),
			tryParse<Node_Bool>(element, "constant", parseBool),
			parseFirst<INode_Expression>(element, "value", parseExpression));
	}
	
	Node_DeclarationPervasive parseDeclarationPervasive(XmlElement element) {
		checkElement(element, "declaration-pervasive");
		return new Node_DeclarationPervasive(
			parseFirst<Node_Identifier>(element, "identifier", parseIdentifier),
			parseFirst<Node_ReferenceType>(element, "reference-type", parseReferenceType),
			parseFirst<INode_Expression>(element, "value", parseExpression));
	}
	
	Node_DeclarationConstEmpty parseDeclarationConstEmpty(XmlElement element) {
		checkElement(element, "declaration-const-empty");
		throw new Error_Unimplemented();
	}
	
	Node_ForRange parseForRange(XmlElement element) {
		checkElement(element, "for-range");		
		return new Node_ForRange(
			parseFirst<Node_Identifier>(element, "identifier", parseIdentifier),
			parseFirst<INode_Expression>(element, "start", parseExpression),
			parseFirst<INode_Expression>(element, "limit", parseExpression),
			parseFirst<Node_Block>(element, "block", parseBlock));
	}
	
	Node_Function parseFunction(XmlElement element) {
		checkElement(element, "function");
		return new Node_Function(
			parseAll<Node_Parameter>(element, "parameter", parseParameter),
			tryParse<Node_ReferenceType>(element, "return-type", parseReferenceType),
			parseFirst<Node_Block>(element, "block", parseBlock));
	}
	
	Node_FunctionCall parseFunctionCall(XmlElement element) {
		checkElement(element, "function-call");
		return new Node_FunctionCall(
			parseFirst<INode_Expression>(element, "function", parseExpression),
			parseAll<INode_Expression>(element, "argument", parseExpression) );
	}
	
	Node_GetProperty parseGetProperty(XmlElement element) {
		checkElement(element, "get-property");
		return new Node_GetProperty(
			parseFirst<INode_Expression>(element, "value", parseExpression),
			parseFirst<Node_Identifier>(element, "property-name", parseIdentifier));
	}

	Node_Plane parseInlinePlane(XmlElement element) {
		checkElement(element, "inline-plane");
		return new Node_Plane(
			parseChildren<Node_DeclarationPervasive>(element, parseDeclarationPervasive));
	}
	
	Node_Interface parseInterface(XmlElement element) {
		checkElement(element, "interface");
		return new Node_Interface(
			parseAll<INode_Expression>(element, "inheritee", parseExpression),
			parseAll<Node_Callee>(element, "callee", parseCallee),
			tryParse<Node_ReferenceType>(element, "return-type", parseReferenceType),
			parseAll<Node_Property>(element, "property", parseProperty),
			parseAll<Node_Method>(element, "method", parseMethod) );
	}
	
	Node_InterfaceImplementation parseInterfaceImplementation(XmlElement element) {
		checkElement(element, "interface-implementation");
		throw new Error_Unimplemented();
	}
	
	Node_Method parseMethod(XmlElement element) {
		checkElement(element, "method");
		return new Node_Method(
			parseFirst<Node_Identifier>(element, "name", parseIdentifier),
			parseFirst<INode_Expression>(element, "interface", parseExpression));
	}
	
	Node_MethodCall parseMethodCall(XmlElement element) {
		checkElement(element, "method-call");
		return new Node_MethodCall(
			parseFirst<INode_Expression>(element, "value", parseExpression),
			parseFirst<Node_Identifier>(element, "method-name", parseIdentifier),
			parseAll<INode_Expression>(element, "argument", parseExpression));
	}
	
	Node_Parameter parseParameter(XmlElement element) {
		checkElement(element, "parameter");
		return new Node_Parameter(
			parseFirst<Node_Identifier>(element, "name", parseIdentifier),
			parseFirst<Node_ReferenceType>(element, "type", parseReferenceType),
			parseFirst<INode_Expression>(element, "default-value", parseExpression),
			parseFirst<Node_Bool>(element, "nullable", parseBool));
	}

	Node_Property parseProperty(XmlElement element) {
		checkElement(element, "property");
		return new Node_Property(
			parseFirst<Node_Identifier>(element, "name", parseIdentifier),
			parseFirst<Node_ReferenceType>(element, "type", parseReferenceType),
			parseFirst<Node_Access>(element, "access", parseAccess));
	}
	
	Node_ReferenceType parseReferenceType(XmlElement element) {
		checkElement(element, "reference-type");
		return new Node_ReferenceType(
			parseFirst<Node_ReferenceCategory>(element, "reference-category", parseReferenceCategory),
			tryParse<INode_Expression>(element, "interface", parseExpression) );			
	}
}
