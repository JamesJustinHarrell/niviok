using System;
using System.Collections.Generic;
using System.Xml;

class DesibleParser {

	//----- static
	
	class UnexpectedElementError : System.ApplicationException {
		public UnexpectedElementError(string expectedTag, string actualTag)
		: base( System.String.Format(
			"expected tag name '{0}' but found '{1}'", expectedTag, actualTag ))
		{}
	}

	delegate INode ParseFunc(XmlElement element);
	const string desible1NS = "urn:desible1";

	//xxx messy
	public static IInterface extractInterface(Bridge aBridge, XmlElement element) {
		DesibleParser parser = new DesibleParser();
		parser.setupParser(aBridge, element.OwnerDocument);
		parser.unhandledWarnLevel = 0;
		parser.setLabels(element);
		return parser
			.parse<Node_Interface>(element)
			.evaluateInterface(new Scope());
	}
	
	
	//----- instance
	
	Bridge _bridge; //used for outputing warnings
	XmlNamespaceManager _nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> _handledElements;
	int _unhandledWarnLevel = 2; //2 outputs lots, 0 outputs none
	IDictionary<string, Type> _tagToType;
	IDictionary<Type, ParseFunc> _parseFuncs;
	
	public int unhandledWarnLevel {
		get { return _unhandledWarnLevel; }
		set {
			//xxx magic numbers
			if( 0 <= value && value <= 2 )
				_unhandledWarnLevel = value;
			else
				throw new System.Exception("bad value");
		}
	}
	
	public Node_Bundle parsePath(Bridge aBridge, string path) {
		XmlDocument doc = new XmlDocument();
		doc.Load(path);
		return parseDocument(aBridge, doc);
	}
	
	public Node_Bundle parseMarkup(Bridge aBridge, string markup) {
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(markup);
		return parseDocument(aBridge, doc);
	}

	public Node_Bundle parseDocument(Bridge aBridge, XmlDocument doc) {
		setupParser(aBridge, doc);
		//xxx check XML with Relax NG
		setLabels(doc.DocumentElement);
		Node_Bundle bundle = parse<Node_Bundle>(doc.DocumentElement);
		warnAboutUnhandled(doc.DocumentElement);
		return bundle;
	}
	
