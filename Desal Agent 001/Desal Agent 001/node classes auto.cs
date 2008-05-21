
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;

class Node_And : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	
	public Node_And(
	INode_Expression @first,
	INode_Expression @second ) {
		m_first = @first;
		m_second = @second;
	}
	
	public INode_Expression @first {
		get { return m_first; }
	}

	public INode_Expression @second {
		get { return m_second; }
	}

	public string typeName {
		get { return "and"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_first,
				m_second );
		}
	}
}

class Node_Function : INode_Expression {
	IList<Node_ParameterImpl> m_parameterImpls;
	Node_NullableType m_returnInfo;
	INode_Expression m_body;
	
	public Node_Function(
	IList<Node_ParameterImpl> @parameterImpls,
	Node_NullableType @returnInfo,
	INode_Expression @body ) {
		m_parameterImpls = @parameterImpls;
		m_returnInfo = @returnInfo;
		m_body = @body;
	}
	
	public IList<Node_ParameterImpl> @parameterImpls {
		get { return m_parameterImpls; }
	}

	public Node_NullableType @returnInfo {
		get { return m_returnInfo; }
	}

	public INode_Expression @body {
		get { return m_body; }
	}

	public string typeName {
		get { return "function"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameterImpls,
				m_returnInfo,
				m_body );
		}
	}
}

class Node_DeclareFirst : INode_Expression {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	Node_Boolean m_breed;
	INode_Expression m_value;
	
	public Node_DeclareFirst(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	Node_Boolean @breed,
	INode_Expression @value ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_breed = @breed;
		m_value = @value;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_IdentikeyType @identikeyType {
		get { return m_identikeyType; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "declare-first"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_identikeyType,
				m_breed,
				m_value );
		}
	}
}

class Node_InstantiateGeneric : INode_Expression {
	INode_Expression m_generic;
	IList<Node_Argument> m_arguments;
	
	public Node_InstantiateGeneric(
	INode_Expression @generic,
	IList<Node_Argument> @arguments ) {
		m_generic = @generic;
		m_arguments = @arguments;
	}
	
	public INode_Expression @generic {
		get { return m_generic; }
	}

	public IList<Node_Argument> @arguments {
		get { return m_arguments; }
	}

	public string typeName {
		get { return "instantiate-generic"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_generic,
				m_arguments );
		}
	}
}

class Node_GenericParameter : INode {
	Node_Identifier m_name;
	INode_Expression m_defaultInterface;
	
	public Node_GenericParameter(
	Node_Identifier @name,
	INode_Expression @defaultInterface ) {
		m_name = @name;
		m_defaultInterface = @defaultInterface;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @defaultInterface {
		get { return m_defaultInterface; }
	}

	public string typeName {
		get { return "generic-parameter"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_defaultInterface );
		}
	}
}

class Node_Xnor : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	
	public Node_Xnor(
	INode_Expression @first,
	INode_Expression @second ) {
		m_first = @first;
		m_second = @second;
	}
	
	public INode_Expression @first {
		get { return m_first; }
	}

	public INode_Expression @second {
		get { return m_second; }
	}

	public string typeName {
		get { return "xnor"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_first,
				m_second );
		}
	}
}

class Node_Conditional : INode_Expression {
	INode_Expression m_test;
	INode_Expression m_result;
	INode_Expression m_else;
	
	public Node_Conditional(
	INode_Expression @test,
	INode_Expression @result,
	INode_Expression @else ) {
		m_test = @test;
		m_result = @result;
		m_else = @else;
	}
	
	public INode_Expression @test {
		get { return m_test; }
	}

	public INode_Expression @result {
		get { return m_result; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}

	public string typeName {
		get { return "conditional"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_test,
				m_result,
				m_else );
		}
	}
}

class Node_Argument : INode {
	Node_Identifier m_parameterName;
	INode_Expression m_value;
	
	public Node_Argument(
	Node_Identifier @parameterName,
	INode_Expression @value ) {
		m_parameterName = @parameterName;
		m_value = @value;
	}
	
	public Node_Identifier @parameterName {
		get { return m_parameterName; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "argument"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameterName,
				m_value );
		}
	}
}

class Node_DeclareAssign : INode_Expression {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	Node_Boolean m_breed;
	Node_Boolean m_inferInterface;
	INode_Expression m_value;
	
