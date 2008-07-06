#external libraries
import xml.dom.minidom as dom
import datetime
import libxml2 #see /var/lib/python-support/python2.5/libxml2.py for documentation
import os
import re
import subprocess
import tempfile

#my libraries
import lib
import paths
extractor = __import__('extract nodes')

#constants
inputDir = os.path.join(paths.specsDir,'docbook')
outputDir = os.path.join(paths.specsDir,'docbook processed')
nodeTypes = extractor.extractNodeTypes(extractor.inputPath)
fileBases = (
	'Niviok 1.0 Specification',
	'Desible',
	'Fujin'
)
relaxngSchema = libxml2.relaxNGNewParserCtxt(os.path.join(paths.docbookDir,"docbook.rng")).relaxNGParse()

#ensure the Docbook document is valid
#@document is a libxml2 XML document
def relaxngValidate(document) :
	def relaxngOutput(type, message) :
		print "RelaxNG %s" % type
		e = libxml2.lastError()
		print "\t%s:%s" % (e.file(), e.line())
		print "\t%s" % message.strip()
	
	def relaxngWarning(message, unused) :
		relaxngOutput("warning", message)
	
	def relaxngError(message, unused) :
		relaxngOutput("error", message)
	
	relaxngValidator = relaxngSchema.relaxNGNewValidCtxt()
	relaxngValidator.setValidityErrorHandler(relaxngWarning, relaxngError)
	result = relaxngValidator.relaxNGValidateDoc(document)
	if result == -1 : #0 means no problems, 1+ means problem, -1 is internal problem
		print "internal error while validating document with RelaxNG"

#ensure the Docbook document at @path is valid
def schematronValidate(path) :
	tempFilePath = tempfile.mkstemp()[1]
	subprocess.call([
		"xsltproc",
		"-o",
		tempFilePath,
		os.path.join(paths.docbookDir, "skeleton1-5.xsl"),
		os.path.join(paths.docbookDir, "docbook.sch") ])
	subprocess.call([
		"xsltproc",
		"--xinclude",
		tempFilePath,
		path ])

#parse and validate Docbook document at @path
def parseValidateDocbook(path) :
	docbookDocument = libxml2.parseFile(path)
	docbookDocument.xincludeProcess()
	relaxngValidate(docbookDocument)
	schematronValidate(path)
	
	docbookDom = dom.parseString(docbookDocument.serialize())
	if docbookDom.documentElement.getAttribute("version") != "5.0" :
		raise Exception("docbook not labeled as version 5.0")
	return docbookDom

#calls the callback on all text nodes
def for_text_nodes(callback, node) :
	document = node.ownerDocument
	parent = node.parentNode
	type = node.nodeType
	
	if type == node.DOCUMENT_NODE :
		for_text_nodes(callback, node.documentElement)
	elif type == node.ELEMENT_NODE :
		for child in node.childNodes :
			for_text_nodes(callback, child)
	elif type == node.TEXT_NODE :
		callback(node)
	else :
		raise Exception("unexpected node type: %s" % type)

#replace matches of @pattern in @textNode with nodes given by @callback
def replaceBy(textNode, pattern, callback) :
	doc = textNode.ownerDocument
	data = textNode.data
	parent = textNode.parentNode
	startIndex = 0
	for match in re.finditer(pattern, data) :
		parent.insertBefore(doc.createTextNode(data[startIndex : match.start()]), textNode)
		parent.insertBefore(callback(match), textNode)
		startIndex = match.end()
	parent.insertBefore(doc.createTextNode(data[startIndex:]), textNode)
	parent.removeChild(textNode)

#links the node type to #node.type
#@member is a <member/> element
def setupLayoutMember(member) :
	assert( len(member.childNodes) == 1 )
	assert( member.childNodes[0].nodeType == member.TEXT_NODE )
	doc = member.ownerDocument
	tokens = lib.textValue(member).split(' ')
	member.removeChild(member.firstChild)
	if len(tokens[0]) > 1 :
		typeName = tokens[0]
		textAfter = tokens[1:]
	else :
		assert( tokens[0] in ['?', '*', '+'] )
		member.appendChild( doc.createTextNode(tokens[0] + ' ') )
		typeName = tokens[1]
		textAfter = tokens[2:]
	typeName = typeName.lower()
	link = doc.createElement('link')
	link.setAttribute('linkend', 'node.' + typeName)
	link.appendChild(doc.createTextNode(typeName))
	member.appendChild(link)
	member.appendChild( doc.createTextNode(' ' + ' '.join(textAfter)) )

