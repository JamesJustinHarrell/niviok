import os
import re #regular expressions

#constants
specsDir = "/media/files/Desal/specs"
inputDir = specsDir + '/docbook'
outputDir = specsDir + '/html-xslt'
stylesheetPath = specsDir + '/transform.xsl'
fileBases = (
	'Desal Semantics',
	'Desal XML Representation',
	'Desal Text Representation'
)

def updateHtml(fileBase) :
	inputPath = inputDir + '/' + fileBase + '.docbook'
	outputPath = outputDir + '/' + fileBase + '.html'
	
	command = 'xsltproc --xinclude -o "%s" "%s" "%s"' \
	% ( outputPath, stylesheetPath, inputPath )
	os.system(command)
	
	print fileBase + " updated"

#entry point
for fileBase in fileBases:
	updateHtml(fileBase)