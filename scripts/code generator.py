#----- library references

import os
import re
import sys
import string
import xml.dom.minidom as DOM
import paths
import lib
extractor = __import__('extract nodes')
import templates


#----- constants

pj = os.path.join
nodeClassesOutputPath = pj(paths.solutionDir,"NodeTypes/node classes auto.cs")
desibleParserOutputPath = pj(paths.solutionDir,"Desible/DesibleParserAuto.cs")
desibleSerializerOutputPath = pj(paths.solutionDir,"Desible/DesibleSerializerAuto.cs")
ivanGrammarInputPath = pj(paths.ivanSableccDir,"Ivan.sablecc.template")
ivanGrammarOutputPath = pj(paths.ivanSableccDir,"Ivan.sablecc")
ivanParserOutputPath = pj(paths.solutionDir,"Ivan/IvanParserAuto.cs")
toyParserOutputPath = pj(paths.solutionDir,"Toy/ToyParserAuto.cs")
executorOutputPath = pj(paths.solutionDir,"Execution/Executor auto.cs")


#----- global functions

def mapByCategory(nodeTypes, funcMap) :
	def performCallback(nodeType) :
		return funcMap[nodeType["category"]](nodeType)
	return map(performCallback, nodeTypes.values())

def isEnumNode(typename) :
	return typename in ['boolean', 'direction', 'member-status', 'member-type']

def lowerCamelCase(text) :
	upperCamel = upperCamelCase(text)
	return string.lower(upperCamel[0]) + upperCamel[1:]

def lowerLatin(text) :
	return text.replace("-", "").replace(" ", "").lower()

def upperCamelCase(text) :
	#a sequence of any character besides space and dash
	words = re.findall("[^ -]+", text)
	words = [string.upper(x[0]) + x[1:] for x in words]
	return "".join(words)

def getNodeTypesDict() :
	nodeTypesDict = {}
	nodeTypes = extractor.extractNodeTypes(extractor.inputPath)
	for nodeType in nodeTypes :
		nodeTypesDict[ nodeType["typename"] ] = nodeType
	return nodeTypesDict

def methFromCount(count) :
	return {
		"1" : "parseOne",
		"?" : "parseOpt",
		"*" : "parseMult0",
		"+" : "parseMult1"
	}[count]

#add csTypename, csFamilies, csName, csTagName, csLabel, csType ("cs" refers to C#)
def setupNodeTypes(nodeTypes) :
	#add some cs* keys
	for (typename, nodeType) in nodeTypes.iteritems() :
		nodeType["csName"] = upperCamelCase(typename)
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
				entry["csLabel"] = '"%s"' % entry["label"]
			else :
				entry["csName"] = lowerCamelCase(entry["typename"])
				entry["csTagName"] = '"%s"' % entry["typename"]
				if nodeTypes[entry["typename"]]["category"] == "family" :
					print entry
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

def titleLatin(text) :
	return text.replace("-", "").replace(" ", "").title()

def withoutCategory(nodeTypes, category) :
	return dict(filter(lambda x: x[1]["category"] != category, nodeTypes.items()))

def write(content, location) :
	outFile = file(location, "w")
	outFile.write(content)
	outFile.close()


#----- createFoo functions

def createNodeClasses(nodeTypes) :
	def createClass(nodeType) :
		def createField(child) :
			return "%(csType)s m_%(csName)s;" % child
		
		def createParameter(child) :
			return "%(csType)s @%(csName)s" % child
		
		def createAssignment(child) :
			return "m_%(csName)s = @%(csName)s;" % child
		
		def createGetter(child) :
			return templates.nodeGetter % child
		
		def getPrivateName(child) :
			return "m_%(csName)s" % child
	
		entries = nodeType["layout"]
		inheritStr = ", ".join(nodeType["csFamilies"])
		if inheritStr == "" :
			inheritStr = "INode"
	
		return templates.nodeClass % {
			"csType" : nodeType["csTypename"],
			"inherit" : inheritStr,
			"fields" : "\n\t".join(map(createField, entries)),
			"parameters" : ",\n\t".join(map(createParameter, entries)),
			"assignments" : "\n\t\t".join(map(createAssignment, entries)),
			"getters" : "\n\n\t".join(map(createGetter, entries)),
			"specType" : nodeType["typename"],
			"fieldList" : ",\n\t\t\t\t".join(map(getPrivateName, entries)) }
	
	treeNodeTypes = filter( lambda x: x["category"] == "tree", nodeTypes.values() )
	classes = map( createClass, treeNodeTypes )
	content = templates.nodeClassFile % "\n\n".join(classes)
	write( content, nodeClassesOutputPath )

