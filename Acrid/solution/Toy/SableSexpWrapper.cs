using System;
using System.Collections;
using System.Collections.Generic;
using Acrid.Toy.SableCC.node;
using Token = Acrid.Toy.SableCC.node.Token;

namespace Acrid.Toy {

class SableSexpWrapper {
	static Sexp terminal(SexpType type, Token t) {
		return new Sexp(type, t.Text, null, t.Line, t.Pos);
	}

	public static Sexp wrap(PSexp psexp) {
		if( psexp is AWordSexp )
			return terminal(
				SexpType.WORD,
				(psexp as AWordSexp).GetTWord());
		else if( psexp is AIntegerSexp )
			return terminal(
				SexpType.INTEGER,
				(psexp as AIntegerSexp).GetTInteger());
		else if( psexp is ARationalSexp )
			return terminal(
				SexpType.RATIONAL,
				(psexp as ARationalSexp).GetTRational());
		else if( psexp is AStringSexp )
			return terminal(
				SexpType.STRING,
				(psexp as AStringSexp).GetTString());
		else if( psexp is APlaceholderSexp )
			return terminal(
				SexpType.PLACEHOLDER,
				(psexp as APlaceholderSexp).GetTPlaceholder());
		else if( psexp is AListSexp ) {
			AListSexp lsexp = (psexp as AListSexp);
			return new Sexp(
				SexpType.LIST,
				null,
				wrapEach(lsexp.GetSexp()),
				lsexp.GetTParenopen().Line,
				lsexp.GetTParenopen().Pos);
		}
		else
			throw new Exception("unknown sexp type: " + psexp);
	}

	public static LinkedList<Sexp> wrapEach(IList inList) {
		#if TOYDEBUG
		Console.WriteLine( "count: " + inList.Count );
		#endif
		LinkedList<Sexp> outList = new LinkedList<Sexp>();
		foreach( PSexp child in inList ) {
		#if TOYDEBUG
			Console.WriteLine( child.GetType().ToString() );
			Console.WriteLine( child.ToString() );
		#endif
			outList.AddLast( wrap(child) );
		}
		return outList;
	}
}

} //namespace
