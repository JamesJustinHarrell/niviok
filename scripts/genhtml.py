# coding: utf-8

#external libraries
import xml.dom.minidom as dom
from exceptions import Exception
import os
from random import random
import re
import subprocess
import sys

#my libraries
import lib
import paths
extractor = __import__('extract nodes')
processdocbook = __import__('process docbook')

#constants
nodeTypes = extractor.extractNodeTypes(extractor.inputPath)
inputDir = os.path.join(paths.specsDir,'docbook processed')
outputDir = os.path.join(paths.specsDir,'html')
knownRoles = ( 'allowance', 'enum', 'exec', 'family-members', 'layout', 'process', 'rationale', 'xxx' )
xhtmlNS = "http://www.w3.org/1999/xhtml"
domImpl = dom.getDOMImplementation()
fileBases = (
	'Niviok 1.0 Specification',
	'Desible',
	'Fujin'
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
#xxx not namespace aware
def transferAttributes(source, destination) :
	rv = destination
	
	for attr in source.attributes.values() :
		if attr.name == "xml:id" :
			destination.setAttribute("id", attr.value)
			#tell python that "id" attribute is of type ID
			destination.setIdAttribute("id")
		elif attr.name == "role" :
			if attr.value not in knownRoles :
				warn("unknown role '%s'" % attr.value)
			destination.setAttribute("class", attr.value)
		elif attr.name == "linkend" :
			if destination.tagName == 'a' :
				link = destination
			else :
				link = destination.ownerDocument.createElement("a")
				destination.appendChild(link)
				rv = link
			link.setAttribute("href", '#' + attr.value)
		elif attr.name.find("xmlns") == 0 or attr.name == "version" :
			pass
		else :
			warn("unrecognized attribute '%s'" % attr.localName)

	return rv

#copy information from Docbook document to HTML document
#translates the information from Docbook to HTML
def copyElement(docbookElement, htmlElement) :
	tagName = docbookElement.tagName
		
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
	
	#note: Python's DOM impl won't output @xmlns attribute otherwise
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

#output an XHTML and HTML document from the specified Docbook document
def createHtmlFile(fileBase) :
	print fileBase + ":"
	
	inputPath = os.path.join(inputDir, "%s.docbook" % fileBase)
	xhtmlOutputPath = os.path.join(outputDir, "%s.xhtml" % fileBase)
	htmlOutputPath = os.path.join(outputDir, "%s.html" % fileBase)
	
	docbookDocument = dom.parse(inputPath)
	htmlDocument = createHtmlDocument(docbookDocument)
	
	#note: Python's DOM implementation refuses to output a DTD
	#note: Python's DOM implementation only outputs an @xmlns when the attribute is explicitly added
	lib.writeXmlDocument(htmlDocument, xhtmlOutputPath)

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
result = subprocess.call(['python', 'process docbook.py'])
if result != 0 :
	exit(1)
lib.each(createHtmlFile, fileBases)
