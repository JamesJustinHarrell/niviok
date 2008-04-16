import xml.dom.minidom as DOM

def childElements(elem) :
	return filter(isElement, elem.childNodes)

def descendantElements(elem) :
	return elem.getElementsByTagName("*")

def hasAncestorElement(node, test) :
		if test(node) :
			return True
		if node.parentNode.nodeType == DOM.Node.ELEMENT_NODE :
			return hasAncestorElement(node.parentNode, test)
		return False

def isElement(elem) :
	return elem.nodeType == DOM.Node.ELEMENT_NODE

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