	public Node_DeclareAssign(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	Node_Boolean @breed,
	Node_Boolean @inferInterface,
	INode_Expression @value ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_breed = @breed;
		m_inferInterface = @inferInterface;
		m_value = @value;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_IdentikeyType @identikeyType {
		get { return m_identikeyType; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
	}

	public Node_Boolean @inferInterface {
		get { return m_inferInterface; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "declare-assign"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_identikeyType,
				m_breed,
				m_inferInterface,
				m_value );
		}
	}
}

class Node_ConditionalLoop : INode_Expression {
	INode_Expression m_test;
	INode_Expression m_body;
	
	public Node_ConditionalLoop(
	INode_Expression @test,
	INode_Expression @body ) {
		m_test = @test;
		m_body = @body;
	}
	
	public INode_Expression @test {
		get { return m_test; }
	}

	public INode_Expression @body {
		get { return m_body; }
	}

	public string typeName {
		get { return "conditional-loop"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_test,
				m_body );
		}
	}
}

class Node_DictionaryEntry : INode {
	INode_Expression m_key;
	INode_Expression m_value;
	
	public Node_DictionaryEntry(
	INode_Expression @key,
	INode_Expression @value ) {
		m_key = @key;
		m_value = @value;
	}
	
	public INode_Expression @key {
		get { return m_key; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "dictionary-entry"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_key,
				m_value );
		}
	}
}

class Node_Null : INode_Expression {
	INode_Expression m_interface;
	
	public Node_Null(
	INode_Expression @interface ) {
		m_interface = @interface;
	}
	
	public INode_Expression @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "null"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_interface );
		}
	}
}

class Node_GenericInterface : INode_Expression {
	IList<Node_GenericParameter> m_parameters;
	Node_Interface m_interface;
	
	public Node_GenericInterface(
	IList<Node_GenericParameter> @parameters,
	Node_Interface @interface ) {
		m_parameters = @parameters;
		m_interface = @interface;
	}
	
	public IList<Node_GenericParameter> @parameters {
		get { return m_parameters; }
	}

	public Node_Interface @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "generic-interface"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameters,
				m_interface );
		}
	}
}

class Node_Ignore : INode_Expression {
	INode_Expression m_content;
	IList<Node_IgnoreMember> m_ignoreMembers;
	
	public Node_Ignore(
	INode_Expression @content,
	IList<Node_IgnoreMember> @ignoreMembers ) {
		m_content = @content;
		m_ignoreMembers = @ignoreMembers;
	}
	
	public INode_Expression @content {
		get { return m_content; }
	}

	public IList<Node_IgnoreMember> @ignoreMembers {
		get { return m_ignoreMembers; }
	}

	public string typeName {
		get { return "ignore"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_content,
				m_ignoreMembers );
		}
	}
}

class Node_ParameterImpl : INode {
	Node_Direction m_direction;
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	INode_Expression m_defaultValue;
	
	public Node_ParameterImpl(
	Node_Direction @direction,
	Node_NullableType @nullableType,
	Node_Identifier @name,
	INode_Expression @defaultValue ) {
		m_direction = @direction;
		m_nullableType = @nullableType;
		m_name = @name;
		m_defaultValue = @defaultValue;
	}
	
	public Node_Direction @direction {
		get { return m_direction; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @defaultValue {
		get { return m_defaultValue; }
	}

	public string typeName {
		get { return "parameter-impl"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_direction,
				m_nullableType,
				m_name,
				m_defaultValue );
		}
	}
}

class Node_MemberIdentification : INode {
	Node_MemberType m_memberType;
	Node_Identifier m_name;
	INode_Expression m_interface;
	
	public Node_MemberIdentification(
	Node_MemberType @memberType,
	Node_Identifier @name,
	INode_Expression @interface ) {
		m_memberType = @memberType;
		m_name = @name;
		m_interface = @interface;
	}
	
	public Node_MemberType @memberType {
		get { return m_memberType; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "member-identification"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_memberType,
				m_name,
				m_interface );
		}
	}
}

class Node_Generator : INode_Expression {
	Node_NullableType m_nullableType;
	INode_Expression m_body;
	
	public Node_Generator(
	Node_NullableType @nullableType,
	INode_Expression @body ) {
		m_nullableType = @nullableType;
		m_body = @body;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public INode_Expression @body {
		get { return m_body; }
	}

	public string typeName {
		get { return "generator"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_body );
		}
	}
}

