pathbase="/media/files/Desal/Desal Agent 001/SableCC"
python "$pathbase/generateSableGrammar.py"
java -jar "$pathbase/sablecc-3.2/lib/sablecc.jar" -d "$pathbase/out32" "$pathbase/dextr.sablecc"
rm -r "$pathbase/out32/Dextr"