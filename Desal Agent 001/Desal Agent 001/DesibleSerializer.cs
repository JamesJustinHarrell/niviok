using System;
using Reflection = System.Reflection;
using System.Collections.Generic;
using System.Xml;

partial class DesibleSerializer {
	//----- static
	
	const string desible1NS = "urn:desible1";
	
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
		return ser.serialize(node);
	}
	

	//----- instance

	XmlDocument _doc;
	
	DesibleSerializer(XmlDocument doc) {
		_doc = doc;
	}

	void append<T>(XmlElement parent, ICollection<T> children, string label) where T : INode {
		foreach( T child in children )
			append<T>(parent, child, label);
	}
	
	void append<T>(XmlElement parent, T child, string label) where T : INode {
		if( child == null )
			return;
		XmlElement childElem = serialize(child);
		if( label != null )
			childElem.SetAttribute("label", label);
		parent.AppendChild(childElem);
	}

	#pragma warning disable 0169

	//bundle
	XmlElement serialize(Node_Bundle node) {
		XmlElement elem = _doc.CreateElement("bundle", desible1NS);
		append<Node_Plane>(elem, node.planes, null);
		return elem;
	}
	
	//plane
	XmlElement serialize(Node_Plane node) {
		XmlElement elem = _doc.CreateElement("inline-plane", desible1NS);
		append<Node_DeclareFirst>(elem, node.declareFirsts, null);
		return elem;
	}
	
	//terminals
	XmlElement serializeTerminal(INode node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild( _doc.CreateTextNode(node.ToString()) );
		return elem;
	}
	XmlElement serialize(Node_Access node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Boolean node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Direction node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Identifier node) { return serializeTerminal(node); }
	XmlElement serialize(Node_IdentikeyCategory node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Integer node) { return serializeTerminal(node); }
	XmlElement serialize(Node_Rational node) { return serializeTerminal(node); }
	XmlElement serialize(Node_String node) { return serializeTerminal(node); }
	
	#pragma warning restore 0169
	
	//node
	XmlElement serialize(INode node) {
		Type classType = typeof(DesibleSerializer);
		Type nodeType = ((Object)node).GetType();
		Reflection.MethodInfo meth = classType.GetMethod(
			"serialize",
			Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance,
			null,
			new Type[]{nodeType},
			null );

		if(	meth.GetParameters()[0].ParameterType == typeof(INode) )
			throw new Exception(
				String.Format("can't serialize node of type {0}", node.typeName));
		
		try {
			return (XmlElement)meth.Invoke(this, new object[]{node});
		}
		catch(Reflection.TargetInvocationException e) {
			if( e.InnerException is ClientException )
				throw e.InnerException;
			throw e;
		}
	}
}
