
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;

using System.Xml;

abstract class DesibleParserAuto : DesibleParserBase {
	protected virtual Node_And parseAnd(XmlElement element) {
		checkElement(element, "and");
		return new Node_And(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Function parseFunction(XmlElement element) {
		checkElement(element, "function");
		return new Node_Function(
			parseMult<Node_ParameterImpl>(parseParameterImpl, element, "parameter-impl", null),
			parseOpt<Node_NullableType>(parseNullableType, element, "*", "return-info"),
			parseOne<INode_Expression>(parseExpression, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_DeclareFirst parseDeclareFirst(XmlElement element) {
		checkElement(element, "declare-first");
		return new Node_DeclareFirst(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, element, "identikey-type", null),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "breed"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_InstantiateGeneric parseInstantiateGeneric(XmlElement element) {
		checkElement(element, "instantiate-generic");
		return new Node_InstantiateGeneric(
			parseOne<INode_Expression>(parseExpression, element, "*", "generic"),
			parseMult<Node_Argument>(parseArgument, element, "argument", null),
			getSource(element) );
	}

	protected virtual Node_GenericParameter parseGenericParameter(XmlElement element) {
		checkElement(element, "generic-parameter");
		return new Node_GenericParameter(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "default-interface"),
			getSource(element) );
	}

	protected virtual Node_Xnor parseXnor(XmlElement element) {
		checkElement(element, "xnor");
		return new Node_Xnor(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
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

	protected virtual Node_GenericFunction parseGenericFunction(XmlElement element) {
		checkElement(element, "generic-function");
		return new Node_GenericFunction(
			parseMult<Node_GenericParameter>(parseGenericParameter, element, "*", "parameter"),
			parseOne<Node_Function>(parseFunction, element, "function", null),
			getSource(element) );
	}

	protected virtual Node_Argument parseArgument(XmlElement element) {
		checkElement(element, "argument");
		return new Node_Argument(
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "parameter-name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Module parseModule(XmlElement element) {
		checkElement(element, "module");
		return new Node_Module(
			parseMult<Node_Import>(parseImport, element, "import", null),
			parseOne<Node_LimitOld>(parseLimitOld, element, "limit-old", null),
			getSource(element) );
	}

	protected virtual Node_ConditionalLoop parseConditionalLoop(XmlElement element) {
		checkElement(element, "conditional-loop");
		return new Node_ConditionalLoop(
			parseOne<INode_Expression>(parseExpression, element, "*", "test"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "body"),
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

	protected virtual Node_DictionaryEntry parseDictionaryEntry(XmlElement element) {
		checkElement(element, "dictionary-entry");
		return new Node_DictionaryEntry(
			parseOne<INode_Expression>(parseExpression, element, "*", "key"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual INode_IdentikeySpecialOld parseIdentikeySpecialOld(XmlElement element) {
		switch(element.LocalName) {
			case "using":
				return parseUsing(element);
			case "expose":
				return parseExpose(element);
			default:
				throw new ParseError(
					String.Format(
						"element with name '{0}' is not recognized as a IdentikeySpecialOld node",
						element.LocalName),
					getSource(element));
		}
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
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
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
			parseMult<Node_GenericParameter>(parseGenericParameter, element, "*", "parameter"),
			parseOne<Node_Interface>(parseInterface, element, "interface", null),
			getSource(element) );
	}

	protected virtual Node_Ignore parseIgnore(XmlElement element) {
		checkElement(element, "ignore");
		return new Node_Ignore(
			parseOne<INode_Expression>(parseExpression, element, "*", "content"),
			parseMult<Node_IgnoreMember>(parseIgnoreMember, element, "ignore-member", null),
			getSource(element) );
	}

	protected virtual Node_ParameterImpl parseParameterImpl(XmlElement element) {
		checkElement(element, "parameter-impl");
		return new Node_ParameterImpl(
			parseOne<Node_Direction>(parseDirection, element, "direction", null),
			parseOpt<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "default-value"),
			getSource(element) );
	}

	protected virtual Node_Null parseNull(XmlElement element) {
		checkElement(element, "null");
		return new Node_Null(
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_MemberIdentification parseMemberIdentification(XmlElement element) {
		checkElement(element, "member-identification");
		return new Node_MemberIdentification(
			parseOne<Node_MemberType>(parseMemberType, element, "member-type", null),
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_Generator parseGenerator(XmlElement element) {
		checkElement(element, "generator");
		return new Node_Generator(
			parseOpt<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_Cast parseCast(XmlElement element) {
		checkElement(element, "cast");
		return new Node_Cast(
			parseOne<INode_Expression>(parseExpression, element, "*", "source"),
			parseOpt<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			getSource(element) );
	}

	protected virtual Node_ExceptionHandler parseExceptionHandler(XmlElement element) {
		checkElement(element, "exception-handler");
		return new Node_ExceptionHandler(
			parseOne<Node_Boolean>(parseBoolean, element, "*", "catch"),
			parseOne<INode_Expression>(parseExpression, element, "*", "interface"),
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<INode_Expression>(parseExpression, element, "*", "result"),
			getSource(element) );
	}

	protected virtual Node_Breed parseBreed(XmlElement element) {
		checkElement(element, "breed");
		return new Node_Breed(
			parseOne<INode_Expression>(parseExpression, element, "*", "parent"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_Namespace parseNamespace(XmlElement element) {
		checkElement(element, "namespace");
		return new Node_Namespace(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_LimitOld>(parseLimitOld, element, "limit-old", null),
			getSource(element) );
	}

	protected virtual Node_Labeled parseLabeled(XmlElement element) {
		checkElement(element, "labeled");
		return new Node_Labeled(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "label"),
			parseOne<INode_Expression>(parseExpression, element, "*", "child"),
			getSource(element) );
	}

	protected virtual Node_Hidable parseHidable(XmlElement element) {
		checkElement(element, "hidable");
		return new Node_Hidable(
			parseOne<Node_Boolean>(parseBoolean, element, "*", "hidden"),
			parseOne<INode_IdentikeySpecialNew>(parseIdentikeySpecialNew, element, "*", "declaration"),
			getSource(element) );
	}

	protected virtual Node_IdentikeyCategory parseIdentikeyCategory(XmlElement element) {
		checkElement(element, "identikey-category");
		return new Node_IdentikeyCategory(element.InnerText, getSource(element));
	}

	protected virtual Node_Call parseCall(XmlElement element) {
		checkElement(element, "call");
		return new Node_Call(
			parseOne<INode_Expression>(parseExpression, element, "*", "receiver"),
			parseMult<Node_Argument>(parseArgument, element, "argument", null),
			getSource(element) );
	}

	protected virtual Node_Implements parseImplements(XmlElement element) {
		checkElement(element, "implements");
		return new Node_Implements(
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			parseOne<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_Rational parseRational(XmlElement element) {
		checkElement(element, "rational");
		return new Node_Rational(element.InnerText, getSource(element));
	}

	protected virtual Node_FunctionInterface parseFunctionInterface(XmlElement element) {
		checkElement(element, "function-interface");
		return new Node_FunctionInterface(
			parseOpt<INode_Expression>(parseExpression, element, "*", "template-argument-count"),
			parseMult<Node_ParameterInfo>(parseParameterInfo, element, "parameter-info", null),
			parseOpt<Node_NullableType>(parseNullableType, element, "*", "return-info"),
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

	protected virtual Node_NamespacedValueIdentikey parseNamespacedValueIdentikey(XmlElement element) {
		checkElement(element, "namespaced-value-identikey");
		return new Node_NamespacedValueIdentikey(
			parseMult<Node_Identifier>(parseIdentifier, element, "*", "namespace"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "identikey-name"),
			getSource(element) );
	}

	protected virtual Node_Case parseCase(XmlElement element) {
		checkElement(element, "case");
		return new Node_Case(
			parseMult<INode_Expression>(parseExpression, element, "*", "value"),
			parseOne<INode_Expression>(parseExpression, element, "*", "result"),
			getSource(element) );
	}

	protected virtual Node_Direction parseDirection(XmlElement element) {
		checkElement(element, "direction");
		return new Node_Direction(element.InnerText, getSource(element));
	}

	protected virtual Node_Xor parseXor(XmlElement element) {
		checkElement(element, "xor");
		return new Node_Xor(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Return parseReturn(XmlElement element) {
		checkElement(element, "return");
		return new Node_Return(
			parseOpt<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Continue parseContinue(XmlElement element) {
		checkElement(element, "continue");
		return new Node_Continue(
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "label"),
			getSource(element) );
	}

	protected virtual Node_Dictionary parseDictionary(XmlElement element) {
		checkElement(element, "dictionary");
		return new Node_Dictionary(
			parseOne<Node_NullableType>(parseNullableType, element, "*", "key-type"),
			parseOne<Node_NullableType>(parseNullableType, element, "*", "value-type"),
			parseMult<Node_DictionaryEntry>(parseDictionaryEntry, element, "dictionary-entry", null),
			getSource(element) );
	}

	protected virtual Node_Callee parseCallee(XmlElement element) {
		checkElement(element, "callee");
		return new Node_Callee(
			parseMult<Node_ParameterInfo>(parseParameterInfo, element, "parameter-info", null),
			parseOpt<Node_NullableType>(parseNullableType, element, "*", "return-info"),
			getSource(element) );
	}

	protected virtual Node_Expose parseExpose(XmlElement element) {
		checkElement(element, "expose");
		return new Node_Expose(
			parseMult<Node_Identifier>(parseIdentifier, element, "identifier", null),
			getSource(element) );
	}

	protected virtual Node_DeclareEmpty parseDeclareEmpty(XmlElement element) {
		checkElement(element, "declare-empty");
		return new Node_DeclareEmpty(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, element, "identikey-type", null),
			getSource(element) );
	}

	protected virtual Node_Worker parseWorker(XmlElement element) {
		checkElement(element, "worker");
		return new Node_Worker(
			parseOne<INode_Expression>(parseExpression, element, "*", "face"),
			parseMult<Node_Worker>(parseWorker, element, "*", "child"),
			parseMult<Node_MemberImplementation>(parseMemberImplementation, element, "member-implementation", null),
			getSource(element) );
	}

	protected virtual Node_Break parseBreak(XmlElement element) {
		checkElement(element, "break");
		return new Node_Break(
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "label"),
			getSource(element) );
	}

	protected virtual Node_Nand parseNand(XmlElement element) {
		checkElement(element, "nand");
		return new Node_Nand(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_Enum parseEnum(XmlElement element) {
		checkElement(element, "enum");
		return new Node_Enum(
			parseOpt<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			parseMult<Node_EnumEntry>(parseEnumEntry, element, "enum-entry", null),
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

	protected virtual Node_LimitOld parseLimitOld(XmlElement element) {
		checkElement(element, "limit-old");
		return new Node_LimitOld(
			parseMult<INode_IdentikeySpecialOld>(parseIdentikeySpecialOld, element, "*", "declaration"),
			parseMult<Node_Hidable>(parseHidable, element, "hidable", null),
			getSource(element) );
	}

	protected virtual Node_NullableType parseNullableType(XmlElement element) {
		checkElement(element, "nullable-type");
		return new Node_NullableType(
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "nullable"),
			getSource(element) );
	}

	protected virtual Node_EnumeratorLoop parseEnumeratorLoop(XmlElement element) {
		checkElement(element, "enumerator-loop");
		return new Node_EnumeratorLoop(
			parseOne<INode_Expression>(parseExpression, element, "*", "container"),
			parseMult<Node_Receiver>(parseReceiver, element, "receiver", null),
			parseOpt<INode_Expression>(parseExpression, element, "*", "test"),
			parseOne<INode_Expression>(parseExpression, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_Compound parseCompound(XmlElement element) {
		checkElement(element, "compound");
		return new Node_Compound(
			parseMult<INode_IdentikeySpecialOld>(parseIdentikeySpecialOld, element, "*", "old-declaration"),
			parseMult<INode_IdentikeySpecialNew>(parseIdentikeySpecialNew, element, "*", "new-declaration"),
			parseMult<INode_Expression>(parseExpression, element, "*", "member"),
			getSource(element) );
	}

	protected virtual Node_Interface parseInterface(XmlElement element) {
		checkElement(element, "interface");
		return new Node_Interface(
			parseMult<INode_Expression>(parseExpression, element, "*", "inheritee"),
			parseMult<INode_InterfaceMember>(parseInterfaceMember, element, "*", "member"),
			getSource(element) );
	}

	protected virtual Node_Using parseUsing(XmlElement element) {
		checkElement(element, "using");
		return new Node_Using(
			parseMult<Node_Identifier>(parseIdentifier, element, "*", "target"),
			parseOpt<Node_Identifier>(parseIdentifier, element, "*", "name"),
			getSource(element) );
	}

	protected virtual Node_SetProperty parseSetProperty(XmlElement element) {
		checkElement(element, "set-property");
		return new Node_SetProperty(
			parseOne<INode_Expression>(parseExpression, element, "*", "source"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "property-name"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_ExtractMember parseExtractMember(XmlElement element) {
		checkElement(element, "extract-member");
		return new Node_ExtractMember(
			parseOne<INode_Expression>(parseExpression, element, "*", "source"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "member-name"),
			getSource(element) );
	}

	protected virtual Node_Throw parseThrow(XmlElement element) {
		checkElement(element, "throw");
		return new Node_Throw(
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Nor parseNor(XmlElement element) {
		checkElement(element, "nor");
		return new Node_Nor(
			parseOne<INode_Expression>(parseExpression, element, "*", "first"),
			parseOne<INode_Expression>(parseExpression, element, "*", "second"),
			getSource(element) );
	}

	protected virtual Node_IgnoreMember parseIgnoreMember(XmlElement element) {
		checkElement(element, "ignore-member");
		return new Node_IgnoreMember(
			parseOne<Node_String>(parseString, element, "*", "name"),
			parseOne<Node_Integer>(parseInteger, element, "*", "depth"),
			getSource(element) );
	}

	protected virtual INode_IdentikeySpecialNew parseIdentikeySpecialNew(XmlElement element) {
		switch(element.LocalName) {
			case "declare-first":
				return parseDeclareFirst(element);
			case "limit-old":
				return parseLimitOld(element);
			case "namespace":
				return parseNamespace(element);
			default:
				throw new ParseError(
					String.Format(
						"element with name '{0}' is not recognized as a IdentikeySpecialNew node",
						element.LocalName),
					getSource(element));
		}
	}

	protected virtual Node_Property parseProperty(XmlElement element) {
		checkElement(element, "property");
		return new Node_Property(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "writable"),
			parseOne<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			getSource(element) );
	}

	protected virtual Node_Object parseObject(XmlElement element) {
		checkElement(element, "object");
		return new Node_Object(
			parseMult<Node_Worker>(parseWorker, element, "worker", null),
			getSource(element) );
	}

	protected virtual Node_TryCatch parseTryCatch(XmlElement element) {
		checkElement(element, "try-catch");
		return new Node_TryCatch(
			parseOne<INode_Expression>(parseExpression, element, "*", "try"),
			parseMult<Node_ExceptionHandler>(parseExceptionHandler, element, "exception-handler", null),
			parseOpt<INode_Expression>(parseExpression, element, "*", "else"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "finally"),
			getSource(element) );
	}

	protected virtual Node_ParameterInfo parseParameterInfo(XmlElement element) {
		checkElement(element, "parameter-info");
		return new Node_ParameterInfo(
			parseOne<Node_Direction>(parseDirection, element, "direction", null),
			parseOpt<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "has-default-value"),
			getSource(element) );
	}

	protected virtual Node_Caller parseCaller(XmlElement element) {
		checkElement(element, "caller");
		return new Node_Caller(
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "method-name"),
			getSource(element) );
	}

	protected virtual Node_Yield parseYield(XmlElement element) {
		checkElement(element, "yield");
		return new Node_Yield(
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
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
			parseOne<Node_MemberIdentification>(parseMemberIdentification, element, "member-identification", null),
			parseOne<INode_Expression>(parseExpression, element, "*", "function"),
			getSource(element) );
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

	protected virtual Node_DeclareAssign parseDeclareAssign(XmlElement element) {
		checkElement(element, "declare-assign");
		return new Node_DeclareAssign(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_IdentikeyType>(parseIdentikeyType, element, "identikey-type", null),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "breed"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "infer-interface"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_IdentikeyType parseIdentikeyType(XmlElement element) {
		checkElement(element, "identikey-type");
		return new Node_IdentikeyType(
			parseOne<Node_IdentikeyCategory>(parseIdentikeyCategory, element, "identikey-category", null),
			parseOne<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			getSource(element) );
	}

	protected virtual Node_EnumEntry parseEnumEntry(XmlElement element) {
		checkElement(element, "enum-entry");
		return new Node_EnumEntry(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOpt<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}

	protected virtual Node_Receiver parseReceiver(XmlElement element) {
		checkElement(element, "receiver");
		return new Node_Receiver(
			parseOpt<Node_NullableType>(parseNullableType, element, "nullable-type", null),
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			getSource(element) );
	}

	protected virtual Node_Breeder parseBreeder(XmlElement element) {
		checkElement(element, "breeder");
		return new Node_Breeder(
			parseOpt<INode_Expression>(parseExpression, element, "*", "interface"),
			getSource(element) );
	}

	protected virtual Node_UnconditionalLoop parseUnconditionalLoop(XmlElement element) {
		checkElement(element, "unconditional-loop");
		return new Node_UnconditionalLoop(
			parseOne<INode_Expression>(parseExpression, element, "*", "body"),
			getSource(element) );
	}

	protected virtual Node_Identifier parseIdentifier(XmlElement element) {
		checkElement(element, "identifier");
		return new Node_Identifier(element.InnerText, getSource(element));
	}

	protected virtual INode_Expression parseExpression(XmlElement element) {
		switch(element.LocalName) {
			case "conditional-loop":
				return parseConditionalLoop(element);
			case "enumerator-loop":
				return parseEnumeratorLoop(element);
			case "unconditional-loop":
				return parseUnconditionalLoop(element);
			case "break":
				return parseBreak(element);
			case "continue":
				return parseContinue(element);
			case "return":
				return parseReturn(element);
			case "throw":
				return parseThrow(element);
			case "yield":
				return parseYield(element);
			case "declare-empty":
				return parseDeclareEmpty(element);
			case "null":
				return parseNull(element);
			case "assign":
				return parseAssign(element);
			case "call":
				return parseCall(element);
			case "cast":
				return parseCast(element);
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
			case "ignore":
				return parseIgnore(element);
			case "labeled":
				return parseLabeled(element);
			case "namespaced-value-identikey":
				return parseNamespacedValueIdentikey(element);
			case "select":
				return parseSelect(element);
			case "try-catch":
				return parseTryCatch(element);
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
			case "implements":
				return parseImplements(element);
			case "rational":
				return parseRational(element);
			case "set-property":
				return parseSetProperty(element);
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

	protected virtual Node_Assign parseAssign(XmlElement element) {
		checkElement(element, "assign");
		return new Node_Assign(
			parseOne<Node_Identifier>(parseIdentifier, element, "*", "name"),
			parseOne<Node_Boolean>(parseBoolean, element, "*", "breed"),
			parseOne<INode_Expression>(parseExpression, element, "*", "value"),
			getSource(element) );
	}
}

