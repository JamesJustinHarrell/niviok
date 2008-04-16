"""
outputs the labels of layout entries with a count of * or +
this is helpful in ensuring plural names are not accidentially used
"""

import os
import xml.dom.minidom as DOM
from lib import *

desalBase = "/media/files/Desal"
inputPath = os.path.join(desalBase,"nodes.xml")

doc = DOM.parse(inputPath)

for child in selectAll(doc, "entry") :
	count = textValue(selectFirst(child, "count")).strip()
	if len(selectAll(child, "label")) == 0 :
		continue
	label = textValue(selectFirst(child, "label"))
	if count in ["*", "+"] :
		print '"%s" in "%s"' % (
			label,
			textValue(selectFirst(child.parentNode, "typename"))
		)
