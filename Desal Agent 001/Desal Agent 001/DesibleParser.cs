using System;
using System.Collections.Generic;
using System.Xml;

abstract class DesibleParserBase {
	protected delegate INode ParseFunc(XmlElement element);
	
	protected abstract void addParser<T>(string tagName, ParseFunc func);
	protected abstract T parseOne<T>(XmlElement element, string label, string tagName);
	protected abstract T parseOpt<T>(XmlElement element, string label, string tagName);
	protected abstract IList<T> parseMult<T>(XmlElement element, string label, string tagName);
}

class DesibleParser : DesibleParserAuto {
	//----- static

	const string desible1NS = "urn:desible1";

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
		Node_Bundle bundle = parser.parse<Node_Bundle>(root);
		if( warnUnhandled )
			parser.warnAboutUnhandled(root, warnAllNS);
		return bundle;
	}

	//----- instance

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
		//xxx generate these automatically

		addParser<INode_Expression>(delegate(XmlElement element) {
			return parseSuper<INode_Expression>(element, "expression");
		});
		
		addParser<INode_Declaration>(delegate(XmlElement element) {
			return parseSuper<INode_Declaration>(element, "declaration");
		});
		
		addParser<INode_ScopeAlteration>(delegate(XmlElement element) {
			return parseSuper<INode_ScopeAlteration>(element, "scope-alteration");
		});
		
		addParser<INode_InterfaceMember>(delegate(XmlElement element) {
			return parseSuper<INode_InterfaceMember>(element, "interface-member");
		});
		
		
		//----- base
				
		addParser<Node_Boolean>("boolean", delegate(XmlElement element) {
			return new Node_Boolean(
				Boolean.Parse(element.InnerText));
		});
		
		addParser<Node_Direction>("direction", delegate(XmlElement element) {
			return new Node_Direction(
				G.parseEnum<Direction>(element.InnerText));
		});
		
		addParser<Node_Identifier>("identifier", delegate(XmlElement element) {
			return new Node_Identifier(
				new Identifier(element.InnerText));
		});
		
		addParser<Node_IdentikeyCategory>("identikey-category", delegate(XmlElement element) {
			return new Node_IdentikeyCategory(
				G.parseEnum<IdentikeyCategory>(element.InnerText));
		});
		
		addParser<Node_Integer>("integer", delegate(XmlElement element) {
			//xxx use bignum
			return new Node_Integer(
				Int64.Parse(element.InnerText));
		});

		addParser<Node_Rational>("rational", delegate(XmlElement element) {
			return new Node_Rational(
				Double.Parse(element.InnerText) );
		});
		
		addParser<Node_String>("string", delegate(XmlElement element) {		
			return new Node_String(element.InnerText);
		});
		

		//----- tree

		addTreeParsers();
		
		//xxx need better way to do this
		_tagToType.Remove("bundle");
		_tagToType.Remove("plane");
		_parseFuncs.Remove(typeof(Node_Bundle));
		_parseFuncs.Remove(typeof(Node_Plane));
	
		addParser<Node_Bundle>("bundle", delegate(XmlElement element) {
			IList<Node_Plane> planes = parseMult<Node_Plane>(element, "inline-plane", null);
			foreach( Node_Plane plane in parseMult<Node_Plane>(element, "plane-reference", null) )
				planes.Add(plane);
			return new Node_Bundle(
				parseMult<Node_Import>(element, "import", null),
				parseMult<INode_ScopeAlteration>(element, "*", "alt"),
				planes );
		});

		addParser<Node_Plane>(delegate(XmlElement element) {
			if( element.LocalName == "inline-plane" ) {
				_handledElements.Add(element);
				return new Node_Plane(
					parseMult<INode_ScopeAlteration>(element, "*", "alt"),
					parseMult<Node_DeclareFirst>(element, "declare-first", null));
			}
			else {
				throw new NotImplementedException("only inline-plane planes are supported for now");
			}
		});
		_tagToType.Add("inline-plane", typeof(Node_Plane));
		_tagToType.Add("plane-reference", typeof(Node_Plane));
	}	
	
	void addParser<T>(ParseFunc func) {
		_parseFuncs.Add( typeof(T), func );
	}		

	protected override void addParser<T>(string tagName, ParseFunc func) {
		Type type = typeof(T);	
		_tagToType.Add( tagName, type );
		_parseFuncs.Add(
			type,
			new ParseFunc(delegate(XmlElement element) {
				checkElement(element, tagName);
				return func(element);
			}));
	}
	
	void checkElement(XmlElement element, string expectedTagName) {
		if( element.LocalName != expectedTagName ) {
			throw new ParseError(
				String.Format(
					"expected tag name '{0}' but found '{1}'",
					expectedTagName, element.LocalName));
		}
		if( element.NamespaceURI != desible1NS ) {
			throw new ParseError(
				String.Format(
					"attempted to handle an element in namespace '{0}",
					element.NamespaceURI));
		}
		_handledElements.Add(element);
	}
	
	//if @deep, warn about unhandled descendants of unhandled elements
	void warnAboutUnhandled(XmlElement element, bool warnAllNS) {
		if( _handledElements.Contains(element) ) {
			if( warnAllNS )
				foreach( XmlElement child in element.SelectNodes("*") )
					warnAboutUnhandled(child, warnAllNS);
			else
				foreach( XmlElement child in element.SelectNodes("desible1:*", _nsManager) )
					warnAboutUnhandled(child, warnAllNS);
		}
		else {
			_bridge.printlnWarning(
				String.Format(
					"unhandled element with tag name '{0}' in namespace '{1}'\n{2}",
					element.LocalName, element.NamespaceURI, element.OuterXml ) );
		}
	}

	ParseFunc getParseFunc<T>() {
		Type type = typeof(T);
		
		if( ! _parseFuncs.ContainsKey(type) ) {
			throw new ParseError(
				String.Format(
					"no parser defined for type '{0}'",
					type.FullName));
		}
		
		return _parseFuncs[type];
	}
	
	
	//----- select
	
	string createXpathString(string tagName, string label) {
		return ( label == null ?
			"desible1:"+tagName+"[not(@label)]" :
			"desible1:"+tagName+"[@label='"+label+"']" );
	}
	
	XmlElement trySelectFirst(XmlElement element, string tagName, string label) {
		return (XmlElement)element.SelectSingleNode(
			createXpathString(tagName, label), _nsManager );
	}
	
	XmlElement selectFirst(XmlElement element, string tagName, string label) {
		XmlElement child = trySelectFirst(element, tagName, label);
		if( child == null ) {
			throw new ParseError(
				String.Format(
					"'{0}' element did not contain '{1}' element with '{2}' label",
					element.LocalName, tagName, label ));
		}
		return child;
	}
	
	XmlNodeList selectAll(XmlElement element, string tagName, string label) {
		return element.SelectNodes(
			createXpathString(tagName, label), _nsManager );
	}


	//----- parse
	
	//parse first child with label
	protected override T parseOne<T>(XmlElement element, string label, string tagName) {
		return (T)getParseFunc<T>()( selectFirst(element, label, tagName) );
	}

	//parse first child with label, if such a child exists
	protected override T parseOpt<T>(XmlElement element, string label, string tagName) {
		XmlElement child = trySelectFirst(element, label, tagName);
		if( child == null )
			return default(T); //null - google CS0403 for info
		return (T)getParseFunc<T>()( child );
	}

	//parse all children with label
	protected override IList<T> parseMult<T>(XmlElement element, string label, string tagName) {
		ParseFunc func = getParseFunc<T>();
		IList<T> rv = new List<T>();
		foreach( XmlElement child in selectAll(element, label, tagName) ) {
			rv.Add( (T)func(child) );
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
			throw new ParseError(
				String.Format(
					"element with tag name '{0}' and label '{1}' is not an {2}",
					tagName, element.GetAttribute("label"), typeName ));
		}
	}
	
	public T parse<T>(XmlElement element) {
		return (T)_parseFuncs[typeof(T)](element);
	}
}

