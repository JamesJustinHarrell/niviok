import os
import sys
import string
import xml.dom.minidom as DOM

desalBase = "/media/files/Desal"
agentBase = os.path.join(desalBase,"Desal Agent 001/Desal Agent 001")
inputPath = os.path.join(desalBase,"nodes.xml")
nodeClassesOutputPath = os.path.join(agentBase,"node classes auto.cs")
desibleParserOutputPath = os.path.join(agentBase,"DesibleParserAuto.cs")
desibleSerializerOutputPath = os.path.join(agentBase,"DesibleSerializerAuto.cs")
desexpParserOutputPath = os.path.join(agentBase,"DesexpParserAuto.cs")
executorOutputPath = os.path.join(agentBase,"Executor auto.cs")

sys.path.append(desalBase)
from lib import *
sys.path.append(os.path.join(desalBase,"Desal Agent 001"))
from templates import *

def lowerCamelCase(text) :
	upperCamel = upperCamelCase(text)
	return string.lower(upperCamel[0]) + upperCamel[1:]

def upperCamelCase(text) :
	text = text.replace(" ", "-")
	words = text.split("-")
	words = [string.upper(x[0]) + x[1:] for x in words]
	return "".join(words)

#@return: { typename -> NodeType }
#NodeType: { category:"family", typename:String, members:[String] }
#NodeType: { category:"tree", typename:String, layout:[LayoutEntry] }
#NodeType: { category:"terminal", typename:String }
#LayoutEntry: { count:String, typename:String, ?label:String }
def parseNodeTypes(filePath) :
	def parseFamily(elem) :
		assert elem.tagName == "family"
		return {
			"category" : "family",
			"typename" : textValue(selectChild(elem, "typename")),
			"members" : map(textValue, selectChildren(selectChild(elem, "members"), "typename"))
		}
	
	def parseTree(nodeElem) :
		def parseEntry(elem) :
			child = {
				"count" : textValue(selectChild(elem, "count")),
				"typename" : textValue(selectChild(elem, "typename"))
			}
			if len(selectChildren(elem, "label")) > 0 :
				child["label"] = textValue(selectChild(elem, "label"))
			return child
		assert elem.tagName == "tree"
		return {
			"category" : "tree",
			"typename" : textValue(selectChild(elem, "typename")),
			"layout" : map(parseEntry, selectChildren(elem, "entry"))
		}
	
	def parseTerminal(elem) :
		assert elem.tagName == "terminal"
		return {
			"category" : "terminal",
			"typename" : textValue(selectChild(elem, "typename"))
		}
	
	doc = DOM.parse(filePath)
	nodeTypes = {}
	for elem in childElements(doc.documentElement) :
		nodeType = {
			"family" : parseFamily,
			"tree" : parseTree,
			"terminal" : parseTerminal
		}[ elem.tagName ](elem)
		nodeTypes[nodeType["typename"]] = nodeType	
	return nodeTypes
	
