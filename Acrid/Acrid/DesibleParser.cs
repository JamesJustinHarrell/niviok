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
	protected abstract string getSource(
		XmlElement element);
}

class DesibleParser : DesibleParserAuto {
	//----- static
	const string desible1NS = "urn:desible1";

	public static Node_Module parseDocument(
	Bridge bridge, string path, bool warnUnhandled, bool warnAllNS) {
		XmlDocument document = new XmlDocument();
		document.Load(path);
		return parseDocument(bridge, document, path, warnUnhandled, warnAllNS);
	}
	
	public static Node_Module parseDocument(
	Bridge bridge, XmlDocument document, string documentSource, bool warnUnhandled, bool warnAllNS) {
		DesibleParser parser = new DesibleParser(bridge, document, documentSource);
		//xxx check XML with Relax NG
		XmlElement root = document.DocumentElement;
		Node_Module bundle = parser.parseModule(root);
		if( warnUnhandled )
			parser.warnAboutUnhandled(root, warnAllNS);
		return bundle;
	}

	//----- instance

	Bridge _bridge; //used for outputing warnings
	string _documentSource;
	XmlNamespaceManager _nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> _handledElements;

	public DesibleParser(Bridge bridge, XmlDocument document, string documentSource) {
		_bridge = bridge;
		_documentSource = documentSource;
		_nsManager = new XmlNamespaceManager(document.NameTable);
		_nsManager.AddNamespace("desible1", desible1NS);
		_handledElements = new List<XmlElement>();
	}
	
	protected override void checkElement(XmlElement element, string expectedTagName) {
		if( element.LocalName != expectedTagName ) {
			throw new ParseError(
				String.Format(
					"expected tag name '{0}'",
					expectedTagName),
				getSource(element));
		}
		if( element.NamespaceURI != desible1NS ) {
			throw new ParseError(
				String.Format(
					"attempted to handle an element in namespace '{0}",
					element.NamespaceURI),
				getSource(element));
		}
		_handledElements.Add(element);
	}
	
	protected override string getSource(XmlElement element) {
		string location = "xxx"; //generate XPath for element location
		return String.Format(
			"Desible : {0} : {1}",
			_documentSource, location );
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
					"did not contain '{1}' element with '{2}' label",
					tagName, label ),
				getSource(element));
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
