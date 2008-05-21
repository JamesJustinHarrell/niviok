
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;

using System.Xml;

abstract class DesibleSerializerAuto : DesibleSerializerBase {
	protected virtual XmlElement serialize(Node_And node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Function node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterImpl>(elem, node.@parameterImpls, null);
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_DeclareFirst node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, null);
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_InstantiateGeneric node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@generic, "generic");
		append<Node_Argument>(elem, node.@arguments, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_GenericParameter node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@defaultInterface, "default-interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Xnor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Conditional node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@result, "result");
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Argument node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@parameterName, "parameter-name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_DeclareAssign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, null);
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<Node_Boolean>(elem, node.@inferInterface, "infer-interface");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_ConditionalLoop node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Integer node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_Boolean node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_DictionaryEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@key, "key");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Null node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_MemberType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_String node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_GenericInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_GenericParameter>(elem, node.@parameters, "parameter");
		append<Node_Interface>(elem, node.@interface, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Ignore node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@content, "content");
		append<Node_IgnoreMember>(elem, node.@ignoreMembers, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_ParameterImpl node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, null);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@defaultValue, "default-value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_MemberIdentification node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_MemberType>(elem, node.@memberType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Generator node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Cast node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_NullableType>(elem, node.@nullableType, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_ExceptionHandler node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Boolean>(elem, node.@catch, "catch");
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Breed node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@parent, "parent");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Nand node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_IdentikeyCategory node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_Call node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@receiver, "receiver");
		append<Node_Argument>(elem, node.@arguments, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Select node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Case>(elem, node.@cases, null);
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Rational node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_FunctionInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@templateArgumentCount, "template-argument-count");
		append<Node_ParameterInfo>(elem, node.@parameterInfos, null);
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Import node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@library, "library");
		append<Node_Identifier>(elem, node.@alias, "alias");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Method node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_NamespacedValueIdentikey node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@namespaces, "namespace");
		append<Node_Identifier>(elem, node.@identikeyName, "identikey-name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Case node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@values, "value");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Direction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_Xor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Return node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Continue node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Dictionary node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@keyType, "key-type");
		append<Node_NullableType>(elem, node.@valueType, "value-type");
		append<Node_DictionaryEntry>(elem, node.@dictionaryEntrys, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Implements node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Callee node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterInfo>(elem, node.@parameterInfos, null);
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Expose node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@identifiers, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_DeclareEmpty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Worker node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@face, "face");
		append<Node_Worker>(elem, node.@childs, "child");
		append<Node_MemberImplementation>(elem, node.@memberImplementations, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Bundle node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Import>(elem, node.@imports, null);
		append<INode_ScopeAlteration>(elem, node.@alts, "alt");
		append<Node_Plane>(elem, node.@planes, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Break node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		return elem;
	}

	protected virtual XmlElement serialize(Node_GenericFunction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_GenericParameter>(elem, node.@parameters, "parameter");
		append<Node_Function>(elem, node.@function, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Enum node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_EnumEntry>(elem, node.@enumEntrys, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Plane node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_ScopeAlteration>(elem, node.@alts, "alt");
		append<Node_DeclareFirst>(elem, node.@declareFirsts, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_UnconditionalLoop node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_NullableType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Boolean>(elem, node.@nullable, "nullable");
		return elem;
	}

	protected virtual XmlElement serialize(Node_EnumeratorLoop node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<Node_Receiver>(elem, node.@receivers, null);
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Compound node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_ScopeAlteration>(elem, node.@alts, "alt");
		append<INode_Expression>(elem, node.@members, "member");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Interface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@inheritees, "inheritee");
		append<INode_InterfaceMember>(elem, node.@members, "member");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Using node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@targets, "target");
		append<Node_Identifier>(elem, node.@name, "name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_SetProperty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@propertyName, "property-name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_ExtractMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@memberName, "member-name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Throw node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Nor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_IgnoreMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@name, "name");
		append<Node_Integer>(elem, node.@depth, "depth");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Property node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@writable, "writable");
		append<Node_NullableType>(elem, node.@nullableType, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Object node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Worker>(elem, node.@workers, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_TryCatch node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@try, "try");
		append<Node_ExceptionHandler>(elem, node.@exceptionHandlers, null);
		append<INode_Expression>(elem, node.@else, "else");
		append<INode_Expression>(elem, node.@finally, "finally");
		return elem;
	}

	protected virtual XmlElement serialize(Node_ParameterInfo node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, null);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@hasDefaultValue, "has-default-value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Caller node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@methodName, "method-name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Yield node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Or node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_MemberImplementation node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_MemberIdentification>(elem, node.@memberIdentification, null);
		append<INode_Expression>(elem, node.@function, "function");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Labeled node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		append<INode_Expression>(elem, node.@child, "child");
		return elem;
	}

	protected virtual XmlElement serialize(Node_IdentikeyType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_IdentikeyCategory>(elem, node.@identikeyCategory, null);
		append<Node_NullableType>(elem, node.@nullableType, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_EnumEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Receiver node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Breeder node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Curry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@function, "function");
		append<Node_Argument>(elem, node.@arguments, null);
		append<Node_Boolean>(elem, node.@call, "call");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Identifier node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_Assign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}
	
	protected XmlElement serializeAny(INode node) {
		switch(node.typeName) {
			case "and":
				return serialize((Node_And)node);
			case "function":
				return serialize((Node_Function)node);
			case "declare-first":
				return serialize((Node_DeclareFirst)node);
			case "instantiate-generic":
				return serialize((Node_InstantiateGeneric)node);
			case "generic-parameter":
				return serialize((Node_GenericParameter)node);
			case "xnor":
				return serialize((Node_Xnor)node);
			case "conditional":
				return serialize((Node_Conditional)node);
			case "argument":
				return serialize((Node_Argument)node);
			case "declare-assign":
				return serialize((Node_DeclareAssign)node);
			case "conditional-loop":
				return serialize((Node_ConditionalLoop)node);
			case "integer":
				return serialize((Node_Integer)node);
			case "boolean":
				return serialize((Node_Boolean)node);
			case "dictionary-entry":
				return serialize((Node_DictionaryEntry)node);
			case "null":
				return serialize((Node_Null)node);
			case "member-type":
				return serialize((Node_MemberType)node);
			case "string":
				return serialize((Node_String)node);
			case "generic-interface":
				return serialize((Node_GenericInterface)node);
			case "ignore":
				return serialize((Node_Ignore)node);
			case "parameter-impl":
				return serialize((Node_ParameterImpl)node);
			case "member-identification":
				return serialize((Node_MemberIdentification)node);
			case "generator":
				return serialize((Node_Generator)node);
			case "cast":
				return serialize((Node_Cast)node);
			case "exception-handler":
				return serialize((Node_ExceptionHandler)node);
			case "breed":
				return serialize((Node_Breed)node);
			case "nand":
				return serialize((Node_Nand)node);
			case "identikey-category":
				return serialize((Node_IdentikeyCategory)node);
			case "call":
				return serialize((Node_Call)node);
			case "select":
				return serialize((Node_Select)node);
			case "rational":
				return serialize((Node_Rational)node);
			case "function-interface":
				return serialize((Node_FunctionInterface)node);
			case "import":
				return serialize((Node_Import)node);
			case "method":
				return serialize((Node_Method)node);
			case "namespaced-value-identikey":
				return serialize((Node_NamespacedValueIdentikey)node);
			case "case":
				return serialize((Node_Case)node);
			case "direction":
				return serialize((Node_Direction)node);
			case "xor":
				return serialize((Node_Xor)node);
			case "return":
				return serialize((Node_Return)node);
			case "continue":
				return serialize((Node_Continue)node);
			case "dictionary":
				return serialize((Node_Dictionary)node);
			case "implements":
				return serialize((Node_Implements)node);
			case "callee":
				return serialize((Node_Callee)node);
			case "expose":
				return serialize((Node_Expose)node);
			case "declare-empty":
				return serialize((Node_DeclareEmpty)node);
			case "worker":
				return serialize((Node_Worker)node);
			case "bundle":
				return serialize((Node_Bundle)node);
			case "break":
				return serialize((Node_Break)node);
			case "generic-function":
				return serialize((Node_GenericFunction)node);
			case "enum":
				return serialize((Node_Enum)node);
			case "plane":
				return serialize((Node_Plane)node);
			case "unconditional-loop":
				return serialize((Node_UnconditionalLoop)node);
			case "nullable-type":
				return serialize((Node_NullableType)node);
			case "enumerator-loop":
				return serialize((Node_EnumeratorLoop)node);
			case "compound":
				return serialize((Node_Compound)node);
			case "interface":
				return serialize((Node_Interface)node);
			case "using":
				return serialize((Node_Using)node);
			case "set-property":
				return serialize((Node_SetProperty)node);
			case "extract-member":
				return serialize((Node_ExtractMember)node);
			case "throw":
				return serialize((Node_Throw)node);
			case "nor":
				return serialize((Node_Nor)node);
			case "ignore-member":
				return serialize((Node_IgnoreMember)node);
			case "property":
				return serialize((Node_Property)node);
			case "object":
				return serialize((Node_Object)node);
			case "try-catch":
				return serialize((Node_TryCatch)node);
			case "parameter-info":
				return serialize((Node_ParameterInfo)node);
			case "caller":
				return serialize((Node_Caller)node);
			case "yield":
				return serialize((Node_Yield)node);
			case "or":
				return serialize((Node_Or)node);
			case "member-implementation":
				return serialize((Node_MemberImplementation)node);
			case "labeled":
				return serialize((Node_Labeled)node);
			case "identikey-type":
				return serialize((Node_IdentikeyType)node);
			case "enum-entry":
				return serialize((Node_EnumEntry)node);
			case "receiver":
				return serialize((Node_Receiver)node);
			case "breeder":
				return serialize((Node_Breeder)node);
			case "curry":
				return serialize((Node_Curry)node);
			case "identifier":
				return serialize((Node_Identifier)node);
			case "assign":
				return serialize((Node_Assign)node);
			default:
				throw new ApplicationException(String.Format(
					"can't serialize node of type {0}", node.typeName));
		}
	}
}
