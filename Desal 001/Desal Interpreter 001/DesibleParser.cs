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
		return Evaluator.evaluate(
			parser.parse<Node_Interface>(element),
			new Scope(bridge) );
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
		
		addParser<INode_Declaration>(delegate(XmlElement element) {
			return parseSuper<INode_Declaration>(element, "declaration");
		});
		
		addParser<INode_ScopeAlteration>(delegate(XmlElement element) {
			return parseSuper<INode_ScopeAlteration>(element, "scope-alteration");
		});
		
		
		//----- base
		
		addParser<Node_Access>("access", delegate(XmlElement element) {
			return new Node_Access(parseEnum<Access>(element));
		});
				
		addParser<Node_Boolean>("boolean", delegate(XmlElement element) {
			return new Node_Boolean(System.Boolean.Parse(element.InnerText));
		});
		
		addParser<Node_Direction>("direction", delegate(XmlElement element) {
			return new Node_Direction(parseEnum<Direction>(element));
		});
		
		addParser<Node_Identifier>("identifier", delegate(XmlElement element) {
			return new Node_Identifier(new Identifier(element.InnerText));
		});
		
		addParser<Node_IdentikeyCategory>("identikey-category", delegate(XmlElement element) {
			return new Node_IdentikeyCategory(parseEnum<IdentikeyCategory>(element));
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

		addTreeParsers();
	
		addParser<Node_Bundle>("bundle", delegate(XmlElement element) {
			IList<Node_Plane> planes = parseMult<Node_Plane>(element, "inline-plane");
			foreach( Node_Plane plane in parseMult<Node_Plane>(element, "plane-reference") )
				planes.Add(plane);
			return new Node_Bundle(
				parseMult<Node_Import>(element, "import"),
				parseMult<INode_ScopeAlteration>(element, "scope-alteration"),
				planes );
		});

		addParser<Node_Plane>(delegate(XmlElement element) {
			if( element.LocalName == "inline-plane" ) {
				_handledElements.Add(element);
				return new Node_Plane(
					parseMult<INode_ScopeAlteration>(element, "scope-alteration"),
					parseMult<Node_DeclareFirst>(element, "declare-first"));
			}
			else {
				throw new System.Exception();
			}
		});
		_tagToType.Add("inline-plane", typeof(Node_Plane));
		_tagToType.Add("plane-reference", typeof(Node_Plane));
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
	
	T parseEnum<T>(XmlElement element) {
		return (T)System.Enum.Parse(typeof(T), element.InnerText, true);
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
