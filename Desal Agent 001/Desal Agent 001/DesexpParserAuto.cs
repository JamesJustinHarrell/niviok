
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;
using System.Xml;

namespace Desexp {

abstract class DesexpParserAuto : DesexpParserBase {
	protected Node_Chain parseChain(Sexp sexp) {
		return new Node_Chain(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_SetProperty parseSetProperty(Sexp sexp) {
		return new Node_SetProperty(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ClassProperty parseClassProperty(Sexp sexp) {
		return new Node_ClassProperty(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOpt<Node_Function>(parseFunction, sexp),
			parseOpt<Node_Function>(parseFunction, sexp) );
	}

	protected Node_Using parseUsing(Sexp sexp) {
		return new Node_Using(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Generator parseGenerator(Sexp sexp) {
		return new Node_Generator(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_NamedFunction parseNamedFunction(Sexp sexp) {
		return new Node_NamedFunction(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Function>(parseFunction, sexp) );
	}

	protected Node_GenericFunction parseGenericFunction(Sexp sexp) {
		return new Node_GenericFunction(
   			parseMult1<Node_GenericParameter>(parseGenericParameter, sexp),
			parseOne<Node_Function>(parseFunction, sexp) );
	}

	protected Node_Xnor parseXnor(Sexp sexp) {
		return new Node_Xnor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Return parseReturn(Sexp sexp) {
		return new Node_Return(
   			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_DeclareEmpty parseDeclareEmpty(Sexp sexp) {
		return new Node_DeclareEmpty(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp) );
	}

	protected Node_Break parseBreak(Sexp sexp) {
		return new Node_Break(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Integer parseInteger(Sexp sexp) {
		try {
			return new Node_Integer(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type Integer at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Nor parseNor(Sexp sexp) {
		return new Node_Nor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_InterfaceImplementation parseInterfaceImplementation(Sexp sexp) {
		return new Node_InterfaceImplementation(
   			parseMult0<Node_InterfaceImplementation>(parseInterfaceImplementation, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Function>(parseFunction, sexp),
			parseMult0<Node_NamedFunction>(parseNamedFunction, sexp),
			parseMult0<Node_NamedFunction>(parseNamedFunction, sexp),
			parseMult0<Node_NamedFunction>(parseNamedFunction, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp) );
	}

	protected Node_Yield parseYield(Sexp sexp) {
		return new Node_Yield(
   			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Ignore parseIgnore(Sexp sexp) {
		return new Node_Ignore(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult1<Node_IgnoreMember>(parseIgnoreMember, sexp) );
	}

	protected Node_EnumEntry parseEnumEntry(Sexp sexp) {
		return new Node_EnumEntry(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Identifier parseIdentifier(Sexp sexp) {
		try {
			return new Node_Identifier(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type Identifier at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_DeclareFirst parseDeclareFirst(Sexp sexp) {
		return new Node_DeclareFirst(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_InstantiateGeneric parseInstantiateGeneric(Sexp sexp) {
		return new Node_InstantiateGeneric(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult1<Node_Argument>(parseArgument, sexp) );
	}

	protected Node_DictionaryEntry parseDictionaryEntry(Sexp sexp) {
		return new Node_DictionaryEntry(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForPair parseForPair(Sexp sexp) {
		return new Node_ForPair(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_Xor parseXor(Sexp sexp) {
		return new Node_Xor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForRange parseForRange(Sexp sexp) {
		return new Node_ForRange(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Nand parseNand(Sexp sexp) {
		return new Node_Nand(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Access parseAccess(Sexp sexp) {
		try {
			return new Node_Access(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type Access at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Rational parseRational(Sexp sexp) {
		try {
			return new Node_Rational(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type Rational at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Import parseImport(Sexp sexp) {
		return new Node_Import(
   			parseOne<Node_String>(parseString, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Method parseMethod(Sexp sexp) {
		return new Node_Method(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Dictionary parseDictionary(Sexp sexp) {
		return new Node_Dictionary(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<Node_DictionaryEntry>(parseDictionaryEntry, sexp) );
	}

	protected Node_Enum parseEnum(Sexp sexp) {
		return new Node_Enum(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseMult1<Node_EnumEntry>(parseEnumEntry, sexp) );
	}

	protected Node_Possibility parsePossibility(Sexp sexp) {
		return new Node_Possibility(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForKey parseForKey(Sexp sexp) {
		return new Node_ForKey(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_Throw parseThrow(Sexp sexp) {
		return new Node_Throw(
   			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_IdentikeyType parseIdentikeyType(Sexp sexp) {
		return new Node_IdentikeyType(
   			parseOne<Node_IdentikeyCategory>(parseIdentikeyCategory, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Convertor parseConvertor(Sexp sexp) {
		return new Node_Convertor(
   			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Curry parseCurry(Sexp sexp) {
		return new Node_Curry(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Argument>(parseArgument, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp) );
	}

	protected Node_Or parseOr(Sexp sexp) {
		return new Node_Or(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Block parseBlock(Sexp sexp) {
		return new Node_Block(
   			parseMult0<INode_ScopeAlteration>(parseScopeAlteration, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_GenericParameter parseGenericParameter(Sexp sexp) {
		return new Node_GenericParameter(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_While parseWhile(Sexp sexp) {
		return new Node_While(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_DeclareAssign parseDeclareAssign(Sexp sexp) {
		return new Node_DeclareAssign(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Interface parseInterface(Sexp sexp) {
		return new Node_Interface(
   			parseMult0<Node_Property>(parseProperty, sexp),
			parseMult0<Node_Method>(parseMethod, sexp) );
	}

	protected Node_Boolean parseBoolean(Sexp sexp) {
		try {
			return new Node_Boolean(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type Boolean at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Array parseArray(Sexp sexp) {
		return new Node_Array(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Select parseSelect(Sexp sexp) {
		return new Node_Select(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Case>(parseCase, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_IgnoreMember parseIgnoreMember(Sexp sexp) {
		return new Node_IgnoreMember(
   			parseOne<Node_String>(parseString, sexp),
			parseOne<Node_Integer>(parseInteger, sexp) );
	}

	protected Node_Breed parseBreed(Sexp sexp) {
		return new Node_Breed(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Call parseCall(Sexp sexp) {
		return new Node_Call(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Argument>(parseArgument, sexp) );
	}

	protected Node_Function parseFunction(Sexp sexp) {
		return new Node_Function(
   			parseMult0<Node_Parameter>(parseParameter, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Direction parseDirection(Sexp sexp) {
		try {
			return new Node_Direction(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type Direction at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_Conditional parseConditional(Sexp sexp) {
		return new Node_Conditional(
   			parseMult1<Node_Possibility>(parsePossibility, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Expose parseExpose(Sexp sexp) {
		return new Node_Expose(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_NullableType parseNullableType(Sexp sexp) {
		return new Node_NullableType(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp) );
	}

	protected Node_Case parseCase(Sexp sexp) {
		return new Node_Case(
   			parseMult1<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_TryCatch parseTryCatch(Sexp sexp) {
		return new Node_TryCatch(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_ExceptionHandler>(parseExceptionHandler, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Caller parseCaller(Sexp sexp) {
		return new Node_Caller(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Cast parseCast(Sexp sexp) {
		return new Node_Cast(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Labeled parseLabeled(Sexp sexp) {
		return new Node_Labeled(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForValue parseForValue(Sexp sexp) {
		return new Node_ForValue(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_Property parseProperty(Sexp sexp) {
		return new Node_Property(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Access>(parseAccess, sexp) );
	}

	protected Node_Callee parseCallee(Sexp sexp) {
		return new Node_Callee(
   			parseMult0<Node_Parameter>(parseParameter, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Loop parseLoop(Sexp sexp) {
		return new Node_Loop(
   			parseOne<Node_Block>(parseBlock, sexp) );
	}

	protected Node_And parseAnd(Sexp sexp) {
		return new Node_And(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_DeclareConstEmpty parseDeclareConstEmpty(Sexp sexp) {
		return new Node_DeclareConstEmpty(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp) );
	}

	protected Node_Argument parseArgument(Sexp sexp) {
		return new Node_Argument(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ForManual parseForManual(Sexp sexp) {
		return new Node_ForManual(
   			parseMult0<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseMult0<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_Block>(parseBlock, sexp) );
	}

	protected Node_DoWhile parseDoWhile(Sexp sexp) {
		return new Node_DoWhile(
   			parseOne<Node_Block>(parseBlock, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_DoTimes parseDoTimes(Sexp sexp) {
		return new Node_DoTimes(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Block>(parseBlock, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_ExceptionHandler parseExceptionHandler(Sexp sexp) {
		return new Node_ExceptionHandler(
   			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_IdentikeyCategory parseIdentikeyCategory(Sexp sexp) {
		try {
			return new Node_IdentikeyCategory(sexp.atom);
		}
		catch(Exception e) {
			throw new Exception(
				String.Format(
					"node of type IdentikeyCategory at [{0}:{1}] cannot be of value '{2}'",
					sexp.line, sexp.column, sexp.atom),
				e);
		}
	}

	protected Node_FunctionInterface parseFunctionInterface(Sexp sexp) {
		return new Node_FunctionInterface(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Parameter>(parseParameter, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp) );
	}

	protected Node_Parameter parseParameter(Sexp sexp) {
		return new Node_Parameter(
   			parseOne<Node_Direction>(parseDirection, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_NamespacedValueIdentikey parseNamespacedValueIdentikey(Sexp sexp) {
		return new Node_NamespacedValueIdentikey(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Plane parsePlane(Sexp sexp) {
		return new Node_Plane(
   			parseMult0<INode_ScopeAlteration>(parseScopeAlteration, sexp),
			parseMult1<Node_DeclareFirst>(parseDeclareFirst, sexp) );
	}

	protected Node_ExtractMember parseExtractMember(Sexp sexp) {
		return new Node_ExtractMember(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp) );
	}

	protected Node_Implements parseImplements(Sexp sexp) {
		return new Node_Implements(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected Node_Assign parseAssign(Sexp sexp) {
		return new Node_Assign(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp) );
	}

	protected INode_Expression parseExpression(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalExpression(sexp);
		if( sexp.list.Count == 0 )
			throw new Exception(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseExpressionDefault(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "chain":
				return parseChain(sexp);
			case "set-property":
				return parseSetProperty(sexp);
			case "generator":
				return parseGenerator(sexp);
			case "generic-function":
				return parseGenericFunction(sexp);
			case "xnor":
				return parseXnor(sexp);
			case "return":
				return parseReturn(sexp);
			case "string":
				return parseString(sexp);
			case "declare-empty":
				return parseDeclareEmpty(sexp);
			case "break":
				return parseBreak(sexp);
			case "integer":
				return parseInteger(sexp);
			case "nor":
				return parseNor(sexp);
			case "yield":
				return parseYield(sexp);
			case "ignore":
				return parseIgnore(sexp);
			case "identifier":
				return parseIdentifier(sexp);
			case "declare-first":
				return parseDeclareFirst(sexp);
			case "instantiate-generic":
				return parseInstantiateGeneric(sexp);
			case "for-pair":
				return parseForPair(sexp);
			case "xor":
				return parseXor(sexp);
			case "for-range":
				return parseForRange(sexp);
			case "nand":
				return parseNand(sexp);
			case "rational":
				return parseRational(sexp);
			case "dictionary":
				return parseDictionary(sexp);
			case "enum":
				return parseEnum(sexp);
			case "possibility":
				return parsePossibility(sexp);
			case "for-key":
				return parseForKey(sexp);
			case "throw":
				return parseThrow(sexp);
			case "curry":
				return parseCurry(sexp);
			case "or":
				return parseOr(sexp);
			case "block":
				return parseBlock(sexp);
			case "while":
				return parseWhile(sexp);
			case "declare-assign":
				return parseDeclareAssign(sexp);
			case "interface":
				return parseInterface(sexp);
			case "array":
				return parseArray(sexp);
			case "select":
				return parseSelect(sexp);
			case "breed":
				return parseBreed(sexp);
			case "call":
				return parseCall(sexp);
			case "function":
				return parseFunction(sexp);
			case "conditional":
				return parseConditional(sexp);
			case "try-catch":
				return parseTryCatch(sexp);
			case "caller":
				return parseCaller(sexp);
			case "cast":
				return parseCast(sexp);
			case "labeled":
				return parseLabeled(sexp);
			case "for-value":
				return parseForValue(sexp);
			case "loop":
				return parseLoop(sexp);
			case "and":
				return parseAnd(sexp);
			case "for-manual":
				return parseForManual(sexp);
			case "do-while":
				return parseDoWhile(sexp);
			case "do-times":
				return parseDoTimes(sexp);
			case "function-interface":
				return parseFunctionInterface(sexp);
			case "namespaced-value-identikey":
				return parseNamespacedValueIdentikey(sexp);
			case "extract-member":
				return parseExtractMember(sexp);
			case "implements":
				return parseImplements(sexp);
			case "assign":
				return parseAssign(sexp);
			default:
				sexp.list.AddFirst(first);
				return parseExpressionDefault(sexp);
		}
	}

	protected INode_ScopeAlteration parseScopeAlteration(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			throw new Exception(
				String.Format(
					"ScopeAlteration S-Expression at [{0}:{1}] must be a list",
					sexp.line, sexp.column));
		if( sexp.list.Count == 0 )
			throw new Exception(
				String.Format(
					"the list at [{0}:{1}] cannot be empty",
					sexp.line, sexp.column));
		if( sexp.list.First.Value.type != SexpType.WORD )
			throw new Exception(
				String.Format(
					"S-Expression at [{0}:{1}] must begin with a word",
					sexp.line, sexp.column));
		string specType = sexp.list.First.Value.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "using":
				return parseUsing(sexp);
			case "import":
				return parseImport(sexp);
			case "expose":
				return parseExpose(sexp);
			default:
				throw new Exception(
					String.Format(
						"unknown type of ScopeAlteration '{0}' at [{1}:{2}]",
						specType, sexp.line, sexp.column));
		}
	}
}

} //end namespace Desexp

