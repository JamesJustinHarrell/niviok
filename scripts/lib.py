import xml.dom.minidom as DOM

def childElements(elem) :
	return filter(isElement, elem.childNodes)

def descendantElements(elem) :
	return elem.getElementsByTagName("*")

def each(function, iterable) :
	for i in iterable :
		function(i)

def getElementsByAttribute(root, attrName, attrValue) :
	return [x for x in descendantElements(root)
		if x.hasAttribute(attrName) and x.getAttribute(attrName) == attrValue]

def hasAncestorElement(node, test) :
		if test(node) :
			return True
		if node.parentNode.nodeType == DOM.Node.ELEMENT_NODE :
			return hasAncestorElement(node.parentNode, test)
		return False

def isElement(elem) :
	return elem.nodeType == DOM.Node.ELEMENT_NODE

#remove comments and convert CDATA
#Python's DOM doesn't support Document.normalizeDocument()
def normalize(node) :
	document = node.ownerDocument
	parent = node.parentNode
	type = node.nodeType
	
	if type == node.DOCUMENT_NODE :
		for child in node.childNodes :
			if child.nodeType == node.COMMENT_NODE :
				node.removeChild(child)
		#xxx Python doesn't include the root element as a child of the document node
		normalize(node.documentElement)
	elif type == node.ELEMENT_NODE :
		each(normalize, node.childNodes)
		node.normalize()
	elif type == node.TEXT_NODE :
		pass
	elif type == node.CDATA_SECTION_NODE :
		textNode = document.createTextNode(node.data)
		parent.insertBefore(textNode, node)
		parent.removeChild(node)
	elif type == node.COMMENT_NODE :
		parent.removeChild(node)
	elif type == node.ENTITY_REFERENCE_NODE :
		raise Exception("unexpected entity reference")
	elif type == node.ENTITY_NODE :
		raise Exception("unexpected entity")
	else :
		raise Exception("unexpected node type: %s %s" % (type, node.toxml()))

def selectAll(node, selector) :
	return node.getElementsByTagName(selector)

def selectChild(node, selector) :
	return selectChildren(node, selector)[0]

def selectChildren(node, selector) :
	return filter(lambda x: x.tagName == selector, childElements(node))

def selectFirst(node, selector) :
	return selectAll(node, selector)[0]

#tells Python that xml:id attributes are IDs
#required for getElementById to work
def setXmlId(node) :
	if isElement(node) and node.hasAttribute("xml:id") :
		#xxx note that Python's version differs from the standard
		#the standard has a boolean parameter to turn ID status on/off
		node.setIdAttribute("xml:id")
	for child in node.childNodes :
		setXmlId(child)

def textValue(elem) :
	return "".join([x.nodeValue for x in elem.childNodes if x.nodeType == DOM.Node.TEXT_NODE])

#serializes @xmlDocument and writes to @outputPath
def writeXmlDocument(xmlDocument, outputPath) :
	markup = xmlDocument.toxml('utf-8')
	outFile = open(outputPath, "w")
	outFile.write(markup)
	outFile.close()
