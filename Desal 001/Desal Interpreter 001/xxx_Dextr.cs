/* xxx enable or remove

using Funsynn;

static class Dextr {
	static string grammarPath = "/media/files/source/Dextr 001/grammar.funsynn";

	public static Node_Global parseFile(string filePath) {
		Parser parser = new Parser();
		parser.setGrammar(grammarPath);
		
		Match matchGlobal = parser.parseFile(filePath);
		return new Node_Global(matchGlobal);
	}
}

*/