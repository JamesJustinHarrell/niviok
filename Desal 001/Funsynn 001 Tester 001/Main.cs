using Funsynn;

namespace Funsynn001Tester001 {
	static class MainClass {
		//Funsynn file describing the grammar of Dextr
		static string dextrGrammarPath = "/media/files/source/Desal 001/test-grammar.funsynn";
		
		//source code written in Dextr
		static string dextrSourcePath = "/media/files/source/Desal 001/test.dextr";
	
		public static void Main() {
			Grammar dextrGrammar = new Grammar(dextrGrammarPath);
			Parser parser = new Parser();
			parser.setGrammar(dextrGrammar);
			object result = parser.parseFile(dextrSourcePath);
		}
	}
}