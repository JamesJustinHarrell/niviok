# coding: utf-8

import os
import re
from exceptions import Exception
import libxml2 #see /var/lib/python-support/python2.5/libxml2.py for documentation
import xml.dom.minidom as dom
from random import random
import subprocess
import datetime
import paths
import sys
import lib
extractor = __import__('extract nodes')

#constants
inputDir = os.path.join(paths.specsDir,'docbook')
outputDir = os.path.join(paths.specsDir,'html')
knownRoles = ( 'exec', 'family-members', 'layout', 'process', 'xxx', 'rationale' )
xhtmlNS = "http://www.w3.org/1999/xhtml"
domImpl = dom.getDOMImplementation()
relaxngSchema = libxml2.relaxNGNewParserCtxt(os.path.join(paths.docbookDir,"docbook.rng")).relaxNGParse()

fileBases = (
	'Niviok 1.0 Specification',
	'Desal XML Representation',
	'Desal Text Representation'
)

tagMap = {
	'chapter' : 'div',
	'glossdef' : 'dd',
	'glossentry' : None,
	'glosslist' : 'dl',
	'glossterm' : 'dt',
	'holder' : None,
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
	'variablelist' : 'dl',
	'year' : None
}

def warn(message) :
	print "WARNING: %s" % message

#adds a @tagName element to @parentOutElement
#if @inElement given, its contents copied to that new element
#returns the @tagName element or the most deeply nested element from transfering attributes
def appendNewElement(parentOutElement, tagName, inElement) :
	outDocument = parentOutElement.ownerDocument
	newOutElement = outDocument.createElement(tagName)
	parentOutElement.appendChild(newOutElement)
	if inElement != None :
		newOutElement = transferAttributes(inElement, newOutElement)
		copyContent(inElement, newOutElement)
	return newOutElement

#adds an appropriate HTML attribute/element for each docbook attribute
#returns the most nested element
def transferAttributes(source, destination) :
	rv = destination
	i = 0
	while i < source.attributes.length :
		name = source.attributes.item(i).name
		value = source.getAttribute(name)
		
		if name == "xml:id" :
			destination.setAttribute("id", value)
			#tell python that "id" attribute is of type ID
			destination.setIdAttribute("id")
		elif name == "role" :
			if value not in knownRoles :
				warn("unknown role %s" % value)
			destination.setAttribute("class", value)
		elif name == "linkend" :
			if destination.tagName == 'a' :
				link = destination
			else :
				link = destination.ownerDocument.createElement("a")
				destination.appendChild(link)
				rv = link
			link.setAttribute("href", '#' + value)
		elif name in ["xmlns", "version"] :
			pass
		else :
			warn("unrecognized attribute %s" % name)
		
		i += 1
	return rv

#links the node type to #node.type
#@member is a <member/> element
def setupLayoutMember(member) :
	doc = member.ownerDocument
	#assumes member contains a single text node
	tokens = member.firstChild.data.split(' ')
	member.removeChild(member.firstChild)
	if len(tokens[0]) > 1 :
		typeName = tokens[0]
		textAfter = tokens[1:]
	else : #token[0] is e.g. ? * +
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

#warns if entire text is indented
def checkPreText(text) :
	for line in text.splitlines() :
		if line != "" and line == line.strip() :
			return
	warn("entirely indented text: %s" % text)

#copy information from Docbook document to HTML document
#translates the information from Docbook to HTML
def copyElement(docbookElement, htmlElement) :
	tagName = docbookElement.tagName
	
	if tagName in ['screen', 'programlisting'] :
		textNode = docbookElement.firstChild
		checkPreText(textNode.data)
		textNode.data = textNode.data.strip()
	elif 'member' == tagName :
		role = docbookElement.parentNode.getAttribute('role')
		if role == 'layout' :
			setupLayoutMember(docbookElement)
		elif role == 'family-members' :
			setupFamilyMember(docbookElement)
	
	if tagName in tagMap :
		if tagMap[tagName] == None :
			copyContent(docbookElement, htmlElement)
		else :
			appendNewElement(htmlElement, tagMap[tagName], docbookElement)
	elif tagName == 'listitem' and \
	'orderedlist' == docbookElement.parentNode.tagName :
		appendNewElement(htmlElement, 'li', docbookElement)
	elif tagName == 'listitem' and \
	'varlistentry' == docbookElement.parentNode.tagName :
		appendNewElement(htmlElement, 'dd', docbookElement)
	elif 'copyright' == tagName :
		element = appendNewElement(htmlElement, 'p', None)
		appendText(element, u'Copyright © ')
		copyContent(docbookElement, element)
	elif 'programlisting' == tagName :
		element = appendNewElement(htmlElement, 'code', docbookElement)
		element.setAttribute('xml:space', 'preserve')
	elif 'title' == tagName :
		parentName = docbookElement.parentNode.tagName
		if ["book", "chapter", "section"].count(parentName) == 0 :
			raise Exception("Error: %s elements cannot have titles" % parentName)
		appendNewElement(htmlElement, "h%d" % docbookElement.nestLevel, docbookElement)
	else :
		warn( "DocBook '%s' element not recognized (child of '%s')" \
			% ( tagName, docbookElement.parentNode.tagName ) )
		copyContent(docbookElement, htmlElement)

