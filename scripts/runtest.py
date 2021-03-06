"""
arguments:
<prog> [ mode [ trace ] ]

mode defaults to all
trace defaults to false

examples:
<prog>
<prog> all true
<prog> desible
"""

import os
import subprocess
import sys
import paths

executablePath = os.path.join(paths.solutionDir, "CommandLine/bin/Debug/CommandLine.exe")

def runTest_desible_1() :
	return runTest([
		"-path=%s" % os.path.join(paths.testsDir, "test1.desible"),
		"-representation=desible",
		"-desible-warn-unhandled=true",
		"-desible-warn-allNS=true"
	])

def runTest_desible_2() :
	#create Desible file
	result = subprocess.call([
		"mono",
		"--debug",
		executablePath,
		"-path=%s" % os.path.join(paths.testsDir, "test.toy"),
		"-representation=toy",
		"-run=false",
		"-output-desible=true",
		"-desible-output-path=%s" % os.path.join(paths.testsDir, "test2.desible") ])
	
	if result != 0 :
		print "unable to create Desible test file"
		exit(1)
	
	return runTest([
		"-path=%s" % os.path.join(paths.testsDir, "test2.desible"),
		"-representation=desible",
		"-desible-warn-unhandled=true",
		"-desible-warn-allNS=true" ])

def runTest_fujin() :
	return runTest([
		"-path=%s" % os.path.join(paths.testsDir, "test.fujin"),
		"-representation=fujin",
		"-fujin-parser=SableCC" ])

def runTest_ivan() :
	return runTest([
		"-path=%s" % os.path.join(paths.testsDir, "test.ivan"),
		"-representation=ivan" ])

def runTest_toy() :
	return runTest([
		"-path=%s" % os.path.join(paths.testsDir, "test.toy"),
		"-representation=toy" ])

def runTest(args) :
	traceCommand = "--trace=N:Acrid.Execution"
	args = [
		"mono",
		"--debug",
		traceCommand,
		executablePath,
#		"-print-tree=true",
	] + args
	
	if not trace : args.remove(traceCommand)
	
	subprocess.call("clear")
	process = subprocess.Popen(args, stderr=subprocess.PIPE)
	process.wait()
	print "-----"
	print "return code: %s" % process.returncode
	print "stderr:"
	print "-----"
	print process.stderr.read()
	return process.returncode

def runTestMode(mode) :
	functionName = "runTest_" + mode.replace("-", "_")
	result = globals()[functionName]()
	return result


#----- entry point

if len(sys.argv) > 3 :
	print "Error: too many arguments"
	exit(1)

if len(sys.argv) is 1 :
	mode = "all"
else :
	mode = sys.argv[1]

#Python has no boolean parsing functionality
trace = sys.argv[2].lower() == "true" if (2 < len(sys.argv)) else False

modes = []
for functionName in globals().copy() :
	if functionName.find("runTest_") == 0 :
		#e.g. runTest_foo_bar -> foo-bar
		modeName = functionName.replace("runTest_", "").replace("_", "-")
		modes.append(modeName)

if mode == "all" :
	winModes = []
	failModes = []
	for mode in modes :
		result = runTestMode(mode)
		if result == 0 :
			winModes.append(mode)
		else :
			failModes.append(mode)
	print "successful tests: %s" % ", ".join(winModes)
	print "failed tests: %s" % ", ".join(failModes)
elif mode in modes :
	runTestMode(mode)
else :
	print "Error: unknown mode"
	exit(1)

