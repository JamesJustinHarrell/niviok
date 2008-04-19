
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;
using System.Xml;

namespace Desexp {

abstract class DesexpParserAuto : DesexpParserBase {
	protected Node_Chain parseChain(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Chain(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_SetProperty parseSetProperty(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_SetProperty(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Using parseUsing(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Using(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Generator parseGenerator(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Generator(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_GenericFunction parseGenericFunction(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_GenericFunction(
   			parseMult1<Node_GenericParameter>(parseGenericParameter, sexp),
			parseOne<Node_Function>(parseFunction, sexp) );
	}

	protected Node_Xnor parseXnor(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Xnor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Return parseReturn(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Return(
   			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	virtual protected Node_String parseString(Sexp sexp) {
		try {
			return new Node_String(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type String at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_DeclareEmpty parseDeclareEmpty(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_DeclareEmpty(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp) );
	}

	protected Node_Break parseBreak(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Break(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp) );
	}

	virtual protected Node_Integer parseInteger(Sexp sexp) {
		try {
			return new Node_Integer(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type Integer at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Nor parseNor(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Nor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Yield parseYield(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Yield(
   			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Ignore parseIgnore(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Ignore(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult1<Node_IgnoreMember>(parseIgnoreMember, sexp) );
	}

	protected Node_Continue parseContinue(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Continue(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_EnumEntry parseEnumEntry(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_EnumEntry(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Breeder parseBreeder(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Breeder(
   			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	virtual protected Node_Identifier parseIdentifier(Sexp sexp) {
		try {
			return new Node_Identifier(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type Identifier at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_ParameterInfo parseParameterInfo(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_ParameterInfo(
   			parseOne<Node_Direction>(parseDirection, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp) );
	}

	protected Node_DeclareFirst parseDeclareFirst(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_DeclareFirst(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_InstantiateGeneric parseInstantiateGeneric(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_InstantiateGeneric(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult1<Node_Argument>(parseArgument, sexp) );
	}

	protected Node_DictionaryEntry parseDictionaryEntry(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_DictionaryEntry(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForPair parseForPair(Sexp sexp) {
		if( sexp.list.Count != 6 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 6 children",
					sexp.line, sexp.column));
		return new Node_ForPair(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_MemberImplementation parseMemberImplementation(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_MemberImplementation(
   			parseOne<Node_MemberIdentification>(parseMemberIdentification, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_MemberIdentification parseMemberIdentification(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_MemberIdentification(
   			parseOne<Node_MemberType>(parseMemberType, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForRange parseForRange(Sexp sexp) {
		if( sexp.list.Count != 6 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 6 children",
					sexp.line, sexp.column));
		return new Node_ForRange(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Nand parseNand(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Nand(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	virtual protected Node_Rational parseRational(Sexp sexp) {
		try {
			return new Node_Rational(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type Rational at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Import parseImport(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Import(
   			parseOne<Node_String>(parseString, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Method parseMethod(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Method(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Dictionary parseDictionary(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Dictionary(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<Node_DictionaryEntry>(parseDictionaryEntry, sexp) );
	}

	protected Node_Enum parseEnum(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Enum(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseMult1<Node_EnumEntry>(parseEnumEntry, sexp) );
	}

	protected Node_Possibility parsePossibility(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Possibility(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Interface parseInterface(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Interface(
   			parseMult0<INode_Expression>(parseExpression, sexp),
			parseMult0<INode_InterfaceMember>(parseInterfaceMember, sexp) );
	}

	protected Node_Throw parseThrow(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Throw(
   			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected INode_InterfaceMember parseInterfaceMember(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			throw new ParseError(
				String.Format(
					"InterfaceMember S-Expression at [{0}:{1}] must be a list",
					sexp.line, sexp.column));
		if( sexp.list.Count == 0 )
			throw new ParseError(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			throw new ParseError(
				String.Format(
					"S-Expression at [{0}:{1}] must begin with a word",
					sexp.line, sexp.column));
		string specType = sexp.list.First.Value.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "breeder":
				return parseBreeder(sexp);
			case "callee":
				return parseCallee(sexp);
			case "property":
				return parseProperty(sexp);
			case "method":
				return parseMethod(sexp);
			default:
				throw new ParseError(
					String.Format(
						"unknown type of InterfaceMember '{0}' at [{1}:{2}]",
						specType, sexp.line, sexp.column));
		}
	}

	protected Node_IdentikeyType parseIdentikeyType(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_IdentikeyType(
   			parseOne<Node_IdentikeyCategory>(parseIdentikeyCategory, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Curry parseCurry(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Curry(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Argument>(parseArgument, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp) );
	}

	protected Node_Or parseOr(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Or(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Block parseBlock(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Block(
   			parseMult0<INode_ScopeAlteration>(parseScopeAlteration, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_GenericParameter parseGenericParameter(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_GenericParameter(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_While parseWhile(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_While(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_DeclareAssign parseDeclareAssign(Sexp sexp) {
		if( sexp.list.Count != 5 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 5 children",
					sexp.line, sexp.column));
		return new Node_DeclareAssign(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForKey parseForKey(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_ForKey(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	virtual protected Node_Boolean parseBoolean(Sexp sexp) {
		try {
			return new Node_Boolean(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type Boolean at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Comprehension parseComprehension(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_Comprehension(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Array parseArray(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Array(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Select parseSelect(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Select(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Case>(parseCase, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ParameterImpl parseParameterImpl(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_ParameterImpl(
   			parseOne<Node_Direction>(parseDirection, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_IgnoreMember parseIgnoreMember(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_IgnoreMember(
   			parseOne<Node_String>(parseString, sexp),
			parseOne<Node_Integer>(parseInteger, sexp) );
	}

	protected Node_Breed parseBreed(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Breed(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Call parseCall(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Call(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Argument>(parseArgument, sexp) );
	}

	protected Node_Function parseFunction(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Function(
   			parseMult0<Node_ParameterImpl>(parseParameterImpl, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	virtual protected Node_Direction parseDirection(Sexp sexp) {
		try {
			return new Node_Direction(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type Direction at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Conditional parseConditional(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Conditional(
   			parseMult1<Node_Possibility>(parsePossibility, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Expose parseExpose(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Expose(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Worker parseWorker(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Worker(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Worker>(parseWorker, sexp),
			parseMult0<Node_MemberImplementation>(parseMemberImplementation, sexp) );
	}

	protected Node_NullableType parseNullableType(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_NullableType(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp) );
	}

	protected Node_Case parseCase(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Case(
   			parseMult1<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_TryCatch parseTryCatch(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_TryCatch(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_ExceptionHandler>(parseExceptionHandler, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Caller parseCaller(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Caller(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Cast parseCast(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Cast(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Labeled parseLabeled(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Labeled(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForValue parseForValue(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_ForValue(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_Property parseProperty(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Property(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Callee parseCallee(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Callee(
   			parseMult0<Node_ParameterInfo>(parseParameterInfo, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Loop parseLoop(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Loop(
   			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_And parseAnd(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_And(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Argument parseArgument(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Argument(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForManual parseForManual(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_ForManual(
   			parseMult0<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_Block>(parseBlock, sexp) );
	}

	protected Node_DoWhile parseDoWhile(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_DoWhile(
   			parseOne<Node_Block>(parseBlock, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_DoTimes parseDoTimes(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_DoTimes(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Block>(parseBlock, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Null parseNull(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Null(
   			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	virtual protected Node_MemberType parseMemberType(Sexp sexp) {
		try {
			return new Node_MemberType(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type MemberType at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected INode_ScopeAlteration parseScopeAlteration(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			throw new ParseError(
				String.Format(
					"ScopeAlteration S-Expression at [{0}:{1}] must be a list",
					sexp.line, sexp.column));
		if( sexp.list.Count == 0 )
			throw new ParseError(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			throw new ParseError(
				String.Format(
					"S-Expression at [{0}:{1}] must begin with a word",
					sexp.line, sexp.column));
		string specType = sexp.list.First.Value.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "using":
				return parseUsing(sexp);
			case "expose":
				return parseExpose(sexp);
			default:
				throw new ParseError(
					String.Format(
						"unknown type of ScopeAlteration '{0}' at [{1}:{2}]",
						specType, sexp.line, sexp.column));
		}
	}

	protected Node_GenericInterface parseGenericInterface(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_GenericInterface(
   			parseMult1<Node_GenericParameter>(parseGenericParameter, sexp),
			parseOne<Node_Interface>(parseInterface, sexp) );
	}

	protected Node_ExceptionHandler parseExceptionHandler(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 4 children",
					sexp.line, sexp.column));
		return new Node_ExceptionHandler(
   			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	virtual protected Node_IdentikeyCategory parseIdentikeyCategory(Sexp sexp) {
		try {
			return new Node_IdentikeyCategory(sexp.atom);
		}
		catch(ParseError e) {
			throw new ParseError(
				String.Format(
					"node of type IdentikeyCategory at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_FunctionInterface parseFunctionInterface(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_FunctionInterface(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_ParameterInfo>(parseParameterInfo, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_NamespacedValueIdentikey parseNamespacedValueIdentikey(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_NamespacedValueIdentikey(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Object parseObject(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 1 children",
					sexp.line, sexp.column));
		return new Node_Object(
   			parseMult1<Node_Worker>(parseWorker, sexp) );
	}

	protected Node_Bundle parseBundle(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Bundle(
   			parseMult0<Node_Import>(parseImport, sexp),
			parseMult0<INode_ScopeAlteration>(parseScopeAlteration, sexp),
			parseMult1<Node_Plane>(parsePlane, sexp) );
	}

	protected Node_Plane parsePlane(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Plane(
   			parseMult0<INode_ScopeAlteration>(parseScopeAlteration, sexp),
			parseMult1<Node_DeclareFirst>(parseDeclareFirst, sexp) );
	}

	protected Node_Xor parseXor(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Xor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ExtractMember parseExtractMember(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_ExtractMember(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Implements parseImplements(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 2 children",
					sexp.line, sexp.column));
		return new Node_Implements(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected INode_Expression parseExpression(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalExpression(sexp);
		if( sexp.list.Count == 0 )
			throw new ParseError(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseExpressionDefault(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "do-while":
				return parseDoWhile(sexp);
			case "do-times":
				return parseDoTimes(sexp);
			case "for-key":
				return parseForKey(sexp);
			case "for-pair":
				return parseForPair(sexp);
			case "for-manual":
				return parseForManual(sexp);
			case "for-range":
				return parseForRange(sexp);
			case "for-value":
				return parseForValue(sexp);
			case "loop":
				return parseLoop(sexp);
			case "while":
				return parseWhile(sexp);
			case "break":
				return parseBreak(sexp);
			case "continue":
				return parseContinue(sexp);
			case "declare-empty":
				return parseDeclareEmpty(sexp);
			case "labeled":
				return parseLabeled(sexp);
			case "null":
				return parseNull(sexp);
			case "return":
				return parseReturn(sexp);
			case "throw":
				return parseThrow(sexp);
			case "yield":
				return parseYield(sexp);
			case "assign":
				return parseAssign(sexp);
			case "block":
				return parseBlock(sexp);
			case "call":
				return parseCall(sexp);
			case "cast":
				return parseCast(sexp);
			case "conditional":
				return parseConditional(sexp);
			case "curry":
				return parseCurry(sexp);
			case "declare-assign":
				return parseDeclareAssign(sexp);
			case "declare-first":
				return parseDeclareFirst(sexp);
			case "ignore":
				return parseIgnore(sexp);
			case "namespaced-value-identikey":
				return parseNamespacedValueIdentikey(sexp);
			case "possibility":
				return parsePossibility(sexp);
			case "select":
				return parseSelect(sexp);
			case "try-catch":
				return parseTryCatch(sexp);
			case "and":
				return parseAnd(sexp);
			case "nand":
				return parseNand(sexp);
			case "or":
				return parseOr(sexp);
			case "nor":
				return parseNor(sexp);
			case "xor":
				return parseXor(sexp);
			case "xnor":
				return parseXnor(sexp);
			case "array":
				return parseArray(sexp);
			case "breed":
				return parseBreed(sexp);
			case "bundle":
				return parseBundle(sexp);
			case "caller":
				return parseCaller(sexp);
			case "chain":
				return parseChain(sexp);
			case "object":
				return parseObject(sexp);
			case "comprehension":
				return parseComprehension(sexp);
			case "dictionary":
				return parseDictionary(sexp);
			case "enum":
				return parseEnum(sexp);
			case "extract-member":
				return parseExtractMember(sexp);
			case "function":
				return parseFunction(sexp);
			case "function-interface":
				return parseFunctionInterface(sexp);
			case "generator":
				return parseGenerator(sexp);
			case "generic-function":
				return parseGenericFunction(sexp);
			case "generic-interface":
				return parseGenericInterface(sexp);
			case "identifier":
				return parseIdentifier(sexp);
			case "instantiate-generic":
				return parseInstantiateGeneric(sexp);
			case "integer":
				return parseInteger(sexp);
			case "interface":
				return parseInterface(sexp);
			case "implements":
				return parseImplements(sexp);
			case "rational":
				return parseRational(sexp);
			case "set-property":
				return parseSetProperty(sexp);
			case "string":
				return parseString(sexp);
			default:
				sexp.list.AddFirst(first);
				return parseExpressionDefault(sexp);
		}
	}

	protected Node_Assign parseAssign(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"node at [{0}:{1}] must have 3 children",
					sexp.line, sexp.column));
		return new Node_Assign(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}
}

} //end namespace Desexp