#appends a text node to @element
def appendText(element, text) :
	element.appendChild(element.ownerDocument.createTextNode(text))

#copies child nodes of docbook element to html element
def copyContent(docbookElement, htmlElement) :
	for docbookNode in docbookElement.childNodes :
		if docbookNode.nodeType == docbookNode.ELEMENT_NODE :
			copyElement(docbookNode, htmlElement)
		elif docbookNode.nodeType == docbookNode.TEXT_NODE :
			htmlElement.appendChild(docbookNode.cloneNode(False))
		elif docbookNode.nodeType == docbookNode.COMMENT_NODE :
			pass
		elif docbookNode.nodeType == docbookNode.CDATA_SECTION_NODE :
			htmlElement.appendChild(docbookNode.cloneNode(False))
		else :
			warn("unknown node type %s" % docbookNode.nodeType)

#@tocList is HTML, @sectionElement is DocBook
def setupSection(tocList, sectionElement, prefix, index) :
	def warnOfAbsense() :
		warn(
			"chapter/section with id '%s' doesn't have a title" % \
			sectionElement.getAttribute("xml:id") )
	
	childTitleElements = sectionElement.getElementsByTagName("title")
	if childTitleElements.length == 0 :
		warnOfAbsense()
		return
	titleElement = childTitleElements[0]
	if titleElement.parentNode != sectionElement :
		warnOfAbsense()
		return
	titleText = titleElement.firstChild.data

	htmlDocument = tocList.ownerDocument
	listItem = htmlDocument.createElement("li")
	link = htmlDocument.createElement("a")
	sectionID = sectionElement.getAttribute("xml:id")
	if sectionID == '' :
		warn( "section with title '%s' doesn't have ID" % titleText )
		sectionID = str(random())
		sectionElement.setAttribute("xml:id", sectionID)
	link.setAttribute("href", "#%s" % sectionID)
	appendText(link, titleText)
	listItem.appendChild(link)
	tocList.appendChild(listItem)
	numberChain = "%s%s." % (prefix, index)
	link.insertBefore(
		htmlDocument.createTextNode("%s " % numberChain),
		link.firstChild )
	titleElement.insertBefore(
		htmlDocument.createTextNode("%s " % numberChain),
		titleElement.firstChild )
	if sectionElement.tagName == "chapter" :
		titleElement.insertBefore(
			htmlDocument.createTextNode('Chapter '),
			titleElement.firstChild )
	titleElement.nestLevel = numberChain.count('.')
	
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

#try to ensure all links refer to valid anchors
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
				warn("no element with ID '%s'" % ID)

#translate @docbookDocument into an HTML document
def createHtmlDocument(docbookDocument) :
	docbookRoot = docbookDocument.documentElement
	
	htmlDocument = domImpl.createDocument(xhtmlNS, "html", None)
	
	htmlRoot = htmlDocument.documentElement
	htmlHead = htmlDocument.createElement("head")
	htmlTitle = htmlDocument.createElement("title")
	htmlStyleLink = htmlDocument.createElement("link")
	htmlScript = htmlDocument.createElement("script")
	htmlBody = htmlDocument.createElement("body")
	
	#note: python's DOM impl won't output xmlns otherwise
	htmlRoot.setAttribute("xmlns", xhtmlNS)
	htmlStyleLink.setAttribute("href", "styling.css")
	htmlStyleLink.setAttribute("rel", "stylesheet")
	htmlStyleLink.setAttribute("type", "text/css")
	htmlScript.appendChild(htmlDocument.createTextNode("""
		/* hack around Gecko bug */
		function checkHash() {
			if( oldHash != document.location.hash ) {
				oldHash = document.location.hash;
				causeReflow();
			}
		}
		function causeReflow() {
			var s = document.documentElement.style;
			s.position = 'relative';
			setTimeout( function(){ s.position = ''; }, 10 );
		}
		var oldHash = document.location.hash;
		setInterval( checkHash, 100 );
	"""));
	htmlRoot.appendChild(htmlHead)
	htmlHead.appendChild(htmlTitle)
	htmlHead.appendChild(htmlStyleLink)
	htmlHead.appendChild(htmlScript)
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