class Node_Cast : INode_Expression {
	INode_Expression m_source;
	Node_NullableType m_nullableType;
	
	public Node_Cast(
	INode_Expression @source,
	Node_NullableType @nullableType ) {
		m_source = @source;
		m_nullableType = @nullableType;
	}
	
	public INode_Expression @source {
		get { return m_source; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public string typeName {
		get { return "cast"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_source,
				m_nullableType );
		}
	}
}

class Node_ExceptionHandler : INode {
	Node_Boolean m_catch;
	INode_Expression m_interface;
	Node_Identifier m_name;
	INode_Expression m_result;
	
	public Node_ExceptionHandler(
	Node_Boolean @catch,
	INode_Expression @interface,
	Node_Identifier @name,
	INode_Expression @result ) {
		m_catch = @catch;
		m_interface = @interface;
		m_name = @name;
		m_result = @result;
	}
	
	public Node_Boolean @catch {
		get { return m_catch; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @result {
		get { return m_result; }
	}

	public string typeName {
		get { return "exception-handler"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_catch,
				m_interface,
				m_name,
				m_result );
		}
	}
}

class Node_Breed : INode_Expression {
	INode_Expression m_parent;
	INode_Expression m_interface;
	
	public Node_Breed(
	INode_Expression @parent,
	INode_Expression @interface ) {
		m_parent = @parent;
		m_interface = @interface;
	}
	
	public INode_Expression @parent {
		get { return m_parent; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "breed"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parent,
				m_interface );
		}
	}
}

class Node_Nand : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	
	public Node_Nand(
	INode_Expression @first,
	INode_Expression @second ) {
		m_first = @first;
		m_second = @second;
	}
	
	public INode_Expression @first {
		get { return m_first; }
	}

	public INode_Expression @second {
		get { return m_second; }
	}

	public string typeName {
		get { return "nand"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_first,
				m_second );
		}
	}
}

class Node_Call : INode_Expression {
	INode_Expression m_receiver;
	IList<Node_Argument> m_arguments;
	
	public Node_Call(
	INode_Expression @receiver,
	IList<Node_Argument> @arguments ) {
		m_receiver = @receiver;
		m_arguments = @arguments;
	}
	
	public INode_Expression @receiver {
		get { return m_receiver; }
	}

	public IList<Node_Argument> @arguments {
		get { return m_arguments; }
	}

	public string typeName {
		get { return "call"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_receiver,
				m_arguments );
		}
	}
}

class Node_Select : INode_Expression {
	INode_Expression m_value;
	IList<Node_Case> m_cases;
	INode_Expression m_else;
	
	public Node_Select(
	INode_Expression @value,
	IList<Node_Case> @cases,
	INode_Expression @else ) {
		m_value = @value;
		m_cases = @cases;
		m_else = @else;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public IList<Node_Case> @cases {
		get { return m_cases; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}

	public string typeName {
		get { return "select"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value,
				m_cases,
				m_else );
		}
	}
}

class Node_FunctionInterface : INode_Expression {
	INode_Expression m_templateArgumentCount;
	IList<Node_ParameterInfo> m_parameterInfos;
	Node_NullableType m_returnInfo;
	
	public Node_FunctionInterface(
	INode_Expression @templateArgumentCount,
	IList<Node_ParameterInfo> @parameterInfos,
	Node_NullableType @returnInfo ) {
		m_templateArgumentCount = @templateArgumentCount;
		m_parameterInfos = @parameterInfos;
		m_returnInfo = @returnInfo;
	}
	
	public INode_Expression @templateArgumentCount {
		get { return m_templateArgumentCount; }
	}

	public IList<Node_ParameterInfo> @parameterInfos {
		get { return m_parameterInfos; }
	}

	public Node_NullableType @returnInfo {
		get { return m_returnInfo; }
	}

	public string typeName {
		get { return "function-interface"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_templateArgumentCount,
				m_parameterInfos,
				m_returnInfo );
		}
	}
}

class Node_Import : INode {
	Node_String m_library;
	Node_Identifier m_alias;
	
	public Node_Import(
	Node_String @library,
	Node_Identifier @alias ) {
		m_library = @library;
		m_alias = @alias;
	}
	
	public Node_String @library {
		get { return m_library; }
	}

	public Node_Identifier @alias {
		get { return m_alias; }
	}

