"""
outputs the labels of layout entries with a count of * or +
this is helpful in ensuring plural names are not accidentially used
ignores layout entries without labels
"""

extractor = __import__('extract nodes')
nodeTypes = extractor.extractNodeTypes(extractor.inputPath)
treeNodeTypes = filter(lambda x: x["category"] == "tree", nodeTypes)
for type in treeNodeTypes :
	for entry in type["layout"] :
		if "label" not in entry :
			continue
		if entry["count"] in ["*", "+"] :
			print '"%s" in "%s"' % (
				entry["label"],
				type["typename"]
			)
