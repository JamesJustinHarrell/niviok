using System;
using System.Collections.Generic;

namespace Dextr.Custom {

class SableccAdapter : Sablecc.analysis.AnalysisAdapter {
	public override void DefaultCase(Sablecc.node.Node node) {
		//xxx should be using Bridge
		Console.WriteLine(node);
    }
}

class ParserManager {
	static Node_Module parseWithSablecc(Bridge bridge, string filePath) {
		Sablecc.parser.Parser parser =
			new Sablecc.parser.Parser(
				new Dextr.Custom.SableccLexer(
					new System.IO.StreamReader(
						filePath ),
					filePath ));
		Sablecc.node.Start start = parser.Parse();
		start.Apply(new SableccAdapter());
		return null;
	}

	public static Node_Module parseDocument(
	Bridge bridge, string filePath, string parserName) {
		if( parserName == "SableCC" ) {
			return parseWithSablecc(bridge, filePath);
		}
		else
			throw new UserError(
				String.Format("unknown Dextr parser: {0}", parserName));
	}
}

} //end namespace Dextr.Custom