	void setupParser(Bridge aBridge, XmlDocument doc) {
		_bridge = aBridge;
		_nsManager = new XmlNamespaceManager(doc.NameTable);
		_nsManager.AddNamespace("desible1", desible1NS);
		_handledElements = new List<XmlElement>();
		_tagToType = new Dictionary<string, Type>();
		_parseFuncs = new Dictionary<Type, ParseFunc>();		
		
		
		//----- super/family
		//These don't declare a tag name.

		addParser<INode_Expression>(delegate(XmlElement element) {
			return parseSuper<INode_Expression>(element, "expression");
		});
		
		addParser<INode_Statement>(delegate(XmlElement element) {
			return parseSuper<INode_Statement>(element, "statement");
		});
		
		addParser<INode_DeclarationAny>(delegate(XmlElement element) {
			return parseSuper<INode_DeclarationAny>(element, "declaration-any");
		});
		
		
		//----- base
		
		addParser<Node_Access>("access", delegate(XmlElement element) {
			return new Node_Access(
				(Access)System.Enum.Parse(
					typeof(Access), element.InnerText, true ) );
		});
				
		addParser<Node_Bool>("bool", delegate(XmlElement element) {
			return new Node_Bool(
				System.Boolean.Parse( element.InnerText ) );
		});
		
		addParser<Node_Identifier>("identifier", delegate(XmlElement element) {
			return new Node_Identifier(_bridge, element.InnerText);
		});
		
		addParser<Node_Integer>("integer", delegate(XmlElement element) {
			//xxx use bignum
			return new Node_Integer( _bridge, System.Int64.Parse(element.InnerText) );
		});
		
		addParser<Node_String>("string", delegate(XmlElement element) {		
			return new Node_String(_bridge, element.InnerText);
		});
		
		addParser<Node_ReferenceCategory>("reference-category", delegate(XmlElement element) {
			return new Node_ReferenceCategory(
				(ReferenceCategory)System.Enum.Parse(
					typeof(ReferenceCategory), element.InnerText, true ) );
		});
			
		
		//----- tree
		
		addParser<Node_Bind>("bind", delegate(XmlElement element) {
			return new Node_Bind(
				parseOne<Node_Identifier>(element, "identifier"),
				parseOne<INode_Expression>(element, "value"));
		});
		
		addParser<Node_Block>("block", delegate(XmlElement element) {
			return new Node_Block(
				parseChildren<INode_Statement>(element));
		});
	
		addParser<Node_Bundle>("bundle", delegate(XmlElement element) {
			IList<Node_Plane> inlinePlanes =
				parseMult<Node_Plane>(element, "inline-plane");
			/* xxx
			IList<Node_Plane> otherPlanes =
				parseMult<Node_Plane>(element, "plane-reference", parsePlaneReference);
			IList<Node_Plane> allPlanes = inlinePlanes.Concat(otherPlanes);
			*/
			return new Node_Bundle(inlinePlanes);
		});
	
		addParser<Node_Callee>("callee", delegate(XmlElement element) {
			return new Node_Callee(
				parseMult<Node_Parameter>(element, "parameter") );
		});
	
		addParser<Node_Class>("class", delegate(XmlElement element) {
			return new Node_Class(
				parseMult<Node_DeclarationClass>(element, "static-declaration"),
				parseOpt<Node_Block>(element, "static-constructor"),
				parseMult<Node_Function>(element, "static-callee"),
				parseMult<Node_ClassProperty>(element, "static-property"),
				parseMult<Node_Function>(element, "instance-constructor"),
				parseMult<INode_DeclarationAny>(element, "instance-declaration"),
				parseMult<Node_InterfaceImplementation>(element, "interface-implementation") );
		});


		addParser<Node_ClassProperty>("class-property", delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_ConditionalBlock>("conditional-block", delegate(XmlElement element) {	
			return new Node_ConditionalBlock(
				parseOne<INode_Expression>(element, "test"),
				parseOne<Node_Block>(element, "action") );
		});
		
		addParser<Node_Declaration>("declaration", delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_DeclarationBind>("declaration-bind", delegate(XmlElement element) {
			return new Node_DeclarationBind(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_ReferenceType>(element, "type"),
				parseOpt<Node_Bool>(element, "constant"),
				parseOne<INode_Expression>(element, "value") );
		});
		
		addParser<Node_DeclarationClass>("declaration-class",
		delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_DeclarationConstEmpty>("declaration-const-empty",
		delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_DeclarationPervasive>("declaration-pervasive",
		delegate(XmlElement element) {
			return new Node_DeclarationPervasive(
				parseOne<Node_Identifier>(element, "identifier"),
				parseOne<Node_ReferenceType>(element, "reference-type"),
				parseOne<INode_Expression>(element, "value") );
		});
		
		addParser<Node_ForRange>("for-range", delegate(XmlElement element) {	
			return new Node_ForRange(
				parseOne<Node_Identifier>(element, "identifier"),
				parseOne<INode_Expression>(element, "start"),
				parseOne<INode_Expression>(element, "limit"),
				parseOne<Node_Block>(element, "block") );
		});
		
		addParser<Node_Function>("function", delegate(XmlElement element) {;
			return new Node_Function(
				_bridge,
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_ReferenceType>(element, "return-type"),
				parseOne<Node_Block>(element, "block") );
		});
		
		addParser<Node_FunctionCall>("function-call", delegate(XmlElement element) {
			return new Node_FunctionCall(
				parseOne<INode_Expression>(element, "function"),
				parseMult<INode_Expression>(element, "argument") );
		});
		
		addParser<Node_FunctionInterface>("function-interface",
		delegate(XmlElement element) {
			return new Node_FunctionInterface(
				_bridge,
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_ReferenceType>(element, "return-type"));
		});
		
		addParser<Node_GetProperty>("get-property", delegate(XmlElement element) {
			return new Node_GetProperty(
				parseOne<INode_Expression>(element, "value"),
				parseOne<Node_Identifier>(element, "property-name") );
		});
		
		//xxx how will this exist along with plane-reference?
		addParser<Node_Plane>("inline-plane", delegate(XmlElement element) {
			return new Node_Plane(
				parseChildren<Node_DeclarationPervasive>(element));
		});
		
		addParser<Node_Interface>("interface", delegate(XmlElement element) {
			return new Node_Interface(
				parseMult<INode_Expression>(element, "inheritee"),
				parseMult<Node_Callee>(element, "callee"),
				parseOpt<Node_ReferenceType>(element, "return-type"),
				parseMult<Node_Property>(element, "property"),
				parseMult<Node_Method>(element, "method") );
		});
		
		addParser<Node_InterfaceImplementation>("interface-implementation",
		delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_Method>("method", delegate(XmlElement element) {
			return new Node_Method(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<INode_Expression>(element, "interface") );
		});
		
		addParser<Node_MethodCall>("method-call", delegate(XmlElement element) {
			return new Node_MethodCall(
				parseOne<INode_Expression>(element, "value"),
				parseOne<Node_Identifier>(element, "method-name"),
				parseMult<INode_Expression>(element, "argument") );
		});
		
		addParser<Node_Parameter>("parameter", delegate(XmlElement element) {
			return new Node_Parameter(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_ReferenceType>(element, "type"),
				parseOne<INode_Expression>(element, "default-value"),
				parseOne<Node_Bool>(element, "nullable") );
		});

		addParser<Node_Property>("property", delegate(XmlElement element) {
			return new Node_Property(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_ReferenceType>(element, "type"),
				parseOne<Node_Access>(element, "access") );
		});
		
		addParser<Node_ReferenceType>("reference-type", delegate(XmlElement element) {
			return new Node_ReferenceType(
				parseOne<Node_ReferenceCategory>(element, "reference-category"),
				parseOpt<INode_Expression>(element, "interface") );			
		});
	}

	void addParser<T>(ParseFunc func) {
		_parseFuncs.Add( typeof(T), func );
	}		

	void addParser<T>(string tagName, ParseFunc func) {
		Type type = typeof(T);	
		_tagToType.Add( tagName, type );
		_parseFuncs.Add(
			type,
			new ParseFunc(delegate(XmlElement element) {
				/* xxx
				System.Console.WriteLine("parsing element with tag name " + element.LocalName);
				System.Console.Out.Flush(); */
				checkElement(element, tagName);
				return func(element);
			}));
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
		_handledElements.Add(element);
	}
	
	void warnAboutUnhandled(XmlElement element) {
		if( _handledElements.Contains(element) ) {
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
		else {
			_bridge.warning(
				System.String.Format(
					"unhandled element with tag name '{0}' in namespace '{1}'",
					element.LocalName, element.NamespaceURI ) );
		}
	}

	ParseFunc getParseFunc<T>() {
		Type type = typeof(T);
		
		if( ! _parseFuncs.ContainsKey(type) ) {
			throw new ApplicationException(
				"no parser defined for the specifed type " + type.FullName);
		}
		
		return _parseFuncs[type];
	}
	
	
	//----- select
	
	XmlElement trySelectFirst(XmlElement element, string label) {
		return (XmlElement)element.SelectSingleNode(
			"desible1:*[@label='" + label + "']", _nsManager);
	}
	
	XmlElement selectFirst(XmlElement element, string label) {
		XmlElement child = trySelectFirst(element, label);
		if( child == null ) {
			throw new System.Exception(
				String.Format(
					"element with tag name '{0}' did not contain element with label '{1}'",
					element.LocalName, label ));
		}
		return child;
	}
	
	XmlNodeList selectAll(XmlElement element, string label) {
		return element.SelectNodes(
			"desible1:*[@label='" + label + "']", _nsManager);
	}
	
	XmlNodeList selectChildren(XmlElement element) {
		return element.SelectNodes("desible1:*", _nsManager);
	}


	//----- parse
	
	//parse first child with label
	T parseOne<T>( XmlElement element, string label ) {
		try {
			return (T)getParseFunc<T>()( selectFirst(element, label) );
		}
		catch(Exception e) {
			throw new Exception("inside parseOne", e);
		}
	}

	//parse first child with label, if such a child exists
	T parseOpt<T>( XmlElement element, string label ) {
		XmlElement child = trySelectFirst(element, label);
		if( child == null )
			return default(T); //null - google CS0403 for info
		return (T)getParseFunc<T>()( child );
	}

	//parse all children with label
	IList<T> parseMult<T>( XmlElement element, string label ) {
		ParseFunc func = getParseFunc<T>();
		IList<T> rv = new List<T>();
		foreach( XmlElement child in selectAll(element, label) ) {
			rv.Add( (T)func(child) );
		}
		return rv;
	}
	
	//parse all children
	IList<T> parseChildren<T>( XmlElement element ) {
		ParseFunc parser = _parseFuncs[typeof(T)];	
		IList<T> rv = new List<T>();		
		foreach( XmlElement child in selectChildren(element) ) {
			rv.Add( (T)parser(child) );
		}
		return rv;
	}

	//parse element as a super type
	//should only be used by expression/statement/declaration-any parser delegates
	T parseSuper<T>(XmlElement element, string typeName) {
		string tagName = element.LocalName;
		if( _tagToType.ContainsKey(tagName) ) {
			Type type = _tagToType[tagName];
			ParseFunc parser = _parseFuncs[type];
			return (T)parser(element);
		}
		else {
			throw new ApplicationException( String.Format(
				"element with tag name '{0}' and label '{1}' is not an {2}",
				tagName, element.GetAttribute("label"), typeName ));
		}
	}
	
	T parse<T>(XmlElement element) {
		return (T)_parseFuncs[typeof(T)](element);
	}
}
