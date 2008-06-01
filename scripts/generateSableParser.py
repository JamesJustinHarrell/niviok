import os
import subprocess
import sys
import paths

def call(args) :
	result = subprocess.call(args)
	if result != 0 :
		print "FAILURE"
		exit()

if len(sys.argv) < 2 :
	print "Error: no version specified (3.2 or 3b3alt)"
	quit()

if len(sys.argv) > 2 :
	print "Error: too many arguments"
	quit()

version = sys.argv[1]

call([
	"python",
	os.path.join(paths.scriptsDir, "generateSableGrammar.py")
])

if version == "3.2" :
	call([
		"java",
		"-jar",
		os.path.join(paths.sableDir, "sablecc-3.2/lib/sablecc.jar"),
		"-d",
		os.path.join(paths.sableDir, "out32"),
		os.path.join(paths.sableDir, "dextr.sablecc")
	])
	#xxx remove using Python API
	call([
		"rm",
		"-r",
		os.path.join(paths.sableDir, "out32/Dextr")
	])

else :
	assert version == "3b3alt"
	call([
		"java",
		"-jar",
		os.path.join(paths.sableDir, "sablecc-3b3 alt/lib/sablecc.jar"),
		"-o",
		os.path.join(paths.sableDir, "test2"),
		os.path.join(paths.sableDir, "dextr.sablecc")
	])
