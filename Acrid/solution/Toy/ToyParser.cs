using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acrid.NodeTypes;

namespace Acrid.Toy {

public abstract class ToyParserBase {
	protected abstract string getSource(Sexp sexp);
	protected abstract T parseOne<T>( Func<Sexp,T> func, Sexp parent );
	protected abstract T parseOpt<T>( Func<Sexp,T> func, Sexp parent );
	protected abstract IList<T> parseMult0<T>( Func<Sexp,T> func, Sexp sexp );
	protected abstract IList<T> parseMult1<T>( Func<Sexp,T> func, Sexp sexp );
}

public class ToyParser : ToyParserAuto {

	public static Node_Module parseFile(string path) {
		SableCC.parser.Parser sableParser = new SableCC.parser.Parser(
			new SableCC.lexer.Lexer(
				new System.IO.StreamReader(path)));
		SableCC.node.Start start;
		try {
			start = sableParser.Parse();
		}
		catch( SableCC.lexer.LexerException e ) {
			throw new ParseError( e.Message, path, e );
		}
		catch( SableCC.parser.ParserException e ) {
			throw new ParseError( e.Message, path, e );
		}
		return parse(
			path,
			SableSexpWrapper.wrapEach(
				(start.GetPDocument() as SableCC.node.ADocument).GetSexp() ));
	}

	public static Node_Module parseFileCocor(string path) {
		CocoR.Parser cocoParser = new CocoR.Parser(new CocoR.Scanner(path));
		cocoParser.Parse();
		return parse(path, cocoParser.roots);
	}
	
	public static Node_Module parse(string fileSource, LinkedList<Sexp> rootList) {
		ToyParser parser = new ToyParser(fileSource);
		return parser.parseModule(
			new Sexp(SexpType.LIST, null, rootList, 0, 0));
	}		
	
	string _fileSource;
	
	ToyParser(string fileSource) {
		_fileSource = fileSource;
	}
	
	protected override INode_Expression parseTerminalExpression(Sexp sexp) {
		if( sexp.type == SexpType.INTEGER )
			return parseInteger(sexp);
		if( sexp.type == SexpType.RATIONAL )
			return parseRational(sexp);
		if( sexp.type == SexpType.STRING )
			return parseString(sexp);
		if( sexp.type == SexpType.WORD )
			return parseIdentifier(sexp);
		throw new ParseError(
			String.Format(
				"unknown type of terminal S-Expression: '{0}'",
				sexp),
			getSource(sexp));
	}
	
	protected override INode_Expression parseNonwordExpression(Sexp sexp) {
		return parseExpressionDefault(sexp);
	}
	
	//when automatically generated code is unable to determine how to parse an expression
	protected override INode_Expression parseExpressionDefault(Sexp sexp) {
		Sexp first = sexp.list.First.Value;
		sexp.list.RemoveFirst();
	
		//create list of IList<Argument>
		LinkedList<Sexp> argsList = new LinkedList<Sexp>();
		foreach( Sexp argPart in sexp.list ) {
			LinkedList<Sexp> arg = new LinkedList<Sexp>();
			arg.AddLast(new Sexp(SexpType.PLACEHOLDER, "-", null, argPart.line, argPart.column));
			arg.AddLast(argPart);
			argsList.AddLast(new Sexp(SexpType.LIST, null, arg, argPart.line, argPart.column));
		}
		
		//wrap list as an S-Expression
		Sexp argsSexp = new Sexp(SexpType.LIST, null, argsList, sexp.line, sexp.column);
		
		//create list for call node
		LinkedList<Sexp> callList = new LinkedList<Sexp>();
		callList.AddLast(first);
		callList.AddLast(argsSexp);
		
		//wrap call as an S-Expression
		Sexp callSexp = new Sexp(SexpType.LIST, null, callList, sexp.line, sexp.column);
		
		//parse the call S-Expression
		return parseCall(callSexp);
	}

	protected override Node_String parseString(Sexp sexp) {
		string val = sexp.atom;
		val = val.Substring(1, val.Length-2);
		val = val.Replace("\\\"", "\"");
		return new Node_String(val, getSource(sexp));
	}
	
	//parse first child of @parent and remove
	protected override T parseOne<T>( Func<Sexp,T> func, Sexp parent ) {
		T t = func(parent.list.First.Value);
		parent.list.RemoveFirst();
		return t;
	}
	
	//parse first child of @parent and remove
	protected override T parseOpt<T>( Func<Sexp,T> func, Sexp parent ) {
		if( parent.type != SexpType.LIST )
			throw new ParseError(
				"S-Expression must be a list",
				getSource(parent));
		if( parent.list.Count == 0 )
			throw new ParseError(
				"S-Expression is missing children",
				getSource(parent));
		T t;
		if( parent.list.First.Value.type != SexpType.PLACEHOLDER )
			t = func(parent.list.First.Value);
		parent.list.RemoveFirst();
		return t;
	}
	
	//parse children of first child of @parent and remove first child
	protected override IList<T> parseMult0<T>( Func<Sexp,T> func, Sexp sexp ) {
		if( sexp.type != SexpType.LIST )
			throw new ParseError(
				"S-Expression must be a list",
				getSource(sexp));
		if( sexp.list.Count == 0 )
			throw new ParseError(
				"S-Expression at doesn't have enough children",
				getSource(sexp));
		if( sexp.list.First.Value.type != SexpType.LIST )
			throw new ParseError(
				"list's first child must be list",
				getSource(sexp.list.First.Value));
		IList<T> rv = new List<T>();
		LinkedList<Sexp> contents = sexp.list.First.Value.list;
		sexp.list.RemoveFirst();
		foreach( Sexp child in contents ) {
			rv.Add(func(child));
		}
		return rv;
	}

	//parse children of first child of @parent and remove first child
	protected override IList<T> parseMult1<T>( Func<Sexp,T> func, Sexp sexp ) {
		IList<T> rv = parseMult0(func, sexp);
		if( rv.Count == 0 )
			throw new ParseError(
				"list at must contain at least 1 child",
				getSource(sexp));
		return rv;
	}
	
	protected override string getSource(Sexp sexp) {
		string location = String.Format("{0}:{1}", sexp.line, sexp.column);
		return String.Format(
			"Toy : {0} : {1}",
			_fileSource, location);
	}
}

} //namespace