	public string typeName {
		get { return "import"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_library,
				m_alias );
		}
	}
}

class Node_Method : INode_InterfaceMember {
	Node_Identifier m_name;
	INode_Expression m_interface;
	
	public Node_Method(
	Node_Identifier @name,
	INode_Expression @interface ) {
		m_name = @name;
		m_interface = @interface;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "method"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_interface );
		}
	}
}

class Node_NamespacedValueIdentikey : INode_Expression {
	IList<Node_Identifier> m_namespaces;
	Node_Identifier m_identikeyName;
	
	public Node_NamespacedValueIdentikey(
	IList<Node_Identifier> @namespaces,
	Node_Identifier @identikeyName ) {
		m_namespaces = @namespaces;
		m_identikeyName = @identikeyName;
	}
	
	public IList<Node_Identifier> @namespaces {
		get { return m_namespaces; }
	}

	public Node_Identifier @identikeyName {
		get { return m_identikeyName; }
	}

	public string typeName {
		get { return "namespaced-value-identikey"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_namespaces,
				m_identikeyName );
		}
	}
}

class Node_Case : INode {
	IList<INode_Expression> m_values;
	INode_Expression m_result;
	
	public Node_Case(
	IList<INode_Expression> @values,
	INode_Expression @result ) {
		m_values = @values;
		m_result = @result;
	}
	
	public IList<INode_Expression> @values {
		get { return m_values; }
	}

	public INode_Expression @result {
		get { return m_result; }
	}

	public string typeName {
		get { return "case"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_values,
				m_result );
		}
	}
}

class Node_Xor : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	
	public Node_Xor(
	INode_Expression @first,
	INode_Expression @second ) {
		m_first = @first;
		m_second = @second;
	}
	
	public INode_Expression @first {
		get { return m_first; }
	}

	public INode_Expression @second {
		get { return m_second; }
	}

	public string typeName {
		get { return "xor"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_first,
				m_second );
		}
	}
}

class Node_Return : INode_Expression {
	INode_Expression m_value;
	
	public Node_Return(
	INode_Expression @value ) {
		m_value = @value;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "return"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value );
		}
	}
}

class Node_Continue : INode_Expression {
	Node_Identifier m_label;
	
	public Node_Continue(
	Node_Identifier @label ) {
		m_label = @label;
	}
	
	public Node_Identifier @label {
		get { return m_label; }
	}

	public string typeName {
		get { return "continue"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_label );
		}
	}
}

class Node_Dictionary : INode_Expression {
	Node_NullableType m_keyType;
	Node_NullableType m_valueType;
	IList<Node_DictionaryEntry> m_dictionaryEntrys;
	
	public Node_Dictionary(
	Node_NullableType @keyType,
	Node_NullableType @valueType,
	IList<Node_DictionaryEntry> @dictionaryEntrys ) {
		m_keyType = @keyType;
		m_valueType = @valueType;
		m_dictionaryEntrys = @dictionaryEntrys;
	}
	
	public Node_NullableType @keyType {
		get { return m_keyType; }
	}

	public Node_NullableType @valueType {
		get { return m_valueType; }
	}

	public IList<Node_DictionaryEntry> @dictionaryEntrys {
		get { return m_dictionaryEntrys; }
	}

	public string typeName {
		get { return "dictionary"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_keyType,
				m_valueType,
				m_dictionaryEntrys );
		}
	}
}

class Node_Implements : INode_Expression {
	INode_Expression m_value;
	INode_Expression m_interface;
	
	public Node_Implements(
	INode_Expression @value,
	INode_Expression @interface ) {
		m_value = @value;
		m_interface = @interface;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "implements"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value,
				m_interface );
		}
	}
}

class Node_Callee : INode_InterfaceMember {
	IList<Node_ParameterInfo> m_parameterInfos;
	Node_NullableType m_returnInfo;
	
	public Node_Callee(
	IList<Node_ParameterInfo> @parameterInfos,
	Node_NullableType @returnInfo ) {
		m_parameterInfos = @parameterInfos;
		m_returnInfo = @returnInfo;
	}
	
	public IList<Node_ParameterInfo> @parameterInfos {
		get { return m_parameterInfos; }
	}

	public Node_NullableType @returnInfo {
		get { return m_returnInfo; }
	}

	public string typeName {
		get { return "callee"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameterInfos,
				m_returnInfo );
		}
	}
}

