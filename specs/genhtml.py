# coding: utf-8

import os
import re
from exceptions import Exception
import libxml2 #see /var/lib/python-support/python2.5/libxml2.py for documentation
import xml.dom.minidom as dom

#constants
specsDir = "/media/files/Desal/specs"
inputDir = specsDir + '/docbook'
outputDir = specsDir + '/html'
docbookDir = "/media/files/other/docbook"
fileBases = (
	'Desal Semantics',
	'Desal XML Representation',
	'Desal Text Representation'
)
tagMap = {
	'chapter' : 'div',
	'glossdef' : 'dd',
	'glossentry' : None,
	'glosslist' : 'dl',
	'glossterm' : 'dt',
	'info' : None,
	'legalnotice' : 'p',
	'link' : 'a',
	'member' : 'li',
	'orderedlist' : 'ol',
	'para' : 'p',
	'screen' : 'pre',
	'section' : 'div',
	'simplelist' : 'ul',
	'subtitle' : 'h2',
	'table' : 'table',
	'tbody' : 'tbody',
	'td' : 'td',
	'term' : 'dt',
	'th' : 'th',
	'titleabbrev' : 'h2',
	'tr' : 'tr',
	'varlistentry' : None,
	'variablelist' : 'dl'
}
knownRoles = ( 'exec', 'eval', 'family-members', 'layout', 'process' )
xhtmlNS = "http://www.w3.org/1999/xhtml"
domImpl = dom.getDOMImplementation()
relaxngSchema = libxml2.relaxNGNewParserCtxt("%s/docbook.rng" % docbookDir).relaxNGParse()

#adds a tagName element to parentOutElement and returns the new element
#if inElement given, its contents copied to that new element
def transferElement(parentOutElement, tagName, inElement) :
	outDocument = parentOutElement.ownerDocument
	newOutElement = outDocument.createElement(tagName)
	parentOutElement.appendChild(newOutElement)
	if inElement != None :
		newOutElement = transferAttributes(inElement, newOutElement)
		copyContent(inElement, newOutElement)
	return newOutElement

#adds an appropriate HTML attribute/element for each docbook attribute
#returns the lowest element
def transferAttributes(source, destination) :
	rv = destination
	i = 0
	while i < source.attributes.length :
		name = source.attributes.item(i).name
		value = source.getAttribute(name)
		
		if name == "xml:id" :
			destination.setAttribute("id", value)
			#required for getElementById to work
			#note that the W3C version has a second parameter
			destination.setIdAttribute("id")
		elif name == "role" :
			if value not in knownRoles :
				print "WARNING: unknown role %s" % value
			destination.setAttribute("class", value)
		elif name == "linkend" :
			if destination.tagName == 'a' :
				link = destination
			else :
				link = destination.ownerDocument.createElement("a")
				destination.appendChild(link)
				rv = link
			link.setAttribute("href", '#' + value)
		elif name == "xmlns" or name == "version":
			pass
		else :
			print "WARNING: Unrecognized attribute " + name
		
		i += 1
	return rv

def setupLayoutMember(member) :
	doc = member.ownerDocument
	tokens = member.firstChild.data.split(' ')
	member.removeChild(member.firstChild)
	if len(tokens[0]) > 1 :
		typeName = tokens[0]
		textAfter = tokens[1:]
	else :
		member.appendChild( doc.createTextNode(tokens[0] + ' ') )
		typeName = tokens[1]
		textAfter = tokens[2:]
	typeName = typeName.lower()
	link = doc.createElement('link')
	link.setAttribute('linkend', 'node.' + typeName)
	link.appendChild(doc.createTextNode(typeName))
	member.appendChild(link)
	member.appendChild( doc.createTextNode(' ' + ' '.join(textAfter)) )