def createDesibleParserMethods(nodeTypes) :
	def createFamilyMethod(nodeType) :
		def createCase(memberTypename) :
			return templates.desibleFamilyCase % nodeTypes[memberTypename]

		return templates.desibleFamilyParser % nodeType % \
			"\n\t\t\t".join(map(createCase, nodeType["members"]))
	
	def createTerminalMethod(nodeType) :
		return templates.desibleTerminalParser % nodeType
	
	def createTreeMethod(nodeType) :
		def createCall(entry) :
			return templates.desibleTreeParserCall % (
				methFromCount(entry["count"]),
				nodeTypes[entry["typename"]]["csTypename"],
				upperCamelCase(entry["typename"]),
				entry["csTagName"],
				entry["csLabel"] )

		return templates.desibleTreeParser % nodeType % \
			",\n\t\t\t".join(map(createCall, nodeType["layout"]))
	
	meths = mapByCategory(nodeTypes, {
		"family" : createFamilyMethod,
		"terminal" : createTerminalMethod,
		"tree" : createTreeMethod })
	content = templates.desibleParserFile % "\n\n\t".join(meths)
	write( content, desibleParserOutputPath )

def createDesibleSerializerMethods(nodeTypes) :	
	def createTerminalMethod(nodeType) :
		return templates.desibleSerializerTerminal % nodeType
	
	def createTreeMethod(nodeType) :
		def createCall(entry) :
			return "append<%s>(elem, node.@%s, %s);" % (
				nodeTypes[entry["typename"]]["csTypename"],
				entry["csName"],
				entry["csLabel"]
			)

		return templates.desibleSerializerTree % nodeType % \
			"\n\t\t".join(map(createCall, nodeType["layout"]))
	
	def createCase(nodeType) :
		return templates.desibleSerializerCase % nodeType
	
	nonFamilyTypes = withoutCategory(nodeTypes, "family")
	meths = mapByCategory(nonFamilyTypes, {
		"terminal" : createTerminalMethod,
		"tree" : createTreeMethod })
	cases = map(createCase, nonFamilyTypes.values())
	content = templates.desibleSerializerFile % {
		"methods" : "\n\n\t".join(meths),
		"cases" : "\n\t\t\t".join(cases) }
	write( content, desibleSerializerOutputPath )

def createIvanGrammar(nodeTypes) :
	def createProduction(nodeType) :
		def createOption(memberTypename) :
			return templates.ivanGrammarFamilyOption % {
				"familyName" : familyName,
				"optionName" : lowerLatin(memberTypename) }
		
		familyName = lowerLatin(nodeType["typename"])
		options = map(createOption, nodeType["members"])
		return templates.ivanGrammarFamilyProduction % {
			"familyName" : familyName,
			"options" : "\n\t\t| ".join(options) }

	def createFamilyANode(nodeType) :
		def createOption(memberTypename) :
			return "{%(name)s} %(name)s" % {
				"name" : lowerLatin(memberTypename) }
		
		options = map(createOption, nodeType["members"])
		return lowerLatin(nodeType["typename"]) + "\n\t\t= " + "\n\t\t| ".join(options) + " ;"

	def createTreeANode(nodeType) :
		def createEntry(entry) :
			rv = ""
			if "label" in entry :
				rv = "[%s]:" % lowerLatin(entry["label"])
			rv = rv + lowerLatin(entry["typename"])
			if entry["count"] in ["?", "*", "+"] :
				rv = rv + entry["count"]
			return rv
		
		entries = map(createEntry, nodeType["layout"])
		return templates.ivanGrammarTreeANode % {
			"name" : lowerLatin(nodeType["typename"]),
			"entries" : " ".join(entries) }

	productions = [createProduction(x) for x in nodeTypes.values() if x["category"] == "family"]
	nonTerminalNodes = withoutCategory(nodeTypes, "terminal")
	anodes = mapByCategory(nonTerminalNodes, {
		"family" : createFamilyANode,
		"tree" : createTreeANode
	})
	
	infile = file(ivanGrammarInputPath)
	template = infile.read()
	infile.close()
	
	content = templates.warning + "\n\n" + template % {
		"familyproductions" : "\n\n\t".join(productions),
		"astanodes" : "\n\n\t".join(anodes) }
	write( content, ivanGrammarOutputPath )

