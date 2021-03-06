import os
import subprocess
import sys
import tempfile

import paths

def call(args) :
	result = subprocess.call(args)
	if result != 0 :
		print "FAILURE"
		exit(1)

if len(sys.argv) < 2 :
	print "Error: no version specified (3.2 or 3b3alt)"
	exit(1)

if len(sys.argv) > 2 :
	print "Error: too many arguments"
	exit(1)

version = sys.argv[1]

call([
	"python",
	os.path.join(paths.scriptsDir, "generate Fujin SableCC grammar.py")
])

#This version is only used because its error messages are sometimes more helpful.
#It only outputs Java code, so the code it generates is not useful.
if version == "3.2" :
	outputDir = tempfile.mkdtemp()
	call([
		"java",
		"-jar",
		os.path.join(paths.sablecc32Dir, "lib/sablecc.jar"),
		"-d",
		outputDir,
		os.path.join(paths.fujinSableccDir, "Fujin.sablecc")
	])

else :
	assert version == "3b3alt"
	call([
		"java",
		"-jar",
		os.path.join(paths.sablecc3b3altDir, "lib/sablecc.jar"),
		"-d",
		os.path.join(paths.fujinSableccDir, "output"),
		"-t",
		"csharp",
		os.path.join(paths.fujinSableccDir, "Fujin.sablecc")
	])
