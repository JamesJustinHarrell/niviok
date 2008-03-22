using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dextr {

	/*
	character types:
		" "
		"\n"
		"\t"
		free
		illegal //cannot appear anywhere, even in strings
		numerical
		reserved
	*/

	enum TokenType {	
		//value == null
		DOCUMENT_OPEN,
		DOCUMENT_CLOSE,
		INDENT_OPEN, 
		INDENT_CLOSE,
		NEWLINE,
		
		//value != null
		FREE,
		INTEGER,
		RATIONAL,
		RESERVED,
		STRING
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
		
		class EOF : Exception {};
		
		string content;
		int index;
		int indentLevel;
		int lineNumber;
		int currentColumn;
		Mode mode;
		IList<Token> tokens;
		
		bool isFree(char c) {
			return !(
				c == ' ' ||
				c == '\n' ||
				c == '\t' ||
				isIllegal(c) ||
				isNumerical(c) ||
				isReserved(c) );
		}

		bool isIllegal(char c) {
			string chars = "\0";
			return System.Array.IndexOf(chars.ToCharArray(), c) > -1;
		}
		
		bool isNumerical(char c) {
			string chars = "0123456789";
			return System.Array.IndexOf(chars.ToCharArray(), c) > -1;
		}

		bool isReserved(char c) {
			string chars = ",.=+-*/`~@%^&|\\(){}[]<>";
			return System.Array.IndexOf(chars.ToCharArray(), c) > -1;
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
				throw new EOF();
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
			else if( isCurrent('\n') )
				consumeNewline();
			else if( isCurrent('#') )
				consumePoundComment(); //also consumes newline
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

			if( c == ' ' )
				consumeSpace();
			else if( c == '\n' )
				consumeNewline();
			else if( c == '\t' )
				throw new System.Exception("tab found after non-tab character");
			else if( c == '"' || c == '\'' )
				consumeString();
			else if( c == '#' )
				consumePoundComment();
			else if( c == '/' && peek() == '*' )
				consumeSlashStarComment();
			else if( isFree(c) )
				consumeFree();
			else if( isNumerical(c) )
				consumeNumber();
			else if( isReserved(c) )
				consumeReserved();
			else {
				System.Console.WriteLine("unknown character: '{0}'", c);
				index = content.Length;
				return;
			}
		}
		
		//xxx need to ensure only consuming values from a list
		//e.g. +=, but not `~%^$
		void consumeReserved() {
			Debug.Assert(isReserved(current()));
			addToken(
				TokenType.RESERVED, current().ToString(),
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

			//the last character will always be a newline, because
			//a newline character is appended before tokenization
			try {
				//consume additional newlines without adding tokens
				do {
					advance();
					lineNumber++;
				}
				while( isCurrent('\n') );
				
				currentColumn = 1;
				mode = Mode.BEGIN_LINE;
			}
			catch( EOF e ) {
				mode = Mode.DONE;
			}
		}
		
		//xxx also needs to work for rational numbers
		//xxx unless it could be handled in the grammar
		void consumeNumber() {
			int startColumn = currentColumn;
			string s = "";
			while( isNumerical(current()) ) {
				s += current();
				advance();
			}
			addToken(TokenType.INTEGER, s, startColumn, currentColumn-1);
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
		
		void consumeFree() {
			int startColumn = currentColumn;
			string word = "";
			while( isFree(current()) ) {
				word += current();
				advance();
			}
			addToken(TokenType.FREE, word, startColumn, currentColumn-1);
		}

		Tokenizer(string a_content) {
			content = a_content + "\n";
			index = 0;
			indentLevel = 0;
			lineNumber = 1;
			currentColumn = 1;
			mode = Mode.BEGIN_LINE;
			tokens = new List<Token>();

			try {
				while(true) {
					if( index >= content.Length ) {
						if( mode == Mode.DONE )
							break;
						else
							throw new System.Exception("ran past end of text");
					}
				
					if( mode == Mode.BEGIN_LINE )
						beginLine();
					else if( mode == Mode.CONTINUE_NONCODE_LINE )
						continueNoncodeLine();
					else if( mode == Mode.CONTINUE_CODE_LINE )
						continueCodeLine();
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