#@member is a <member/> element
def setupFamilyMember(member) :
	if not member.hasAttribute("linkend") :
		setupLayoutMember(member)

def processPreElem(elem) :
	#warns if entire text is indented
	def checkText(text) :
		for line in text.splitlines() :
			if line != "" and line == line.strip() :
				return
		print "WARNING: entirely indented text: %s" % text
	
	try :
		assert( len(elem.childNodes) == 1 )
		assert( elem.childNodes[0].nodeType == elem.TEXT_NODE )
	except AssertionError, e :
		print elem.toxml()
		raise e
	
	textNode = elem.firstChild
	checkText(textNode.data)
	textNode.data = textNode.data.strip()

def handleTextReplacements(textNode) :
	data = textNode.data
	
	#handle $$CURRENTDATE
	data = data.replace(
		"$$CURRENTDATE",
		datetime.date.today().isoformat())
		
	#handle $$NODECOUNT
	data = data.replace(
		"$$NODECOUNT",
		str(len(nodeTypes)))
		
	#handle additional $$FOO
	if data.count("$$") :
		warn("couldn't replace all $$FOO placeholders")
	
	textNode.data = data

def processDocbook(doc) :
	lib.normalize(doc)
	
	for_text_nodes(handleTextReplacements, doc)
	
	#convert @@foo to <link linkend="node.foo">foo</link>
	def handleAtAt(match) :
		text = match.group(1)
		elem = doc.createElement('link')
		elem.setAttribute('linkend', 'node.%s' % text)
		elem.appendChild(doc.createTextNode(text))
		return elem
	for_text_nodes(
		lambda x: replaceBy(x, r'@@([a-z\-]+)', handleAtAt),
		doc)

	#convert ##foo to <link linkend="def.foo">foo</link>
	def handleHashHash(match) :
		elem = doc.createElement('link')
		elem.setAttribute('linkend', 'def.%s' % match.group(2))
		elem.appendChild(doc.createTextNode(match.group(1)))
		return elem
	for_text_nodes(
		lambda x: replaceBy(x, r'##(([a-z\-]+)(:?s))', handleHashHash),
		doc)

	for glossterm in doc.getElementsByTagName("glossterm") :
		glossterm.setAttribute("xml:id", "def.%s" % lib.textValue(glossterm).replace(" ", "-"))
	
	specTitle = lib.textValue(doc.getElementsByTagName("title")[0])
	if specTitle == "Niviok 1.0 Specification" :
		terminalTypenames = sorted( x["typename"] for x in nodeTypes if x["category"] == "terminal" )
		listElem = lib.getElementsByAttribute(doc, "xml:id", "terminal-node-types-list")[0]
		for typename in terminalTypenames :
			memberElem = doc.createElement("member")
			listElem.appendChild(memberElem)
			memberElem.appendChild(doc.createTextNode(typename))
			memberElem.setAttribute("linkend", "node.%s" % typename)

	for elem in doc.getElementsByTagName("screen") :
		processPreElem(elem)
	for elem in doc.getElementsByTagName("programlistinge") :
		processPreElem(elem)

	for elem in doc.getElementsByTagName("member") :
		role = elem.parentNode.getAttribute('role')
		if role == 'layout' :
			setupLayoutMember(elem)
		elif role == 'family-members' :
			setupFamilyMember(elem)

def main() :
	for fileBase in fileBases :
		inputPath = os.path.join(inputDir, "%s.docbook" % fileBase)
		outputPath = os.path.join(outputDir, "%s.docbook" % fileBase)
		docbookDocument = parseValidateDocbook(inputPath)
		processDocbook(docbookDocument)
		lib.writeXmlDocument(docbookDocument, outputPath)

if __name__ == "__main__" :
	main()
