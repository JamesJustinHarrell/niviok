using System.Collections.Generic;

enum TokenType { //comment denotes type of value
	INDENT_OPEN, //null
	INDENT_CLOSE, //null
	LINE_COMMENT, //null - includes /* ... */
	MULTILINE_COMMENT, //null - /* ... \n ... */
	NEWLINE, //null
	NUMBER, //string
	SPACE, //null
	STRING, //string
	WORD, //string
	OTHER
}

class Token {
	TokenType _type;
	object _value;
	
	public Token( TokenType type, object value ) {
		_type = type;
		_value = value;
	}
	
	public TokenType type {
		get { return _type; }
	}
	
	public object value {
		get { return _value; }
	}
}

//breaks up a string into a series of tokens
class DextrTokenizer {
	public static IList<Token> tokenize(string a_content) {
		return new DextrTokenizer(a_content + "\n\0").tokens;
	}
	
	enum Mode {
		BEGIN_LINE,
		CONTINUE_LINE,
		DONE
	}
	
	string content;
	int index;
	int indentLevel;
	Mode mode;
	IList<Token> tokens;
	
	DextrTokenizer(string a_content) {
		content = a_content;
		index = 0;
		indentLevel = 0;
		mode = Mode.BEGIN_LINE;
		tokens = new List<Token>();

		while(true) {
			if( index >= content.Length )
				throw new System.Exception("unexpected end of text");
		
			if( mode == Mode.BEGIN_LINE )
				beginLine();
			else if( mode == Mode.CONTINUE_LINE )
				continueLine();
			else if( mode == Mode.DONE )
				break;
			else
				throw new System.Exception("unknown mode");
		}
	}
	
	void beginLine() {
		int newIndentLevel = 0;
		while( isCurrent('\t') ) {
			newIndentLevel++;
			advance();
		}
		while( newIndentLevel > indentLevel ) {
			tokens.Add( new Token(TokenType.INDENT_OPEN, null) );
			indentLevel++;
		}
		while( newIndentLevel < indentLevel ) {
			tokens.Add( new Token(TokenType.INDENT_CLOSE, null) );
			indentLevel--;
		}
		mode = Mode.CONTINUE_LINE;
	}
	
	void continueLine() {
		char c = current();
		if( isWordChar(c) ) {
			consumeWord();
		}
		else if( c == ' ' ) {
			consumeSpace();
		}
		else if( c == '\n' ) {
			consumeNewline();
		}
		else if( c == '\t' ) {
			throw new System.Exception("tab found after non-tab characters");
		}
		else if( c == '"' ) {
			consumeString();
		}
		else if( isNumeral(c) ) {
			consumeNumber();
		}
		else if( c == '/' && peek() == '/' ) {
			consumeSlashSlashComment();
		}
		else if( c == '/' && peek() == '*' ) {
			consumeSlashStarComment();
		}
		else if( isOperatorChar(c) ) {
			consumeCharacter();
		}
		else if( c == '\0' ) {
			mode = Mode.DONE;
		}
		else {
			System.Console.WriteLine("unknown character: '{0}'", c);
			index = content.Length;
			return;
		}
	}
	
	bool isWordChar(char c) {
		return ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || c == '?' || c == '!';
	}
	
	bool isOperatorChar(char c) {
		char[] operatorCharacters = "(){},.=/".ToCharArray();
		return System.Array.IndexOf(operatorCharacters, c) > -1;
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
		if( index >= content.Length )
			throw new System.Exception("unexpected end of text");
	}
	
	void checkedAdvance(char c) {
		if( current() != c )
			throw new System.Exception(
				System.String.Format("current character was not '{0}'", c));
		advance();
	}
	
	void consumeCharacter() {
		tokens.Add( new Token(TokenType.OTHER, current().ToString()) );
		advance();
	}
	
	void consumeSlashSlashComment() {
		checkedAdvance('/');
		checkedAdvance('/');
		while( ! isCurrent('\n') ) {
			advance();
		}
		consumeNewline();
	}
	
	void consumeSlashStarComment() {
		checkedAdvance('/');
		checkedAdvance('*');
		while( ! (current() == '*' && peek() == '/') ) {
			advance();
		}
		checkedAdvance('*');
		checkedAdvance('/');
	}
	
	void consumeNewline() {
		if( ! isCurrent('\n') )
			throw new System.Exception("newline not found");
		if( tokens[tokens.Count-1].type != TokenType.NEWLINE )
			tokens.Add( new Token(TokenType.NEWLINE, null) );
		mode = Mode.BEGIN_LINE;
		while( isCurrent('\n') ) {
			advance();
		}
	}
	
	void consumeNumber() {
		string s = "";
		while( isNumeral(current()) ) {
			s += current();
			advance();
		}
		tokens.Add( new Token(TokenType.NUMBER, s) );
	}
	
	void consumeSpace() {
		while( isCurrent(' ') ) {
			advance();
		}
	}
	
	void consumeString() {
		//xxx restrict opening character, allow multichar openings
		char opening = current();
		advance();
		string s = "";
		while( ! isCurrent(opening) ) {
			s += current();
			advance();
		}
		advance();
		tokens.Add( new Token(TokenType.STRING, s) );
	}
	
	void consumeWord() {
		string word = "";
		while( isWordChar(current()) ) {
			word += current();
			advance();
		}
		tokens.Add( new Token(TokenType.WORD, word) );
	}
}