clear
mono \
	"Desal Interpreter 001/bin/Debug/Desal Interpreter 001.exe" \
	-path="/media/files/Desal/Desal 001/test.dextr" \
	-print-tree=true \
	-representation=dextr \
	-dextr-parser=token-info-displayer \
	2> stderr.tmp
export ExitCode=$?
echo "----- exit status: "
echo $ExitCode
echo "----- stderr: "
cat stderr.tmp