def createIvanParser(nodeTypes) :
	def createFamilyMethod(nodeType) :
		def createIf(memberTypename) :
			memberType = nodeTypes[memberTypename]
			return {
				"tree" : templates.ivanFamilyIfTree,
				"terminal" : templates.ivanFamilyIfTerminal
			}[ memberType["category"] ] % memberType % {
				"parentSableName" : titleLatin(nodeType['typename']),
				"childSableName" : titleLatin(memberType['typename']) }
		
		return templates.ivanFamilyParser % nodeType % {
			"sableName" : "P" + titleLatin(nodeType['typename']),
			"ifs" : "\n\t\t".join(map(createIf, nodeType["members"])) }
	
	def createTerminalMethod(nodeType) :
		template = templates.ivanTerminalParser
		if isEnumNode(nodeType['typename']) :
			template = templates.ivanTerminalEnumParser
		return template % nodeType % {
			"sableName" : titleLatin(nodeType['typename']) }
	
	def createTreeMethod(nodeType) :		
		def sableTypename(typename) :
			cat = nodeTypes[typename]['category']
			title = titleLatin(typename)
			if cat == 'family' or isEnumNode(typename) :
				return 'P' + title
			if cat == 'terminal' :
				return 'T' + title
			return 'A' + title
		
		def createCall(entry) :
			if "label" in entry :
				d = titleLatin(entry["label"])
			else :
				d = titleLatin(entry["typename"])
			return templates.ivanTreeParserCall % (
				methFromCount(entry['count']),
				sableTypename(entry['typename']),
				nodeTypes[entry['typename']]['csTypename'],
				nodeTypes[entry['typename']]['csName'],
				'' if entry['count'] in ['*', '+'] else '(%s)' % sableTypename(entry['typename']),
				d)
		
		return templates.ivanTreeParser % nodeType % {
			"sableName" : titleLatin(nodeType['typename']),
			"calls" : ",\n\t\t\t".join(map(createCall, nodeType["layout"])) }
	
	meths = mapByCategory(nodeTypes, {
		"family" : createFamilyMethod,
		"terminal" : createTerminalMethod,
		"tree" : createTreeMethod })
	content = templates.ivanParserFile % "\n\n\t".join(meths)
	write( content, ivanParserOutputPath )

def createToyParser(nodeTypes) :
	def createFamilyMethod(nodeType) :
		def createCase(memberTypename) :
			return templates.toyFamilyCase % nodeTypes[memberTypename]

		return templates.toyFamilyParser % nodeType % \
			"\n\t\t\t".join(map(createCase, nodeType["members"]))
	
	def createTerminalMethod(nodeType) :
		return templates.toyTerminalParser % nodeType
	
	def createTreeMethod(nodeType) :
		def createCall(entry) :
			return templates.toyTreeParserCall % (
				methFromCount(entry['count']),
				nodeTypes[entry["typename"]]["csTypename"],
				upperCamelCase(entry["typename"]))

		return templates.toyTreeParser % nodeType % {
			"childNodes" : ",\n\t\t\t".join(map(createCall, nodeType["layout"])),
			"childCount" : len(nodeType["layout"]) }
	
	meths = mapByCategory(nodeTypes, {
		"family" : createFamilyMethod,
		"terminal" : createTerminalMethod,
		"tree" : createTreeMethod })
	content = templates.toyParserFile % "\n\n\t".join(meths)
	write( content, toyParserOutputPath )

def createExecutor(nodeTypes) :
	def createCase(typename) :
		return templates.executorCase % nodeTypes[typename]
	
	cases = map(createCase, nodeTypes["expression"]["members"])
	content = templates.executorFile % "\n\t\t\t".join(cases)
	write( content, executorOutputPath )


#----- entry point

nodeTypes = getNodeTypesDict()
setupNodeTypes(nodeTypes)
lib.each( lambda x: x(nodeTypes), [
	createNodeClasses,
	createDesibleParserMethods,
	createDesibleSerializerMethods,
	createIvanGrammar,
	createIvanParser,
	createToyParser,
	createExecutor ])
