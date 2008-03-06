clear
mono \
	"Desal Interpreter 001/bin/Debug/Desal Interpreter 001.exe" \
	-path="/media/files/Desal/Desal 001/test.desible" \
	-print-tree=true \
	-desible-warn-unhandled=true \
	-desible-warn-allNS=true \
	-representation=desible
	2> stderr.tmp
export ExitCode=$?
echo "----- exit status: "
echo $ExitCode
echo "----- stderr: "
cat stderr.tmp
