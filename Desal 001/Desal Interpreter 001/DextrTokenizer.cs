using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dextr {

	enum TokenType {	
		//value = null
		INDENT_OPEN = 1, 
		INDENT_CLOSE,
		NEWLINE,
		
		//value != null
		BRACKET_OPEN,
		BRACKET_CLOSE,
		NUMBER,
		STRING,
		WORD,
		OTHER, // = 9
		EOF = 0 //Coco/R reserves 0 for EOF
	}

	class Token {
		TokenType _type;
		string _value;
		int _lineNumber;
		int _startColumn;
		int _endColumn;
		
		public Token(
		TokenType type, string value, int lineNumber,
		int startColumn, int endColumn ) {
			_type = type;
			_value = value;
			_lineNumber = lineNumber;
			_startColumn = startColumn;
			_endColumn = endColumn;
		}
		
		public TokenType type {
			get { return _type; }
		}
		
		public string value {
			get { return _value; }
		}
		
		public int lineNumber {
			get { return _lineNumber; }
		}
		
		public int startColumn {
			get { return _startColumn; }
		}
		
		public int endColumn {
			get { return _endColumn; }
		}
	}

	//breaks up a string into a series of tokens
	class Tokenizer {	
		enum Mode {
			BEGIN_LINE,
			CONTINUE_NONCODE_LINE,
			CONTINUE_CODE_LINE,
			DONE
		}
		
		string content;
		int index;
		int indentLevel;
		int lineNumber;
		int currentColumn;
		Mode mode;
		IList<Token> tokens;
			
		bool isWordChar(char c) {
			return ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || c == '?' || c == '!';
		}
		
		bool isOpeningBracket(char c) {
			return System.Array.IndexOf("({[<".ToCharArray(), c) > -1;
		}
		
		bool isClosingBracket(char c) {
			return System.Array.IndexOf(")}]>".ToCharArray(), c) > -1;
		}
		
		bool isOperatorChar(char c) {
			return System.Array.IndexOf(",.=/+-*".ToCharArray(), c) > -1;
		}
		
		bool isNumeral(char c) {
			return (c == '0' || '1' <= c && c <= '9');
		}
		
		//the current character
		char current() {
			return content[index];
		}
		
		//the next character
		char peek() {
			return content[index+1];
		}
		
		bool isCurrent(char c) {
			return current() == c;
		}
		
		void advance() {
			index++;
			currentColumn++;
			if( index >= content.Length )
				throw new System.Exception("unexpected end of text");
		}
		
		void checkedAdvance(char c) {
			if( current() != c )
				throw new System.Exception(
					System.String.Format("current character was not '{0}'", c));
			advance();
		}

		void addToken(
		TokenType type, string value,
		int startColumn, int endColumn ) {
			tokens.Add(
				new Token(
					type, value, lineNumber,
					startColumn, endColumn ));
		}
		
		void beginLine() {
			//count leading tabs
			int newIndentLevel = 0;
			while( isCurrent('\t') ) {
				newIndentLevel++;
				advance();
			}
			
			//line contains only 0+ tabs
			if( isCurrent('\n') ) {
				consumeNewline();
			}
			
			//line can't have any code on it
			else if( isCurrent(' ') || isCurrent('#') || isCurrent('/') ) {
				mode = Mode.CONTINUE_NONCODE_LINE;
			}
			
			//this line begins with code
			else {
				//create appropriate INDENT_OPEN tokens
				while( newIndentLevel > indentLevel ) {
					addToken(TokenType.INDENT_OPEN, null, 0,0);
					indentLevel++;
				}
				
				//create appropriate INDENT_CLOSE tokens
				while( newIndentLevel < indentLevel ) {
					addToken(TokenType.INDENT_CLOSE, null, 0,0);
					indentLevel--;
				}
				
				mode = Mode.CONTINUE_CODE_LINE;
			}
		}
		
		void continueNoncodeLine() {
			if( isCurrent(' ') )
				consumeSpace();
			else if( isCurrent('#') )
				consumePoundComment(); //also consumes newline
			else if( isCurrent('\n') )
				consumeNewline();
			else if( current() == '/' && peek() == '*' )
				consumeSlashStarComment();
			else
				throw new System.Exception(
					System.String.Format(
						"unexpected character on noncode line: {0}",
						current() ) );
		}
		
		void continueCodeLine() {
			char c = current();
			
			if( c == '\t' )
				throw new System.Exception("tab found after non-tab character");
			
			if( isWordChar(c) )
				consumeWord();
			else if( c == ' ' )
				consumeSpace();
			else if( c == '\n' )
				consumeNewline();
			else if( c == '"' || c == '\'' )
				consumeString();
			else if( isNumeral(c) )
				consumeNumber();
			else if( c == '#' )
				consumePoundComment();
			else if( c == '/' && peek() == '*' )
				consumeSlashStarComment();
			else if( isOpeningBracket(c) ) {
				addToken(
					TokenType.BRACKET_OPEN, c.ToString(),
					currentColumn, currentColumn );
				advance();
			}
			else if( isClosingBracket(c) ) {
				addToken(
					TokenType.BRACKET_CLOSE, c.ToString(),
					currentColumn, currentColumn );
				advance();
			}
			else if( isOperatorChar(c) )
				consumeCharacter();
			else if( c == '\0' )
				mode = Mode.DONE;
			else {
				System.Console.WriteLine("unknown character: '{0}'", c);
				index = content.Length;
				return;
			}
		}
		
		void consumeCharacter() {
			addToken(
				TokenType.OTHER, current().ToString(),
				currentColumn, currentColumn );
			advance();
		}
		
		void consumePoundComment() {
			checkedAdvance('#');
			while( ! isCurrent('\n') ) {
				advance();
			}
			consumeNewline();
		}
		
		void consumeSlashStarComment() {
			checkedAdvance('/');
			checkedAdvance('*');
			
			bool multiline = false;
			while( ! (current() == '*' && peek() == '/') ) {
				if( current() == '\n' ) {
					multiline = true;
					lineNumber++;
					currentColumn = 1; //reset currentColumn
				}
				advance();
			}
			
			checkedAdvance('*');
			checkedAdvance('/');
			
			if( multiline ) {
				consumeNewline();
			}
		}
		
		void consumeNewline() {
			Debug.Assert( isCurrent('\n') );
		
			//consume newline and add token if needed
			Token lastToken = tokens[tokens.Count-1];
			if( lastToken.type != TokenType.NEWLINE )
				addToken(TokenType.NEWLINE, null, currentColumn, currentColumn);

			//consume additional newlines without adding tokens
			do {
				advance();
				lineNumber++;
			}
			while( isCurrent('\n') );
			
			currentColumn = 1;
			mode = Mode.BEGIN_LINE;
		}
		
		void consumeNumber() {
			int startColumn = currentColumn;
			string s = "";
			while( isNumeral(current()) ) {
				s += current();
				advance();
			}
			addToken(TokenType.NUMBER, s, startColumn, currentColumn-1);
		}
		
		void consumeSpace() {
			while( isCurrent(' ') ) {
				advance();
			}
		}
		
		//xxx lineNumber will be last line the string is on
		//would be better to have start and end line numbers, but at least use start instead of end
		void consumeString() {
			//xxx restrict opening character, allow multichar openings
			int startColumn = currentColumn;
			char opening = current();
			advance();
			string s = "";
			while( ! isCurrent(opening) ) {
				if( current() == '\n' ) {
					lineNumber++;
					currentColumn = 1;
				}
				s += current();
				advance();
			}
			advance();
			addToken(TokenType.STRING, s, startColumn, currentColumn-1);
		}
		
		void consumeWord() {
			int startColumn = currentColumn;
			string word = "";
			while( isWordChar(current()) ) {
				word += current();
				advance();
			}
			addToken(TokenType.WORD, word, startColumn, currentColumn-1);
		}

		Tokenizer(string a_content) {
			content = a_content + "\n\0";
			index = 0;
			indentLevel = 0;
			lineNumber = 1;
			currentColumn = 1;
			mode = Mode.BEGIN_LINE;
			tokens = new List<Token>();

			try {
				while(true) {
					if( index >= content.Length )
						throw new System.Exception("unexpected end of text");
				
					if( mode == Mode.BEGIN_LINE )
						beginLine();
					else if( mode == Mode.CONTINUE_NONCODE_LINE )
						continueNoncodeLine();
					else if( mode == Mode.CONTINUE_CODE_LINE )
						continueCodeLine();
					else if( mode == Mode.DONE )
						break;
					else
						throw new System.Exception("unknown mode");
				}
			}
			catch(System.Exception e) {
				throw new System.Exception(
					System.String.Format("Line {0} Column {1}", lineNumber, currentColumn),
					e);
			}
		}
		
		public static IList<Token> tokenize(string content) {
			return new Tokenizer(content).tokens;
		}
	}
	
} //namespace Dextr
