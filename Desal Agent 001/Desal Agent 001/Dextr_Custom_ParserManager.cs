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
	static void displayTokens(Bridge bridge, IList<Custom.Token> tokens) {
		int indentLevel = 0;
		bool indentedThisLine = false;
		foreach( Custom.Token token in tokens ) {
			if( token.type == Custom.TokenType.INDENT_OPEN ) {
				indentLevel++;
				continue;
			}
			if( token.type == Custom.TokenType.INDENT_CLOSE ) {
				indentLevel--;
				continue;
			}
			if( token.type == Custom.TokenType.NEWLINE ) {
				bridge.println();
				indentedThisLine = false;
				continue;
			}
			
			if( ! indentedThisLine ) {
				for( int i = 0; i < indentLevel; i++ )
					bridge.print("\t");
				indentedThisLine = true;
			}
			
			if( token.type == Custom.TokenType.STRING )
				bridge.print(
					String.Format("\"{0}\"", token.value));
			else
				bridge.print(token.value);

			bridge.print(" ");
		}

		bridge.println();
	}
	
	static void displayTokenInfo(Bridge bridge, IList<Custom.Token> tokens) {
		foreach( Custom.Token token in tokens ) {
			bridge.print(
				String.Format(
					"{0}({1}) '{2}' on line {3}, col {4}",
					token.type, (int)token.type, token.value,
					token.lineNumber, token.startColumn ));
			if( token.endColumn != token.startColumn )
				bridge.print(
					String.Format(
						"-{0}", token.endColumn ));
			bridge.println();
		}
	}
	
	static Node_Bundle parseWithSablecc(Bridge bridge, string filePath) {
		Sablecc.parser.Parser parser =
			new Sablecc.parser.Parser(
				new Dextr.Custom.SableccLexer(
					new System.IO.StreamReader(
						filePath )));
		Sablecc.node.Start start = parser.Parse();
		start.Apply(new SableccAdapter());
		return null;
	}

	public static Node_Bundle parseDocument(
	Bridge bridge, string filePath, string parserName) {
		if( parserName == "token-displayer" || parserName == "token-info-displayer" ) {
			string content = System.IO.File.ReadAllText(filePath);
			IList<Custom.Token> tokens = Custom.Tokenizer.tokenize(content);
			if( parserName == "token-displayer" )
				displayTokens(bridge, tokens);
			else
				displayTokenInfo(bridge, tokens);
			return null;
		}
		else if( parserName == "SableCC" ) {
			return parseWithSablecc(bridge, filePath);
		}
		else
			throw new UserError(
				String.Format("unknown Dextr parser: {0}", parserName));
	}
}

} //end namespace Dextr.Custom
