
//This file was generated programmatically, so
//don't edit this file directly.

using System.Collections.Generic;
using System.Xml;

partial class DesibleParser {
	void addTreeParsers() {
		addParser<Node_GenericParameter>("generic-parameter", delegate(XmlElement element) {
			return new Node_GenericParameter(
				parseOne<Node_Identifier>(element, "name"),
				parseOpt<INode_Expression>(element, "default-interface") );
		});

		addParser<Node_Chain>("chain", delegate(XmlElement element) {
			return new Node_Chain(
				parseOne<Node_NullableType>(element, "nullable-type"),
				parseMult<INode_Expression>(element, "element") );
		});

		addParser<Node_While>("while", delegate(XmlElement element) {
			return new Node_While(
				parseOne<INode_Expression>(element, "test"),
				parseOpt<Node_Block>(element, "block") );
		});

		addParser<Node_DeclareAssign>("declare-assign", delegate(XmlElement element) {
			return new Node_DeclareAssign(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "identikey-type"),
				parseOne<INode_Expression>(element, "value"),
				parseOne<Node_Boolean>(element, "breed") );
		});

		addParser<Node_ClassProperty>("class-property", delegate(XmlElement element) {
			return new Node_ClassProperty(
				parseOne<Node_Identifier>(element, "identifier"),
				parseOne<Node_IdentikeyType>(element, "identikey-type"),
				parseOpt<Node_Function>(element, "getter"),
				parseOpt<Node_Function>(element, "setter") );
		});

		addParser<Node_Array>("array", delegate(XmlElement element) {
			return new Node_Array(
				parseOne<Node_NullableType>(element, "nullable-type"),
				parseMult<INode_Expression>(element, "element") );
		});

		addParser<Node_Select>("select", delegate(XmlElement element) {
			return new Node_Select(
				parseOne<INode_Expression>(element, "value"),
				parseMult<Node_Case>(element, "case"),
				parseOpt<INode_Expression>(element, "else") );
		});

		addParser<Node_IgnoreMember>("ignore-member", delegate(XmlElement element) {
			return new Node_IgnoreMember(
				parseOne<Node_String>(element, "name"),
				parseOne<Node_Integer>(element, "depth") );
		});

		addParser<Node_Generator>("generator", delegate(XmlElement element) {
			return new Node_Generator(
				parseOpt<Node_NullableType>(element, "nullable-type"),
				parseOne<INode_Expression>(element, "expression") );
		});

		addParser<Node_Breed>("breed", delegate(XmlElement element) {
			return new Node_Breed(
				parseOne<INode_Expression>(element, "parent"),
				parseOpt<INode_Expression>(element, "interface") );
		});

		addParser<Node_NamedFunction>("named-function", delegate(XmlElement element) {
			return new Node_NamedFunction(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_Function>(element, "function") );
		});

		addParser<Node_Call>("call", delegate(XmlElement element) {
			return new Node_Call(
				parseOne<INode_Expression>(element, "value"),
				parseMult<Node_Argument>(element, "argument") );
		});

		addParser<Node_GenericFunction>("generic-function", delegate(XmlElement element) {
			return new Node_GenericFunction(
				parseMult<Node_GenericParameter>(element, "parameters"),
				parseOne<Node_Function>(element, "function") );
		});

		addParser<Node_Function>("function", delegate(XmlElement element) {
			return new Node_Function(
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_NullableType>(element, "return-info"),
				parseOne<INode_Expression>(element, "body") );
		});

		addParser<Node_Xnor>("xnor", delegate(XmlElement element) {
			return new Node_Xnor(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});

		addParser<Node_InstantiateGeneric>("instantiate-generic", delegate(XmlElement element) {
			return new Node_InstantiateGeneric(
				parseOne<INode_Expression>(element, "generic"),
				parseMult<Node_Argument>(element, "argument") );
		});

		addParser<Node_Return>("return", delegate(XmlElement element) {
			return new Node_Return(
				parseOpt<INode_Expression>(element, "expression") );
		});

		addParser<Node_Expose>("expose", delegate(XmlElement element) {
			return new Node_Expose(
				parseMult<Node_Identifier>(element, "identifier") );
		});

		addParser<Node_DeclareEmpty>("declare-empty", delegate(XmlElement element) {
			return new Node_DeclareEmpty(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "identikey-type") );
		});

