//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;
using System.Xml;
using Acrid.NodeTypes;

namespace Acrid.Desible {

public abstract class DesibleParserAuto : DesibleParserBase {
	protected virtual Node_And parseAnd(XmlElement element) {
		checkElement(element, "and");
		return new Node_And(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_MemberStatus parseMemberStatus(XmlElement element) {
		checkElement(element, "member-status");
		return new Node_MemberStatus(element.InnerText, getSource(element));
	}

	protected virtual Node_DeclareFirst parseDeclareFirst(XmlElement element) {
		checkElement(element, "declare-first");
		return new Node_DeclareFirst(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "overload"),
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "breed"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Conditional parseConditional(XmlElement element) {
		checkElement(element, "conditional");
		return new Node_Conditional(
			parseOne<INode_Expression>(parseExpression, element, "*", "test"),
			parseOne<INode_Expression>(parseExpression, element, "*", "result"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "else"),
			getSource(element) );
	}

	protected virtual Node_Argument parseArgument(XmlElement element) {
		checkElement(element, "argument");
		return new Node_Argument(
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "parameter name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Module parseModule(XmlElement element) {
		checkElement(element, "module");
		return new Node_Module(
			parseOne<Node_Integer>(parseInteger, element, "*", "niviok major version number"),
			parseOne<Node_Integer>(parseInteger, element, "*", "niviok minor version number"),
			parseMult<Node_Import>(parseImport, element, "import", null),
			parseOne<Node_Sieve>(parseSieve, element, "sieve", null),
			getSource(element) );
	}

	protected virtual Node_Integer parseInteger(XmlElement element) {
		checkElement(element, "integer");
		return new Node_Integer(element.InnerText, getSource(element));
	}

	protected virtual Node_Boolean parseBoolean(XmlElement element) {
		checkElement(element, "boolean");
		return new Node_Boolean(element.InnerText, getSource(element));
	}

	protected virtual Node_Yield parseYield(XmlElement element) {
		checkElement(element, "yield");
		return new Node_Yield(
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_DictionaryEntry parseDictionaryEntry(XmlElement element) {
		checkElement(element, "dictionary-entry");
		return new Node_DictionaryEntry(
			parseOne<INode_Expression>(parseExpression, element, "*", "key"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Property parseProperty(XmlElement element) {
		checkElement(element, "property");
		return new Node_Property(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "writable"),
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			getSource(element) );
	}

	protected virtual Node_MemberType parseMemberType(XmlElement element) {
		checkElement(element, "member-type");
		return new Node_MemberType(element.InnerText, getSource(element));
	}

	protected virtual Node_ImportAttempt parseImportAttempt(XmlElement element) {
		checkElement(element, "import-attempt");
		return new Node_ImportAttempt(
			parseOne<Node_String>(parseString, element, "*", "scheme"),
			parseOne<Node_String>(parseString, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_Select parseSelect(XmlElement element) {
		checkElement(element, "select");
		return new Node_Select(
			parseOne<INode_Expression>(parseExpression, element, "*", "input value"),
			parseMult<Node_Case>(parseCase, element, "case", null),
			parseOpt<INode_Expression>(parseExpression, element, "*", "else"),
			getSource(element) );
	}

	protected virtual Node_String parseString(XmlElement element) {
		checkElement(element, "string");
		return new Node_String(element.InnerText, getSource(element));
	}

	protected virtual Node_GenericInterface parseGenericInterface(XmlElement element) {
		checkElement(element, "generic-interface");
		return new Node_GenericInterface(
			parseMult<Node_ParameterInfo>(parseParameterInfo, element, "*", "parameter"),
			parseOne<Node_Interface>(parseInterface, element, "interface", null),
			getSource(element) );
	}

	protected virtual Node_ParameterImpl parseParameterImpl(XmlElement element) {
		checkElement(element, "parameter-impl");
		return new Node_ParameterImpl(
			parseOne<Node_Direction>(parseDirection, element, "direction", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "default value"),
			getSource(element) );
	}

	protected virtual Node_Xor parseXor(XmlElement element) {
		checkElement(element, "xor");
		return new Node_Xor(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Generator parseGenerator(XmlElement element) {
		checkElement(element, "generator");
		return new Node_Generator(
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseOne<INode_Expression>(parseExpression, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_GenericFunction parseGenericFunction(XmlElement element) {
		checkElement(element, "generic-function");
		return new Node_GenericFunction(
			parseMult<Node_ParameterInfo>(parseParameterInfo, element, "*", "parameter"),
			parseOne<Node_Function>(parseFunction, element, "function", null),
			getSource(element) );
	}

	protected virtual Node_Breed parseBreed(XmlElement element) {
		checkElement(element, "breed");
		return new Node_Breed(
			parseOne<INode_Expression>(parseExpression, element, "*", "parent"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_Nand parseNand(XmlElement element) {
		checkElement(element, "nand");
		return new Node_Nand(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Hidable parseHidable(XmlElement element) {
		checkElement(element, "hidable");
		return new Node_Hidable(
			parseOne<Node_Boolean>(parseBoolean, element, "*", "hidden"),
			parseOne<INode_StatementDeclaration>(parseStatementDeclaration, element, "*", "declaration"),
			getSource(element) );
	}

	protected virtual Node_Call parseCall(XmlElement element) {
		checkElement(element, "call");
		return new Node_Call(
			parseOne<INode_Expression>(parseExpression, element, "*", "receiver"),
			parseMult<Node_Argument>(parseArgument, element, "argument", null),
			getSource(element) );
	}

	protected virtual Node_Nor parseNor(XmlElement element) {
		checkElement(element, "nor");
		return new Node_Nor(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Rational parseRational(XmlElement element) {
		checkElement(element, "rational");
		return new Node_Rational(element.InnerText, getSource(element));
	}

	protected virtual Node_TypeCase parseTypeCase(XmlElement element) {
		checkElement(element, "type-case");
		return new Node_TypeCase(
			parseMult<INode_Expression>(parseExpression, element, "*", "test type"),
			parseOne<INode_Expression>(parseExpression, element, "*", "result"),
			getSource(element) );
	}

	protected virtual Node_Import parseImport(XmlElement element) {
		checkElement(element, "import");
		return new Node_Import(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "alias"),
			parseMult<Node_ImportAttempt>(parseImportAttempt, element, "import-attempt", null),
			getSource(element) );
	}

	protected virtual Node_Method parseMethod(XmlElement element) {
		checkElement(element, "method");
		return new Node_Method(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_Function parseFunction(XmlElement element) {
		checkElement(element, "function");
		return new Node_Function(
			parseMult<Node_ParameterImpl>(parseParameterImpl, element, "parameter-impl", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "return type"),
			parseOne<INode_Expression>(parseExpression, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_Xnor parseXnor(XmlElement element) {
		checkElement(element, "xnor");
		return new Node_Xnor(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Direction parseDirection(XmlElement element) {
		checkElement(element, "direction");
		return new Node_Direction(element.InnerText, getSource(element));
	}

	protected virtual Node_Object parseObject(XmlElement element) {
		checkElement(element, "object");
		return new Node_Object(
			parseMult<Node_Worker>(parseWorker, element, "worker", null),
			getSource(element) );
	}

	protected virtual Node_Curry parseCurry(XmlElement element) {
		checkElement(element, "curry");
		return new Node_Curry(
			parseOne<INode_Expression>(parseExpression, element, "*", "function"),
			parseMult<Node_Argument>(parseArgument, element, "argument", null),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "call"),
			getSource(element) );
	}

	protected virtual Node_Dictionary parseDictionary(XmlElement element) {
		checkElement(element, "dictionary");
		return new Node_Dictionary(
			parseOne<INode_Expression>(parseExpression, element, "*", "key type"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value type"),
			parseMult<Node_DictionaryEntry>(parseDictionaryEntry, element, "dictionary-entry", null),
			getSource(element) );
	}

	protected virtual Node_Callee parseCallee(XmlElement element) {
		checkElement(element, "callee");
		return new Node_Callee(
			parseMult<Node_ParameterInfo>(parseParameterInfo, element, "parameter-info", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "return type"),
			getSource(element) );
	}

	protected virtual Node_InstantiateGeneric parseInstantiateGeneric(XmlElement element) {
		checkElement(element, "instantiate-generic");
		return new Node_InstantiateGeneric(
			parseOne<INode_Expression>(parseExpression, element, "*", "generic"),
			parseMult<Node_Argument>(parseArgument, element, "argument", null),
			getSource(element) );
	}

	protected virtual Node_DeclareEmpty parseDeclareEmpty(XmlElement element) {
		checkElement(element, "declare-empty");
		return new Node_DeclareEmpty(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			getSource(element) );
	}

	protected virtual Node_Worker parseWorker(XmlElement element) {
		checkElement(element, "worker");
		return new Node_Worker(
			parseOne<INode_Expression>(parseExpression, element, "*", "face"),
			parseMult<Node_Worker>(parseWorker, element, "*", "child worker"),
			parseMult<Node_MemberImplementation>(parseMemberImplementation, element, "member-implementation", null),
			getSource(element) );
	}

	protected virtual Node_FunctionInterface parseFunctionInterface(XmlElement element) {
		checkElement(element, "function-interface");
		return new Node_FunctionInterface(
			parseOpt<INode_Expression>(parseExpression, element, "*", "template-argument-count"),
			parseMult<Node_ParameterInfo>(parseParameterInfo, element, "parameter-info", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "return type"),
			getSource(element) );
	}

	protected virtual Node_Enum parseEnum(XmlElement element) {
		checkElement(element, "enum");
		return new Node_Enum(
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseMult<Node_EnumEntry>(parseEnumEntry, element, "enum-entry", null),
			getSource(element) );
	}

	protected virtual Node_DeclareAssign parseDeclareAssign(XmlElement element) {
		checkElement(element, "declare-assign");
		return new Node_DeclareAssign(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "constant"),
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "breed"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Compound parseCompound(XmlElement element) {
		checkElement(element, "compound");
		return new Node_Compound(
			parseMult<INode_Expression>(parseExpression, element, "*", "expose"),
			parseMult<INode_StatementDeclaration>(parseStatementDeclaration, element, "*", "declaration"),
			parseMult<INode_Expression>(parseExpression, element, "*", "member"),
			getSource(element) );
	}

	protected virtual Node_Interface parseInterface(XmlElement element) {
		checkElement(element, "interface");
		return new Node_Interface(
			parseMult<INode_Expression>(parseExpression, element, "*", "inheritee"),
			parseMult<Node_StatusedMember>(parseStatusedMember, element, "*", "member"),
			getSource(element) );
	}

	protected virtual INode_StatementDeclaration parseStatementDeclaration(XmlElement element) {
		switch(element.LocalName) {
			case "declare-first":
				return parseDeclareFirst(element);
			case "sieve":
				return parseSieve(element);
			default:
				throw new ParseError(
					String.Format(
						"element with name '{0}' is not recognized as a StatementDeclaration node",
						element.LocalName),
					getSource(element));
		}
	}

	protected virtual Node_SetProperty parseSetProperty(XmlElement element) {
		checkElement(element, "set-property");
		return new Node_SetProperty(
			parseOne<INode_Expression>(parseExpression, element, "*", "source"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "property name"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Sieve parseSieve(XmlElement element) {
		checkElement(element, "sieve");
		return new Node_Sieve(
			parseMult<INode_Expression>(parseExpression, element, "*", "expose"),
			parseMult<Node_Hidable>(parseHidable, element, "hidable", null),
			getSource(element) );
	}

	protected virtual Node_ExtractMember parseExtractMember(XmlElement element) {
		checkElement(element, "extract-member");
		return new Node_ExtractMember(
			parseOne<INode_Expression>(parseExpression, element, "*", "source"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "member name"),
			getSource(element) );
	}

	protected virtual Node_Throw parseThrow(XmlElement element) {
		checkElement(element, "throw");
		return new Node_Throw(
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_TypeSelect parseTypeSelect(XmlElement element) {
		checkElement(element, "type-select");
		return new Node_TypeSelect(
			parseOne<INode_Expression>(parseExpression, element, "*", "input value"),
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "casted name"),
			parseOpt<Node_Boolean>(parseBoolean, element, "*", "require match"),
			parseMult<Node_TypeCase>(parseTypeCase, element, "type-case", null),
			parseOpt<INode_Expression>(parseExpression, element, "*", "else"),
			getSource(element) );
	}

	protected virtual Node_Case parseCase(XmlElement element) {
		checkElement(element, "case");
		return new Node_Case(
			parseMult<INode_Expression>(parseExpression, element, "*", "test value"),
			parseOne<INode_Expression>(parseExpression, element, "*", "result"),
			getSource(element) );
	}

	protected virtual Node_Catcher parseCatcher(XmlElement element) {
		checkElement(element, "catcher");
		return new Node_Catcher(
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "test"),
			parseOne<INode_Expression>(parseExpression, element, "*", "result"),
			getSource(element) );
	}

	protected virtual Node_TryCatch parseTryCatch(XmlElement element) {
		checkElement(element, "try-catch");
		return new Node_TryCatch(
			parseOne<INode_Expression>(parseExpression, element, "*", "try"),
			parseMult<Node_Catcher>(parseCatcher, element, "catcher", null),
			parseOpt<INode_Expression>(parseExpression, element, "*", "on success"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "finally"),
			getSource(element) );
	}

	protected virtual Node_ParameterInfo parseParameterInfo(XmlElement element) {
		checkElement(element, "parameter-info");
		return new Node_ParameterInfo(
			parseOne<Node_Direction>(parseDirection, element, "direction", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "type"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "has default value"),
			getSource(element) );
	}

	protected virtual Node_Caller parseCaller(XmlElement element) {
		checkElement(element, "caller");
		return new Node_Caller(
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "method name"),
			getSource(element) );
	}

	protected virtual Node_Remit parseRemit(XmlElement element) {
		checkElement(element, "remit");
		return new Node_Remit(
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "label"),
			getSource(element) );
	}

	protected virtual Node_Or parseOr(XmlElement element) {
		checkElement(element, "or");
		return new Node_Or(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_MemberImplementation parseMemberImplementation(XmlElement element) {
		checkElement(element, "member-implementation");
		return new Node_MemberImplementation(
			parseOne<Node_MemberType>(parseMemberType, element, "member-type", null),
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			parseOne<INode_Expression>(parseExpression, element, "*", "function"),
			getSource(element) );
	}

	protected virtual Node_StatusedMember parseStatusedMember(XmlElement element) {
		checkElement(element, "statused-member");
		return new Node_StatusedMember(
			parseOne<Node_MemberStatus>(parseMemberStatus, element, "member-status", null),
			parseOne<INode_InterfaceMember>(parseInterfaceMember, element, "*", "member"),
			getSource(element) );
	}

	protected virtual INode_Expression parseExpression(XmlElement element) {
		switch(element.LocalName) {
			case "remit":
				return parseRemit(element);
			case "throw":
				return parseThrow(element);
			case "yield":
				return parseYield(element);
			case "declare-empty":
				return parseDeclareEmpty(element);
			case "assign":
				return parseAssign(element);
			case "call":
				return parseCall(element);
			case "compound":
				return parseCompound(element);
			case "conditional":
				return parseConditional(element);
			case "curry":
				return parseCurry(element);
			case "declare-assign":
				return parseDeclareAssign(element);
			case "identifier":
				return parseIdentifier(element);
			case "select":
				return parseSelect(element);
			case "set-property":
				return parseSetProperty(element);
			case "try-catch":
				return parseTryCatch(element);
			case "type-select":
				return parseTypeSelect(element);
			case "and":
				return parseAnd(element);
			case "nand":
				return parseNand(element);
			case "or":
				return parseOr(element);
			case "nor":
				return parseNor(element);
			case "xor":
				return parseXor(element);
			case "xnor":
				return parseXnor(element);
			case "breed":
				return parseBreed(element);
			case "caller":
				return parseCaller(element);
			case "object":
				return parseObject(element);
			case "dictionary":
				return parseDictionary(element);
			case "enum":
				return parseEnum(element);
			case "extract-member":
				return parseExtractMember(element);
			case "function":
				return parseFunction(element);
			case "function-interface":
				return parseFunctionInterface(element);
			case "generator":
				return parseGenerator(element);
			case "generic-function":
				return parseGenericFunction(element);
			case "generic-interface":
				return parseGenericInterface(element);
			case "instantiate-generic":
				return parseInstantiateGeneric(element);
			case "integer":
				return parseInteger(element);
			case "interface":
				return parseInterface(element);
			case "rational":
				return parseRational(element);
			case "string":
				return parseString(element);
			default:
				throw new ParseError(
					String.Format(
						"element with name '{0}' is not recognized as a Expression node",
						element.LocalName),
					getSource(element));
		}
	}

	protected virtual Node_EnumEntry parseEnumEntry(XmlElement element) {
		checkElement(element, "enum-entry");
		return new Node_EnumEntry(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Breeder parseBreeder(XmlElement element) {
		checkElement(element, "breeder");
		return new Node_Breeder(
			parseOpt<INode_Expression>(parseExpression, element, "*", "type"),
			getSource(element) );
	}

	protected virtual Node_Identifier parseIdentifier(XmlElement element) {
		checkElement(element, "identifier");
		return new Node_Identifier(element.InnerText, getSource(element));
	}

	protected virtual INode_InterfaceMember parseInterfaceMember(XmlElement element) {
		switch(element.LocalName) {
			case "breeder":
				return parseBreeder(element);
			case "callee":
				return parseCallee(element);
			case "property":
				return parseProperty(element);
			case "method":
				return parseMethod(element);
			default:
				throw new ParseError(
					String.Format(
						"element with name '{0}' is not recognized as a InterfaceMember node",
						element.LocalName),
					getSource(element));
		}
	}

	protected virtual Node_Assign parseAssign(XmlElement element) {
		checkElement(element, "assign");
		return new Node_Assign(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "breed"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}
}

} //namespace

