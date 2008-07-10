import os
import subprocess
import paths

#Python used instead of a bash script to take advantage of paths.py
#must change directory because CocoR breaks on paths that contain spaces

startingDir = os.getcwd()
os.chdir(paths.toyCocorDir)
exitCode = subprocess.call([
	"cococs",
	"Toy.atg",
	"-namespace",
	"Acrid.Toy.CocoR"
])
os.chdir(startingDir)
exit(exitCode)
