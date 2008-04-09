import subprocess
import os

desalBase = "/media/files/Desal"
agentBase = os.path.join(desalBase, "Desal Agent 001")

def call(args) :
	result = subprocess.call(args)
	if result != 0 :
		print "FAILURE"
		exit()

#generate HTML from DocBook
call([
	"python",
	os.path.join(desalBase, "specs/genhtml.py")
])

#generate nodes.xml from DocBook
call([
	"python",
	os.path.join(desalBase, "extract nodes.py")
])

#generate C# code
call([
	"python",
	os.path.join(agentBase, "code generator.py")
])

#generate Desexp parser with CocoR
#WARNING: Coco/R returns 0 even on failure
#WARNING: the runcoco.sh script changes the working directory
call([
	"bash",
	os.path.join(agentBase, "CocoR/runcoco.sh")
])

#generate Desible grammar
call([
	"python",
	os.path.join(agentBase, "SableCC/generateSableGrammar.py")
])

#generate Desible parser with SableCC
call([
	"bash",
	os.path.join(agentBase, "SableCC/run3b3alt.sh")
])

#compile Desal Agent 001
#xxx mdtool is broken

#run all Desal Agent 001 tests using "runtest.py"
call([
	"python",
	os.path.join(agentBase, "runtest.py"),
	"all"
])
