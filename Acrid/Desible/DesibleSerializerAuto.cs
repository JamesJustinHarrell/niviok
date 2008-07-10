//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;
using System.Xml;
using Acrid.NodeTypes;

namespace Acrid.Desible {

public abstract class DesibleSerializerAuto : DesibleSerializerBase {
	protected virtual XmlElement serialize(Node_And node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_MemberStatus node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_DeclareFirst node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@overload, "overload");
		append<INode_Expression>(elem, node.@type, "type");
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
		append<Node_Identifier>(elem, node.@parameterName, "parameter name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Module node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Integer>(elem, node.@niviokMajorVersionNumber, "niviok major version number");
		append<Node_Integer>(elem, node.@niviokMinorVersionNumber, "niviok minor version number");
		append<Node_Import>(elem, node.@imports, null);
		append<Node_Sieve>(elem, node.@sieve, null);
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

	protected virtual XmlElement serialize(Node_Yield node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Property node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@writable, "writable");
		append<INode_Expression>(elem, node.@type, "type");
		return elem;
	}

	protected virtual XmlElement serialize(Node_MemberType node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_ImportAttempt node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_String>(elem, node.@scheme, "scheme");
		append<Node_String>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Select node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@inputValue, "input value");
		append<Node_Case>(elem, node.@cases, null);
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	protected virtual XmlElement serialize(Node_String node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_GenericInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterInfo>(elem, node.@parameters, "parameter");
		append<Node_Interface>(elem, node.@interface, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_ParameterImpl node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, null);
		append<INode_Expression>(elem, node.@type, "type");
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@defaultValue, "default value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Xor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Generator node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@type, "type");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_GenericFunction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterInfo>(elem, node.@parameters, "parameter");
		append<Node_Function>(elem, node.@function, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Breed node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@parent, "parent");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Namespace node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Sieve>(elem, node.@sieve, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Nand node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Hidable node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Boolean>(elem, node.@hidden, "hidden");
		append<INode_StatementDeclaration>(elem, node.@declaration, "declaration");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Call node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@receiver, "receiver");
		append<Node_Argument>(elem, node.@arguments, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Nor node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@first, "first");
		append<INode_Expression>(elem, node.@second, "second");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Rational node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_TypeCase node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@testTypes, "test type");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Import node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@alias, "alias");
		append<Node_ImportAttempt>(elem, node.@importAttempts, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Method node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Function node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterImpl>(elem, node.@parameterImpls, null);
		append<INode_Expression>(elem, node.@returnType, "return type");
		append<INode_Expression>(elem, node.@body, "body");
		return elem;
	}

	protected virtual XmlElement serialize(Node_NamespacedWoScidentre node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@namespaces, "namespace");
		append<Node_Identifier>(elem, node.@identikeyName, "identikey name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Direction node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		elem.AppendChild(_doc.CreateTextNode(node.ToString()));
		return elem;
	}