def copyElement(docbookElement, htmlElement) :
	tagName = docbookElement.tagName
	
	if 'screen' == tagName or 'programlisting' == tagName :
		textNode = docbookElement.firstChild
		textNode.data = textNode.data.strip()
	elif 'member' == tagName :
		role = docbookElement.parentNode.getAttribute('role')
		if 'layout' == role or 'family-members' == role :
			setupLayoutMember(docbookElement)
	
	if tagName in tagMap :
		if tagMap[tagName] == None :
			copyContent(docbookElement, htmlElement)
		else :
			transferElement(htmlElement, tagMap[tagName], docbookElement)
	elif tagName == 'listitem' and \
	'orderedlist' == docbookElement.parentNode.tagName :
		transferElement(htmlElement, 'li', docbookElement)
	elif tagName == 'listitem' and \
	'varlistentry' == docbookElement.parentNode.tagName :
		transferElement(htmlElement, 'dd', docbookElement)
	elif 'copyright' == tagName :
		element = transferElement(htmlElement, 'p', None)
		appendText(u'Copyright © ', element)
		for node in docbookElement.childNodes :
			if node.nodeType == node.ELEMENT_NODE :
				copyContent(node, element)
			elif node.nodeType == node.TEXT_NODE :
				copyText(node, element)
			else :
				raise Exception('unknown node type')	
	elif 'programlisting' == tagName :
		element = transferElement(htmlElement, 'code', docbookElement)
		element.setAttribute('xml:space', 'preserve')
	elif 'title' == tagName :
		parentName = docbookElement.parentNode.tagName
		if ["book", "chapter", "section"].count(parentName) == 0 :
			raise ("Error: %s elements cannot have titles" % parentName).__str__()
		transferElement(htmlElement, "h%d" % docbookElement.nestLevel, docbookElement)
	else :
		print "WARNING: DocBook '%s' element not recognized (child of '%s')" \
		% ( tagName, docbookElement.parentNode.tagName )
		copyContent(docbookElement, htmlElement)

def appendText(text, htmlElement) :
	htmlElement.appendChild(
		htmlElement.ownerDocument.createTextNode(
			text ))

def copyText(docbookNode, htmlElement) :
	appendText( docbookNode.data, htmlElement )

#copies child nodes of docbook element to html element
def copyContent(docbookElement, htmlElement) :
	for docbookNode in docbookElement.childNodes :
		if docbookNode.nodeType == docbookNode.ELEMENT_NODE :
			copyElement(docbookNode, htmlElement)
		elif docbookNode.nodeType == docbookNode.TEXT_NODE :
			copyText(docbookNode, htmlElement)
		elif docbookNode.nodeType == docbookNode.COMMENT_NODE :
			pass
		elif docbookNode.nodeType == docbookNode.CDATA_SECTION_NODE :
			copyText(docbookNode, htmlElement)
		else :
			print "WARNING: unknown node type " + `docbookNode.nodeType`

#tocList is HTML, sectionElement is DocBook
def setupSection(tocList, sectionElement, prefix, index) :
	try :
		titleText = sectionElement\
			.getElementsByTagName("title")[0]\
			.firstChild.data
	except :
		"WARNING: section doesn't have title"
		return
	
	htmlDocument = tocList.ownerDocument
	#title is a DocBook element
	title = sectionElement.getElementsByTagName("title")[0]
	if title == None or title.parentNode != sectionElement :
		print "WARNING: chapter/section with " + \
		"id '%s' doesn't have a title" % \
		chapterElement.getAttribute("xml:id")
	listItem = htmlDocument.createElement("li")
	link = htmlDocument.createElement("a")
	link.setAttribute("href", "#" + sectionElement.getAttribute("xml:id"))
	copyContent(title, link)
	listItem.appendChild(link)
	tocList.appendChild(listItem)
	numberChain = prefix + `index` + '.'
	link.insertBefore(
		htmlDocument.createTextNode(numberChain + ' '),
		link.firstChild )
	title.insertBefore(
		htmlDocument.createTextNode(numberChain + ' '),
		title.firstChild )
	if sectionElement.tagName == "chapter" :
		title.insertBefore(
			htmlDocument.createTextNode('Chapter '),
			title.firstChild )
	title.nestLevel = numberChain.count('.')
	
	childTocList = htmlDocument.createElement("ol")
	index = 1
	for child in sectionElement.childNodes :
		if child.nodeType == child.ELEMENT_NODE and child.tagName == "section" :
			setupSection(childTocList, child, numberChain, index)
			index += 1
	if childTocList.childNodes.length != 0 :
		listItem.appendChild(childTocList)

def createTocAndSetTitles(docbookRoot, htmlDocument) :
	toc = htmlDocument.createElement("ol")
	toc.setAttribute("class", "toc")
	chapters = docbookRoot.getElementsByTagName("chapter")
	index = 1
	for chapter in chapters :
		setupSection(toc, chapter, "", index)
		index += 1
	return toc

def checkLinks(htmlDocument) :
	links = htmlDocument.getElementsByTagName('a')
	for link in links :
		if link.hasAttribute('href') == False :
			raise Exception("link doesn't have href attribute")
		href = link.getAttribute('href')
		if href[0] == '#' :
			ID = href[1:]
			element = htmlDocument.getElementById(ID)
			if element == None :
				print "WARNING: no element with ID '%s'" % ID

