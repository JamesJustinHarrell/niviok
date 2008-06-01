
autoHeader = """
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;
"""

nodeClassFileTemplate = autoHeader + """
%s

"""

nodeClassTemplate = """
class %(csType)s : %(inherit)s {
	%(fields)s
	string m_nodeSource;
	
	public %(csType)s(
	%(parameters)s,
	string @nodeSource ) {
		%(assignments)s
		m_nodeSource = @nodeSource;
	}
	
	%(getters)s

	public string typeName {
		get { return "%(specType)s"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				%(fieldList)s );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}
""".strip()

nodeGetterTemplate = """
	public %(csType)s @%(csName)s {
		get { return m_%(csName)s; }
	}
""".strip()

desibleParserFileTemplate = autoHeader + """
using System.Xml;

abstract class DesibleParserAuto : DesibleParserBase {
	%s
}

"""

desibleFamilyParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(XmlElement element) {
		switch(element.LocalName) {
			%(cases)s
			default:
				throw new ParseError(
					String.Format(
						"element with name '{0}' is not recognized as a %(csName)s node",
						element.LocalName),
					getSource(element));
		}
	}
""".strip()

desibleFamilyCaseTemplate = """
			case "%(specType)s":
				return parse%(csName)s(element);
""".strip()

desibleTerminalParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(XmlElement element) {
		checkElement(element, "%(typename)s");
		return new %(csType)s(element.InnerText, getSource(element));
	}
""".strip()

desibleTreeParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(XmlElement element) {
		checkElement(element, "%(specType)s");
		return new %(csType)s(
			%(childNodes)s,
			getSource(element) );
	}
""".strip()

desibleSerializerFileTemplate = autoHeader + """
using System.Xml;

abstract class DesibleSerializerAuto : DesibleSerializerBase {
	%(methods)s
	
	protected XmlElement serializeAny(INode node) {
		switch(node.typeName) {
			%(cases)s
			default:
				throw new ApplicationException(
					String.Format(
						"can't serialize node of type {0} from {1}",
						node.typeName,
						node.nodeSource));
		}
	}
}
"""

desibleSerializerTerminalTemplate = """
	protected virtual XmlElement serialize(%(csType)s node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}
""".strip()

desibleSerializerTreeTemplate = """
	protected virtual XmlElement serialize(%(csType)s node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		%(children)s
		return elem;
	}
""".strip()

desibleSerializerCaseTemplate = """
			case "%(typename)s":
				return serialize((%(csTypename)s)node);
""".strip()

desexpParserFileTemplate = autoHeader + """
using System.Xml;

namespace Desexp {

abstract class DesexpParserAuto : DesexpParserBase {
	%s
}

} //end namespace Desexp

"""

desexpParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(Sexp sexp) {
		if( sexp.list.Count != %(childCount)s )
			throw new ParseError(
				String.Format(
					"'%(typename)s' node must have %(childCount)s children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new %(csType)s(
   			%(childNodes)s,
			getSource(sexp) );
	}
""".strip()

desexpTerminalParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(Sexp sexp) {
		try {
			return new %(csType)s(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type %(csName)s cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}
""".strip()

desexpFamilyParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminal%(csName)s(sexp);
		if( sexp.list.Count == 0 )
			throw new ParseError(
				"this list cannot be empty",
				getSource(sexp));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseNonword%(csName)s(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			%(cases)s
			default:
				sexp.list.AddFirst(first);
				return parse%(csName)sDefault(sexp);
		}
	}
	protected virtual %(csType)s parseTerminal%(csName)s(Sexp sexp) {
		throw new ParseError(
			"%(csName)s expression must be a list",
			getSource(sexp));
	}
	protected virtual %(csType)s parseNonword%(csName)s(Sexp sexp) {
		throw new ParseError(
			"expression must begin with a word",
			getSource(sexp));
	}
	protected virtual %(csType)s parse%(csName)sDefault(Sexp sexp) {
		throw new ParseError(
			String.Format(
				"unknown type of %(csName)s '{0}'",
				sexp.list.First.Value.atom),
			getSource(sexp));
	}
""".strip()

desexpFamilyCaseTemplate = """
			case "%(specType)s":
				return parse%(csName)s(sexp);
""".strip()

executorFileTemplate = autoHeader + """
static partial class Executor {
	public static IWorker executeAny(INode_Expression node, Scope scope) {
		switch(node.typeName) {
			%s
			default:
				throw new ApplicationException(String.Format(
					"can't execute node of type '{0}'",
					node.typeName));
		}
	}
}
""".strip()

executorCaseTemplate = """
			case "%(typename)s":
				return execute((%(csTypename)s)node, scope);
""".strip()
