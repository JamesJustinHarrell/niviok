# coding: utf-8

import sys
import string
import xml.dom.minidom as DOM

inputPath = "specs/docbook/Desal Semantics - nodes.docbook"
nodeClassesOutputPath = "Desal 001/Desal Interpreter 001/node classes.cs"
desibleParserOutputPath = "Desal 001/Desal Interpreter 001/DesibleParser auto.cs"

#xxx need better way to store this information
#specType -> string (e.g. identifier -> INode_Expression)
superTypes = {}

classFileTemplate = """
//This file was generated programmatically, so
//don't edit this file directly.

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
	%(executeMeth)s
	public string typeName {
		get { return "%(specType)s"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				%(fieldList)s );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}
""".strip()

executeMethCode = """
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}
"""

getterTemplate = """
	public %(csType)s @%(csName)s {
		get { return m_%(csName)s; }
	}
""".strip()

parserFileTemplate = """
//This file was generated programmatically, so
//don't edit this file directly.

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
		executeMeth = ""
		if superType in ["INode_Expression", "INode_Declaration"] :
			executeMeth = executeMethCode
	
		return classTemplate % {
			"csType" : csTypes[specType],
			"superType" : superType,
			"fields" : "\n\t".join(map(createField, children)),
			"parameters" : ",\n\t".join(map(createParameter, children)),
			"assignments" : "\n\t\t".join(map(createAssignment, children)),
			"getters" : "\n\n\t".join(map(createGetter, children)),
			"executeMeth" : executeMeth,
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
	
	layouts = layouts.copy()
	
	out = file(desibleParserOutputPath, "w")
	out.write(parserFileTemplate % "\n\n\t\t".join(map(createMethod, layouts)))
	out.close()

layouts = extractLayouts(inputPath)

#xxx remove types that I don't want to deal with yet
del layouts["interface"]
del layouts["class"]
del layouts["generic-interface"]
del layouts["generic-class"]
del layouts["statused-member"]
del layouts["comprehension"]
del layouts["bundle"]
del layouts["plane"]

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
		if child["count"] == "1" or child["count"] == "?" :
			child["csType"] = csType
		else :
			child["csType"] = "IList<%s>" % csType

createNodeClasses(layouts, csTypes)
createDesibleParserMethods(layouts, csTypes)

