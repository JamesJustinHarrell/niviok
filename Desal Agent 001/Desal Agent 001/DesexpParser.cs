using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Desexp {

enum SexpType {
	INTEGER, PLACEHOLDER, RATIONAL, STRING, WORD, LIST
}

class Sexp {
	SexpType _type;
	string _atom;
	LinkedList<Sexp> _list;
	int _line;
	int _column;
	
	public Sexp(
	SexpType type, string atom, LinkedList<Sexp> list,
	int line, int column) {
		if( type == SexpType.LIST )
			Debug.Assert( list != null );
		_type = type;
		_atom = atom;
		_list = list;
		_line = line;
		_column = column;
	}
	
	public SexpType type {
		get { return _type; }
	}
	
	public string atom {
		get { return _atom; }
	}
	
	public LinkedList<Sexp> list {
		get { return _list; }
	}
	
	public int line {
		get { return _line; }
	}
	
	public int column {
		get { return _column; }
	}
	
	public override string ToString() {
		if( _type == SexpType.LIST ) {
			string rv = "(";
			bool follow = false;
			foreach( Sexp s in _list ) {
				if(follow)
					rv += " ";
				else follow = true;
				rv += s.ToString();
			}
			rv += ")";
			return rv;
		}
		return _atom;
	}
}

abstract class DesexpParserBase {
	protected delegate T ParseFunc<T>(Sexp sexp);
	protected abstract Node_String parseString(Sexp sexp);
	protected abstract INode_Expression parseTerminalExpression(Sexp sexp);
	protected abstract INode_Expression parseExpressionDefault(Sexp sexp);
	protected abstract T parseOne<T>( ParseFunc<T> func, Sexp parent );
	protected abstract T parseOpt<T>( ParseFunc<T> func, Sexp parent );
	protected abstract IList<T> parseMult0<T>( ParseFunc<T> func, Sexp sexp );
	protected abstract IList<T> parseMult1<T>( ParseFunc<T> func, Sexp sexp );
}
	
class DesexpParser : DesexpParserAuto {
	public static Node_Bundle parseFile(Bridge bridge, string path) {
		CocoR.Parser cocoParser = new CocoR.Parser(new CocoR.Scanner(path));
		cocoParser.Parse();
		LinkedList<Sexp> roots = cocoParser.roots;
		DesexpParser myParser = new DesexpParser();
		return myParser.parseBundle(roots);
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
		throw new Exception(String.Format(
			"unknown type of terminal S-Expression: '{0}'", sexp));
	}
	
	Node_Bundle parseBundle(LinkedList<Sexp> roots) {
		LinkedListNode<Sexp> cur = roots.First;
		
		IList<Node_Import> imports = new List<Node_Import>();
		while( cur != null && cur.Value.list.First.Value.atom == "import" ) {
			cur.Value.list.RemoveFirst();
			imports.Add(parseImport(cur.Value));
			cur = cur.Next;
		}
		
		IList<INode_ScopeAlteration> scopeAlterations =
			new List<INode_ScopeAlteration>();
		while( cur != null && cur.Value.list.First.Value.atom != "declare-first" ) {
			scopeAlterations.Add(parseScopeAlteration(cur.Value));
			cur = cur.Next;
		}
		
		IList<Node_DeclareFirst> declareFirsts = new List<Node_DeclareFirst>();
		while( cur != null && cur.Value.list.First.Value.atom == "declare-first" ) {
			cur.Value.list.RemoveFirst();
			declareFirsts.Add(parseDeclareFirst(cur.Value));
			cur = cur.Next;
		}
		IList<Node_Plane> planes = new List<Node_Plane>();
		planes.Add(
			new Node_Plane(
				new INode_ScopeAlteration[]{},
				declareFirsts));
		
		return new Node_Bundle(imports, scopeAlterations, planes);
	}
	
	//when automatically generated code is unable to determine how to parse an expression
	protected override INode_Expression parseExpressionDefault(Sexp sexp) {
		Sexp first = sexp.list.First.Value;
		sexp.list.RemoveFirst();
	
		//create list of arguments
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
		return new Node_String(val);
	}
	
	//parse first child of @parent and remove
	protected override T parseOne<T>( ParseFunc<T> func, Sexp parent ) {
		T t = func(parent.list.First.Value);
		parent.list.RemoveFirst();
		return t;
	}
	
	//parse first child of @parent and remove
	protected override T parseOpt<T>( ParseFunc<T> func, Sexp parent ) {
		if( parent.type != SexpType.LIST )
			throw new Exception(
				String.Format(
					"S-Expression at [{0}:{1}] must be a list",
					parent.line, parent.column));
		if( parent.list.Count == 0 )
			throw new Exception(
				String.Format(
					"S-Expression at [{0}:{1}] is missing children",
					parent.line, parent.column));
		T t;
		if( parent.list.First.Value.type != SexpType.PLACEHOLDER )
			t = func(parent.list.First.Value);
		parent.list.RemoveFirst();
		return t;
	}
	
	//parse children of first child of @parent and remove first child
	protected override IList<T> parseMult0<T>( ParseFunc<T> func, Sexp sexp ) {
		if( sexp.type != SexpType.LIST )
			throw new Exception(
				String.Format(
					"S-Expression at [{0}:{1}] must be a list",
					sexp.line, sexp.column));
		if( sexp.list.Count == 0 )
			throw new Exception(
				String.Format(
					"S-Expression at [{0}:{1}] doesn't have enough children",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.LIST )
			throw new Exception(
				String.Format(
					"list's first child at [{0}:{1}] must be list",
					sexp.list.First.Value.line, sexp.list.First.Value.column));
		IList<T> rv = new List<T>();
		LinkedList<Sexp> contents = sexp.list.First.Value.list;
		sexp.list.RemoveFirst();
		foreach( Sexp child in contents ) {
			rv.Add(func(child));
		}
		return rv;
	}

	//parse children of first child of @parent and remove first child
	protected override IList<T> parseMult1<T>( ParseFunc<T> func, Sexp sexp ) {
		IList<T> rv = parseMult0(func, sexp);
		if( rv.Count == 0 )
			throw new System.Exception("list must contain at least 1 child");
		return rv;
	}
}

} //end namespace Desexp
