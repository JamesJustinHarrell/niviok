#constants
specsDir="/media/files/Desal/specs"
inputDir="$specsDir/docbook"
outputDir="$specsDir/html-xslt"
stylesheetPath="$specsDir/transform.xsl"
fileBases=(
	'Desal Semantics'
	'Desal XML Representation'
	'Desal Text Representation'
)

updateHtml () {
	fileBase="$1"
	inputPath="$inputDir/$fileBase.docbook"
	outputPath="$outputDir/$fileBase.html"
	
	xsltproc --xinclude -o "$outputPath" "$stylesheetPath" "$inputPath"
	
	echo "$fileBase updated"
}

#entry point
for fileBase in "${fileBases[@]}" ; do
	updateHtml "$fileBase"
done