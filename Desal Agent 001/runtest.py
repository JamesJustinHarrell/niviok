#!/usr/bin/python

"""
		"-output-desible=true",
		"-desible-output-path=desibleoutput.xml"
"""

import os
import subprocess
import sys

agentBase = "/media/files/Desal/Desal Agent 001"

argsTable = {
	"desible" : [
		"-path=%s" % os.path.join(agentBase, "test.desible"),
		"-print-tree=true",
		"-desible-warn-unhandled=true",
		"-desible-warn-allNS=true",
		"-representation=desible",
		"-test-desible-serializer=true"
	],
	"dextr-display" : [
		"-path=%s" % os.path.join(agentBase, "test.dextr"),
		"-print-tree=true",
		"-representation=dextr",
		"-dextr-parser=token-displayer"
	],
	"dextr-info" : [
		"-path=%s" % os.path.join(agentBase, "test.dextr"),
		"-print-tree=true",
		"-representation=dextr",
		"-dextr-parser=token-info-displayer"
	],
	"dextr-sablecc" : [
		"-path=%s" % os.path.join(agentBase, "test.dextr"),
		"-print-tree=true",
		"-representation=dextr",
		"-dextr-parser=SableCC"
	],
	"desexp" : [
		"-path=%s" % os.path.join(agentBase, "test.desexp"),
#		"-print-tree=true",
		"-representation=desexp",
	]
}

def runTest(args) :
	args = [
		"mono",
		"--debug",
		os.path.join(agentBase, "Desal Agent 001/bin/Debug/Desal Agent 001.exe")
	] + args
	
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

if len(sys.argv) > 2 :
	print "Error: too many arguments"
	quit()

mode = sys.argv[1]

if mode == "all" :
	winModes = []
	failModes = []
	for mode in argsTable :
		result = runTest(argsTable[mode])
		if result == 0 :
			winModes.append(mode)
		else :
			failModes.append(mode)
	print "successful tests: %s" % ", ".join(winModes)
	print "failed tests: %s" % ", ".join(failModes)
elif mode in argsTable :
	runTest(argsTable[mode])
else :
	print "Error: unknown mode"
	quit()

