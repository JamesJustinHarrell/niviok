"""
extracts the node layout information from the Desam spec
and writes it out to an XML file
"""

import os
import xml.dom.minidom as DOM
import lib
import paths

inputPath = os.path.join(paths.specsDir, "docbook/Niviok 1.0 Specification - Node types.docbook")
outputPath = os.path.join(paths.niviokDir, "nodes.xml")

#@return: List<NodeType>
#NodeType: { category:"family", typename:String, members:List<String> }
#NodeType: { category:"tree", typename:String, layout:List<LayoutEntry> }
#NodeType: { category:"terminal", typename:String }
#LayoutEntry: { count:String, typename:String, ?label:String }
def extractNodeTypes(filePath) :
	def definesNode(elem) :
		return elem.getAttribute("xml:id").find("node.") == 0
	
	def isFamily(node) :
		return lib.hasAncestorElement(
			node, lambda x: x.getAttribute("xml:id") == "family-node-types" )
	
	def isTree(elem) :
		for child in lib.childElements(elem) :
			if child.getAttribute("role") == "layout" :
				return True
		return False
	
	def getTitle(elem) :
		assert definesNode(elem)
		return lib.textValue(elem.getElementsByTagName("title")[0])

	def isExpressionNodeType(elem) :
		def isNonExec(elem) :
			return lib.hasAncestorElement(
				elem, lambda x: x.getAttribute("xml:id") == "non-executable" )
		
		def isNestedNode(elem) :
			return lib.hasAncestorElement(
				elem, lambda x: x.getAttribute("xml:id").find("node.") == 0 )
		
		assert definesNode(elem)

		return \
			not isFamily(elem) and \
			not isNonExec(elem) and \
			not isNestedNode(elem.parentNode)
	
	def extractFamilyType(sectElem) :
		def findTypes() :
			listElem = filter(
				lambda x: x.getAttribute("role") == "family-members",
				lib.childElements(sectElem) )[0]
			def getTypes(memberElem) :
				#defines a node, and is not defined within the section of another node
				def isTopNode(elem) :
					return definesNode(elem) and not lib.hasAncestorElement(elem.parentNode, definesNode)
				assert memberElem.tagName == "member"
				if memberElem.hasAttribute("linkend") and \
				memberElem.getAttribute("linkend").find("node.") != 0 :
					id = memberElem.getAttribute("linkend")
					sect = memberElem.ownerDocument.getElementById(id)
					if sect == None :
						raise Exception("no element with id '%s'" % id)
					return map(getTitle, filter(isTopNode, lib.descendantElements(sect)))
				return [lib.textValue(memberElem)]
			rv = []
			for member in lib.childElements(listElem) :
				rv += getTypes(member)
			return rv
		
		typename = getTitle(sectElem)
		types = findTypes()
		
		return {
			"category" : "family",
			"typename" : typename,
			"members" : types
		}
	
	def extractTreeType(sectElem) :
		def parseChild(member) :
			text = lib.textValue(member)
			tokens = text.split(" ")
			child = {}
			
			#count (e.g. *)
			if tokens[0] in ["*", "+", "?"] :
				child["count"] = tokens.pop(0)
			else :
				child["count"] = "1"
			
			#typename (e.g. "alpha-beta-gamma")
			child["typename"] = tokens.pop(0)
			
			#label (e.g. "alpha beta gamma")
			if len(tokens) > 0 :
				child["label"] = " ".join(tokens)
	
			return child
		
		layoutElem = filter(
			lambda x: x.getAttribute("role") == "layout",
			lib.childElements(sectElem) )[0]
		members = layoutElem.getElementsByTagName("member")
		
		return {
			"category" : "tree",
			"typename" : getTitle(sectElem),
			"layout" : map(parseChild, members)
		}
	
	def extractTerminalType(elem) :
		return {
			"category" : "terminal",
			"typename" : getTitle(elem)
		}
	
	def extractNodeType(elem) :
		assert definesNode(elem)

		if isFamily(elem) :
			return extractFamilyType(elem)
		elif isTree(elem) :
			return extractTreeType(elem)
		else :
			return extractTerminalType(elem)

	doc = DOM.parse(filePath)
	lib.setXmlId(doc)
	nodeTypes = map(extractNodeType, filter(definesNode, lib.descendantElements(doc)))
	
	#ensure no types are listed twice (it's happened)
	typenames = set(map(lambda x: x["typename"], nodeTypes))
	assert len(nodeTypes) == len(typenames)

	return nodeTypes