class Node_Expose : INode_ScopeAlteration {
	IList<Node_Identifier> m_identifiers;
	
	public Node_Expose(
	IList<Node_Identifier> @identifiers ) {
		m_identifiers = @identifiers;
	}
	
	public IList<Node_Identifier> @identifiers {
		get { return m_identifiers; }
	}

	public string typeName {
		get { return "expose"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_identifiers );
		}
	}
}

class Node_DeclareEmpty : INode_Expression {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	
	public Node_DeclareEmpty(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_IdentikeyType @identikeyType {
		get { return m_identikeyType; }
	}

	public string typeName {
		get { return "declare-empty"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_identikeyType );
		}
	}
}

class Node_Worker : INode {
	INode_Expression m_face;
	IList<Node_Worker> m_childs;
	IList<Node_MemberImplementation> m_memberImplementations;
	
	public Node_Worker(
	INode_Expression @face,
	IList<Node_Worker> @childs,
	IList<Node_MemberImplementation> @memberImplementations ) {
		m_face = @face;
		m_childs = @childs;
		m_memberImplementations = @memberImplementations;
	}
	
	public INode_Expression @face {
		get { return m_face; }
	}

	public IList<Node_Worker> @childs {
		get { return m_childs; }
	}

	public IList<Node_MemberImplementation> @memberImplementations {
		get { return m_memberImplementations; }
	}

	public string typeName {
		get { return "worker"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_face,
				m_childs,
				m_memberImplementations );
		}
	}
}

class Node_Bundle : INode_Expression {
	IList<Node_Import> m_imports;
	IList<INode_ScopeAlteration> m_alts;
	IList<Node_Plane> m_planes;
	
	public Node_Bundle(
	IList<Node_Import> @imports,
	IList<INode_ScopeAlteration> @alts,
	IList<Node_Plane> @planes ) {
		m_imports = @imports;
		m_alts = @alts;
		m_planes = @planes;
	}
	
	public IList<Node_Import> @imports {
		get { return m_imports; }
	}

	public IList<INode_ScopeAlteration> @alts {
		get { return m_alts; }
	}

	public IList<Node_Plane> @planes {
		get { return m_planes; }
	}

	public string typeName {
		get { return "bundle"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_imports,
				m_alts,
				m_planes );
		}
	}
}

class Node_Break : INode_Expression {
	Node_Identifier m_label;
	
	public Node_Break(
	Node_Identifier @label ) {
		m_label = @label;
	}
	
	public Node_Identifier @label {
		get { return m_label; }
	}

	public string typeName {
		get { return "break"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_label );
		}
	}
}

class Node_GenericFunction : INode_Expression {
	IList<Node_GenericParameter> m_parameters;
	Node_Function m_function;
	
	public Node_GenericFunction(
	IList<Node_GenericParameter> @parameters,
	Node_Function @function ) {
		m_parameters = @parameters;
		m_function = @function;
	}
	
	public IList<Node_GenericParameter> @parameters {
		get { return m_parameters; }
	}

	public Node_Function @function {
		get { return m_function; }
	}

	public string typeName {
		get { return "generic-function"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameters,
				m_function );
		}
	}
}

class Node_Enum : INode_Expression {
	Node_NullableType m_nullableType;
	IList<Node_EnumEntry> m_enumEntrys;
	
	public Node_Enum(
	Node_NullableType @nullableType,
	IList<Node_EnumEntry> @enumEntrys ) {
		m_nullableType = @nullableType;
		m_enumEntrys = @enumEntrys;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public IList<Node_EnumEntry> @enumEntrys {
		get { return m_enumEntrys; }
	}

	public string typeName {
		get { return "enum"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_enumEntrys );
		}
	}
}

class Node_Plane : INode {
	IList<INode_ScopeAlteration> m_alts;
	IList<Node_DeclareFirst> m_declareFirsts;
	
	public Node_Plane(
	IList<INode_ScopeAlteration> @alts,
	IList<Node_DeclareFirst> @declareFirsts ) {
		m_alts = @alts;
		m_declareFirsts = @declareFirsts;
	}
	
	public IList<INode_ScopeAlteration> @alts {
		get { return m_alts; }
	}

	public IList<Node_DeclareFirst> @declareFirsts {
		get { return m_declareFirsts; }
	}

