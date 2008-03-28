# coding: utf-8

import sys
import string
import xml.dom.minidom as DOM

inputPath = "specs/docbook/Desal Semantics - Nodes.docbook"
nodeClassesOutputPath = "Desal 001/Desal Interpreter 001/node classes.cs"
desibleParserOutputPath = "Desal 001/Desal Interpreter 001/DesibleParser auto.cs"
desibleSerializerOutputPath = "Desal 001/Desal Interpreter 001/DesibleSerializer auto.cs"

#xxx need better way to store this information
#specType -> string (e.g. identifier -> INode_Expression)
superTypes = {}

autoWarning = """
//This file was generated programmatically, so
//don't edit this file directly.
"""

classFileTemplate = autoWarning + """
using System.Collections.Generic;

%s

"""

classTemplate = """
class %(csType)s : %(superType)s {
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

getterTemplate = """
	public %(csType)s @%(csName)s {
		get { return m_%(csName)s; }
	}
""".strip()

parserFileTemplate = autoWarning + """
using System.Collections.Generic;
using System.Xml;

partial class DesibleParser {
	void addTreeParsers() {
		%s
	}
}

"""

parserTemplate = """
		addParser<%(csType)s>("%(specType)s", delegate(XmlElement element) {
			return new %(csType)s(
				%(childNodes)s );
		});
""".strip()

serializerFileTemplate = autoWarning + """
using System.Collections.Generic;
using System.Xml;

partial class DesibleSerializer {
	%s
}
"""

serializerTemplate = """
	XmlElement serialize(%(csType)s node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		%(children)s
		return elem;
	}
