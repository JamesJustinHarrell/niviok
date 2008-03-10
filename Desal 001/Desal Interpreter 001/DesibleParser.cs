using System;
using System.Collections.Generic;
using System.Xml;

//static
partial class DesibleParser {

	class UnexpectedElementError : System.ApplicationException {
		public UnexpectedElementError(string expectedTag, string actualTag)
		: base( System.String.Format(
			"expected tag name '{0}' but found '{1}'", expectedTag, actualTag ))
		{}
	}

	delegate INode ParseFunc(XmlElement element);
	const string desible1NS = "urn:desible1";

	public static IInterface createNativeInterface(Bridge bridge, XmlElement element) {
		DesibleParser parser = new DesibleParser(bridge, element.OwnerDocument);
		parser.setLabels(element);
		return parser
			.parse<Node_Interface>(element)
			.evaluateInterface(new Scope(bridge));
	}

	public static Node_Bundle parseDocument(
	Bridge bridge, string path, bool warnUnhandled, bool warnAllNS) {
		XmlDocument document = new XmlDocument();
		document.Load(path);
		return parseDocument(bridge, document, warnUnhandled, warnAllNS);
	}
	
	public static Node_Bundle parseDocument(
	Bridge bridge, XmlDocument document, bool warnUnhandled, bool warnAllNS) {
		DesibleParser parser = new DesibleParser(bridge, document);
		//xxx check XML with Relax NG
		XmlElement root = document.DocumentElement;
		parser.setLabels(root);
		Node_Bundle bundle = parser.parse<Node_Bundle>(root);
		if( warnUnhandled )
			parser.warnAboutUnhandled(root, warnAllNS);
		return bundle;
	}
}


//instance
partial class DesibleParser {	

	Bridge _bridge; //used for outputing warnings
	XmlNamespaceManager _nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> _handledElements;
	IDictionary<string, Type> _tagToType;
	IDictionary<Type, ParseFunc> _parseFuncs;
	
