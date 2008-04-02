desalBase="/media/files/Desal"
clear
mono \
	--debug \
	"$desalBase/Desal Agent 001/Desal Agent 001/bin/Debug/Desal Agent 001.exe" \
	-path="$desalBase/Desal Agent 001/test.dextr" \
	-print-tree=true \
	-representation=dextr \
	-dextr-parser=token-info-displayer \
	2> stderr.tmp
export ExitCode=$?
echo "----- exit status: "
echo $ExitCode
echo "----- stderr: "
cat stderr.tmp