def createHtmlDocument(docbookDocument) :
	docbookRoot = docbookDocument.documentElement
	
	htmlDocument = domImpl.createDocument(xhtmlNS, "html", None)
	
	htmlRoot = htmlDocument.documentElement
	htmlHead = htmlDocument.createElement("head")
	htmlTitle = htmlDocument.createElement("title")
	htmlStyleLink = htmlDocument.createElement("link")
	htmlBody = htmlDocument.createElement("body")
	
	#note: python's DOM impl won't output xmlns otherwise
	htmlRoot.setAttribute("xmlns", xhtmlNS)
	htmlStyleLink.setAttribute("href", "styling.css")
	htmlStyleLink.setAttribute("rel", "stylesheet")
	htmlStyleLink.setAttribute("type", "text/css")
	htmlRoot.appendChild(htmlHead)
	htmlHead.appendChild(htmlTitle)
	htmlHead.appendChild(htmlStyleLink)
	htmlRoot.appendChild(htmlBody)
	
	docbookTitle = docbookRoot.getElementsByTagName("title")[0]
	docbookTitle.nestLevel = 1
	copyContent(docbookTitle, htmlTitle)
	
	toc = createTocAndSetTitles(docbookRoot, htmlDocument)
	
	copyContent(docbookRoot, htmlBody)
	
	htmlBody.insertBefore(
		toc, htmlBody.getElementsByTagName("div")[0] )
		
	checkLinks(htmlDocument)
	
	return htmlDocument

def writeXmlDocument(xmlDocument, outputPath) :
	markup = xmlDocument.toxml('utf-8')
	outFile = open(outputPath, "w")
	outFile.write(markup)
	outFile.close()

#document is a libxml2 XML document
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

def schematronValidate(path) :
	command = '''
		xsltproc -o "%(db)s/temp.xsl" "%(db)s/skeleton1-5.xsl" "%(db)s/docbook.sch"
		xsltproc --xinclude "%(db)s/temp.xsl" "%(path)s"
		rm "%(db)s/temp.xsl"
	''' % { 'db' : docbookDir, 'path' : path }
	command = command.replace('\t', '')
	#print command
	os.system(command)

def normalize(node) :
	document = node.ownerDocument
	parent = node.parentNode
	type = node.nodeType
	
	if node.ELEMENT_NODE == type :
		for child in node.childNodes :
			normalize(child)
		node.normalize()
	elif node.TEXT_NODE == type :
		pass
	elif node.CDATA_SECTION_NODE == type :
		textNode = document.createTextNode(node.data)
		parent.insertBefore(textNode, node)
		parent.removeChild(node)
	elif node.COMMENT_NODE == type :
		parent.removeChild(node)
	else :
		raise "error " + `type`
		#ENTITY_REFERENCE_NODE = 5
		#ENTITY_NODE = 6

def getDocbookDom(path) :
	docbookDocument = libxml2.parseFile(path)
	docbookDocument.xincludeProcess()
	relaxngValidate(docbookDocument)
	schematronValidate(path)
	
	docbookDom = dom.parseString(docbookDocument.serialize())
	normalize(docbookDom.documentElement)
	if docbookDom.documentElement.getAttribute("version") != "5.0" :
		raise Exception("docbook not labeled as version 5.0")
	return docbookDom

def crushCommand(command) :
	#replace all whitespace substrings with a single space
	return re.sub('\\s+', ' ', command).strip()

def createHtmlFile(fileBase) :
	inputPath = "%s/docbook/%s.docbook" % (specsDir, fileBase)
	xhtmlOutputPath = "%s/html/%s.xhtml" % (specsDir, fileBase)
	htmlOutputPath = "%s/html/%s.html" % (specsDir, fileBase)
	
	print fileBase + ":"
	
	docbookDocument = getDocbookDom(inputPath)
	htmlDocument = createHtmlDocument(docbookDocument)
	#note: python's DOM refuses to output a DTD (or @xmlns without being told)
	writeXmlDocument(htmlDocument, xhtmlOutputPath)

	command = '''
		/usr/local/bin/tidy
		--quiet yes
		--output-file "%s"
		--char-encoding utf8
		--input-xml yes
		--output-html yes
		--doctype strict
		--indent auto
		--indent-spaces 4
		--wrap 0
		"%s"
	''' % (htmlOutputPath, xhtmlOutputPath)
	os.system( crushCommand(command) )
	#xxx tidy refuses to output HTML
	#note: wrapping will wrap text in code elements, so it's disabled
	#note: tidy adds newlines to code elements, which show up as gaps

	print "done\n"

#entry point
for fileBase in fileBases:
	createHtmlFile(fileBase)