def setupNodeTypes(nodeTypes) :
	#add cs* keys
	for (typename, nodeType) in nodeTypes.iteritems() :
		if nodeType["category"] == "family" :
			nodeType["csTypename"] = "INode_%s" % upperCamelCase(typename)
		else :
			nodeType["csTypename"] = "Node_%s" % upperCamelCase(typename)
			nodeType["csFamilies"] = set()
	
	#fill in the "csFamilies" sets, now that they've all been added
	for (typename, nodeType) in nodeTypes.iteritems() :
		if nodeType["category"] != "family" :
			continue
		for memberTypename in nodeType["members"] :
			nodeTypes[memberTypename]["csFamilies"].add(nodeType["csTypename"])
			
	#add more cs* keys
	for (typename, nodeType) in nodeTypes.iteritems() :
		if nodeType["category"] != "tree" :
				continue
		
		for entry in nodeType["layout"] :
			if "label" in entry :
				entry["csName"] = lowerCamelCase(entry["label"])
				entry["csTagName"] = '"*"'
				entry["csLabel"] = '"%s"' % entry["label"].replace(" ", "-")
			else :
				entry["csName"] = lowerCamelCase(entry["typename"])
				entry["csTagName"] = '"%s"' % entry["typename"]
				if nodeTypes[entry["typename"]]["category"] == "family" :
					raise Exception(
						"'%s' entry of type '%s' must have a label" \
						% (typename, entry["typename"]))
				entry["csLabel"] = "null"
				
			csType = nodeTypes[entry["typename"]]["csTypename"]
			if entry["count"] in ["1", "?"] :
				entry["csType"] = csType
			else :
				entry["csName"] += "s"
				entry["csType"] = "IList<%s>" % csType

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
		entries = nodeInfo["layout"]
	
		inheritStr = ", ".join(nodeInfo["csFamilies"])
		if inheritStr == "" :
			inheritStr = "INode"
	
		return nodeClassTemplate % {
			"csType" : nodeInfo["csTypename"],
			"inherit" : inheritStr,
			"fields" : "\n\t".join(map(createField, entries)),
			"parameters" : ",\n\t".join(map(createParameter, entries)),
			"assignments" : "\n\t\t".join(map(createAssignment, entries)),
			"getters" : "\n\n\t".join(map(createGetter, entries)),
			"specType" : specType,
			"fieldList" : ",\n\t\t\t\t".join(map(getPrivateName, entries))
		}

	out = file(nodeClassesOutputPath, "w")
	out.write(
		nodeClassFileTemplate % "\n\n".join(
		[createClass(x) for x in nodes if nodes[x]["category"] == "tree"]))
	out.close()

def createDesibleParserMethods(nodeTypes) :
	def createFamilyMethod(typename) :
		def createCase(entry) :
			return desibleFamilyCaseTemplate % {
				"specType" : entry,
				"csType" : nodeTypes[entry]["csTypename"],
				"csName" : upperCamelCase(entry)
			}

		nodeType = nodeTypes[typename]

		return desibleFamilyParserTemplate % {
			"csType" : nodeType["csTypename"],
			"csName" : upperCamelCase(nodeType["typename"]),
			"cases" : "\n\t\t\t".join(map(createCase, nodeType["members"])),
		}
	
	def createTerminalMethod(typename) :
		nodeType = nodeTypes[typename]
		return desibleTerminalParserTemplate % {
			"csType" : nodeType["csTypename"],
			"csName" : upperCamelCase(typename),
			"typename" : typename
		}
	
	def createTreeMethod(typename) :
		def createCall(entry) :
			count = entry["count"]
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
			return '%s<%s>(parse%s, element, %s, %s)' % (
				meth,
				nodeTypes[entry["typename"]]["csTypename"],
				upperCamelCase(entry["typename"]),
				entry["csTagName"],
				entry["csLabel"]
			)
	
		nodeType = nodeTypes[typename]
		entries = nodeType["layout"]
		
		return desibleTreeParserTemplate % {
			"specType" : typename,
			"csType" : nodeType["csTypename"],
			"csName" : upperCamelCase(typename),
			"childNodes" : ",\n\t\t\t".join(map(createCall, entries))
		}
	
	def createMethod(typename) :
		cat = nodeTypes[typename]["category"]
		if cat == "tree" :
			return createTreeMethod(typename)
		if cat == "terminal" :
			return createTerminalMethod(typename)
		if cat == "family" :
			return createFamilyMethod(typename)
		raise Exception()
	
	out = file(desibleParserOutputPath, "w")
	out.write(
		desibleParserFileTemplate % \
		"\n\n\t".join(map(createMethod, nodeTypes)))
	out.close()

