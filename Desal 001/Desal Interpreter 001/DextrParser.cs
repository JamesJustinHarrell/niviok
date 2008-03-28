using System;
using System.Collections.Generic;

namespace Dextr {

	class Parser {		
		static void displayTokens(Bridge bridge, IList<Token> tokens) {
			int indentLevel = 0;
			bool indentedThisLine = false;
			foreach( Token token in tokens ) {
				if( token.type == TokenType.INDENT_OPEN ) {
					indentLevel++;
					continue;
				}
				if( token.type == TokenType.INDENT_CLOSE ) {
					indentLevel--;
					continue;
				}
				if( token.type == TokenType.NEWLINE ) {
					bridge.println();
					indentedThisLine = false;
					continue;
				}
				
				if( ! indentedThisLine ) {
					for( int i = 0; i < indentLevel; i++ )
						bridge.print("\t");
					indentedThisLine = true;
				}
				
				if( token.type == TokenType.STRING )
					bridge.print(
						System.String.Format("\"{0}\"", token.value));
				else
					bridge.print(token.value);

				bridge.print(" ");
			}

			bridge.println();
		}
		
		static void displayTokenInfo(Bridge bridge, IList<Token> tokens) {
			foreach( Token token in tokens ) {
				bridge.print(
					System.String.Format(
						"{0}({1}) '{2}' on line {3}, col {4}",
						token.type, (int)token.type, token.value,
						token.lineNumber, token.startColumn ));
				if( token.endColumn != token.startColumn )
					bridge.print(
						System.String.Format(
							"-{0}", token.endColumn ));
				bridge.println();
			}
		}

		public static Node_Bundle parseDocument(
		Bridge bridge, string filePath, string parserName) {
			string content = System.IO.File.ReadAllText(filePath);
			IList<Token> tokens = Tokenizer.tokenize(content);
			if( parserName == "token-displayer" ) {
				displayTokens(bridge, tokens);
				return null;
			}
			else if( parserName == "token-info-displayer" ) {
				displayTokenInfo(bridge, tokens);
				return null;
			}
			else
				throw new System.Exception(
					System.String.Format("unknown Dextr parser: {0}", parserName));
		}
	}
	
} //namespace Dextr
	