	public DesibleParser(Bridge bridge, XmlDocument document) {
		_bridge = bridge;
		_nsManager = new XmlNamespaceManager(document.NameTable);
		_nsManager.AddNamespace("desible1", desible1NS);
		_handledElements = new List<XmlElement>();
		_tagToType = new Dictionary<string, Type>();
		_parseFuncs = new Dictionary<Type, ParseFunc>();
		
		
		//----- super/family
		//These don't declare a tag name.

		addParser<INode_Expression>(delegate(XmlElement element) {
			return parseSuper<INode_Expression>(element, "expression");
		});
		
		addParser<INode_DeclareAny>(delegate(XmlElement element) {
			return parseSuper<INode_DeclareAny>(element, "declaration-any");
		});
		
		
		//----- base
		
		addParser<Node_Access>("access", delegate(XmlElement element) {
			return new Node_Access(
				(Access)System.Enum.Parse(
					typeof(Access), element.InnerText, true ) );
		});
				
		addParser<Node_Boolean>("boolean", delegate(XmlElement element) {
			return new Node_Boolean(
				System.Boolean.Parse( element.InnerText ) );
		});
		
		addParser<Node_Identifier>("identifier", delegate(XmlElement element) {
			return new Node_Identifier( new Identifier(element.InnerText) );
		});
		
		addParser<Node_IdentikeyCategory>("identikey-category", delegate(XmlElement element) {
			return new Node_IdentikeyCategory(
				(IdentikeyCategory)System.Enum.Parse(
					typeof(IdentikeyCategory), element.InnerText, true ) );
		});
		
		addParser<Node_Integer>("integer", delegate(XmlElement element) {
			//xxx use bignum
			return new Node_Integer( System.Int64.Parse(element.InnerText) );
		});

		addParser<Node_Rational>("rational", delegate(XmlElement element) {
			return new Node_Rational( System.Double.Parse(element.InnerText) );
		});
		
		addParser<Node_String>("string", delegate(XmlElement element) {		
			return new Node_String(element.InnerText);
		});
		
		
		//----- tree
		
		addParser<Node_ActiveInterface>("active-interface", delegate(XmlElement element) {
			return new Node_ActiveInterface(
				parseOne<INode_Expression>(element, "value"));
		});
		
		addParser<Node_And>("and", delegate(XmlElement element) {
			return new Node_And(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second"));
		});
		
		addParser<Node_Array>("array", delegate(XmlElement element) {
			return new Node_Array(
				parseMult<INode_Expression>(element, "element"));
		});
		
		addParser<Node_Assign>("assign", delegate(XmlElement element) {
			return new Node_Assign(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<INode_Expression>(element, "value"));
		});
		
		addParser<Node_Block>("block", delegate(XmlElement element) {
			return new Node_Block(
				parseChildren<INode_Expression>(element));
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
		
		addParser<Node_Call>("call", delegate(XmlElement element) {
			return new Node_Call(
				parseOne<INode_Expression>(element, "value"),
				parseMult<INode_Expression>(element, "argument") );
		});
	
		addParser<Node_Callee>("callee", delegate(XmlElement element) {
			return new Node_Callee(
				parseMult<Node_Parameter>(element, "parameter") );
		});
	
		addParser<Node_Class>("class", delegate(XmlElement element) {
			return new Node_Class(
				parseMult<Node_DeclareClass>(element, "static-declaration"),
				parseOpt<Node_Block>(element, "static-constructor"),
				parseMult<Node_Function>(element, "static-callee"),
				parseMult<Node_ClassProperty>(element, "static-property"),
				parseMult<Node_Function>(element, "instance-constructor"),
				parseMult<INode_DeclareAny>(element, "instance-declaration"),
				parseMult<Node_InterfaceImplementation>(element, "interface-implementation") );
		});

		addParser<Node_ClassProperty>("class-property", delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_DeclareEmpty>("declare-empty", delegate(XmlElement element) {
			return new Node_DeclareEmpty(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "nullable-type"));
		});
		
		addParser<Node_DeclareAssign>("declare-assign", delegate(XmlElement element) {
			return new Node_DeclareAssign(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "identikey-type"),
				parseOne<INode_Expression>(element, "value") );
		});
		
		addParser<Node_DeclareClass>("declaration-class",
		delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_DeclareConstEmpty>("declaration-const-empty",
		delegate(XmlElement element) {
			throw new Error_Unimplemented();
		});
		
		addParser<Node_DeclareFirst>("declare-first",
		delegate(XmlElement element) {
			return new Node_DeclareFirst(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "identikey-type"),
				parseOne<INode_Expression>(element, "value") );
		});
		
		addParser<Node_ExtractMember>("extract-member", delegate(XmlElement element) {
			return new Node_ExtractMember(
				parseOne<INode_Expression>(element, "source"),
				parseOne<Node_Identifier>(element, "member-name") );
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
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_NullableType>(element, "return-type"),
				parseOne<INode_Expression>(element, "body") );
		});
		
		addParser<Node_FunctionInterface>("function-interface",
		delegate(XmlElement element) {
			return new Node_FunctionInterface(
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_NullableType>(element, "return-type"));
		});
		
		addParser<Node_IdentikeyType>("identikey-type", delegate(XmlElement element) {
			return new Node_IdentikeyType(
				parseOne<Node_IdentikeyCategory>(element, "identikey-category"),
				parseOpt<Node_NullableType>(element, "nullable-type"),
				parseOpt<Node_Boolean>(element, "constant"));
		});			
		
		//xxx how will this exist along with plane-reference?
		addParser<Node_Plane>("inline-plane", delegate(XmlElement element) {
			return new Node_Plane(
				parseChildren<Node_DeclareFirst>(element));
		});
		
		addParser<Node_Interface>("interface", delegate(XmlElement element) {
			return new Node_Interface(
				parseMult<INode_Expression>(element, "inheritee"),
				parseMult<Node_Callee>(element, "callee"),
				parseOpt<Node_NullableType>(element, "return-type"),
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
		
		addParser<Node_NullableType>("nullable-type", delegate(XmlElement element) {
			return new Node_NullableType(
				parseOpt<INode_Expression>(element, "interface"),
				parseOne<Node_Boolean>(element, "nullable"));
		});
		
		addParser<Node_Parameter>("parameter", delegate(XmlElement element) {
			return new Node_Parameter(
				/* xxx direction */
				parseOne<Node_NullableType>(element, "nullable-type"),
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_Boolean>(element, "has-default-value"),
				parseOpt<INode_Expression>(element, "default-value") );
		});
		
		addParser<Node_Possibility>("possibility", delegate(XmlElement element) {	
			return new Node_Possibility(
				parseOne<INode_Expression>(element, "test"),
				parseOne<INode_Expression>(element, "result") );
		});

		addParser<Node_Property>("property", delegate(XmlElement element) {
			return new Node_Property(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_NullableType>(element, "nullable-type"),
				parseOne<Node_Access>(element, "access") );
		});
		
		addParser<Node_Nand>("nand", delegate(XmlElement element) {
			return new Node_Nand(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});
		
		addParser<Node_Nor>("nor", delegate(XmlElement element) {
			return new Node_Nor(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});
		
		addParser<Node_Or>("or", delegate(XmlElement element) {
			return new Node_Or(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});
		
		addParser<Node_Unassign>("unassign", delegate(XmlElement element) {
			return new Node_Unassign(
				parseOne<Node_Identifier>(element, "name") );
		});
		
		addParser<Node_Xnor>("xnor", delegate(XmlElement element) {
			return new Node_Xnor(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});
		
		addParser<Node_Xor>("xor", delegate(XmlElement element) {
			return new Node_Xor(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
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
	
	//instance method because it only works on Desible elements, and
	//the nsManager is needed to only select Desible elements
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
	
	//if @deep, warn about unhandled descendants of unhandled elements
	void warnAboutUnhandled(XmlElement element, bool warnAllNS) {
		if( _handledElements.Contains(element) ) {
			if( warnAllNS )
				foreach( XmlElement child in element.SelectNodes("*") )
					warnAboutUnhandled(child, warnAllNS);
			else
				foreach( XmlElement child in selectChildren(element) )
					warnAboutUnhandled(child, warnAllNS);
		}
		else {
			_bridge.warning(
				System.String.Format(
					"unhandled element with tag name '{0}' in namespace '{1}'\n{2}",
					element.LocalName, element.NamespaceURI, element.OuterXml ) );
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
		try { //try-catch for debugging
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
