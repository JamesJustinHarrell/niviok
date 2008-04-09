import os
import xml.dom.minidom as DOM

desalBase = "/media/files/Desal"
inputPath = os.path.join(desalBase,"nodes.xml")

doc = DOM.parse(inputPath)

def getSpecType(elem) :
	return elem.getElementsByTagName("spec-type")[0].firstChild.nodeValue.strip()

nodeTypeNames = map(getSpecType, doc.getElementsByTagName("node"))

nodeTypeNames.sort()

print "\n".join(nodeTypeNames)
