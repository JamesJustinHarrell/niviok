using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Dextr.Custom {

class SableccLexer : Sablecc.lexer.Lexer {
	LinkedList<Sablecc.node.Token> _tokens;

	public SableccLexer(System.IO.TextReader tr)
		: base(tr)
	{
		_tokens = new LinkedList<Sablecc.node.Token>();
		Sablecc.node.Token buffer;
		do {
			buffer = base.Next();
			_tokens.AddLast(buffer);
		}
		while( ! (buffer is Sablecc.node.EOF) );
		_tokens = SableccFixup.fixup(_tokens);
	}
	
	public override Sablecc.node.Token Peek() {
		//* xxx */ Console.Write("Peek() "); G.printSableccToken(_tokens.First.Value); Console.WriteLine();
		return _tokens.First.Value;
    }

    public override Sablecc.node.Token Next() {
    	Sablecc.node.Token buffer = _tokens.First.Value;
    	_tokens.RemoveFirst();
		//* xxx */ Console.Write("Next() "); G.printSableccToken(buffer); Console.WriteLine();
    	return buffer;
    }
}

//implements the "fixup" stage that comes between tokenization and parsing
class SableccFixup {
	enum Mode {
		BEGIN_LINE,
		CONTINUE_NONCODE_LINE,
		CONTINUE_CODE_LINE,
		DONE
	}
	
	LinkedListNode<Sablecc.node.Token> _currentInNode;
	LinkedList<Sablecc.node.Token> _outTokens;
	Mode _mode;
	long _indentLevel;

	void addToken( Sablecc.node.Token token ) {
		Debug.Assert( !(
			token is Sablecc.node.TTab ||
			token is Sablecc.node.TLineComment ||
			token is Sablecc.node.TMultilineComment ||
			token is Sablecc.node.TSpace ),
			"tabs, spaces, and comments cannot be present after fixup");
		Debug.Assert(
			! (token is Sablecc.node.TNewline &&
				_outTokens.Last.Value is Sablecc.node.TNewline),
			"newlines cannot follow newlines after fixup");
		_outTokens.AddLast(token);
	}
	
	void advance() {
		_currentInNode = _currentInNode.Next;
		if( _currentInNode == null )
			_mode = Mode.DONE;
	}
	
	Sablecc.node.Token current() {
		return _currentInNode.Value;
	}
	
	void modeBeginLine() {
		//count leading tabs
		int newIndentLevel = 0;
		while( current() is Sablecc.node.TTab ) {
			newIndentLevel++;
			advance();
		}
		
		//line contains only 0+ tabs
		if( current() is Sablecc.node.TNewline )
			consumeNewline();
		
		//line can't have any code on it
		else if(
			current() is Sablecc.node.TSpace ||
			current() is Sablecc.node.TLineComment ||
			current() is Sablecc.node.TMultilineComment )
		{
			advance();
			_mode = Mode.CONTINUE_NONCODE_LINE;
		}
		
		//this line begins with code
		else {
			if( newIndentLevel != _indentLevel ) {
				Sablecc.node.Token previous = _currentInNode.Previous.Value;
				
				//create appropriate INDENT_OPEN tokens
				while( newIndentLevel > _indentLevel ) {
					addToken(
						new Sablecc.node.TIndentOpen(
							"", previous.Line, previous.Pos ));
					_indentLevel++;
				}
				
				//create appropriate INDENT_CLOSE tokens
				while( newIndentLevel < _indentLevel ) {
					_outTokens.AddBefore(
						_outTokens.Last,
						new Sablecc.node.TIndentClose(
							"", previous.Line, previous.Pos ));
					_indentLevel--;
				}
			}
			
			_mode = Mode.CONTINUE_CODE_LINE;
		}
	}
	
	void modeContinueCodeLine() {
		Sablecc.node.Token c = current();

		//tab
		if( c is Sablecc.node.TTab )
			throw new System.Exception("tab found after non-tab character");
		
		//line comment or space
		else if(
		c is Sablecc.node.TLineComment ||
		c is Sablecc.node.TSpace )
			advance();
		
		//multiline comment
		else if( c is Sablecc.node.TMultilineComment ) {
			advance();
			_mode = Mode.CONTINUE_NONCODE_LINE;
		}
		
		//newline
		else if( c is Sablecc.node.TNewline )
			consumeNewline();
		
		else {
			addToken(c);
			advance();
		}			
	}

	void modeContinueNoncodeLine() {
		if(
		current() is Sablecc.node.TLineComment ||
		current() is Sablecc.node.TMultilineComment ||
		current() is Sablecc.node.TSpace )
			advance();
		else if( current() is Sablecc.node.TNewline )
			consumeNewline();
		else
			throw new Exception(
				"token not allowed on non code line: " +
				tokenInfo(current()) );
	}
	
	void consumeNewline() {
		if( ! (current() is Sablecc.node.TNewline) )
			throw new Exception("expected newline token");
		if( ! (_outTokens.Last.Value is Sablecc.node.TNewline) )
			addToken(current());
		while( current() is Sablecc.node.TNewline )
			advance();
		_mode = Mode.BEGIN_LINE;
	}
	
	string tokenInfo(Sablecc.node.Token token) {
		return String.Format(
			"type {0} with value '{1}' at {2}:{3}",
			(token as object).GetType(),
			token.Text, token.Line, token.Pos);
	}

	SableccFixup( LinkedList<Sablecc.node.Token> inTokens ) {
		_currentInNode = inTokens.First;
		_outTokens = new LinkedList<Sablecc.node.Token>();
		_indentLevel = 0;
		_mode = Mode.BEGIN_LINE;

		while( _mode != Mode.DONE ) {
			if( _mode == Mode.BEGIN_LINE )
				modeBeginLine();
			else if( _mode == Mode.CONTINUE_CODE_LINE )
				modeContinueCodeLine();
			else if( _mode == Mode.CONTINUE_NONCODE_LINE )
				modeContinueNoncodeLine();
		}
	}
	
	public static LinkedList<Sablecc.node.Token>
	fixup( LinkedList<Sablecc.node.Token> inTokens ) {
		return new SableccFixup(inTokens)._outTokens;
	}
}

} //end namespace Dextr.Custom
