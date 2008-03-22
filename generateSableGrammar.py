import re
import sys
import xml.dom.minidom as DOM

inputGrammarPath = "other/Dextr grammar.txt"
outputGrammarPath = "test.sablecc"

outputTemplate = """
package Dextr.SableCC;

Tokens
%(tokens)s

Productions
%(productions)s

"""

def getElemText(doc, name) :
	return doc.getElementsByTagName(name)[0].firstChild.wholeText

doc = DOM.parse(inputGrammarPath)

tokenText = getElemText(doc, "token-types")
tokens = re.findall(r'\S+', tokenText)
tokens = filter(
	(lambda x : x.find("*") == -1),
	tokens )

keywordsText = getElemText(doc, "keywords")
keywords = re.findall(r'\S+', keywordsText)
map(
	(lambda x : tokens.append('KEYWORD_%s = "%s"' % (x.upper(), x))),
	keywords)

tokenText = "\n".join(tokens)

productions = getElemText(doc, "productions")

#replace { and [ with (
productions = re.sub(r'(\s)(\{|\[)', r'\1(', productions)

#replace } with )*
productions = re.sub(r'(\s)(\})', r'\1)*', productions)

#replace ] with )?
productions = re.sub(r'(\s)(\])', r'\1)?', productions)

file = open(outputGrammarPath, "w")
file.write(outputTemplate % {
	"tokens" : tokenText,
	"productions" : productions
})
file.close()
