using System;
using System.Collections.Generic;
using System.Xml;

abstract class DesibleSerializerBase {
	protected const string desible1NS = "urn:desible1";
	protected XmlDocument _doc;
	protected abstract void append<T>(XmlElement parent, ICollection<T> children, string label) where T : INode;
	protected abstract void append<T>(XmlElement parent, T child, string label) where T : INode;
}

class DesibleSerializer : DesibleSerializerAuto {
	//----- static

	public static string serializeToMarkup(INode node) {
		return serializeToElement(node, new XmlDocument()).OuterXml;
	}
	
	public static XmlDocument serializeToDocument(Node_Bundle node) {
		XmlDocument doc = new XmlDocument();
		doc.AppendChild( serializeToElement(node, doc) );
		return doc;
	}
	
	public static XmlElement serializeToElement(INode node, XmlDocument doc) {
		DesibleSerializer ser = new DesibleSerializer(doc);
		return ser.serializeAny(node);
	}

	//----- instance

	DesibleSerializer(XmlDocument doc) {
		_doc = doc;
	}

	protected override void append<T>(XmlElement parent, ICollection<T> children, string label) {
		foreach( T child in children )
			append<T>(parent, child, label);
	}
	
	protected override void append<T>(XmlElement parent, T child, string label) {
		if( child == null )
			return;
		XmlElement childElem = serializeAny(child);
		if( label != null )
			childElem.SetAttribute("label", label);
		parent.AppendChild(childElem);
	}

	//bundle
	protected override XmlElement serialize(Node_Bundle node) {
		XmlElement elem = _doc.CreateElement("bundle", desible1NS);
		append<Node_Import>(elem, node.imports, null);
		append<INode_ScopeAlteration>(elem, node.alts, "alt");
		append<Node_Plane>(elem, node.planes, null);
		return elem;
	}
	
	//plane
	protected override XmlElement serialize(Node_Plane node) {
		XmlElement elem = _doc.CreateElement("inline-plane", desible1NS);
		append<INode_ScopeAlteration>(elem, node.alts, "alt");
		append<Node_DeclareFirst>(elem, node.declareFirsts, null);
		return elem;
	}
	
	//terminals
	XmlElement serializeTerminal(INode node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild( _doc.CreateTextNode(node.ToString()) );
		return elem;
	}
	XmlElement serialize(Node_Boolean node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Direction node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Identifier node) { return serializeTerminal(node); }
	XmlElement serialize(Node_IdentikeyCategory node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Integer node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Rational node) { return serializeTerminal(node); }
	XmlElement serialize(Node_String node) { return serializeTerminal(node); }
}

