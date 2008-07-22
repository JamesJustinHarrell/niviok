import os
import subprocess
import sys
import tempfile

import paths

result = subprocess.call([
	"java",
	"-jar",
	os.path.join(paths.sablecc3b3altDir, "lib/sablecc.jar"),
	"-d",
	os.path.join(paths.ivanSableccDir, "output"),
	"-t",
	"csharp",
	os.path.join(paths.ivanSableccDir, "Ivan.sablecc")
])

if result != 0 :
	print "FAILURE"
	exit(1)
