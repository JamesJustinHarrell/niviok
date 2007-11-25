clear
"Desal Interpreter 001/bin/Debug/Desal Interpreter 001.exe" \
-path="/media/files/Desal/Desal 001/test.desible" \
2> stderr
echo "----- exit status: "
echo $?
echo "----- stderr: "
cat stderr