		addParser<Node_Break>("break", delegate(XmlElement element) {
			return new Node_Break(
				parseOpt<Node_Identifier>(element, "label") );
		});

		addParser<Node_NullableType>("nullable-type", delegate(XmlElement element) {
			return new Node_NullableType(
				parseOpt<INode_Expression>(element, "interface"),
				parseOne<Node_Boolean>(element, "nullable") );
		});

		addParser<Node_StaticMember>("static-member", delegate(XmlElement element) {
			return new Node_StaticMember(
				parseOne<INode_Declaration>(element, "declaration"),
				parseOpt<Node_Access>(element, "access") );
		});

		addParser<Node_Using>("using", delegate(XmlElement element) {
			return new Node_Using(
				parseMult<Node_Identifier>(element, "target"),
				parseOpt<Node_Identifier>(element, "name") );
		});

		addParser<Node_Nor>("nor", delegate(XmlElement element) {
			return new Node_Nor(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});

		addParser<Node_Case>("case", delegate(XmlElement element) {
			return new Node_Case(
				parseMult<INode_Expression>(element, "values"),
				parseOne<INode_Expression>(element, "result") );
		});

		addParser<Node_InterfaceImplementation>("interface-implementation", delegate(XmlElement element) {
			return new Node_InterfaceImplementation(
				parseMult<Node_InterfaceImplementation>(element, "children"),
				parseOne<INode_Expression>(element, "interface"),
				parseMult<Node_Function>(element, "callee"),
				parseMult<Node_NamedFunction>(element, "getter"),
				parseMult<Node_NamedFunction>(element, "setter"),
				parseMult<Node_NamedFunction>(element, "method"),
				parseOne<Node_Boolean>(element, "default") );
		});

		addParser<Node_Caller>("caller", delegate(XmlElement element) {
			return new Node_Caller(
				parseOpt<INode_Expression>(element, "interface"),
				parseOne<Node_Identifier>(element, "method-name") );
		});

		addParser<Node_Yield>("yield", delegate(XmlElement element) {
			return new Node_Yield(
				parseOne<INode_Expression>(element, "expression") );
		});

		addParser<Node_Ignore>("ignore", delegate(XmlElement element) {
			return new Node_Ignore(
				parseOne<INode_Expression>(element, "content"),
				parseMult<Node_IgnoreMember>(element, "ignore-member") );
		});

		addParser<Node_TryCatch>("try-catch", delegate(XmlElement element) {
			return new Node_TryCatch(
				parseOne<INode_Expression>(element, "try"),
				parseMult<Node_ExceptionHandler>(element, "exception-handler"),
				parseOpt<INode_Expression>(element, "else"),
				parseOpt<INode_Expression>(element, "finally") );
		});

		addParser<Node_Labeled>("labeled", delegate(XmlElement element) {
			return new Node_Labeled(
				parseOne<Node_Identifier>(element, "label"),
				parseOne<INode_Expression>(element, "child") );
		});

		addParser<Node_ForValue>("for-value", delegate(XmlElement element) {
			return new Node_ForValue(
				parseOne<INode_Expression>(element, "container"),
				parseOpt<INode_Expression>(element, "value-interface"),
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_Block>(element, "action") );
		});

		addParser<Node_EnumEntry>("enum-entry", delegate(XmlElement element) {
			return new Node_EnumEntry(
				parseOne<Node_Identifier>(element, "name"),
				parseOpt<INode_Expression>(element, "value") );
		});

		addParser<Node_Property>("property", delegate(XmlElement element) {
			return new Node_Property(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_NullableType>(element, "nullable-type"),
				parseOne<Node_Access>(element, "access") );
		});

		addParser<Node_Callee>("callee", delegate(XmlElement element) {
			return new Node_Callee(
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_NullableType>(element, "return-info") );
		});

		addParser<Node_Loop>("loop", delegate(XmlElement element) {
			return new Node_Loop(
				parseOne<Node_Block>(element, "block") );
		});

		addParser<Node_And>("and", delegate(XmlElement element) {
			return new Node_And(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});

		addParser<Node_DeclareFirst>("declare-first", delegate(XmlElement element) {
			return new Node_DeclareFirst(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "identikey-type"),
				parseOne<INode_Expression>(element, "value"),
				parseOne<Node_Boolean>(element, "breed") );
		});

		addParser<Node_DoTimes>("do-times", delegate(XmlElement element) {
			return new Node_DoTimes(
				parseOne<INode_Expression>(element, "times"),
				parseOne<Node_Block>(element, "action"),
				parseOpt<INode_Expression>(element, "test") );
		});

		addParser<Node_DeclareConstEmpty>("declare-const-empty", delegate(XmlElement element) {
			return new Node_DeclareConstEmpty(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_IdentikeyType>(element, "identikey-type") );
		});

		addParser<Node_Conditional>("conditional", delegate(XmlElement element) {
			return new Node_Conditional(
				parseMult<Node_Possibility>(element, "possibility"),
				parseOpt<INode_Expression>(element, "else") );
		});

		addParser<Node_Argument>("argument", delegate(XmlElement element) {
			return new Node_Argument(
				parseOpt<Node_Identifier>(element, "parameter-name"),
				parseOpt<INode_Expression>(element, "value") );
		});

		addParser<Node_ForManual>("for-manual", delegate(XmlElement element) {
			return new Node_ForManual(
				parseMult<INode_Expression>(element, "initializers"),
				parseOpt<INode_Expression>(element, "test"),
				parseMult<INode_Expression>(element, "post-actions"),
				parseOpt<Node_Block>(element, "action") );
		});

		addParser<Node_DoWhile>("do-while", delegate(XmlElement element) {
			return new Node_DoWhile(
				parseOne<Node_Block>(element, "action"),
				parseOne<INode_Expression>(element, "test") );
		});

		addParser<Node_Unassign>("unassign", delegate(XmlElement element) {
			return new Node_Unassign(
				parseOne<Node_Identifier>(element, "identifier") );
		});

		addParser<Node_DictionaryEntry>("dictionary-entry", delegate(XmlElement element) {
			return new Node_DictionaryEntry(
				parseOne<INode_Expression>(element, "key"),
				parseOne<INode_Expression>(element, "value") );
		});

		addParser<Node_ForPair>("for-pair", delegate(XmlElement element) {
			return new Node_ForPair(
				parseOne<INode_Expression>(element, "container"),
				parseOpt<INode_Expression>(element, "key-interface"),
				parseOne<Node_Identifier>(element, "key-name"),
				parseOpt<INode_Expression>(element, "value-interface"),
				parseOne<Node_Identifier>(element, "value-name"),
				parseOne<Node_Block>(element, "action") );
		});

		addParser<Node_Xor>("xor", delegate(XmlElement element) {
			return new Node_Xor(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});

		addParser<Node_Cast>("cast", delegate(XmlElement element) {
			return new Node_Cast(
				parseOne<INode_Expression>(element, "object"),
				parseOne<INode_Expression>(element, "interface") );
		});

		addParser<Node_ForRange>("for-range", delegate(XmlElement element) {
			return new Node_ForRange(
				parseOpt<Node_Identifier>(element, "name"),
				parseOne<INode_Expression>(element, "start"),
				parseOne<INode_Expression>(element, "limit"),
				parseOne<Node_Boolean>(element, "inclusive"),
				parseOpt<INode_Expression>(element, "test"),
				parseOne<Node_Block>(element, "action") );
		});

		addParser<Node_ExceptionHandler>("exception-handler", delegate(XmlElement element) {
			return new Node_ExceptionHandler(
				parseOne<Node_Boolean>(element, "catch"),
				parseOne<INode_Expression>(element, "interface"),
				parseOpt<Node_Identifier>(element, "name"),
				parseOne<INode_Expression>(element, "result") );
		});

		addParser<Node_Nand>("nand", delegate(XmlElement element) {
			return new Node_Nand(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});

		addParser<Node_FunctionInterface>("function-interface", delegate(XmlElement element) {
			return new Node_FunctionInterface(
				parseOpt<INode_Expression>(element, "template-argument-count"),
				parseMult<Node_Parameter>(element, "parameter"),
				parseOpt<Node_NullableType>(element, "return-info") );
		});

		addParser<Node_Import>("import", delegate(XmlElement element) {
			return new Node_Import(
				parseOne<Node_String>(element, "library"),
				parseOne<Node_Identifier>(element, "alias") );
		});

		addParser<Node_Parameter>("parameter", delegate(XmlElement element) {
			return new Node_Parameter(
				parseOne<Node_Direction>(element, "direction"),
				parseOne<Node_NullableType>(element, "nullable-type"),
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_Boolean>(element, "has-default-value"),
				parseOpt<INode_Expression>(element, "default-value") );
		});

		addParser<Node_Method>("method", delegate(XmlElement element) {
			return new Node_Method(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<INode_Expression>(element, "interface") );
		});

		addParser<Node_NamespacedValueIdentikey>("namespaced-value-identikey", delegate(XmlElement element) {
			return new Node_NamespacedValueIdentikey(
				parseMult<Node_Identifier>(element, "namespaces"),
				parseOne<Node_Identifier>(element, "identikey-name") );
		});

		addParser<Node_Dictionary>("dictionary", delegate(XmlElement element) {
			return new Node_Dictionary(
				parseOne<Node_NullableType>(element, "key-type"),
				parseOne<Node_NullableType>(element, "value-type"),
				parseMult<Node_DictionaryEntry>(element, "dictionary-entry") );
		});

		addParser<Node_Enum>("enum", delegate(XmlElement element) {
			return new Node_Enum(
				parseOpt<Node_NullableType>(element, "nullable-type"),
				parseMult<Node_EnumEntry>(element, "enum-entry") );
		});

		addParser<Node_Possibility>("possibility", delegate(XmlElement element) {
			return new Node_Possibility(
				parseOne<INode_Expression>(element, "test"),
				parseOne<INode_Expression>(element, "result") );
		});

		addParser<Node_ForKey>("for-key", delegate(XmlElement element) {
			return new Node_ForKey(
				parseOne<INode_Expression>(element, "container"),
				parseOpt<INode_Expression>(element, "key-interface"),
				parseOne<Node_Identifier>(element, "name"),
				parseOne<Node_Block>(element, "action") );
		});

		addParser<Node_SetProperty>("set-property", delegate(XmlElement element) {
			return new Node_SetProperty(
				parseOne<INode_Expression>(element, "source"),
				parseOne<Node_Identifier>(element, "property-name"),
				parseOne<INode_Expression>(element, "value") );
		});

		addParser<Node_ExtractMember>("extract-member", delegate(XmlElement element) {
			return new Node_ExtractMember(
				parseOne<INode_Expression>(element, "source"),
				parseOne<Node_Identifier>(element, "member-name") );
		});

		addParser<Node_Throw>("throw", delegate(XmlElement element) {
			return new Node_Throw(
				parseOne<INode_Expression>(element, "expression") );
		});

		addParser<Node_Implements>("implements", delegate(XmlElement element) {
			return new Node_Implements(
				parseOne<INode_Expression>(element, "value"),
				parseOne<INode_Expression>(element, "interface") );
		});

		addParser<Node_Assign>("assign", delegate(XmlElement element) {
			return new Node_Assign(
				parseOne<Node_Identifier>(element, "name"),
				parseOne<INode_Expression>(element, "value"),
				parseOne<Node_Boolean>(element, "breed") );
		});

		addParser<Node_IdentikeyType>("identikey-type", delegate(XmlElement element) {
			return new Node_IdentikeyType(
				parseOne<Node_IdentikeyCategory>(element, "identikey-category"),
				parseOpt<Node_NullableType>(element, "nullable-type"),
				parseOpt<Node_Boolean>(element, "constant") );
		});

		addParser<Node_Convertor>("convertor", delegate(XmlElement element) {
			return new Node_Convertor(
				parseOpt<INode_Expression>(element, "interface") );
		});

		addParser<Node_Curry>("curry", delegate(XmlElement element) {
			return new Node_Curry(
				parseOne<INode_Expression>(element, "function"),
				parseMult<Node_Argument>(element, "argument"),
				parseOne<Node_Boolean>(element, "call") );
		});

		addParser<Node_Or>("or", delegate(XmlElement element) {
			return new Node_Or(
				parseOne<INode_Expression>(element, "first"),
				parseOne<INode_Expression>(element, "second") );
		});

		addParser<Node_Block>("block", delegate(XmlElement element) {
			return new Node_Block(
				parseMult<INode_ScopeAlteration>(element, "scope-alteration"),
				parseMult<INode_Expression>(element, "members") );
		});
	}
}

