using System;
using System.Collections.Generic;
using System.Xml;
using Acrid.NodeTypes;

namespace Acrid.Desible {

public abstract class DesibleSerializerBase {
	protected const string desible1NS = "urn:desible1";
	protected XmlDocument _doc;
	protected abstract void append<T>(XmlElement parent, ICollection<T> children, string label) where T : INode;
	protected abstract void append<T>(XmlElement parent, T child, string label) where T : INode;
}

public class DesibleSerializer : DesibleSerializerAuto {
	//----- static

	public static string serializeToMarkup(INode node) {
		return serializeToElement(node, new XmlDocument()).OuterXml;
	}
	
	public static XmlDocument serializeToDocument(Node_Module node) {
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
	
	//terminals
	XmlElement serializeTerminal(INode node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild( _doc.CreateTextNode(node.ToString()) );
		return elem;
	}
	override protected XmlElement serialize(Node_Boolean node) { return serializeTerminal(node); }
	override protected XmlElement serialize(Node_Direction node) { return serializeTerminal(node); }
	override protected XmlElement serialize(Node_Identifier node) { return serializeTerminal(node); }
	override protected XmlElement serialize(Node_WoScidentreCategory node) { return serializeTerminal(node); }
	override protected XmlElement serialize(Node_Integer node) { return serializeTerminal(node); }
	override protected XmlElement serialize(Node_Rational node) { return serializeTerminal(node); }
	override protected XmlElement serialize(Node_String node) { return serializeTerminal(node); }
}

} //namespace
