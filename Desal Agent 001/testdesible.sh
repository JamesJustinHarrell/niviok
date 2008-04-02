desalBase="/media/files/Desal"
clear
mono \
	--debug \
	"$desalBase/Desal Agent 001/Desal Agent 001/bin/Debug/Desal Agent 001.exe" \
	-path="$desalBase/Desal Agent 001/test.desible" \
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
