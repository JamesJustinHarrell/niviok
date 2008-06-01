"""
outputs a list of node typenames
"""

extractor = __import__('extract nodes')

nodeTypes = extractor.extractNodeTypes(extractor.inputPath)
typenames = map(lambda x: x["typename"], nodeTypes)
typenames.sort()
print "%s nodes:" % len(typenames)
print "\n".join(typenames)
