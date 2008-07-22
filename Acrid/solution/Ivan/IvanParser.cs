using System;
using System.Collections;
using System.Collections.Generic;
using Acrid.NodeTypes;
using Acrid.Ivan.SableCC.node;

namespace Acrid.Ivan {

public abstract class IvanParserBase {
	protected abstract string getSource(Node node);
	protected abstract TO parseOne<TI,TO>( Func<TI,TO> func, TI node );
	protected abstract TO parseOpt<TI,TO>( Func<TI,TO> func, TI node );
	protected abstract IList<TO> parseMult0<TI,TO>( Func<TI,TO> func, IList nodes );
	protected abstract IList<TO> parseMult1<TI,TO>( Func<TI,TO> func, IList nodes );
}

/* xxx
public abstract class IvanParserAuto_old : IvanParserBase {
	//family
	
	protected virtual INode_Expression parseExpression(PExpression node) {
		if( node is AIdentifierExpression )
			return parseIdentifier( (node as AIdentifierExpression).GetTIdentifier() );
		else
			throw new Exception("unknown node type: " + node);
	}
	
	protected virtual INode_StatementDeclaration parseStatementDeclaration(PStatementdeclaration node) {
		if( node is ADeclarefirstStatementdeclaration )
			return parseDeclareFirst( (ADeclarefirst)(node as ADeclarefirstStatementdeclaration).GetDeclarefirst() );
		else
			throw new Exception("unknown node type: " + node);
	}
	
	//terminal

	protected virtual Node_Boolean parseBoolean(PBoolean node) {
		return new Node_Boolean(
			node is AFalseBoolean ? false : true,
			getSource(node));
	}
	
	protected virtual Node_Identifier parseIdentifier(TTIdentifier node) {
		return new Node_Identifier(
			node.Text,
			getSource(node));
	}

	protected virtual Node_Integer parseInteger(TTInteger node) {
		return new Node_Integer(
			node.Text,
			getSource(node));
	}
	
	//tree
	
	protected virtual Node_DeclareFirst parseDeclareFirst(ADeclarefirst node) {
		return new Node_DeclareFirst(
			parseIdentifier(node.GetName()),
			parseBoolean(node.GetOverload()),
			parseExpression(node.GetType()),
			parseBoolean(node.GetBreed()),
			parseExpression(node.GetValue()),
			getSource(node));
	}
	
	protected virtual Node_Hidable parseHidable(AHidable node) {
		return new Node_Hidable(
			parseBoolean(node.GetHidden()),
			parseStatementDeclaration(node.GetStatementdeclaration()),
			getSource(node));
	}
	
	protected virtual Node_Import parseImport(AImport node) {
		return new Node_Import(
			parseIdentifier(node.GetTIdentifier()),
			parseMult0<AImportattempt,Node_ImportAttempt>(parseImportAttempt, node.GetImportattempt()),
			getSource(node));
	}
	
	protected virtual Node_ImportAttempt parseImportAttempt(AImportattempt node) {
		return null; //xxx
	}
	
	protected virtual Node_Module parseModule(AModule node) {
		return new Node_Module(
			parseInteger(node.GetMajor()),
			parseInteger(node.GetMinor()),
			parseMult0<AImport,Node_Import>(parseImport, node.GetImport()),
			parseSieve((ASieve)node.GetSieve()),
			getSource(node));
	}
	
	protected virtual Node_Sieve parseSieve(ASieve node) {
		return new Node_Sieve(
			parseMult0<PExpression,INode_Expression>(parseExpression, node.GetExpose()),
			parseMult0<AHidable,Node_Hidable>(parseHidable, node.GetHidable()),
			getSource(node));
	}
}
*/

public class IvanParser : IvanParserAuto {
	public static Node_Module parseFile(string path) {
		SableCC.parser.Parser sableParser = new SableCC.parser.Parser(
			new SableCC.lexer.Lexer(
				new System.IO.StreamReader(path)));
		Start start;
		try {
			start = sableParser.Parse();
		}
		catch( SableCC.lexer.LexerException e ) {
			throw new ParseError( e.Message, path, e );
		}
		catch( SableCC.parser.ParserException e ) {
			throw new ParseError( e.Message, path, e );
		}
		
		IvanParser parser = new IvanParser();
		parser._fileSource = path;
		return parser.parseModule(
			(AModule)((ADocument)start.GetPDocument()).GetModule() );
	}
	
	string _fileSource;
	
	protected override Node_Boolean parseBoolean(PBoolean node) {
		return new Node_Boolean(
			node is AFalseBoolean ? false : true,
			getSource(node));
	}
	
	protected override Node_Direction parseDirection(PDirection node) {
		return new Node_Direction(
			node is AInDirection ? "in" :
			node is AOutDirection ? "out" :
			"inout",
			getSource(node));
	}
	
	protected override Node_MemberStatus parseMemberStatus(PMemberstatus node) {
		return new Node_MemberStatus(
			node is ANewMemberstatus ? "new" :
			node is ANormalMemberstatus ? "normal" :
			"deprecated",
			getSource(node));
	}

	protected override Node_MemberType parseMemberType(PMembertype node) {
		return new Node_MemberType(
			node is ABreederMembertype ? "breeder" :
			node is ACalleeMembertype ? "callee" :
			node is AGetterMembertype ? "getter" :
			node is ASetterMembertype ? "setter" :
			"method",
			getSource(node));
	}
	
	protected override string getSource(Node node) {
		if( node is Token ) {
			Token token = node as Token;
			return String.Format(
				"{0}:{1}",
				token.Line,
				token.Pos);
		}
		else {
			//xxx use Node.Apply(Switch) to visit children, in search for a token
			return "xxx";
		}
	}
	
	protected override TO parseOne<TI,TO>( Func<TI,TO> func, TI node ) {
		return func(node);
	}
	
	protected override TO parseOpt<TI,TO>( Func<TI,TO> func, TI node ) {
		return (node == null) ? default(TO) : func(node);
	}
	
	protected override IList<TO> parseMult0<TI,TO>( Func<TI,TO> func, IList nodes ) {
		IList<TO> rv = new List<TO>();
		foreach( TI element in nodes )
			rv.Add(func(element));
		return rv;
		
	}
	
	protected override IList<TO> parseMult1<TI,TO>( Func<TI,TO> func, IList nodes ) {
		IList<TO> rv = parseMult0<TI,TO>(func, nodes);
		if( rv.Count == 0 )
			//don't throw ParseError because this was supposed to have been caught by SableCC
			throw new Exception("list was supposed to contain at least 1 child");
		return rv;
	}
}

}
