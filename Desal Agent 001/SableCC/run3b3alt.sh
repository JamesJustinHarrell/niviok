pathbase="/media/files/Desal/Desal Agent 001/SableCC"
python "$pathbase/generateSableGrammar.py"
java -jar "$pathbase/sablecc-3b3 alt/lib/sablecc.jar" -t csharp -d "$pathbase/out3b3alt" -o "$pathbase/test2" "$pathbase/dextr.sablecc"