	protected virtual XmlElement serialize(Node_Object node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Worker>(elem, node.@workers, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Curry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@function, "function");
		append<Node_Argument>(elem, node.@arguments, null);
		append<Node_Boolean>(elem, node.@call, "call");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Dictionary node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@keyType, "key type");
		append<INode_Expression>(elem, node.@valueType, "value type");
		append<Node_DictionaryEntry>(elem, node.@dictionaryEntrys, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_Callee node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_ParameterInfo>(elem, node.@parameterInfos, null);
		append<INode_Expression>(elem, node.@returnType, "return type");
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
		append<INode_Expression>(elem, node.@type, "type");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Worker node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@face, "face");
		append<Node_Worker>(elem, node.@childWorkers, "child worker");
		append<Node_MemberImplementation>(elem, node.@memberImplementations, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_FunctionInterface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@templateArgumentCount, "template-argument-count");
		append<Node_ParameterInfo>(elem, node.@parameterInfos, null);
		append<INode_Expression>(elem, node.@returnType, "return type");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Enum node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@type, "type");
		append<Node_EnumEntry>(elem, node.@enumEntrys, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_DeclareAssign node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@constant, "constant");
		append<INode_Expression>(elem, node.@type, "type");
		append<Node_Boolean>(elem, node.@breed, "breed");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Compound node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Expose>(elem, node.@exposes, null);
		append<Node_Using>(elem, node.@usings, null);
		append<INode_StatementDeclaration>(elem, node.@declarations, "declaration");
		append<INode_Expression>(elem, node.@members, "member");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Interface node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@inheritees, "inheritee");
		append<Node_StatusedMember>(elem, node.@members, "member");
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
		append<Node_Identifier>(elem, node.@propertyName, "property name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Sieve node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Expose>(elem, node.@exposes, null);
		append<Node_Using>(elem, node.@usings, null);
		append<Node_Hidable>(elem, node.@hidables, null);
		return elem;
	}

	protected virtual XmlElement serialize(Node_ExtractMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@source, "source");
		append<Node_Identifier>(elem, node.@memberName, "member name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Throw node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_TypeSelect node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@inputValue, "input value");
		append<Node_Identifier>(elem, node.@castedName, "casted name");
		append<Node_Boolean>(elem, node.@requireMatch, "require match");
		append<Node_TypeCase>(elem, node.@typeCases, null);
		append<INode_Expression>(elem, node.@else, "else");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Case node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@testValues, "test value");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected virtual XmlElement serialize(Node_DictionaryEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@key, "key");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Catcher node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@type, "type");
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@test, "test");
		append<INode_Expression>(elem, node.@result, "result");
		return elem;
	}

	protected virtual XmlElement serialize(Node_TryCatch node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@try, "try");
		append<Node_Catcher>(elem, node.@catchers, null);
		append<INode_Expression>(elem, node.@onSuccess, "on success");
		append<INode_Expression>(elem, node.@finally, "finally");
		return elem;
	}

	protected virtual XmlElement serialize(Node_ParameterInfo node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Direction>(elem, node.@direction, null);
		append<INode_Expression>(elem, node.@type, "type");
		append<Node_Identifier>(elem, node.@name, "name");
		append<Node_Boolean>(elem, node.@hasDefaultValue, "has default value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Caller node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@interface, "interface");
		append<Node_Identifier>(elem, node.@methodName, "method name");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Remit node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@label, "label");
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
		append<Node_MemberType>(elem, node.@memberType, null);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@interface, "interface");
		append<INode_Expression>(elem, node.@function, "function");
		return elem;
	}

	protected virtual XmlElement serialize(Node_StatusedMember node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_MemberStatus>(elem, node.@memberStatus, null);
		append<INode_InterfaceMember>(elem, node.@member, "member");
		return elem;
	}

	protected virtual XmlElement serialize(Node_EnumEntry node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<Node_Identifier>(elem, node.@name, "name");
		append<INode_Expression>(elem, node.@value, "value");
		return elem;
	}

	protected virtual XmlElement serialize(Node_Breeder node) {
		XmlElement elem = _doc.CreateElement(node.typeName, desible1NS);
		append<INode_Expression>(elem, node.@type, "type");
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
			case "member-status":
				return serialize((Node_MemberStatus)node);
			case "declare-first":
				return serialize((Node_DeclareFirst)node);
			case "instantiate-generic":
				return serialize((Node_InstantiateGeneric)node);
			case "xnor":
				return serialize((Node_Xnor)node);
			case "conditional":
				return serialize((Node_Conditional)node);
			case "argument":
				return serialize((Node_Argument)node);
			case "module":
				return serialize((Node_Module)node);
			case "integer":
				return serialize((Node_Integer)node);
			case "boolean":
				return serialize((Node_Boolean)node);
			case "yield":
				return serialize((Node_Yield)node);
			case "property":
				return serialize((Node_Property)node);
			case "member-type":
				return serialize((Node_MemberType)node);
			case "import-attempt":
				return serialize((Node_ImportAttempt)node);
			case "select":
				return serialize((Node_Select)node);
			case "string":
				return serialize((Node_String)node);
			case "generic-interface":
				return serialize((Node_GenericInterface)node);
			case "parameter-impl":
				return serialize((Node_ParameterImpl)node);
			case "xor":
				return serialize((Node_Xor)node);
			case "generator":
				return serialize((Node_Generator)node);
			case "generic-function":
				return serialize((Node_GenericFunction)node);
			case "breed":
				return serialize((Node_Breed)node);
			case "namespace":
				return serialize((Node_Namespace)node);
			case "nand":
				return serialize((Node_Nand)node);
			case "hidable":
				return serialize((Node_Hidable)node);
			case "call":
				return serialize((Node_Call)node);
			case "nor":
				return serialize((Node_Nor)node);
			case "rational":
				return serialize((Node_Rational)node);
			case "type-case":
				return serialize((Node_TypeCase)node);
			case "import":
				return serialize((Node_Import)node);
			case "method":
				return serialize((Node_Method)node);
			case "function":
				return serialize((Node_Function)node);
			case "namespaced-wo-scidentre":
				return serialize((Node_NamespacedWoScidentre)node);
			case "direction":
				return serialize((Node_Direction)node);
			case "object":
				return serialize((Node_Object)node);
			case "curry":
				return serialize((Node_Curry)node);
			case "dictionary":
				return serialize((Node_Dictionary)node);
			case "callee":
				return serialize((Node_Callee)node);
			case "expose":
				return serialize((Node_Expose)node);
			case "declare-empty":
				return serialize((Node_DeclareEmpty)node);
			case "worker":
				return serialize((Node_Worker)node);
			case "function-interface":
				return serialize((Node_FunctionInterface)node);
			case "enum":
				return serialize((Node_Enum)node);
			case "declare-assign":
				return serialize((Node_DeclareAssign)node);
			case "compound":
				return serialize((Node_Compound)node);
			case "interface":
				return serialize((Node_Interface)node);
			case "using":
				return serialize((Node_Using)node);
			case "set-property":
				return serialize((Node_SetProperty)node);
			case "sieve":
				return serialize((Node_Sieve)node);
			case "extract-member":
				return serialize((Node_ExtractMember)node);
			case "throw":
				return serialize((Node_Throw)node);
			case "type-select":
				return serialize((Node_TypeSelect)node);
			case "case":
				return serialize((Node_Case)node);
			case "dictionary-entry":
				return serialize((Node_DictionaryEntry)node);
			case "catcher":
				return serialize((Node_Catcher)node);
			case "try-catch":
				return serialize((Node_TryCatch)node);
			case "parameter-info":
				return serialize((Node_ParameterInfo)node);
			case "caller":
				return serialize((Node_Caller)node);
			case "remit":
				return serialize((Node_Remit)node);
			case "or":
				return serialize((Node_Or)node);
			case "member-implementation":
				return serialize((Node_MemberImplementation)node);
			case "statused-member":
				return serialize((Node_StatusedMember)node);
			case "enum-entry":
				return serialize((Node_EnumEntry)node);
			case "breeder":
				return serialize((Node_Breeder)node);
			case "identifier":
				return serialize((Node_Identifier)node);
			case "assign":
				return serialize((Node_Assign)node);
			default:
				throw new ApplicationException(
					String.Format(
						"can't serialize node of type {0} from {1}",
						node.typeName,
						node.nodeSource));
		}
	}
}

} //namespace

