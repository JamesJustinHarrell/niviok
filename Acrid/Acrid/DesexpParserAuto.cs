
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;

using System.Xml;

namespace Desexp {

abstract class DesexpParserAuto : DesexpParserBase {
	protected virtual Node_And parseAnd(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'and' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_And(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Function parseFunction(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'function' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Function(
   			parseMult0<Node_ParameterImpl>(parseParameterImpl, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_DeclareFirst parseDeclareFirst(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"'declare-first' node must have 4 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_DeclareFirst(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_InstantiateGeneric parseInstantiateGeneric(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'instantiate-generic' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_InstantiateGeneric(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult1<Node_Argument>(parseArgument, sexp),
			getSource(sexp) );
	}

	protected virtual Node_GenericParameter parseGenericParameter(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'generic-parameter' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_GenericParameter(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Xnor parseXnor(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'xnor' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Xnor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Conditional parseConditional(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'conditional' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Conditional(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_GenericFunction parseGenericFunction(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'generic-function' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_GenericFunction(
   			parseMult1<Node_GenericParameter>(parseGenericParameter, sexp),
			parseOne<Node_Function>(parseFunction, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Argument parseArgument(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'argument' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Argument(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Module parseModule(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'module' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Module(
   			parseMult0<Node_Import>(parseImport, sexp),
			parseOne<Node_LimitOld>(parseLimitOld, sexp),
			getSource(sexp) );
	}

	protected virtual Node_ConditionalLoop parseConditionalLoop(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'conditional-loop' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_ConditionalLoop(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Integer parseInteger(Sexp sexp) {
		try {
			return new Node_Integer(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type Integer cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_Boolean parseBoolean(Sexp sexp) {
		try {
			return new Node_Boolean(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type Boolean cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_DictionaryEntry parseDictionaryEntry(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'dictionary-entry' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_DictionaryEntry(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual INode_IdentikeySpecialOld parseIdentikeySpecialOld(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalIdentikeySpecialOld(sexp);
		if( sexp.list.Count == 0 )
			throw new ParseError(
				"this list cannot be empty",
				getSource(sexp));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseNonwordIdentikeySpecialOld(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "using":
				return parseUsing(sexp);
			case "expose":
				return parseExpose(sexp);
			default:
				sexp.list.AddFirst(first);
				return parseIdentikeySpecialOldDefault(sexp);
		}
	}
	protected virtual INode_IdentikeySpecialOld parseTerminalIdentikeySpecialOld(Sexp sexp) {
		throw new ParseError(
			"IdentikeySpecialOld expression must be a list",
			getSource(sexp));
	}
	protected virtual INode_IdentikeySpecialOld parseNonwordIdentikeySpecialOld(Sexp sexp) {
		throw new ParseError(
			"expression must begin with a word",
			getSource(sexp));
	}
	protected virtual INode_IdentikeySpecialOld parseIdentikeySpecialOldDefault(Sexp sexp) {
		throw new ParseError(
			String.Format(
				"unknown type of IdentikeySpecialOld '{0}'",
				sexp.list.First.Value.atom),
			getSource(sexp));
	}

	protected virtual Node_MemberType parseMemberType(Sexp sexp) {
		try {
			return new Node_MemberType(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type MemberType cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_ImportAttempt parseImportAttempt(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'import-attempt' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_ImportAttempt(
   			parseOne<Node_String>(parseString, sexp),
			parseOne<Node_String>(parseString, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Select parseSelect(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'select' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Select(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Case>(parseCase, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_String parseString(Sexp sexp) {
		try {
			return new Node_String(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type String cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_GenericInterface parseGenericInterface(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'generic-interface' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_GenericInterface(
   			parseMult1<Node_GenericParameter>(parseGenericParameter, sexp),
			parseOne<Node_Interface>(parseInterface, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Ignore parseIgnore(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'ignore' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Ignore(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult1<Node_IgnoreMember>(parseIgnoreMember, sexp),
			getSource(sexp) );
	}

	protected virtual Node_ParameterImpl parseParameterImpl(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"'parameter-impl' node must have 4 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_ParameterImpl(
   			parseOne<Node_Direction>(parseDirection, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Null parseNull(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'null' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Null(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_MemberIdentification parseMemberIdentification(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'member-identification' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_MemberIdentification(
   			parseOne<Node_MemberType>(parseMemberType, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Generator parseGenerator(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'generator' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Generator(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Cast parseCast(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'cast' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Cast(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			getSource(sexp) );
	}

	protected virtual Node_ExceptionHandler parseExceptionHandler(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"'exception-handler' node must have 4 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_ExceptionHandler(
   			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Breed parseBreed(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'breed' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Breed(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Namespace parseNamespace(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'namespace' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Namespace(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_LimitOld>(parseLimitOld, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Labeled parseLabeled(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'labeled' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Labeled(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Hidable parseHidable(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'hidable' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Hidable(
   			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_IdentikeySpecialNew>(parseIdentikeySpecialNew, sexp),
			getSource(sexp) );
	}

	protected virtual Node_IdentikeyCategory parseIdentikeyCategory(Sexp sexp) {
		try {
			return new Node_IdentikeyCategory(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type IdentikeyCategory cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_Call parseCall(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'call' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Call(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Argument>(parseArgument, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Implements parseImplements(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'implements' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Implements(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Rational parseRational(Sexp sexp) {
		try {
			return new Node_Rational(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type Rational cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_FunctionInterface parseFunctionInterface(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'function-interface' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_FunctionInterface(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_ParameterInfo>(parseParameterInfo, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Import parseImport(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'import' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Import(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseMult1<Node_ImportAttempt>(parseImportAttempt, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Method parseMethod(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'method' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Method(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_NamespacedValueIdentikey parseNamespacedValueIdentikey(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'namespaced-value-identikey' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_NamespacedValueIdentikey(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Case parseCase(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'case' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Case(
   			parseMult1<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Direction parseDirection(Sexp sexp) {
		try {
			return new Node_Direction(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type Direction cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual Node_Xor parseXor(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'xor' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Xor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Return parseReturn(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'return' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Return(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Continue parseContinue(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'continue' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Continue(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Dictionary parseDictionary(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'dictionary' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Dictionary(
   			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			parseMult0<Node_DictionaryEntry>(parseDictionaryEntry, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Callee parseCallee(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'callee' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Callee(
   			parseMult0<Node_ParameterInfo>(parseParameterInfo, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Expose parseExpose(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'expose' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Expose(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_DeclareEmpty parseDeclareEmpty(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'declare-empty' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_DeclareEmpty(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Worker parseWorker(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'worker' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Worker(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Worker>(parseWorker, sexp),
			parseMult0<Node_MemberImplementation>(parseMemberImplementation, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Break parseBreak(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'break' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Break(
   			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Nand parseNand(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'nand' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Nand(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Enum parseEnum(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'enum' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Enum(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseMult1<Node_EnumEntry>(parseEnumEntry, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Curry parseCurry(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'curry' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Curry(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Argument>(parseArgument, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			getSource(sexp) );
	}

	protected virtual Node_LimitOld parseLimitOld(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'limit-old' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_LimitOld(
   			parseMult0<INode_IdentikeySpecialOld>(parseIdentikeySpecialOld, sexp),
			parseMult0<Node_Hidable>(parseHidable, sexp),
			getSource(sexp) );
	}

	protected virtual Node_NullableType parseNullableType(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'nullable-type' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_NullableType(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			getSource(sexp) );
	}

	protected virtual Node_EnumeratorLoop parseEnumeratorLoop(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"'enumerator-loop' node must have 4 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_EnumeratorLoop(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_Receiver>(parseReceiver, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Compound parseCompound(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'compound' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Compound(
   			parseMult0<INode_IdentikeySpecialOld>(parseIdentikeySpecialOld, sexp),
			parseMult0<INode_IdentikeySpecialNew>(parseIdentikeySpecialNew, sexp),
			parseMult1<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Interface parseInterface(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'interface' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Interface(
   			parseMult0<INode_Expression>(parseExpression, sexp),
			parseMult0<INode_InterfaceMember>(parseInterfaceMember, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Using parseUsing(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'using' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Using(
   			parseMult1<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_SetProperty parseSetProperty(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'set-property' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_SetProperty(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_ExtractMember parseExtractMember(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'extract-member' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_ExtractMember(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Throw parseThrow(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'throw' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Throw(
   			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Nor parseNor(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'nor' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Nor(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_IgnoreMember parseIgnoreMember(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'ignore-member' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_IgnoreMember(
   			parseOne<Node_String>(parseString, sexp),
			parseOne<Node_Integer>(parseInteger, sexp),
			getSource(sexp) );
	}

	protected virtual INode_IdentikeySpecialNew parseIdentikeySpecialNew(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalIdentikeySpecialNew(sexp);
		if( sexp.list.Count == 0 )
			throw new ParseError(
				"this list cannot be empty",
				getSource(sexp));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseNonwordIdentikeySpecialNew(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "declare-first":
				return parseDeclareFirst(sexp);
			case "limit-old":
				return parseLimitOld(sexp);
			case "namespace":
				return parseNamespace(sexp);
			default:
				sexp.list.AddFirst(first);
				return parseIdentikeySpecialNewDefault(sexp);
		}
	}
	protected virtual INode_IdentikeySpecialNew parseTerminalIdentikeySpecialNew(Sexp sexp) {
		throw new ParseError(
			"IdentikeySpecialNew expression must be a list",
			getSource(sexp));
	}
	protected virtual INode_IdentikeySpecialNew parseNonwordIdentikeySpecialNew(Sexp sexp) {
		throw new ParseError(
			"expression must begin with a word",
			getSource(sexp));
	}
	protected virtual INode_IdentikeySpecialNew parseIdentikeySpecialNewDefault(Sexp sexp) {
		throw new ParseError(
			String.Format(
				"unknown type of IdentikeySpecialNew '{0}'",
				sexp.list.First.Value.atom),
			getSource(sexp));
	}

	protected virtual Node_Property parseProperty(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'property' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Property(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Object parseObject(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'object' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Object(
   			parseMult1<Node_Worker>(parseWorker, sexp),
			getSource(sexp) );
	}

	protected virtual Node_TryCatch parseTryCatch(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"'try-catch' node must have 4 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_TryCatch(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseMult0<Node_ExceptionHandler>(parseExceptionHandler, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_ParameterInfo parseParameterInfo(Sexp sexp) {
		if( sexp.list.Count != 4 )
			throw new ParseError(
				String.Format(
					"'parameter-info' node must have 4 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_ParameterInfo(
   			parseOne<Node_Direction>(parseDirection, sexp),
			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Caller parseCaller(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'caller' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Caller(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Yield parseYield(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'yield' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Yield(
   			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Or parseOr(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'or' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Or(
   			parseOne<INode_Expression>(parseExpression, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_MemberImplementation parseMemberImplementation(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'member-implementation' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_MemberImplementation(
   			parseOne<Node_MemberIdentification>(parseMemberIdentification, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual INode_InterfaceMember parseInterfaceMember(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalInterfaceMember(sexp);
		if( sexp.list.Count == 0 )
			throw new ParseError(
				"this list cannot be empty",
				getSource(sexp));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseNonwordInterfaceMember(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
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
				sexp.list.AddFirst(first);
				return parseInterfaceMemberDefault(sexp);
		}
	}
	protected virtual INode_InterfaceMember parseTerminalInterfaceMember(Sexp sexp) {
		throw new ParseError(
			"InterfaceMember expression must be a list",
			getSource(sexp));
	}
	protected virtual INode_InterfaceMember parseNonwordInterfaceMember(Sexp sexp) {
		throw new ParseError(
			"expression must begin with a word",
			getSource(sexp));
	}
	protected virtual INode_InterfaceMember parseInterfaceMemberDefault(Sexp sexp) {
		throw new ParseError(
			String.Format(
				"unknown type of InterfaceMember '{0}'",
				sexp.list.First.Value.atom),
			getSource(sexp));
	}

	protected virtual Node_DeclareAssign parseDeclareAssign(Sexp sexp) {
		if( sexp.list.Count != 5 )
			throw new ParseError(
				String.Format(
					"'declare-assign' node must have 5 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_DeclareAssign(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_IdentikeyType parseIdentikeyType(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'identikey-type' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_IdentikeyType(
   			parseOne<Node_IdentikeyCategory>(parseIdentikeyCategory, sexp),
			parseOne<Node_NullableType>(parseNullableType, sexp),
			getSource(sexp) );
	}

	protected virtual Node_EnumEntry parseEnumEntry(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'enum-entry' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_EnumEntry(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Receiver parseReceiver(Sexp sexp) {
		if( sexp.list.Count != 2 )
			throw new ParseError(
				String.Format(
					"'receiver' node must have 2 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Receiver(
   			parseOpt<Node_NullableType>(parseNullableType, sexp),
			parseOne<Node_Identifier>(parseIdentifier, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Breeder parseBreeder(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'breeder' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Breeder(
   			parseOpt<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_UnconditionalLoop parseUnconditionalLoop(Sexp sexp) {
		if( sexp.list.Count != 1 )
			throw new ParseError(
				String.Format(
					"'unconditional-loop' node must have 1 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_UnconditionalLoop(
   			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}

	protected virtual Node_Identifier parseIdentifier(Sexp sexp) {
		try {
			return new Node_Identifier(sexp.atom, getSource(sexp));
		}
		catch(FormatException e) {
			throw new ParseError(
				String.Format(
					"node of type Identifier cannot be of value '{0}'",
					sexp.atom),
				getSource(sexp),
				e);
		}
	}

	protected virtual INode_Expression parseExpression(Sexp sexp) {
		if( sexp.type != SexpType.LIST )
			return parseTerminalExpression(sexp);
		if( sexp.list.Count == 0 )
			throw new ParseError(
				"this list cannot be empty",
				getSource(sexp));
		if( sexp.list.First.Value.type != SexpType.WORD )
			return parseNonwordExpression(sexp);
		Sexp first = sexp.list.First.Value;
		string specType = first.atom;
		sexp.list.RemoveFirst();
		switch(specType) {
			case "conditional-loop":
				return parseConditionalLoop(sexp);
			case "enumerator-loop":
				return parseEnumeratorLoop(sexp);
			case "unconditional-loop":
				return parseUnconditionalLoop(sexp);
			case "break":
				return parseBreak(sexp);
			case "continue":
				return parseContinue(sexp);
			case "return":
				return parseReturn(sexp);
			case "throw":
				return parseThrow(sexp);
			case "yield":
				return parseYield(sexp);
			case "declare-empty":
				return parseDeclareEmpty(sexp);
			case "null":
				return parseNull(sexp);
			case "assign":
				return parseAssign(sexp);
			case "call":
				return parseCall(sexp);
			case "cast":
				return parseCast(sexp);
			case "compound":
				return parseCompound(sexp);
			case "conditional":
				return parseConditional(sexp);
			case "curry":
				return parseCurry(sexp);
			case "declare-assign":
				return parseDeclareAssign(sexp);
			case "identifier":
				return parseIdentifier(sexp);
			case "ignore":
				return parseIgnore(sexp);
			case "labeled":
				return parseLabeled(sexp);
			case "namespaced-value-identikey":
				return parseNamespacedValueIdentikey(sexp);
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
			case "breed":
				return parseBreed(sexp);
			case "caller":
				return parseCaller(sexp);
			case "object":
				return parseObject(sexp);
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
	protected virtual INode_Expression parseTerminalExpression(Sexp sexp) {
		throw new ParseError(
			"Expression expression must be a list",
			getSource(sexp));
	}
	protected virtual INode_Expression parseNonwordExpression(Sexp sexp) {
		throw new ParseError(
			"expression must begin with a word",
			getSource(sexp));
	}
	protected virtual INode_Expression parseExpressionDefault(Sexp sexp) {
		throw new ParseError(
			String.Format(
				"unknown type of Expression '{0}'",
				sexp.list.First.Value.atom),
			getSource(sexp));
	}

	protected virtual Node_Assign parseAssign(Sexp sexp) {
		if( sexp.list.Count != 3 )
			throw new ParseError(
				String.Format(
					"'assign' node must have 3 children ({0} given)",
					sexp.list.Count),
				getSource(sexp));
		return new Node_Assign(
   			parseOne<Node_Identifier>(parseIdentifier, sexp),
			parseOne<Node_Boolean>(parseBoolean, sexp),
			parseOne<INode_Expression>(parseExpression, sexp),
			getSource(sexp) );
	}
}

} //end namespace Desexp

