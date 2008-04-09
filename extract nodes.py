#extracts the node layout information from the Desam spec
#and writes it out to an XML file

import os
import xml.dom.minidom as DOM

desalBase = "/media/files/Desal"
inputPath = os.path.join(desalBase,"specs/docbook/Desal Semantics - Nodes.docbook")
outputPath = os.path.join(desalBase,"nodes.xml")

#@return: array of NodeInfo objects
#NodeInfo: { "specType":String, "specSuperTypes":[String], "children":[Child] }
#Child: { "count":String, "specType":String, "specName":String }
def extractAllNodeInfo(filePath) :
	def definesNode(elem) :
		return \
			elem.hasAttribute("xml:id") and \
			elem.getAttribute("xml:id").find("node.") == 0
	
	def definesLayout(node) :
		return \
			node.nodeType == DOM.Node.ELEMENT_NODE and \
			node.hasAttribute("role") and \
			node.getAttribute("role") == "layout"
	
	def extractNodeInfo(elem) :
		info = {
			"specType" : getTitle(elem),
			"specSuperTypes" : getSuperTypes(elem)
		}
		layouts = filter(definesLayout, elem.childNodes)
		if len(layouts) > 0 :
			info["children"] = [parseChild(x.firstChild.nodeValue) for x in layouts[0].getElementsByTagName("member")]
		return info
	
	def getTitle(elem) :
		if elem.nodeType != DOM.Node.ELEMENT_NODE :
			raise Exception("@elem must be an element")
		childTitles = elem.getElementsByTagName("title")
		if len(childTitles) == 0 :
			return getTitle(elem.parentNode)
		return childTitles[0].firstChild.nodeValue
	
	def getSuperTypes(elem) :
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

#xxx	if specType.find("declare") > -1 :
#xxx		return "INode_Declaration"

		if specType in ["using", "import", "expose"] :
			return ["scope-alteration"]
		if isNonExec(elem) :
			return []
		if isNestedNode(elem.parentNode) :
			return []
		else :
			return ["expression"]

	def parseChild(text) :
		child = {}
		tokens = text.split(" ")
		
		#count (e.g. *)
		if tokens[0] in ["*", "+", "?"] :
			child["count"] = tokens.pop(0)
		else :
			child["count"] = "1"
		
		#spec type (e.g. "alpha-beta-gamma")
		child["specType"] = tokens.pop(0)
		
		#spec name (e.g. "alpha beta gamma")
		if len(tokens) > 0 :
			child["specName"] = " ".join(tokens)

		return child

	doc = DOM.parse(filePath)
	nodeElems = filter(definesNode, doc.getElementsByTagName("*"))
	return map(extractNodeInfo, nodeElems)

def outputLayouts(layouts, path) :
	def append(parentNode, tagName, content) :
		doc = parentNode.ownerDocument
		child = doc.createElement(tagName)
		if content != None :
			child.appendChild(doc.createTextNode(content))
		parentNode.appendChild(child)
		return child
	
	def appendLayout(layout, doc) :
		nodeElem = append(doc.documentElement, "node", None)
		append(nodeElem, "spec-type", layout["specType"])
		for x in layout["specSuperTypes"] :
			append(nodeElem, "spec-super-type", x)
		if "children" in layout :
			for x in layout["children"] :
				appendChild(x, nodeElem)
	
	def appendChild(child, parent) :
		childElem = append(parent, "child", None)
		append(childElem, "count", child["count"])
		append(childElem, "spec-type", child["specType"])
		if "specName" in child :
			append(childElem, "spec-name", child["specName"])
	
	doc = DOM.parseString("<root/>")
	for x in layouts :
		appendLayout(x, doc)
	file = open(path, "w")
	file.write(doc.toprettyxml())
	file.close()

outputLayouts(extractAllNodeInfo(inputPath), outputPath)
