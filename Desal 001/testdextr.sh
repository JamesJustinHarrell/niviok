clear
mono \
"Desal Interpreter 001/bin/Debug/Desal Interpreter 001.exe" \
-path="/media/files/Desal/Desal 001/Dextr/test.dextr" \
-print-tree=true \
-representation=dextr \
2> stderr.tmp
echo "----- exit status: "
echo $?
echo "----- stderr: "
cat stderr.tmp
