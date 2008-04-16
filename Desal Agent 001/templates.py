
autoWarning = """
//This file was generated programmatically, so
//don't edit this file directly.
"""

nodeClassFileTemplate = autoWarning + """
using System.Collections.Generic;

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

desibleParserFileTemplate = autoWarning + """
using System.Collections.Generic;
using System.Xml;

abstract class DesibleParserAuto : DesibleParserBase {
	protected void addTreeParsers() {
		%s
	}
}

"""

desibleParserTemplate = """
		addParser<%(csType)s>("%(specType)s", delegate(XmlElement element) {
			return new %(csType)s(
				%(childNodes)s );
		});
""".strip()

desibleSerializerFileTemplate = autoWarning + """
using System.Collections.Generic;
using System.Xml;

#pragma warning disable 0169

abstract class DesibleSerializerAuto : DesibleSerializerBase {
	%s
}
"""

desibleSerializerTemplate = """
	protected XmlElement serialize(%(csType)s node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		%(children)s
		return elem;
	}
""".strip()

desexpParserFileTemplate = autoWarning + """
using System;
using System.Collections.Generic;
using System.Xml;

namespace Desexp {

abstract class DesexpParserAuto : DesexpParserBase {
	%s
}

} //end namespace Desexp

"""

desexpParserTemplate = """
	protected %(csType)s parse%(csName)s(Sexp sexp) {
		if( sexp.list.Count != %(childCount)s )
			throw new Exception(
				String.Format(
					"node at [{0}:{1}] must have %(childCount)s children",
					sexp.line, sexp.column));
		return new %(csType)s(
   			%(childNodes)s );
	}
""".strip()

desexpTerminalParserTemplate = """
	virtual protected %(csType)s parse%(csName)s(Sexp sexp) {
		try {
			return new %(csType)s(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type %(csName)s at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}
""".strip()

desexpExpressionParserTemplate = """
	protected INode_Expression parseExpression(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalExpression(sexp);
		if( sexp.list.Count == 0 )
			throw new Exception(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseExpressionDefault(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			%(cases)s
			default:
				sexp.list.AddFirst(first);
				return parseExpressionDefault(sexp);
		}
	}
""".strip()

desexpSuperParserTemplate = """
	protected %(csType)s parse%(csName)s(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			throw new Exception(
				String.Format(
					"%(csName)s S-Expression at [{0}:{1}] must be a list",
					sexp.line, sexp.column));
		if( sexp.list.Count == 0 )
			throw new Exception(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			throw new Exception(
				String.Format(
					"S-Expression at [{0}:{1}] must begin with a word",
					sexp.line, sexp.column));
		string specType = sexp.list.First.Value.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			%(cases)s
			default:
				throw new Exception(
					String.Format(
						"unknown type of %(csName)s '{0}' at [{1}:{2}]",
						specType, sexp.line, sexp.column));
		}
	}
""".strip()

desexpSuperCaseTemplate = """
			case "%(specType)s":
				return parse%(csName)s(sexp);
""".strip()