	public string typeName {
		get { return "plane"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_alts,
				m_declareFirsts );
		}
	}
}

class Node_UnconditionalLoop : INode_Expression {
	INode_Expression m_body;
	
	public Node_UnconditionalLoop(
	INode_Expression @body ) {
		m_body = @body;
	}
	
	public INode_Expression @body {
		get { return m_body; }
	}

	public string typeName {
		get { return "unconditional-loop"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_body );
		}
	}
}

class Node_NullableType : INode {
	INode_Expression m_interface;
	Node_Boolean m_nullable;
	
	public Node_NullableType(
	INode_Expression @interface,
	Node_Boolean @nullable ) {
		m_interface = @interface;
		m_nullable = @nullable;
	}
	
	public INode_Expression @interface {
		get { return m_interface; }
	}

	public Node_Boolean @nullable {
		get { return m_nullable; }
	}

	public string typeName {
		get { return "nullable-type"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_interface,
				m_nullable );
		}
	}
}

class Node_EnumeratorLoop : INode_Expression {
	INode_Expression m_container;
	IList<Node_Receiver> m_receivers;
	INode_Expression m_test;
	INode_Expression m_body;
	
	public Node_EnumeratorLoop(
	INode_Expression @container,
	IList<Node_Receiver> @receivers,
	INode_Expression @test,
	INode_Expression @body ) {
		m_container = @container;
		m_receivers = @receivers;
		m_test = @test;
		m_body = @body;
	}
	
	public INode_Expression @container {
		get { return m_container; }
	}

	public IList<Node_Receiver> @receivers {
		get { return m_receivers; }
	}

	public INode_Expression @test {
		get { return m_test; }
	}

	public INode_Expression @body {
		get { return m_body; }
	}

	public string typeName {
		get { return "enumerator-loop"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_container,
				m_receivers,
				m_test,
				m_body );
		}
	}
}

class Node_Compound : INode_Expression {
	IList<INode_ScopeAlteration> m_alts;
	IList<INode_Expression> m_members;
	
	public Node_Compound(
	IList<INode_ScopeAlteration> @alts,
	IList<INode_Expression> @members ) {
		m_alts = @alts;
		m_members = @members;
	}
	
	public IList<INode_ScopeAlteration> @alts {
		get { return m_alts; }
	}

	public IList<INode_Expression> @members {
		get { return m_members; }
	}

	public string typeName {
		get { return "compound"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_alts,
				m_members );
		}
	}
}

class Node_Interface : INode_Expression {
	IList<INode_Expression> m_inheritees;
	IList<INode_InterfaceMember> m_members;
	
	public Node_Interface(
	IList<INode_Expression> @inheritees,
	IList<INode_InterfaceMember> @members ) {
		m_inheritees = @inheritees;
		m_members = @members;
	}
	
	public IList<INode_Expression> @inheritees {
		get { return m_inheritees; }
	}

	public IList<INode_InterfaceMember> @members {
		get { return m_members; }
	}

	public string typeName {
		get { return "interface"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_inheritees,
				m_members );
		}
	}
}

class Node_Using : INode_ScopeAlteration {
	IList<Node_Identifier> m_targets;
	Node_Identifier m_name;
	
	public Node_Using(
	IList<Node_Identifier> @targets,
	Node_Identifier @name ) {
		m_targets = @targets;
		m_name = @name;
	}
	
	public IList<Node_Identifier> @targets {
		get { return m_targets; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public string typeName {
		get { return "using"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_targets,
				m_name );
		}
	}
}

class Node_SetProperty : INode_Expression {
	INode_Expression m_source;
	Node_Identifier m_propertyName;
	INode_Expression m_value;
	
	public Node_SetProperty(
	INode_Expression @source,
	Node_Identifier @propertyName,
	INode_Expression @value ) {
		m_source = @source;
		m_propertyName = @propertyName;
		m_value = @value;
	}
	
	public INode_Expression @source {
		get { return m_source; }
	}

	public Node_Identifier @propertyName {
		get { return m_propertyName; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "set-property"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_source,
				m_propertyName,
				m_value );
		}
	}
}

class Node_ExtractMember : INode_Expression {
	INode_Expression m_source;
	Node_Identifier m_memberName;
	
	public Node_ExtractMember(
	INode_Expression @source,
	Node_Identifier @memberName ) {
		m_source = @source;
		m_memberName = @memberName;
	}
	