#serializes @xmlDocument and writes to @outputPath
def writeXmlDocument(xmlDocument, outputPath) :
	markup = xmlDocument.toxml('utf-8')
	outFile = open(outputPath, "w")
	outFile.write(markup)
	outFile.close()

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
#xxx very messy -- move temp.xsl path to top and use Python API to delete
def schematronValidate(path) :
	subprocess.call([
		"xsltproc",
		"-o",
		os.path.join(paths.docbookDir, "temp.xsl"),
		os.path.join(paths.docbookDir, "skeleton1-5.xsl"),
		os.path.join(paths.docbookDir, "docbook.sch") ])
	subprocess.call([
		"xsltproc",
		"--xinclude",
		os.path.join(paths.docbookDir, "temp.xsl"),
		path ])
	subprocess.call([
		"rm",
		os.path.join(paths.docbookDir, "temp.xsl") ])	

#remove comments and convert CDATA
def normalize(node) :
	document = node.ownerDocument
	parent = node.parentNode
	type = node.nodeType
	
	if type == node.ELEMENT_NODE :
		for child in node.childNodes :
			normalize(child)
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
		raise Exception("unexpected node type: %s" % type)

#converts @@foo to <link linkend="node.foo">foo</link>
#replaces $$$foo$$$
def setupMarkup(markup) :
	markup = re.sub(
		r"@@([a-z\-]+)",
		r'<link linkend="node.\1">\1</link>', markup)
	markup = markup.replace(
		"$$$CURRENTDATE$$$",
		datetime.date.today().isoformat())
	markup = markup.replace(
		"$$$NODECOUNT$$$",
		str(len(extractor.extractNodeTypes(extractor.inputPath))))
	if markup.count("$$$") :
		for f in re.findall(r"(\$\$\$.*\$\$\$)", markup) :
			warn("couldn't replace " + f)
	return markup

#valid Docbook document at @path and generate DOM
def getDocbookDom(path) :
	docbookDocument = libxml2.parseFile(path)
	docbookDocument.xincludeProcess()
	relaxngValidate(docbookDocument)
	schematronValidate(path)
	
	docbookDom = dom.parseString(setupMarkup(docbookDocument.serialize()))
	normalize(docbookDom.documentElement)
	if docbookDom.documentElement.getAttribute("version") != "5.0" :
		raise Exception("docbook not labeled as version 5.0")
	return docbookDom

def setupDocbookDoc(dbDoc) :
	def definesNode(elem) :
		return elem.getAttribute("xml:id").find("node.") == 0
	
	def isFamily(node) :
		return hasAncestorElement(
			node, lambda x: x.getAttribute("xml:id") == "family-node-types" )
	
	def isTree(elem) :
		for child in childElements(elem) :
			if child.getAttribute("role") == "layout" :
				return True
		return False
	
	def getTitle(elem) :
		return textValue(elem.getElementsByTagName("title")[0])
	
	try :
		sectElem = getElementsByAttribute(dbDoc, "xml:id", "terminal-node-types")[0]
	except :
		return
		
	listElem = sectElem.getElementsByTagName("simplelist")[0]
	for elem in descendantElements(dbDoc) :
		if definesNode(elem) and not isFamily(elem) and not isTree(elem) :
			memberElem = appendNewElement(listElem, "member", None)
			appendText(memberElem, getTitle(elem))
			memberElem.setAttribute("linkend", elem.getAttribute("xml:id"))

#output an XHTML and HTML document from the specified Docbook document
def createHtmlFile(fileBase) :
	print fileBase + ":"
	
	inputPath = os.path.join(inputDir, "%s.docbook" % fileBase)
	xhtmlOutputPath = os.path.join(outputDir, "%s.xhtml" % fileBase)
	htmlOutputPath = os.path.join(outputDir, "%s.html" % fileBase)
	
	docbookDocument = getDocbookDom(inputPath)
	setupDocbookDoc(docbookDocument)
	htmlDocument = createHtmlDocument(docbookDocument)
	#note: python's DOM refuses to output a DTD (or @xmlns without being told)
	writeXmlDocument(htmlDocument, xhtmlOutputPath)

	#create HTML document from XHTML document
	#xxx tidy refuses to output HTML
	#note: wrapping will wrap text in <code> elements, so it's disabled
	#note: tidy adds newlines to <code> elements, which show up as gaps
	subprocess.call([
		"tidy",
		"--quiet", "yes",
		"--output-file", htmlOutputPath,
		"--char-encoding", "utf8",
		"--input-xml", "yes",
		"--output-html", "yes",
		"--doctype", "strict",
		"--indent", "auto",
		"--indent-spaces", "4",
		"--wrap", "0",
		xhtmlOutputPath ])

	print "done\n"

#entry point
for fileBase in fileBases:
	createHtmlFile(fileBase)
