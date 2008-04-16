
//This file was generated programmatically, so
//don't edit this file directly.

using System.Collections.Generic;
using System.Xml;

#pragma warning disable 0169

abstract class DesibleSerializerAuto : DesibleSerializerBase {
	protected XmlElement serialize(Node_Chain node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<INode_Expression>(elem, node.@elements, "element");
		return elem;
	}

	protected XmlElement serialize(Node_SetProperty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@propertyName, "property-name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_Using node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@targets, "target");
		append<Node_Identifier>(elem, node.@name, "name");
		return elem;
	}

	protected XmlElement serialize(Node_Generator node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected XmlElement serialize(Node_GenericFunction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_GenericParameter>(elem, node.@parameters, "parameter");
		append<Node_Function>(elem, node.@function, null);
		return elem;
	}

	protected XmlElement serialize(Node_Xnor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected XmlElement serialize(Node_Return node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_DeclareEmpty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, null);
		return elem;
	}

	protected XmlElement serialize(Node_Break node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		return elem;
	}

	protected XmlElement serialize(Node_Nor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected XmlElement serialize(Node_Yield node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_Ignore node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@content, "content");
		append<Node_IgnoreMember>(elem, node.@ignoreMembers, null);
		return elem;
	}

	protected XmlElement serialize(Node_Continue node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		return elem;
	}

	protected XmlElement serialize(Node_EnumEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_Breeder node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected XmlElement serialize(Node_ParameterInfo node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, null);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@hasDefaultValue, "has-default-value");
		return elem;
	}

