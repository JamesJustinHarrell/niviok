using System.Collections.Generic;

namespace CocoR {

	//a Coco/R token
	class Token {
		public int kind; //EOF reserves 0
		public string val;
		public int line; //starting at 1
		public int col; //staring at 1

		public Token(Dextr.Token token) {
			kind = (int)token.type;
			val = token.value;
			line = token.lineNumber;
			col = token.startColumn;
		}
	}

	class Scanner {
		Token[] _tokens;
		int _index;
		int _peekLevel;
	
		public Scanner(IList<Dextr.Token> dextrTokens) {
			_tokens = new Token[dextrTokens.Count];
			for( int i = 0; i < dextrTokens.Count; i++ ) {
				_tokens[i] = new Token(dextrTokens[i]);
			}
			
			_index = 0;
			ResetPeek();			
		}
		
		public Token Scan() {
			ResetPeek();
			return _tokens[_index++];
		}
		
		public Token Peek() {
			return _tokens[_index + _peekLevel++];
		}
		
		public void ResetPeek() {
			/* 0 instead of 1, because
			it returns the same token as Scan(),
			just without advancing index */
			_peekLevel = 0;
		}
	}

} //namespace CocoR
