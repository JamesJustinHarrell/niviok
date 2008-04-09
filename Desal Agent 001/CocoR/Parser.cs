using System.Collections.Generic;

using System;

namespace Desexp.CocoR {



class Parser {
	const int _EOF = 0;
	const int _tINTEGER = 1;
	const int _tRATIONAL = 2;
	const int _tPARENOPEN = 3;
	const int _tPARENCLOSE = 4;
	const int _tPLACEHOLDER = 5;
	const int _tSTRING = 6;
	const int _tWORD = 7;
	const int maxT = 8;

	const bool T = true;
	const bool x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

public LinkedList<Sexp> roots;



	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void Desexp() {
		roots = new LinkedList<Sexp>();
		Sexp sexp;
		
		SExpression(out sexp);
		roots.AddLast(sexp); 
		while (StartOf(1)) {
			SExpression(out sexp);
			roots.AddLast(sexp); 
		}
	}

	void SExpression(out Sexp sexp) {
		sexp = null;
		
		switch (la.kind) {
		case 1: {
			Get();
			sexp = new Sexp(SexpType.INTEGER,
			t.val, null, t.line, t.col); 
			break;
		}
		case 5: {
			Get();
			sexp = new Sexp(SexpType.PLACEHOLDER,
			t.val, null, t.line, t.col); 
			break;
		}
		case 2: {
			Get();
			sexp = new Sexp(SexpType.RATIONAL,
			t.val, null, t.line, t.col); 
			break;
		}
		case 6: {
			Get();
			sexp = new Sexp(SexpType.STRING,
			t.val, null, t.line, t.col); 
			break;
		}
		case 7: {
			Get();
			sexp = new Sexp(SexpType.WORD,
			t.val, null, t.line, t.col); 
			break;
		}
		case 3: {
			List(out sexp);
			break;
		}
		default: SynErr(9); break;
		}
	}

	void List(out Sexp sexp) {
		LinkedList<Sexp> list = new LinkedList<Sexp>();
		Sexp member;
		int beginLine;
		int beginColumn;
		
		Expect(3);
		beginLine = t.line; beginColumn = t.col; 
		while (StartOf(1)) {
			SExpression(out member);
			list.AddLast(member); 
		}
		Expect(4);
		sexp = new Sexp(SexpType.LIST,
		null, list, beginLine, beginColumn); 
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Desexp();

    Expect(0);
	}
	
	static readonly bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x},
		{x,T,T,T, x,T,T,T, x,x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
  public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text
  
	public void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "tINTEGER expected"; break;
			case 2: s = "tRATIONAL expected"; break;
			case 3: s = "tPARENOPEN expected"; break;
			case 4: s = "tPARENCLOSE expected"; break;
			case 5: s = "tPLACEHOLDER expected"; break;
			case 6: s = "tSTRING expected"; break;
			case 7: s = "tWORD expected"; break;
			case 8: s = "??? expected"; break;
			case 9: s = "invalid SExpression"; break;

			default: s = "error " + n; break;
		}
		//xxx begin custom code
		throw new Exception(
			String.Format(
				"parsing error at [{0}:{1}] : {2}",
				line, col, s));
		//xxx end custom code
		//original: errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}

}