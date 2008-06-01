import os
import subprocess
import sys
import paths

argsTable = {
	"desible" : [
		"-path=%s" % os.path.join(paths.acridDir, "test.desible"),
		"-representation=desible",
		"-desible-warn-unhandled=true",
		"-desible-warn-allNS=true",
		"-test-desible-serializer=true"
	],
	"dextr-display" : [
		"-path=%s" % os.path.join(paths.acridDir, "test.dextr"),
		"-representation=dextr",
		"-dextr-parser=token-displayer"
	],
	"dextr-info" : [
		"-path=%s" % os.path.join(paths.acridDir, "test.dextr"),
		"-representation=dextr",
		"-dextr-parser=token-info-displayer"
	],
	"dextr-sablecc" : [
		"-path=%s" % os.path.join(paths.acridDir, "test.dextr"),
		"-representation=dextr",
		"-dextr-parser=SableCC"
	],
	"desexp" : [
		"-path=%s" % os.path.join(paths.acridDir, "test.desexp"),
		"-representation=desexp",
	]
}

def runTest(args, trace) :
	args = [
		"mono",
		"--debug",
		"--trace=program",
		os.path.join(paths.acridSourceDir, "bin/Debug/Desal Agent 001.exe"),
		"-print-tree=true",
	] + args
	
	if not trace : args.remove("--trace=program")
	
	subprocess.call("clear")
	process = subprocess.Popen(args, stderr=subprocess.PIPE)
	process.wait()
	print "-----"
	print "return code: %s" % process.returncode
	print "stderr:"
	print "-----"
	print process.stderr.read()
	return process.returncode


#----- entry point

if len(sys.argv) < 2 :
	print "Error: no mode specified"
	quit()

if len(sys.argv) > 3 :
	print "Error: too many arguments"
	quit()

mode = sys.argv[1]

#Python has no boolean parsing functionality
trace = sys.argv[2].lower() == "true" if (2 < len(sys.argv)) else False

if mode == "all" :
	winModes = []
	failModes = []
	for mode in argsTable :
		result = runTest(argsTable[mode], trace)
		if result == 0 :
			winModes.append(mode)
		else :
			failModes.append(mode)
	print "successful tests: %s" % ", ".join(winModes)
	print "failed tests: %s" % ", ".join(failModes)
elif mode in argsTable :
	runTest(argsTable[mode], trace)
else :
	print "Error: unknown mode"
	quit()