""".strip()

serializeAppendTemplate = \
"append<%(csType)s>(elem, node.@%(csName)s, %(label)s);"

#layout: specType-> children[]
#child: { "count", "specType", "specName" }
def extractLayouts(filePath) :
	def findLayouts(elem) :
		for child in elem.childNodes :
			if child.nodeType == DOM.Node.ELEMENT_NODE :
				findLayouts(child)
		if not elem.hasAttribute("role") :
			return
		role = elem.getAttribute("role")
		if role != "layout" :
			return
		outputLayout(elem)
	
	def outputLayout(elem) :
		specType = getTitle(elem)
		children = elem.getElementsByTagName("member")
		layouts[specType] = []
		for child in children :
			layouts[specType].append(
				parseChild(child.firstChild.nodeValue))
		superTypes[specType] = getSuperType(elem)
	
	def getTitle(elem) :
		if elem.nodeType != DOM.Node.ELEMENT_NODE :
			raise Exception("@elem must be an element")
		childTitles = elem.getElementsByTagName("title")
		if len(childTitles) == 0 :
			return getTitle(elem.parentNode)
		return childTitles[0].firstChild.nodeValue

	def parseChild(text) :
		child = {}
		tokens = text.split(" ")
		
		#count (e.g. *)
		if tokens[0] in ["*", "+", "?"] :
			child["count"] = tokens.pop(0)
		else :
			child["count"] = "1"
		
		#spec type (e.g. alpha-beta-gamma)
		child["specType"] = tokens.pop(0)
		
		#spec name (e.g. alpha beta gamma)
		if len(tokens) > 0 :
			child["specName"] = " ".join(tokens)
		else :
			child["specName"] = child["specType"].replace("-", " ")

		return child
	
	#@return - INode, INode_{ Expression, Declaration, ScopeAlteration }
	def getSuperType(elem) :
		def isNonExec(elem) :
			if elem.getAttribute("xml:id") == "non-executable" :
				return True
			if elem.parentNode.nodeType == DOM.Node.ELEMENT_NODE :
				return isNonExec(elem.parentNode)
			return False
		
		def isNestedNode(elem) :
			id = elem.getAttribute("xml:id") or ""
			if id.find("node.") == 0 :
				return True
			if elem.parentNode.nodeType == DOM.Node.ELEMENT_NODE :
				return isNestedNode(elem.parentNode)
			return False
		
		specType = getTitle(elem)
		
		if specType.find("declare") > -1 :
			return "INode_Declaration"
		if specType in ["using", "import", "expose"] :
			return "INode_ScopeAlteration"
		if isNonExec(elem) :
			return "INode"
		if isNestedNode(elem.parentNode.parentNode) :
			return "INode"
		else :
			return "INode_Expression"

	dom = DOM.parse(filePath)
	layouts = {}
	findLayouts(dom.documentElement)
	return layouts

def lowerCamelCase(text) :
	upperCamel = upperCamelCase(text)
	return string.lower(upperCamel[0]) + upperCamel[1:]

def upperCamelCase(text) :
	upperFirst = lambda t : string.upper(t[0]) + t[1:]
	return "".join(map(upperFirst, text.replace(" ", "-").split("-")))

def createNodeClasses(layouts, csTypes) :
	def createClass(specType) :
		def createField(child) :
			return "%(csType)s m_%(csName)s;" % child
		
		def createParameter(child) :
			return "%(csType)s @%(csName)s" % child
		
		def createAssignment(child) :
			return "m_%(csName)s = @%(csName)s;" % child
		
		def createGetter(child) :
			return getterTemplate % child
		
		def getPrivateName(child) :
			return "m_%(csName)s" % child
	
		children = layouts[specType]
	
		superType = superTypes[specType]
	
		return classTemplate % {
			"csType" : csTypes[specType],
			"superType" : superType,
			"fields" : "\n\t".join(map(createField, children)),
			"parameters" : ",\n\t".join(map(createParameter, children)),
			"assignments" : "\n\t\t".join(map(createAssignment, children)),
			"getters" : "\n\n\t".join(map(createGetter, children)),
			"specType" : specType,
			"fieldList" : ",\n\t\t\t\t".join(map(getPrivateName, children))
		}

	out = file(nodeClassesOutputPath, "w")
	out.write(classFileTemplate % "\n\n".join(map(createClass, layouts)))
	out.close()

def createDesibleParserMethods(layouts, csTypes) :
	def createMethod(specType) :
		def createCall(child) :
			count = child["count"]
			if count == "1" :
				meth = "parseOne"
			elif count == "?" :
				meth = "parseOpt"
			elif count == "*" :
				meth = "parseMult"
			elif count == "+" :
				meth = "parseMult"
			else :
				raise Exception("unknown count " % count)
			return '%s<%s>(element, "%s")' % (meth, csTypes[child["specType"]], child["specName"].replace(" ", "-"))
		
		csType = csTypes[specType]
		children = layouts[specType]
		
		return parserTemplate % {
			"csType" : csType,
			"specType" : specType,
			"childNodes" : ",\n\t\t\t\t".join(map(createCall, children))
		}
	
	out = file(desibleParserOutputPath, "w")
	out.write(parserFileTemplate % "\n\n\t\t".join(map(createMethod, layouts)))
	out.close()

def createDesibleSerializerMethods(layouts, csTypes) :
	def createMethod(specType) :
		def createCall(child) :
			label = child["specName"].replace(" ", "-")
			if label == child["csType"] :
				label == "null"
			else :
				label = '"%s"' % label
			return serializeAppendTemplate % {
				"csType" : csTypes[child["specType"]],
				"csName" : child["csName"],
				"label" : label
			}
		
		csType = csTypes[specType]
		children = "\n\t\t".join(map(createCall, layouts[specType]))
		
		return serializerTemplate % {
			"csType" : csType,
			"children" : children
		}

	out = file(desibleSerializerOutputPath, "w")
	out.write(serializerFileTemplate % "\n\n\t".join(map(createMethod, layouts)))
	out.close()

layouts = extractLayouts(inputPath)

#xxx remove types or children of types that I don't want to deal with yet
for specType in ["interface", "class", "generic-interface", "generic-class", "statused-member", "comprehension"] :
	layouts[specType] = []
layouts["interface"] = [
	{ "count" : "*", "specType" : "property", "specName" : "property" },
	{ "count" : "*", "specType" : "method", "specName" : "method" }
]
#del layouts["bundle"]

#map specType to csType e.g. foo-bar-baz to Node_FooBarBaz
csTypes = { #xxx temporary -- terminal nodes and family nodes -- need way to automate this
	"expression" : "INode_Expression",
	"declaration" : "INode_Declaration",
	"scope-alteration" : "INode_ScopeAlteration",
	
	"identifier" : "Node_Identifier",
	"boolean" : "Node_Boolean",
	"member-status" : "Node_MemberStatus",
	"interface-member" : "Node_InterfaceMember",
	"comprehension-type" : "Node_ComprehensionType",
	"access" : "Node_Access",
	"string" : "Node_String",
	"integer" : "Node_Integer",
	"direction" : "Node_Direction",
	"identikey-category" : "Node_IdentikeyCategory"
}
for specType in layouts :
	csTypes[specType] = "Node_%s" % upperCamelCase(specType)

#add csType and csName to children, e.g. IList<Node_FooBarBaz> and fooBarBaz
for specType in layouts :
	for child in layouts[specType] :
		child["csName"] = lowerCamelCase(child["specName"])
		csType = csTypes[child["specType"]]
		if child["count"] in ["1", "?"] :
			child["csType"] = csType
		else :
			child["csName"] += "s"
			child["csType"] = "IList<%s>" % csType

createNodeClasses(layouts, csTypes)

#handle parsing and serializing these nodes manually
del layouts["bundle"]
del layouts["plane"]

createDesibleParserMethods(layouts, csTypes)
createDesibleSerializerMethods(layouts, csTypes)
