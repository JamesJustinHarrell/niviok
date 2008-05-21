
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
	
	public %(csType)s(
	%(parameters)s ) {
		%(assignments)s
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
				throw new Exception(
					String.Format(
						"element with name '{0}' is not recognized as a %(csName)s node",
						element.LocalName));
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
		return new %(csType)s(element.InnerText);
	}
""".strip()

desibleTreeParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(XmlElement element) {
		checkElement(element, "%(specType)s");
		return new %(csType)s(
			%(childNodes)s );
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
				throw new ApplicationException(String.Format(
					"can't serialize node of type {0}", node.typeName));
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
					"node at [{0}:{1}] must have %(childCount)s children",
					sexp.line, sexp.column));
		return new %(csType)s(
   			%(childNodes)s );
	}
""".strip()

desexpTerminalParserTemplate = """
	protected virtual %(csType)s parse%(csName)s(Sexp sexp) {
		try {
			return new %(csType)s(sexp.atom);
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type %(csName)s at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
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
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
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
			String.Format(
				"%(csName)s S-Expression at [{0}:{1}] must be a list",
				sexp.line, sexp.column));
	}
	protected virtual %(csType)s parseNonword%(csName)s(Sexp sexp) {
		throw new ParseError(
				String.Format(
					"S-Expression at [{0}:{1}] must begin with a word",
					sexp.line, sexp.column));
	}
	protected virtual %(csType)s parse%(csName)sDefault(Sexp sexp) {
		throw new ParseError(
			String.Format(
				"unknown type of %(csName)s '{0}' at [{1}:{2}]",
				sexp.list.First.Value.atom, sexp.line, sexp.column));
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
