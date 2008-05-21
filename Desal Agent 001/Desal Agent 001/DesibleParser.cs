using System;
using System.Collections.Generic;
using System.Xml;

abstract class DesibleParserBase {	
	protected delegate T ParseFunc<T>(XmlElement element);

	protected abstract void checkElement(
		XmlElement element, string expectedTagName);
	protected abstract T parseOne<T>(
		ParseFunc<T> parserFunc, XmlElement element, string label, string tagName);
	protected abstract T parseOpt<T>(
		ParseFunc<T> parserFunc, XmlElement element, string label, string tagName);
	protected abstract IList<T> parseMult<T>(
		ParseFunc<T> parserFunc, XmlElement element, string label, string tagName);
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
		Node_Bundle bundle = parser.parseBundle(root);
		if( warnUnhandled )
			parser.warnAboutUnhandled(root, warnAllNS);
		return bundle;
	}

	//----- instance

	Bridge _bridge; //used for outputing warnings
	XmlNamespaceManager _nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> _handledElements;

	public DesibleParser(Bridge bridge, XmlDocument document) {
		_bridge = bridge;
		_nsManager = new XmlNamespaceManager(document.NameTable);
		_nsManager.AddNamespace("desible1", desible1NS);
		_handledElements = new List<XmlElement>();
	}

	Node_Bundle parseBundle(XmlElement element) {
		checkElement(element, "bundle");
		IList<Node_Plane> planes = parseMult<Node_Plane>(parsePlane, element, "inline-plane", null);
		foreach( Node_Plane plane in parseMult<Node_Plane>(parsePlane, element, "plane-reference", null) )
			planes.Add(plane);
		return new Node_Bundle(
			parseMult<Node_Import>(parseImport, element, "import", null),
			parseMult<INode_ScopeAlteration>(parseScopeAlteration, element, "*", "alt"),
			planes );
	}

	Node_Plane parsePlane(XmlElement element) {
		if( element.LocalName == "inline-plane" ) {
			_handledElements.Add(element);
			return new Node_Plane(
				parseMult<INode_ScopeAlteration>(parseScopeAlteration, element, "*", "alt"),
				parseMult<Node_DeclareFirst>(parseDeclareFirst, element, "declare-first", null));
		}
		else {
			throw new NotImplementedException("only inline-plane planes are supported for now");
		}
	}
	
	protected override void checkElement(XmlElement element, string expectedTagName) {
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
	protected override T parseOne<T>(
	ParseFunc<T> parserFunc, XmlElement element, string label, string tagName) {
		return parserFunc(selectFirst(element, label, tagName));
	}

	//parse first child with label, if such a child exists
	protected override T parseOpt<T>(
	ParseFunc<T> parserFunc, XmlElement element, string label, string tagName) {
		XmlElement child = trySelectFirst(element, label, tagName);
		if( child == null )
			return default(T); //null - google CS0403 for info
		return parserFunc( child );
	}

	//parse all children with label
	protected override IList<T> parseMult<T>(
	ParseFunc<T> parserFunc, XmlElement element, string label, string tagName) {
		IList<T> rv = new List<T>();
		foreach( XmlElement child in selectAll(element, label, tagName) ) {
			rv.Add( parserFunc(child) );
		}
		return rv;
	}
}