	protected XmlElement serialize(Node_DeclareFirst node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, null);
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_InstantiateGeneric node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@generic, "generic");
		append<Node_Argument>(elem, node.@arguments, null);
		return elem;
	}

	protected XmlElement serialize(Node_DictionaryEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@key, "key");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_ForPair node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<INode_Expression>(elem, node.@keyInterface, "key-interface");
		append<Node_Identifier>(elem, node.@keyName, "key-name");
		append<INode_Expression>(elem, node.@valueInterface, "value-interface");
		append<Node_Identifier>(elem, node.@valueName, "value-name");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	protected XmlElement serialize(Node_MemberImplementation node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_MemberIdentification>(elem, node.@memberIdentification, null);
		append<INode_Expression>(elem, node.@function, "function");
		return elem;
	}

	protected XmlElement serialize(Node_MemberIdentification node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_MemberType>(elem, node.@memberType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected XmlElement serialize(Node_ForRange node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@start, "start");
		append<INode_Expression>(elem, node.@limit, "limit");
		append<Node_Boolean>(elem, node.@inclusive, "inclusive");
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@action, "action");
		return elem;
	}

	protected XmlElement serialize(Node_Nand node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected XmlElement serialize(Node_Import node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@library, "library");
		append<Node_Identifier>(elem, node.@alias, "alias");
		return elem;
	}

	protected XmlElement serialize(Node_Method node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected XmlElement serialize(Node_Dictionary node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@keyType, "key-type");
		append<Node_NullableType>(elem, node.@valueType, "value-type");
		append<Node_DictionaryEntry>(elem, node.@dictionaryEntrys, null);
		return elem;
	}

	protected XmlElement serialize(Node_Enum node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_EnumEntry>(elem, node.@enumEntrys, null);
		return elem;
	}

	protected XmlElement serialize(Node_Possibility node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected XmlElement serialize(Node_Interface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@inheritees, "inheritee");
		append<INode_InterfaceMember>(elem, node.@members, "member");
		return elem;
	}

	protected XmlElement serialize(Node_Throw node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_IdentikeyType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_IdentikeyCategory>(elem, node.@identikeyCategory, null);
		append<Node_NullableType>(elem, node.@nullableType, null);
		return elem;
	}

	protected XmlElement serialize(Node_Curry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@function, "function");
		append<Node_Argument>(elem, node.@arguments, null);
		append<Node_Boolean>(elem, node.@call, "call");
		return elem;
	}

	protected XmlElement serialize(Node_Or node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected XmlElement serialize(Node_Block node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_ScopeAlteration>(elem, node.@alts, "alt");
		append<INode_Expression>(elem, node.@members, "member");
		return elem;
	}

	protected XmlElement serialize(Node_GenericParameter node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@defaultInterface, "default-interface");
		return elem;
	}

	protected XmlElement serialize(Node_While node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@test, "test");
		append<Node_Block>(elem, node.@block, null);
		return elem;
	}

	protected XmlElement serialize(Node_DeclareAssign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, null);
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_ForKey node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<INode_Expression>(elem, node.@keyInterface, "key-interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	protected XmlElement serialize(Node_Comprehension node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@sourceCollection, "source-collection");
		append<Node_Identifier>(elem, node.@elementName, "element-name");
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@output, "output");
		return elem;
	}

	protected XmlElement serialize(Node_Array node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<INode_Expression>(elem, node.@elements, "element");
		return elem;
	}

	protected XmlElement serialize(Node_Select node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Case>(elem, node.@cases, null);
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	protected XmlElement serialize(Node_ParameterImpl node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, null);
		append<Node_NullableType>(elem, node.@nullableType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@defaultValue, "default-value");
		return elem;
	}

	protected XmlElement serialize(Node_IgnoreMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@name, "name");
		append<Node_Integer>(elem, node.@depth, "depth");
		return elem;
	}

	protected XmlElement serialize(Node_Breed node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@parent, "parent");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected XmlElement serialize(Node_Call node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@receiver, "receiver");
		append<Node_Argument>(elem, node.@arguments, null);
		return elem;
	}

	protected XmlElement serialize(Node_Function node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterImpl>(elem, node.@parameterImpls, null);
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected XmlElement serialize(Node_Conditional node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Possibility>(elem, node.@possibilitys, null);
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	protected XmlElement serialize(Node_Expose node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@identifiers, null);
		return elem;
	}

	protected XmlElement serialize(Node_Worker node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@face, "face");
		append<Node_Worker>(elem, node.@childs, "child");
		append<Node_MemberImplementation>(elem, node.@memberImplementations, null);
		return elem;
	}

	protected XmlElement serialize(Node_NullableType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Boolean>(elem, node.@nullable, "nullable");
		return elem;
	}

	protected XmlElement serialize(Node_Case node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@values, "value");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected XmlElement serialize(Node_TryCatch node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@try, "try");
		append<Node_ExceptionHandler>(elem, node.@exceptionHandlers, null);
		append<INode_Expression>(elem, node.@else, "else");
		append<INode_Expression>(elem, node.@finally, "finally");
		return elem;
	}

	protected XmlElement serialize(Node_Caller node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@methodName, "method-name");
		return elem;
	}

	protected XmlElement serialize(Node_Cast node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_NullableType>(elem, node.@nullableType, null);
		return elem;
	}

	protected XmlElement serialize(Node_Labeled node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		append<INode_Expression>(elem, node.@child, "child");
		return elem;
	}

	protected XmlElement serialize(Node_ForValue node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<INode_Expression>(elem, node.@valueInterface, "value-interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	protected XmlElement serialize(Node_Property node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@writable, "writable");
		append<Node_NullableType>(elem, node.@nullableType, null);
		return elem;
	}

	protected XmlElement serialize(Node_Callee node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterInfo>(elem, node.@parameterInfos, null);
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		return elem;
	}

	protected XmlElement serialize(Node_Loop node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Block>(elem, node.@block, null);
		return elem;
	}

	protected XmlElement serialize(Node_And node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected XmlElement serialize(Node_Argument node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@parameterName, "parameter-name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected XmlElement serialize(Node_ForManual node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@initializers, "initializer");
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@postActions, "post-action");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	protected XmlElement serialize(Node_DoWhile node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Block>(elem, node.@action, "action");
		append<INode_Expression>(elem, node.@test, "test");
		return elem;
	}

	protected XmlElement serialize(Node_DoTimes node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@times, "times");
		append<Node_Block>(elem, node.@action, "action");
		append<INode_Expression>(elem, node.@test, "test");
		return elem;
	}

	protected XmlElement serialize(Node_Null node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected XmlElement serialize(Node_GenericInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_GenericParameter>(elem, node.@parameters, "parameter");
		append<Node_Interface>(elem, node.@interface, null);
		return elem;
	}

	protected XmlElement serialize(Node_ExceptionHandler node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Boolean>(elem, node.@catch, "catch");
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected XmlElement serialize(Node_FunctionInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@templateArgumentCount, "template-argument-count");
		append<Node_ParameterInfo>(elem, node.@parameterInfos, null);
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		return elem;
	}

	protected XmlElement serialize(Node_NamespacedValueIdentikey node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@namespaces, "namespace");
		append<Node_Identifier>(elem, node.@identikeyName, "identikey-name");
		return elem;
	}

	protected XmlElement serialize(Node_Object node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Worker>(elem, node.@workers, null);
		return elem;
	}

	protected XmlElement serialize(Node_Bundle node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Import>(elem, node.@imports, null);
		append<INode_ScopeAlteration>(elem, node.@alts, "alt");
		append<Node_Plane>(elem, node.@planes, null);
		return elem;
	}

	protected XmlElement serialize(Node_Plane node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_ScopeAlteration>(elem, node.@alts, "alt");
		append<Node_DeclareFirst>(elem, node.@declareFirsts, null);
		return elem;
	}

	protected XmlElement serialize(Node_Xor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected XmlElement serialize(Node_ExtractMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@memberName, "member-name");
		return elem;
	}

	protected XmlElement serialize(Node_Implements node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected XmlElement serialize(Node_Assign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}
}
