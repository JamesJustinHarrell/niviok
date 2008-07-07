using System;
using System.Collections.Generic;
using Acrid;
using Acrid.NodeTypes;

namespace Acrid.Fujin.Custom {

class SableCCAdapter : SableCC.analysis.AnalysisAdapter {
	public override void DefaultCase(SableCC.node.Node node) {
		//xxx should be using Bridge
		Console.WriteLine(node);
    }
}

public class ParserManager {
	static Node_Module parseWithSableCC(string filePath) {
		SableCC.parser.Parser parser =
			new SableCC.parser.Parser(
				new Fujin.Custom.SableCCLexer(
					new System.IO.StreamReader(
						filePath ),
					filePath ));
		SableCC.node.Start start = parser.Parse();
		start.Apply(new SableCCAdapter());
		return null;
	}

	public static Node_Module parseDocument(
	string filePath, string parserName) {
		if( parserName == "SableCC" ) {
			return parseWithSableCC(filePath);
		}
		else
			throw new Exception(
				String.Format("unknown Fujin parser: {0}", parserName));
	}
}

} //namespace
