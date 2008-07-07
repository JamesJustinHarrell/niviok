using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Acrid.Fujin.Custom {

class SableCCLexer : SableCC.lexer.Lexer {
	LinkedList<SableCC.node.Token> _tokens;

	public SableCCLexer(System.IO.TextReader tr, string fileSource)
		: base(tr)
	{
		_tokens = new LinkedList<SableCC.node.Token>();
		SableCC.node.Token buffer;
		do {
			buffer = base.Next();
			_tokens.AddLast(buffer);
		}
		while( ! (buffer is SableCC.node.EOF) );
		_tokens = SableCCFixup.fixup(_tokens, fileSource);
	}
	
	public override SableCC.node.Token Peek() {
		//* xxx */ Console.Write("Peek() "); G.printSableCCToken(_tokens.First.Value); Console.WriteLine();
		return _tokens.First.Value;
    }

    public override SableCC.node.Token Next() {
    	SableCC.node.Token buffer = _tokens.First.Value;
    	_tokens.RemoveFirst();
		//* xxx */ Console.Write("Next() "); G.printSableCCToken(buffer); Console.WriteLine();
    	return buffer;
    }
}

//implements the "fixup" stage that comes between tokenization and parsing
class SableCCFixup {
	enum Mode {
		BEGIN_LINE,
		CONTINUE_NONCODE_LINE,
		CONTINUE_CODE_LINE,
		DONE
	}
	
	public static LinkedList<SableCC.node.Token>
	fixup( LinkedList<SableCC.node.Token> inTokens, string fileSource ) {
		return new SableCCFixup(inTokens, fileSource)._outTokens;
	}
	
	LinkedListNode<SableCC.node.Token> _currentInNode;
	LinkedList<SableCC.node.Token> _outTokens;
	Mode _mode;
	long _indentLevel;
	string _fileSource;

	SableCCFixup( LinkedList<SableCC.node.Token> inTokens, string fileSource ) {
		_currentInNode = inTokens.First;
		_outTokens = new LinkedList<SableCC.node.Token>();
		_indentLevel = 0;
		_mode = Mode.BEGIN_LINE;
		_fileSource = fileSource;

		while( _mode != Mode.DONE ) {
			if( _mode == Mode.BEGIN_LINE )
				modeBeginLine();
			else if( _mode == Mode.CONTINUE_CODE_LINE )
				modeContinueCodeLine();
			else if( _mode == Mode.CONTINUE_NONCODE_LINE )
				modeContinueNoncodeLine();
		}
	}

	void addToken( SableCC.node.Token token ) {
		Debug.Assert( !(
			token is SableCC.node.TTab ||
			token is SableCC.node.TLineComment ||
			token is SableCC.node.TMultilineComment ||
			token is SableCC.node.TSpace ),
			"tabs, spaces, and comments cannot be present after fixup");
		Debug.Assert(
			! (token is SableCC.node.TNewline &&
				_outTokens.Last.Value is SableCC.node.TNewline),
			"newlines cannot follow newlines after fixup");
		_outTokens.AddLast(token);
	}
	
	void advance() {
		_currentInNode = _currentInNode.Next;
		if( _currentInNode == null )
			_mode = Mode.DONE;
	}
	
	SableCC.node.Token current() {
		return _currentInNode.Value;
	}
	
	void modeBeginLine() {
		//count leading tabs
		int newIndentLevel = 0;
		while( current() is SableCC.node.TTab ) {
			newIndentLevel++;
			advance();
		}
		
		//line contains only 0+ tabs
		if( current() is SableCC.node.TNewline )
			consumeNewline();
		
		//line can't have any code on it
		else if(
			current() is SableCC.node.TSpace ||
			current() is SableCC.node.TLineComment ||
			current() is SableCC.node.TMultilineComment )
		{
			advance();
			_mode = Mode.CONTINUE_NONCODE_LINE;
		}
		
		//this line begins with code
		else {
			if( newIndentLevel != _indentLevel ) {
				SableCC.node.Token previous = _currentInNode.Previous.Value;
				
				//create appropriate INDENT_OPEN tokens
				while( newIndentLevel > _indentLevel ) {
					addToken(
						new SableCC.node.TIndentOpen(
							"", previous.Line, previous.Pos ));
					_indentLevel++;
				}
				
				//create appropriate INDENT_CLOSE tokens
				while( newIndentLevel < _indentLevel ) {
					_outTokens.AddBefore(
						_outTokens.Last,
						new SableCC.node.TIndentClose(
							"", previous.Line, previous.Pos ));
					_indentLevel--;
				}
			}
			
			_mode = Mode.CONTINUE_CODE_LINE;
		}
	}
	
	void modeContinueCodeLine() {
		SableCC.node.Token c = current();

		//tab
		if( c is SableCC.node.TTab )
			throw new ParseError(
				"tab found after non-tab character",
				getSource(c) );
		
		//line comment or space
		else if(
		c is SableCC.node.TLineComment ||
		c is SableCC.node.TSpace )
			advance();
		
		//multiline comment
		else if( c is SableCC.node.TMultilineComment ) {
			advance();
			_mode = Mode.CONTINUE_NONCODE_LINE;
		}
		
		//newline
		else if( c is SableCC.node.TNewline )
			consumeNewline();
		
		else {
			addToken(c);
			advance();
		}			
	}

	void modeContinueNoncodeLine() {
		if(
		current() is SableCC.node.TLineComment ||
		current() is SableCC.node.TMultilineComment ||
		current() is SableCC.node.TSpace )
			advance();
		else if( current() is SableCC.node.TNewline )
			consumeNewline();
		else
			throw new ParseError(
				"token not allowed on non code line",
				tokenInfo(current()) );
	}
	
	void consumeNewline() {
		if( ! (current() is SableCC.node.TNewline) )
			throw new ParseError("expected newline token", getSource(current()));
		if( ! (_outTokens.Last.Value is SableCC.node.TNewline) )
			addToken(current());
		while( current() is SableCC.node.TNewline )
			advance();
		_mode = Mode.BEGIN_LINE;
	}
	
	string tokenInfo(SableCC.node.Token token) {
		return String.Format(
			"type {0} with value '{1}' at {2}:{3}",
			(token as object).GetType(),
			token.Text, token.Line, token.Pos);
	}
	
	string getSource(SableCC.node.Token token) {
		string location = String.Format("{0}:{1}", token.Line, token.Pos);
		return String.Format(
			"Fujin : {0} : {1}",
			_fileSource, location);
	}
}

} //namespace
