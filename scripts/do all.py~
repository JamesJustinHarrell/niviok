import subprocess
import os
import paths

def call(args) :
	result = subprocess.call(args)
	if result != 0 :
		print "FAILURE"
		exit()

#generate HTML from DocBook
call([
	"python",
	os.path.join(paths.scriptsDir, "genhtml.py")
])

#generate C# code
call([
	"python",
	os.path.join(paths.scriptsDir, "code generator.py")
])

#generate Toy parser with CocoR
call([
	"python",
	os.path.join(paths.scriptsDir, "runcoco.py")
])

#generate Fujin parser with SableCC
call([
	"python",
	os.path.join(paths.scriptsDir, "generateSableParser.py"),
	"3b3alt"
])

#compile Acrid
call([
	"python",
	os.path.join(paths.scriptsDir, "runmdtoolbuild.py")
])

#run all Acrid tests using "runtest.py"
call([
	"python",
	os.path.join(paths.scriptsDir, "runtest.py"),
	"all"
])

#not used, but I guess I might as well keep it updated
#generate nodes.xml from DocBook
call([
	"python",
	os.path.join(paths.scriptsDir, "extract nodes.py")
])
