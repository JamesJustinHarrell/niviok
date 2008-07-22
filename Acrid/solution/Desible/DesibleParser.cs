using System;
using System.Collections.Generic;
using System.Xml;
using Acrid.NodeTypes;

namespace Acrid.Desible {

public abstract class DesibleParserBase {
	protected abstract void checkElement(XmlElement element, string expectedTagName);
	protected abstract string getSource(XmlElement element);
	protected abstract T parseOne<T>(
		Func<XmlElement,T> func, XmlElement element, string label, string tagName);
	protected abstract T parseOpt<T>(
		Func<XmlElement,T> func, XmlElement element, string label, string tagName);
	protected abstract IList<T> parseMult0<T>(
		Func<XmlElement,T> func, XmlElement element, string label, string tagName);
	protected abstract IList<T> parseMult1<T>(
		Func<XmlElement,T> func, XmlElement element, string label, string tagName);
}

public class DesibleParser : DesibleParserAuto {
	//----- static
	const string desible1NS = "urn:desible1";

	public static Node_Module parseDocument(
	string path, bool warnUnhandled, bool warnAllNS) {
		XmlDocument document = new XmlDocument();
		try {
			document.Load(path);
		}
		catch(XmlException e) {
			throw new ParseError(
				"XmlException : " + e.Message,
				"Desible : " + path,
				e);
		}
		return parseDocument(document, path, warnUnhandled, warnAllNS);
	}
	
	public static Node_Module parseDocument(
	XmlDocument document, string documentSource, bool warnUnhandled, bool warnAllNS) {
		DesibleParser parser = new DesibleParser(document, documentSource);
		//xxx check XML with Relax NG
		XmlElement root = document.DocumentElement;
		Node_Module bundle = parser.parseModule(root);
		if( warnUnhandled )
			parser.warnAboutUnhandled(root, warnAllNS);
		return bundle;
	}

	//----- instance

	string _documentSource;
	XmlNamespaceManager _nsManager; //maps tag name prefixes to namespace URIs
	IList<XmlElement> _handledElements;

	public DesibleParser(XmlDocument document, string documentSource) {
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
			Console.Error.WriteLine(String.Format(
				"unhandled element in namespace '{0}'\n{1}",
				element.NamespaceURI, element.OuterXml));
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
					"did not contain '{0}' element with '{1}' label",
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
	Func<XmlElement,T> func, XmlElement element, string label, string tagName) {
		return func(selectFirst(element, label, tagName));
	}

	//parse first child with label, if such a child exists
	protected override T parseOpt<T>(
	Func<XmlElement,T> func, XmlElement element, string label, string tagName) {
		XmlElement child = trySelectFirst(element, label, tagName);
		if( child == null )
			return default(T); //null - google CS0403 for info
		return func( child );
	}

	//parse all children with label
	protected override IList<T> parseMult0<T>(
	Func<XmlElement,T> func, XmlElement element, string label, string tagName) {
		IList<T> rv = new List<T>();
		foreach( XmlElement child in selectAll(element, label, tagName) ) {
			rv.Add( func(child) );
		}
		return rv;
	}
	
	protected override IList<T> parseMult1<T>(
	Func<XmlElement,T> func, XmlElement element, string label, string tagName) {
		IList<T> rv = parseMult0<T>(func, element, label, tagName);
		if( rv.Count == 0 )
			throw new ParseError(
				"list at must contain at least 1 child",
				getSource(element));
		return rv;
	}
}

} //namespace
