import xml.dom.minidom as DOM

def childElements(elem) :
	return filter(isElement, elem.childNodes)

def descendantElements(elem) :
	return elem.getElementsByTagName("*")

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

#tells Python that xml:id attributes are IDs
#required for getElementById to work
def setXmlId(node) :
	if isElement(node) and node.hasAttribute("xml:id") :
		#xxx note that Python's version differs from the standard
		#the standard has a boolean parameter to turn ID status on/off
		node.setIdAttribute("xml:id")
	for child in node.childNodes :
		setXmlId(child)

def selectAll(node, selector) :
	return node.getElementsByTagName(selector)

def selectFirst(node, selector) :
	return selectAll(node, selector)[0]

def selectChildren(node, selector) :
	return filter(lambda x: x.tagName == selector, childElements(node))

def selectChild(node, selector) :
	return selectChildren(node, selector)[0]

def textValue(elem) :
	return "".join([x.nodeValue for x in elem.childNodes if x.nodeType == DOM.Node.TEXT_NODE])

