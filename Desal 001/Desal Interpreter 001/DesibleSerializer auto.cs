
//This file was generated programmatically, so
//don't edit this file directly.

using System.Collections.Generic;
using System.Xml;

partial class DesibleSerializer {
	XmlElement serialize(Node_And node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	XmlElement serialize(Node_DeclareFirst node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, "identikey-type");
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Boolean>(elem, node.@breed, "breed");
		return elem;
	}

	XmlElement serialize(Node_InstantiateGeneric node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@generic, "generic");
		append<Node_Argument>(elem, node.@arguments, "argument");
		return elem;
	}

	XmlElement serialize(Node_GenericParameter node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@defaultInterface, "default-interface");
		return elem;
	}

	XmlElement serialize(Node_Unassign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@identifier, "identifier");
		return elem;
	}

	XmlElement serialize(Node_Chain node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<INode_Expression>(elem, node.@elements, "element");
		return elem;
	}

	XmlElement serialize(Node_DeclareConstEmpty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, "identikey-type");
		return elem;
	}

	XmlElement serialize(Node_Conditional node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Possibility>(elem, node.@possibilitys, "possibility");
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	XmlElement serialize(Node_Argument node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@parameterName, "parameter-name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	XmlElement serialize(Node_DeclareAssign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, "identikey-type");
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Boolean>(elem, node.@breed, "breed");
		return elem;
	}

	XmlElement serialize(Node_Nand node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	XmlElement serialize(Node_StatusedMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		
		return elem;
	}

	XmlElement serialize(Node_StaticMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Declaration>(elem, node.@declaration, "declaration");
		append<Node_Access>(elem, node.@access, "access");
		return elem;
	}

	XmlElement serialize(Node_ForManual node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@initializers, "initializer");
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@postActions, "post-action");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	XmlElement serialize(Node_DoWhile node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Block>(elem, node.@action, "action");
		append<INode_Expression>(elem, node.@test, "test");
		return elem;
	}

	XmlElement serialize(Node_ClassProperty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@identifier, "identifier");
		append<Node_IdentikeyType>(elem, node.@identikeyType, "identikey-type");
		append<Node_Function>(elem, node.@getter, "getter");
		append<Node_Function>(elem, node.@setter, "setter");
		return elem;
	}

	XmlElement serialize(Node_Comprehension node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		
		return elem;
	}

	XmlElement serialize(Node_DoTimes node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@times, "times");
		append<Node_Block>(elem, node.@action, "action");
		append<INode_Expression>(elem, node.@test, "test");
		return elem;
	}

	XmlElement serialize(Node_DictionaryEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@key, "key");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	XmlElement serialize(Node_ForPair node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<INode_Expression>(elem, node.@keyInterface, "key-interface");
		append<Node_Identifier>(elem, node.@keyName, "key-name");
		append<INode_Expression>(elem, node.@valueInterface, "value-interface");
		append<Node_Identifier>(elem, node.@valueName, "value-name");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	XmlElement serialize(Node_Array node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<INode_Expression>(elem, node.@elements, "element");
		return elem;
	}

	XmlElement serialize(Node_Select node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Case>(elem, node.@cases, "case");
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	XmlElement serialize(Node_GenericInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		
		return elem;
	}

	XmlElement serialize(Node_IgnoreMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@name, "name");
		append<Node_Integer>(elem, node.@depth, "depth");
		return elem;
	}

	XmlElement serialize(Node_Generator node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<INode_Expression>(elem, node.@expression, "expression");
		return elem;
	}

	XmlElement serialize(Node_ForRange node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@start, "start");
		append<INode_Expression>(elem, node.@limit, "limit");
		append<Node_Boolean>(elem, node.@inclusive, "inclusive");
		append<INode_Expression>(elem, node.@test, "test");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	XmlElement serialize(Node_ExceptionHandler node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Boolean>(elem, node.@catch, "catch");
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	XmlElement serialize(Node_Breed node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@parent, "parent");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	XmlElement serialize(Node_Labeled node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		append<INode_Expression>(elem, node.@child, "child");
		return elem;
	}

	XmlElement serialize(Node_NamedFunction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Function>(elem, node.@function, "function");
		return elem;
	}

	XmlElement serialize(Node_Class node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		
		return elem;
	}

	XmlElement serialize(Node_Call node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Argument>(elem, node.@arguments, "argument");
		return elem;
	}

	XmlElement serialize(Node_GenericClass node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		
		return elem;
	}

	XmlElement serialize(Node_FunctionInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@templateArgumentCount, "template-argument-count");
		append<Node_Parameter>(elem, node.@parameters, "parameter");
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		return elem;
	}

	XmlElement serialize(Node_Import node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@library, "library");
		append<Node_Identifier>(elem, node.@alias, "alias");
		return elem;
	}

	XmlElement serialize(Node_Parameter node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, "direction");
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@hasDefaultValue, "has-default-value");
		append<INode_Expression>(elem, node.@defaultValue, "default-value");
		return elem;
	}

	XmlElement serialize(Node_Method node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	XmlElement serialize(Node_Function node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Parameter>(elem, node.@parameters, "parameter");
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	XmlElement serialize(Node_Xnor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	XmlElement serialize(Node_NamespacedValueIdentikey node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@namespaces, "namespace");
		append<Node_Identifier>(elem, node.@identikeyName, "identikey-name");
		return elem;
	}

	XmlElement serialize(Node_Xor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	XmlElement serialize(Node_Return node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@expression, "expression");
		return elem;
	}

	XmlElement serialize(Node_Implements node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	XmlElement serialize(Node_Dictionary node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@keyType, "key-type");
		append<Node_NullableType>(elem, node.@valueType, "value-type");
		append<Node_DictionaryEntry>(elem, node.@dictionaryEntrys, "dictionary-entry");
		return elem;
	}

	XmlElement serialize(Node_InterfaceImplementation node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_InterfaceImplementation>(elem, node.@childImplemenatations, "child-implemenatation");
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Function>(elem, node.@callees, "callee");
		append<Node_NamedFunction>(elem, node.@getters, "getter");
		append<Node_NamedFunction>(elem, node.@setters, "setter");
		append<Node_NamedFunction>(elem, node.@methods, "method");
		append<Node_Boolean>(elem, node.@default, "default");
		return elem;
	}

	XmlElement serialize(Node_Expose node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@identifiers, "identifier");
		return elem;
	}

	XmlElement serialize(Node_DeclareEmpty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_IdentikeyType>(elem, node.@identikeyType, "identikey-type");
		return elem;
	}

	XmlElement serialize(Node_Possibility node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	XmlElement serialize(Node_Break node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
		return elem;
	}

	XmlElement serialize(Node_GenericFunction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_GenericParameter>(elem, node.@parameters, "parameter");
		append<Node_Function>(elem, node.@function, "function");
		return elem;
	}

	XmlElement serialize(Node_Enum node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<Node_EnumEntry>(elem, node.@enumEntrys, "enum-entry");
		return elem;
	}

	XmlElement serialize(Node_NullableType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Boolean>(elem, node.@nullable, "nullable");
		return elem;
	}

	XmlElement serialize(Node_ForKey node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<INode_Expression>(elem, node.@keyInterface, "key-interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	XmlElement serialize(Node_Using node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@targets, "target");
		append<Node_Identifier>(elem, node.@name, "name");
		return elem;
	}

	XmlElement serialize(Node_SetProperty node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@propertyName, "property-name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	XmlElement serialize(Node_ExtractMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@memberName, "member-name");
		return elem;
	}

	XmlElement serialize(Node_Throw node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@expression, "expression");
		return elem;
	}

	XmlElement serialize(Node_Nor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	XmlElement serialize(Node_Case node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@values, "value");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	XmlElement serialize(Node_Property node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<Node_Access>(elem, node.@access, "access");
		return elem;
	}

	XmlElement serialize(Node_TryCatch node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@try, "try");
		append<Node_ExceptionHandler>(elem, node.@exceptionHandlers, "exception-handler");
		append<INode_Expression>(elem, node.@else, "else");
		append<INode_Expression>(elem, node.@finally, "finally");
		return elem;
	}

	XmlElement serialize(Node_Callee node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Parameter>(elem, node.@parameters, "parameter");
		append<Node_NullableType>(elem, node.@returnInfo, "return-info");
		return elem;
	}

	XmlElement serialize(Node_Interface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Property>(elem, node.@propertys, "property");
		append<Node_Method>(elem, node.@methods, "method");
		return elem;
	}

	XmlElement serialize(Node_Caller node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@methodName, "method-name");
		return elem;
	}

	XmlElement serialize(Node_Yield node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@expression, "expression");
		return elem;
	}

	XmlElement serialize(Node_Or node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	XmlElement serialize(Node_Ignore node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@content, "content");
		append<Node_IgnoreMember>(elem, node.@ignoreMembers, "ignore-member");
		return elem;
	}

	XmlElement serialize(Node_Cast node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@object, "object");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	XmlElement serialize(Node_While node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@test, "test");
		append<Node_Block>(elem, node.@block, "block");
		return elem;
	}

	XmlElement serialize(Node_IdentikeyType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_IdentikeyCategory>(elem, node.@identikeyCategory, "identikey-category");
		append<Node_NullableType>(elem, node.@nullableType, "nullable-type");
		append<Node_Boolean>(elem, node.@constant, "constant");
		return elem;
	}

	XmlElement serialize(Node_ForValue node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@container, "container");
		append<INode_Expression>(elem, node.@valueInterface, "value-interface");
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Block>(elem, node.@action, "action");
		return elem;
	}

	XmlElement serialize(Node_Block node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_ScopeAlteration>(elem, node.@scopeAlterations, "scope-alteration");
		append<INode_Expression>(elem, node.@members, "member");
		return elem;
	}

	XmlElement serialize(Node_EnumEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	XmlElement serialize(Node_Convertor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	XmlElement serialize(Node_Curry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@function, "function");
		append<Node_Argument>(elem, node.@arguments, "argument");
		append<Node_Boolean>(elem, node.@call, "call");
		return elem;
	}

	XmlElement serialize(Node_Assign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@value, "value");
		append<Node_Boolean>(elem, node.@breed, "breed");
		return elem;
	}

	XmlElement serialize(Node_Loop node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Block>(elem, node.@block, "block");
		return elem;
	}
}