def serializeNodeTypes(nodeTypes, path) :
	def append(parentNode, tagName, content) :
		doc = parentNode.ownerDocument
		child = doc.createElement(tagName)
		if content != None :
			child.appendChild(doc.createTextNode(content))
		parentNode.appendChild(child)
		return child

	def appendToRoot(tagName, nodeType) :
		elem = append(doc.documentElement, tagName, None)
		append(elem, "typename", nodeType["typename"])
		return elem

	def appendFamilyNodeType(nodeType) :
		elem = appendToRoot("family", nodeType)
		membersElem = append(elem, "members", None)
		for type in nodeType["members"] :
			append(membersElem, "typename", type)
	
	def appendTreeNodeType(nodeType) :
		elem = appendToRoot("tree", nodeType)
		for layoutEntry in nodeType["layout"] :
			entryElem = append(elem, "entry", None)
			append(entryElem, "count", layoutEntry["count"])
			append(entryElem, "typename", layoutEntry["typename"])
			if "label" in layoutEntry :
				append(entryElem, "label", layoutEntry["label"])

	def appendNodeType(nodeType) :
		if nodeType["category"] == "family" :
			appendFamilyNodeType(nodeType)
		elif nodeType["category"] == "tree" :
			appendTreeNodeType(nodeType)
		else :
			assert nodeType["category"] == "terminal"
			appendToRoot("terminal", nodeType)
	
	doc = DOM.parseString("<node-types/>")
	for nodeType in nodeTypes :
		appendNodeType(nodeType)
	file = open(path, "w")
	doc = DOM.parseString(doc.toprettyxml())
	for elem in doc.getElementsByTagName("*") :
		if len(elem.getElementsByTagName("*")) == 0 and elem.firstChild != None :
			elem.firstChild.nodeValue = elem.firstChild.nodeValue.strip()
	file.write(doc.toxml())
	file.close()

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
			"typename" : lib.textValue(lib.selectChild(elem, "typename")),
			"members" : map(lib.textValue, lib.selectChildren(lib.selectChild(elem, "members"), "typename"))
		}
	
	def parseTree(elem) :
		def parseEntry(entryElem) :
			child = {
				"count" : lib.textValue(lib.selectChild(entryElem, "count")),
				"typename" : lib.textValue(lib.selectChild(entryElem, "typename"))
			}
			if len(lib.selectChildren(entryElem, "label")) > 0 :
				child["label"] = lib.textValue(lib.selectChild(entryElem, "label"))
			return child
		assert elem.tagName == "tree"
		return {
			"category" : "tree",
			"typename" : lib.textValue(lib.selectChild(elem, "typename")),
			"layout" : map(parseEntry, lib.selectChildren(elem, "entry"))
		}
	
	def parseTerminal(elem) :
		assert elem.tagName == "terminal"
		return {
			"category" : "terminal",
			"typename" : lib.textValue(lib.selectChild(elem, "typename"))
		}

	def parseNode(elem) :
		tag = elem.tagName
		if tag == "family" :
			return parseFamily(elem)
		elif tag == "tree" :
			return parseTree(elem)
		else :
			assert tag == "terminal"
			return parseTerminal(elem)

	return map(parseNode, lib.childElements(DOM.parse(filePath).documentElement))


#----- entry point
if __name__ == "__main__" :
	serializeNodeTypes(extractNodeTypes(inputPath), outputPath)