	public INode_Expression @source {
		get { return m_source; }
	}

	public Node_Identifier @memberName {
		get { return m_memberName; }
	}

	public string typeName {
		get { return "extract-member"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_source,
				m_memberName );
		}
	}
}

class Node_Throw : INode_Expression {
	INode_Expression m_value;
	
	public Node_Throw(
	INode_Expression @value ) {
		m_value = @value;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "throw"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value );
		}
	}
}

class Node_Nor : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	
	public Node_Nor(
	INode_Expression @first,
	INode_Expression @second ) {
		m_first = @first;
		m_second = @second;
	}
	
	public INode_Expression @first {
		get { return m_first; }
	}

	public INode_Expression @second {
		get { return m_second; }
	}

	public string typeName {
		get { return "nor"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_first,
				m_second );
		}
	}
}

class Node_IgnoreMember : INode {
	Node_String m_name;
	Node_Integer m_depth;
	
	public Node_IgnoreMember(
	Node_String @name,
	Node_Integer @depth ) {
		m_name = @name;
		m_depth = @depth;
	}
	
	public Node_String @name {
		get { return m_name; }
	}

	public Node_Integer @depth {
		get { return m_depth; }
	}

	public string typeName {
		get { return "ignore-member"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_depth );
		}
	}
}

class Node_Property : INode_InterfaceMember {
	Node_Identifier m_name;
	Node_Boolean m_writable;
	Node_NullableType m_nullableType;
	
	public Node_Property(
	Node_Identifier @name,
	Node_Boolean @writable,
	Node_NullableType @nullableType ) {
		m_name = @name;
		m_writable = @writable;
		m_nullableType = @nullableType;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Boolean @writable {
		get { return m_writable; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public string typeName {
		get { return "property"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_writable,
				m_nullableType );
		}
	}
}

class Node_Object : INode_Expression {
	IList<Node_Worker> m_workers;
	
	public Node_Object(
	IList<Node_Worker> @workers ) {
		m_workers = @workers;
	}
	
	public IList<Node_Worker> @workers {
		get { return m_workers; }
	}

	public string typeName {
		get { return "object"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_workers );
		}
	}
}

class Node_TryCatch : INode_Expression {
	INode_Expression m_try;
	IList<Node_ExceptionHandler> m_exceptionHandlers;
	INode_Expression m_else;
	INode_Expression m_finally;
	
	public Node_TryCatch(
	INode_Expression @try,
	IList<Node_ExceptionHandler> @exceptionHandlers,
	INode_Expression @else,
	INode_Expression @finally ) {
		m_try = @try;
		m_exceptionHandlers = @exceptionHandlers;
		m_else = @else;
		m_finally = @finally;
	}
	
	public INode_Expression @try {
		get { return m_try; }
	}

	public IList<Node_ExceptionHandler> @exceptionHandlers {
		get { return m_exceptionHandlers; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}

	public INode_Expression @finally {
		get { return m_finally; }
	}

	public string typeName {
		get { return "try-catch"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_try,
				m_exceptionHandlers,
				m_else,
				m_finally );
		}
	}
}

class Node_ParameterInfo : INode {
	Node_Direction m_direction;
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	Node_Boolean m_hasDefaultValue;
	
	public Node_ParameterInfo(
	Node_Direction @direction,
	Node_NullableType @nullableType,
	Node_Identifier @name,
	Node_Boolean @hasDefaultValue ) {
		m_direction = @direction;
		m_nullableType = @nullableType;
		m_name = @name;
		m_hasDefaultValue = @hasDefaultValue;
	}
	
	public Node_Direction @direction {
		get { return m_direction; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Boolean @hasDefaultValue {
		get { return m_hasDefaultValue; }
	}

	public string typeName {
		get { return "parameter-info"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_direction,
				m_nullableType,
				m_name,
				m_hasDefaultValue );
		}
	}
}

class Node_Caller : INode_Expression {
	INode_Expression m_interface;
	Node_Identifier m_methodName;
	
	public Node_Caller(
	INode_Expression @interface,
	Node_Identifier @methodName ) {
		m_interface = @interface;
		m_methodName = @methodName;
	}
	
	public INode_Expression @interface {
		get { return m_interface; }
	}

	public Node_Identifier @methodName {
		get { return m_methodName; }
	}

