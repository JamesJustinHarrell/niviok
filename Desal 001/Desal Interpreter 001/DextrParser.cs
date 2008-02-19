using System;
using System.Collections.Generic;

//parses a text file into a Desal bundle node
class DextrParser {
	public static Node_Bundle parseDocument(string filePath) {
		return new DextrParser(System.IO.File.ReadAllText(filePath)).bundle;
	}

	IList<Token> tokens;
	Node_Bundle bundle;
	
	DextrParser(string a_content) {
		tokens = DextrTokenizer.tokenize(a_content);
		/*
		INDENT_OPEN, //null
		INDENT_CLOSE, //null
		NEWLINE, //null
		NUMBER, //string
		STRING, //string
		WORD, //string
		OTHER
		*/
		
		foreach( Token token in tokens ) {
			switch( token.type ) {
				case TokenType.INDENT_OPEN :
					Console.Write("INDENT_OPEN");
					break;
				case TokenType.INDENT_CLOSE :
					Console.Write("INDENT_CLOSE");
					break;
				case TokenType.NEWLINE :
					Console.Write('\n');
					continue; //prevent space from being printed
				case TokenType.NUMBER :
					Console.Write(token.value as string);
					break;
				case TokenType.STRING :
					Console.Write("'{0}'", token.value as string);
					break;
				case TokenType.WORD :
					Console.Write(token.value as string);
					break;
				case TokenType.OTHER :
					Console.Write(token.value as string);
					break;
				default :
					Console.Write("UNKNOWN TOKEN TYPE: {0}", token.type.ToString());
					break;
			}
			Console.Write(" ");
		}

		Console.WriteLine("");
		
		//xxx parse tokens into a bundle
	}
}