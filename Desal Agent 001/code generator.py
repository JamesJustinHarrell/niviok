# coding: utf-8

import os
import sys
import string
import xml.dom.minidom as DOM

desalBase = "/media/files/Desal"
implBase = os.path.join(desalBase,"Desal Agent 001/Desal Agent 001")
inputPath = os.path.join(desalBase,"nodes.xml")
nodeClassesOutputPath = os.path.join(implBase,"node classes auto.cs")
desibleParserOutputPath = os.path.join(implBase,"DesibleParserAuto.cs")
desibleSerializerOutputPath = os.path.join(implBase,"DesibleSerializerAuto.cs")
desexpParserOutputPath = os.path.join(implBase,"DesexpParserAuto.cs")

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
		return new %(csType)s(
   			%(childNodes)s );
	}
""".strip()

desexpTerminalParserTemplate = """
	protected %(csType)s parse%(csName)s(Sexp sexp) {
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

def lowerCamelCase(text) :
	upperCamel = upperCamelCase(text)
	return string.lower(upperCamel[0]) + upperCamel[1:]

def upperCamelCase(text) :
	text = text.replace(" ", "-")
	words = text.split("-")
	words = [string.upper(x[0]) + x[1:] for x in words]
	return "".join(words)

#@return: { specType -> Layout }
#Layout: { "specType":String, "specSuperTypes":[String], "children":[Child] }
#Child: { "count":String, "specType":String, ?"specName":String }
def compileNodeInfo(filePath) :
	def selectAll(node, selector) :
		return node.getElementsByTagName(selector)
	
	def selectFirst(node, selector) :
		return selectAll(node, selector)[0]
	
	def textVal(elem) :
		return elem.firstChild.nodeValue.strip()
	
	def evalNodeElem(nodeElem) :
		return {
			"specType" : textVal(selectFirst(nodeElem,"spec-type")),
			"specSuperTypes" : [textVal(x).strip() for x in selectAll(nodeElem,"spec-super-type")],
			"children" : map(evalChild, selectAll(nodeElem,"child"))
		}
	
	def evalChild(elem) :
		child = {
			"count" : textVal(selectFirst(elem, "count")),
			"specType" : textVal(selectFirst(elem, "spec-type"))
		}
		specNames = selectAll(elem,"spec-name")
		if len(specNames) > 0 :
			child["specName"] = textVal(specNames[0])
		return child
	
	doc = DOM.parse(filePath)
	nodes = {}
	for elem in selectAll(doc, "node") :
		nodeInfo = evalNodeElem(elem)
		nodes[nodeInfo["specType"]] = nodeInfo
	return nodes
	
def setupNodeInfo(nodes) :
	def getCsSuperType(specSuperType) :
		if specSuperType == "expression" :
			return "INode_Expression"
		if specSuperType == "scope-alteration" :
			return "INode_ScopeAlteration"
		else :
			raise Exception("unknown super type: %s" % specSuperType)
	
	#xxx simplify layout of interface node
	nodes["interface"]["children"] = [
			{ "count" : "*", "specType" : "property" },
			{ "count" : "*", "specType" : "method" }
	]
	
	#xxx remove types or children of types that I don't want to deal with yet
	for specType in ["class", "generic-interface",
	"generic-class", "statused-member", "comprehension",
	"continue", "interface-member", "null", "comprehension-type",
	"static-member", "member-status"] :
		del nodes[specType]
	
	#add csType to NodeInfo objects
	for specType in nodes :
		nodes[specType]["csType"] = "Node_%s" % upperCamelCase(specType)
	
	#add supertypes
	toAdd = {
		"expression" : "INode_Expression",
		"declaration" : "INode_Declaration",
		"scope-alteration" : "INode_ScopeAlteration"
	}
	for specType in toAdd :
		nodes[specType] = {
			"specType": specType,
			"specSuperTypes": [],
			"children": [],
			"csType": toAdd[specType]
		}
	
	for specType in nodes :
		nodeInfo = nodes[specType]
		
		nodeInfo["csSuperTypes"] = map(getCsSuperType, nodeInfo["specSuperTypes"])

		for child in nodeInfo["children"] :
			if "specName" in child :
				child["csName"] = lowerCamelCase(child["specName"])
				child["csTagName"] = '"*"'
				child["csLabel"] = '"%s"' % child["specName"].replace(" ", "-")
			else :
				child["csName"] = lowerCamelCase(child["specType"])
				child["csTagName"] = '"%s"' % child["specType"]
				if child["specType"] in ["expression", "declaration", "scope-alteration"] :
					raise Exception(
						"'%s' child of type '%s' must have a label" \
						% (specType, child["specType"]))
				child["csLabel"] = "null"
				
			
			csType = nodes[child["specType"]]["csType"]
			if child["count"] in ["1", "?"] :
				child["csType"] = csType
			else :
				child["csName"] += "s"
				child["csType"] = "IList<%s>" % csType

def createNodeClasses(nodes) :
	def createClass(specType) :
		def createField(child) :
			return "%(csType)s m_%(csName)s;" % child
		
		def createParameter(child) :
			return "%(csType)s @%(csName)s" % child
		
		def createAssignment(child) :
			return "m_%(csName)s = @%(csName)s;" % child
		
		def createGetter(child) :
			return nodeGetterTemplate % child
		
		def getPrivateName(child) :
			return "m_%(csName)s" % child
	
		nodeInfo = nodes[specType]
		children = nodeInfo["children"]
	
		inheritStr = ", ".join(nodeInfo["csSuperTypes"])
		if inheritStr == "" :
			inheritStr = "INode"
	
		return nodeClassTemplate % {
			"csType" : nodeInfo["csType"],
			"inherit" : inheritStr,
			"fields" : "\n\t".join(map(createField, children)),
			"parameters" : ",\n\t".join(map(createParameter, children)),
			"assignments" : "\n\t\t".join(map(createAssignment, children)),
			"getters" : "\n\n\t".join(map(createGetter, children)),
			"specType" : specType,
			"fieldList" : ",\n\t\t\t\t".join(map(getPrivateName, children))
		}

	out = file(nodeClassesOutputPath, "w")
	out.write(
		nodeClassFileTemplate % "\n\n".join(
		[createClass(x) for x in nodes if len(nodes[x]["children"]) > 0]))
	out.close()

def createDesibleParserMethods(nodes) :
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
			return '%s<%s>(element, %s, %s)' % (
				meth,
				nodes[child["specType"]]["csType"],
				child["csTagName"],
				child["csLabel"]
			)
	
		nodeInfo = nodes[specType]
		children = nodeInfo["children"]
		
		return desibleParserTemplate % {
			"csType" : nodeInfo["csType"],
			"specType" : specType,
			"childNodes" : ",\n\t\t\t\t".join(map(createCall, children))
		}
	
	out = file(desibleParserOutputPath, "w")
	out.write(
		desibleParserFileTemplate % "\n\n\t\t".join(
		[createMethod(x) for x in nodes if len(nodes[x]["children"]) > 0 and x not in ["bundle", "plane"]]))
	out.close()

def createDesibleSerializerMethods(nodes) :
	def createMethod(specType) :
		def createCall(child) :
			return "append<%s>(elem, node.@%s, %s);" % (
				nodes[child["specType"]]["csType"],
				child["csName"],
				child["csLabel"]
			)
		
		csType = nodes[specType]["csType"]
		children = "\n\t\t".join(map(createCall, nodes[specType]["children"]))
		
		return desibleSerializerTemplate % {
			"csType" : csType,
			"children" : children
		}

	out = file(desibleSerializerOutputPath, "w")
	out.write(
		desibleSerializerFileTemplate % "\n\n\t".join(
		[createMethod(x) for x in nodes if len(nodes[x]["children"]) > 0 and x not in ["bundle", "plane"]]))
	out.close()

def createDesexpParser(nodes) :
	def createMethod(specType) :
		templates = {
			"1" : "parseOne<%s>(parse%s, sexp)",
			"?" : "parseOpt<%s>(parse%s, sexp)",
			"*" : "parseMult0<%s>(parse%s, sexp)",
			"+" : "parseMult1<%s>(parse%s, sexp)"
		}
		
		def createCall(child) :
			return templates[child["count"]] % (
				nodes[child["specType"]]["csType"],
				upperCamelCase(child["specType"]))

		nodeInfo = nodes[specType]
		
		if len(nodeInfo["children"]) == 0 :
			template = desexpTerminalParserTemplate
		else :
			template = desexpParserTemplate
			
		return template % {
			"csType" : nodeInfo["csType"],
			"csName" : upperCamelCase(specType),
			"childNodes" : ",\n\t\t\t".join(map(createCall, nodeInfo["children"]))
		}
	
	def createSuperTypeFuncs() :
		def createSuperTypeFunc(specType) :
			def createCase(specType) :
				return desexpSuperCaseTemplate % {
					"specType" : specType,
					"csType" : nodes[specType]["csType"],
					"csName" : upperCamelCase(specType)
				}
			
			template = desexpSuperParserTemplate
			if specType == "expression" :
				template = desexpExpressionParserTemplate

			return template % {
				"csType" : nodes[specType]["csType"],
				"csName" : upperCamelCase(specType),
				"cases" : "\n\t\t\t".join(map(createCase, superTypes[specType])),
			}
		
		#e.g. "Expression" -> ["integer", "string", ...]
		superTypes = {}
		
		for specType in filter(lambda x:x != "bundle", nodes) :
			for superType in nodes[specType]["specSuperTypes"] :
				if superType not in superTypes :
					superTypes[superType] = []
				superTypes[superType].append(specType)
	
		return map(createSuperTypeFunc, superTypes)
	
	funcs = [createMethod(x) for x in nodes if nodes[x]["csType"].find("INode") != 0 and x not in ["bundle", "string"]]
	funcs += createSuperTypeFuncs()
	
	out = file(desexpParserOutputPath, "w")
	out.write(desexpParserFileTemplate % "\n\n\t".join(funcs))
	out.close()


#----- entry point

nodes = compileNodeInfo(inputPath)
setupNodeInfo(nodes)

createNodeClasses(nodes)
createDesibleParserMethods(nodes)
createDesibleSerializerMethods(nodes)
createDesexpParser(nodes)