	public string typeName {
		get { return "caller"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_interface,
				m_methodName );
		}
	}
}

class Node_Yield : INode_Expression {
	INode_Expression m_value;
	
	public Node_Yield(
	INode_Expression @value ) {
		m_value = @value;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "yield"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value );
		}
	}
}

class Node_Or : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	
	public Node_Or(
	INode_Expression @first,
	INode_Expression @second ) {
		m_first = @first;
		m_second = @second;
	}
	
	public INode_Expression @first {
		get { return m_first; }
	}

	public INode_Expression @second {
		get { return m_second; }
	}

	public string typeName {
		get { return "or"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_first,
				m_second );
		}
	}
}

class Node_MemberImplementation : INode {
	Node_MemberIdentification m_memberIdentification;
	INode_Expression m_function;
	
	public Node_MemberImplementation(
	Node_MemberIdentification @memberIdentification,
	INode_Expression @function ) {
		m_memberIdentification = @memberIdentification;
		m_function = @function;
	}
	
	public Node_MemberIdentification @memberIdentification {
		get { return m_memberIdentification; }
	}

	public INode_Expression @function {
		get { return m_function; }
	}

	public string typeName {
		get { return "member-implementation"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_memberIdentification,
				m_function );
		}
	}
}

class Node_Labeled : INode_Expression {
	Node_Identifier m_label;
	INode_Expression m_child;
	
	public Node_Labeled(
	Node_Identifier @label,
	INode_Expression @child ) {
		m_label = @label;
		m_child = @child;
	}
	
	public Node_Identifier @label {
		get { return m_label; }
	}

	public INode_Expression @child {
		get { return m_child; }
	}

	public string typeName {
		get { return "labeled"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_label,
				m_child );
		}
	}
}

class Node_IdentikeyType : INode {
	Node_IdentikeyCategory m_identikeyCategory;
	Node_NullableType m_nullableType;
	
	public Node_IdentikeyType(
	Node_IdentikeyCategory @identikeyCategory,
	Node_NullableType @nullableType ) {
		m_identikeyCategory = @identikeyCategory;
		m_nullableType = @nullableType;
	}
	
	public Node_IdentikeyCategory @identikeyCategory {
		get { return m_identikeyCategory; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public string typeName {
		get { return "identikey-type"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_identikeyCategory,
				m_nullableType );
		}
	}
}

class Node_EnumEntry : INode {
	Node_Identifier m_name;
	INode_Expression m_value;
	
	public Node_EnumEntry(
	Node_Identifier @name,
	INode_Expression @value ) {
		m_name = @name;
		m_value = @value;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "enum-entry"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_value );
		}
	}
}

class Node_Receiver : INode {
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	
	public Node_Receiver(
	Node_NullableType @nullableType,
	Node_Identifier @name ) {
		m_nullableType = @nullableType;
		m_name = @name;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public string typeName {
		get { return "receiver"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_name );
		}
	}
}

class Node_Breeder : INode_InterfaceMember {
	INode_Expression m_interface;
	
	public Node_Breeder(
	INode_Expression @interface ) {
		m_interface = @interface;
	}
	
	public INode_Expression @interface {
		get { return m_interface; }
	}

	public string typeName {
		get { return "breeder"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_interface );
		}
	}
}

class Node_Curry : INode_Expression {
	INode_Expression m_function;
	IList<Node_Argument> m_arguments;
	Node_Boolean m_call;
	
	public Node_Curry(
	INode_Expression @function,
	IList<Node_Argument> @arguments,
	Node_Boolean @call ) {
		m_function = @function;
		m_arguments = @arguments;
		m_call = @call;
	}
	
	public INode_Expression @function {
		get { return m_function; }
	}

	public IList<Node_Argument> @arguments {
		get { return m_arguments; }
	}

	public Node_Boolean @call {
		get { return m_call; }
	}

	public string typeName {
		get { return "curry"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_function,
				m_arguments,
				m_call );
		}
	}
}

class Node_Assign : INode_Expression {
	Node_Identifier m_name;
	Node_Boolean m_breed;
	INode_Expression m_value;
	
	public Node_Assign(
	Node_Identifier @name,
	Node_Boolean @breed,
	INode_Expression @value ) {
		m_name = @name;
		m_breed = @breed;
		m_value = @value;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public string typeName {
		get { return "assign"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_breed,
				m_value );
		}
	}
}