def createDesibleSerializerMethods(nodeTypes) :
	def notFamilyType(typename) :
		return nodeTypes[typename]["category"] != "family"
	
	def createTerminalMethod(typename) :
		return desibleSerializerTerminalTemplate % {
			"csType" : nodeTypes[typename]["csTypename"]
		}
	
	def createTreeMethod(typename) :
		def createCall(child) :
			return "append<%s>(elem, node.@%s, %s);" % (
				nodeTypes[child["typename"]]["csTypename"],
				child["csName"],
				child["csLabel"]
			)
		
		csType = nodeTypes[typename]["csTypename"]
		entries = "\n\t\t".join(map(createCall, nodeTypes[typename]["layout"]))
		
		return desibleSerializerTreeTemplate % {
			"csType" : csType,
			"children" : entries
		}
	
	def createMethod(typename) :
		cat = nodeTypes[typename]["category"]
		if cat == "terminal" :
			return createTerminalMethod(typename)
		if cat == "tree" :
			return createTreeMethod(typename)
		return ""
	
	def createCase(typename) :
		return desibleSerializerCaseTemplate % {
			"typename" : typename,
			"csTypename" : nodeTypes[typename]["csTypename"]
		}

	out = file(desibleSerializerOutputPath, "w")
	out.write(desibleSerializerFileTemplate % {
		"methods" : "\n\n\t".join(map(createMethod, filter(notFamilyType, nodeTypes))),
		"cases" : "\n\t\t\t".join(map(createCase, filter(notFamilyType, nodeTypes)))
	})
	out.close()

def createDesexpParser(nodeTypes) :
	def createFamilyMethod(nodeType) :
		def createCase(entry) :
			return desexpFamilyCaseTemplate % {
				"specType" : entry,
				"csType" : nodeTypes[entry]["csTypename"],
				"csName" : upperCamelCase(entry)
			}
		
		assert nodeType["category"] == "family"
		template = desexpFamilyParserTemplate

		return template % {
			"csType" : nodeType["csTypename"],
			"csName" : upperCamelCase(nodeType["typename"]),
			"cases" : "\n\t\t\t".join(map(createCase, nodeType["members"])),
		}
	
	def createTreeMethod(nodeType) :
		templates = {
			"1" : "parseOne<%s>(parse%s, sexp)",
			"?" : "parseOpt<%s>(parse%s, sexp)",
			"*" : "parseMult0<%s>(parse%s, sexp)",
			"+" : "parseMult1<%s>(parse%s, sexp)"
		}
		
		def createCall(entry) :
			return templates[entry["count"]] % (
				nodeTypes[entry["typename"]]["csTypename"],
				upperCamelCase(entry["typename"]))

		assert nodeType["category"] == "tree"
		return desexpParserTemplate % {
			"csType" : nodeType["csTypename"],
			"csName" : upperCamelCase(nodeType["typename"]),
			"childNodes" : ",\n\t\t\t".join(map(createCall, nodeType["layout"])),
			"childCount" : len(nodeType["layout"])
		}
	
	def createTerminalMethod(nodeType) :
		assert nodeType["category"] == "terminal"
		return desexpTerminalParserTemplate % {
			"csType" : nodeType["csTypename"],
			"csName" : upperCamelCase(nodeType["typename"])
		}
	
	meths = []
	for (typename, nodeType) in nodeTypes.iteritems() :
		if nodeType["category"] == "family" :
			meths.append(createFamilyMethod(nodeType))
		elif nodeType["category"] == "tree" :
			meths.append(createTreeMethod(nodeType))
		else :
			meths.append(createTerminalMethod(nodeType))
	
	out = file(desexpParserOutputPath, "w")
	out.write(desexpParserFileTemplate % "\n\n\t".join(meths))
	out.close()

def createExecutor(nodeTypes) :
	def createCase(typename) :
		return executorCaseTemplate % {
			"typename" : typename,
			"csTypename" : nodeTypes[typename]["csTypename"]
		}
	
	members = nodeTypes["expression"]["members"]
	cases = map(createCase, filter(lambda x: x != "bundle", members))
	out = file(executorOutputPath, "w")
	out.write(executorFileTemplate % "\n\t\t\t".join(cases))
	out.close()

#----- entry point

nodeTypes = parseNodeTypes(inputPath)
setupNodeTypes(nodeTypes)

createNodeClasses(nodeTypes)
createDesibleParserMethods(nodeTypes)
createDesibleSerializerMethods(nodeTypes)
createDesexpParser(nodeTypes)
createExecutor(nodeTypes)
