
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;

class Node_And : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	string m_nodeSource;
	
	public Node_And(
	INode_Expression @first,
	INode_Expression @second,
	string @nodeSource ) {
		m_first = @first;
		m_second = @second;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Function : INode_Expression {
	IList<Node_ParameterImpl> m_parameterImpls;
	Node_NullableType m_returnInfo;
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_Function(
	IList<Node_ParameterImpl> @parameterImpls,
	Node_NullableType @returnInfo,
	INode_Expression @body,
	string @nodeSource ) {
		m_parameterImpls = @parameterImpls;
		m_returnInfo = @returnInfo;
		m_body = @body;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_DeclareFirst : INode_IdentikeySpecialNew {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	Node_Boolean m_breed;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_DeclareFirst(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	Node_Boolean @breed,
	INode_Expression @value,
	string @nodeSource ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_breed = @breed;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_InstantiateGeneric : INode_Expression {
	INode_Expression m_generic;
	IList<Node_Argument> m_arguments;
	string m_nodeSource;
	
	public Node_InstantiateGeneric(
	INode_Expression @generic,
	IList<Node_Argument> @arguments,
	string @nodeSource ) {
		m_generic = @generic;
		m_arguments = @arguments;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_GenericParameter : INode {
	Node_Identifier m_name;
	INode_Expression m_defaultInterface;
	string m_nodeSource;
	
	public Node_GenericParameter(
	Node_Identifier @name,
	INode_Expression @defaultInterface,
	string @nodeSource ) {
		m_name = @name;
		m_defaultInterface = @defaultInterface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Xnor : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	string m_nodeSource;
	
	public Node_Xnor(
	INode_Expression @first,
	INode_Expression @second,
	string @nodeSource ) {
		m_first = @first;
		m_second = @second;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Conditional : INode_Expression {
	INode_Expression m_test;
	INode_Expression m_result;
	INode_Expression m_else;
	string m_nodeSource;
	
	public Node_Conditional(
	INode_Expression @test,
	INode_Expression @result,
	INode_Expression @else,
	string @nodeSource ) {
		m_test = @test;
		m_result = @result;
		m_else = @else;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_GenericFunction : INode_Expression {
	IList<Node_GenericParameter> m_parameters;
	Node_Function m_function;
	string m_nodeSource;
	
	public Node_GenericFunction(
	IList<Node_GenericParameter> @parameters,
	Node_Function @function,
	string @nodeSource ) {
		m_parameters = @parameters;
		m_function = @function;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Argument : INode {
	Node_Identifier m_parameterName;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_Argument(
	Node_Identifier @parameterName,
	INode_Expression @value,
	string @nodeSource ) {
		m_parameterName = @parameterName;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Module : INode {
	IList<Node_Import> m_imports;
	Node_LimitOld m_limitOld;
	string m_nodeSource;
	
	public Node_Module(
	IList<Node_Import> @imports,
	Node_LimitOld @limitOld,
	string @nodeSource ) {
		m_imports = @imports;
		m_limitOld = @limitOld;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_Import> @imports {
		get { return m_imports; }
	}

	public Node_LimitOld @limitOld {
		get { return m_limitOld; }
	}

	public string typeName {
		get { return "module"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_imports,
				m_limitOld );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_ConditionalLoop : INode_Expression {
	INode_Expression m_test;
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_ConditionalLoop(
	INode_Expression @test,
	INode_Expression @body,
	string @nodeSource ) {
		m_test = @test;
		m_body = @body;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_DictionaryEntry : INode {
	INode_Expression m_key;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_DictionaryEntry(
	INode_Expression @key,
	INode_Expression @value,
	string @nodeSource ) {
		m_key = @key;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_ImportAttempt : INode {
	Node_String m_scheme;
	Node_String m_body;
	string m_nodeSource;
	
	public Node_ImportAttempt(
	Node_String @scheme,
	Node_String @body,
	string @nodeSource ) {
		m_scheme = @scheme;
		m_body = @body;
		m_nodeSource = @nodeSource;
	}
	
	public Node_String @scheme {
		get { return m_scheme; }
	}

	public Node_String @body {
		get { return m_body; }
	}

	public string typeName {
		get { return "import-attempt"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_scheme,
				m_body );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Select : INode_Expression {
	INode_Expression m_value;
	IList<Node_Case> m_cases;
	INode_Expression m_else;
	string m_nodeSource;
	
	public Node_Select(
	INode_Expression @value,
	IList<Node_Case> @cases,
	INode_Expression @else,
	string @nodeSource ) {
		m_value = @value;
		m_cases = @cases;
		m_else = @else;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_GenericInterface : INode_Expression {
	IList<Node_GenericParameter> m_parameters;
	Node_Interface m_interface;
	string m_nodeSource;
	
	public Node_GenericInterface(
	IList<Node_GenericParameter> @parameters,
	Node_Interface @interface,
	string @nodeSource ) {
		m_parameters = @parameters;
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Ignore : INode_Expression {
	INode_Expression m_content;
	IList<Node_IgnoreMember> m_ignoreMembers;
	string m_nodeSource;
	
	public Node_Ignore(
	INode_Expression @content,
	IList<Node_IgnoreMember> @ignoreMembers,
	string @nodeSource ) {
		m_content = @content;
		m_ignoreMembers = @ignoreMembers;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_ParameterImpl : INode {
	Node_Direction m_direction;
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	INode_Expression m_defaultValue;
	string m_nodeSource;
	
	public Node_ParameterImpl(
	Node_Direction @direction,
	Node_NullableType @nullableType,
	Node_Identifier @name,
	INode_Expression @defaultValue,
	string @nodeSource ) {
		m_direction = @direction;
		m_nullableType = @nullableType;
		m_name = @name;
		m_defaultValue = @defaultValue;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Null : INode_Expression {
	INode_Expression m_interface;
	string m_nodeSource;
	
	public Node_Null(
	INode_Expression @interface,
	string @nodeSource ) {
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_MemberIdentification : INode {
	Node_MemberType m_memberType;
	Node_Identifier m_name;
	INode_Expression m_interface;
	string m_nodeSource;
	
	public Node_MemberIdentification(
	Node_MemberType @memberType,
	Node_Identifier @name,
	INode_Expression @interface,
	string @nodeSource ) {
		m_memberType = @memberType;
		m_name = @name;
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Generator : INode_Expression {
	Node_NullableType m_nullableType;
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_Generator(
	Node_NullableType @nullableType,
	INode_Expression @body,
	string @nodeSource ) {
		m_nullableType = @nullableType;
		m_body = @body;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Cast : INode_Expression {
	INode_Expression m_source;
	Node_NullableType m_nullableType;
	string m_nodeSource;
	
	public Node_Cast(
	INode_Expression @source,
	Node_NullableType @nullableType,
	string @nodeSource ) {
		m_source = @source;
		m_nullableType = @nullableType;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_ExceptionHandler : INode {
	Node_Boolean m_catch;
	INode_Expression m_interface;
	Node_Identifier m_name;
	INode_Expression m_result;
	string m_nodeSource;
	
	public Node_ExceptionHandler(
	Node_Boolean @catch,
	INode_Expression @interface,
	Node_Identifier @name,
	INode_Expression @result,
	string @nodeSource ) {
		m_catch = @catch;
		m_interface = @interface;
		m_name = @name;
		m_result = @result;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Breed : INode_Expression {
	INode_Expression m_parent;
	INode_Expression m_interface;
	string m_nodeSource;
	
	public Node_Breed(
	INode_Expression @parent,
	INode_Expression @interface,
	string @nodeSource ) {
		m_parent = @parent;
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Namespace : INode_IdentikeySpecialNew {
	Node_Identifier m_name;
	Node_LimitOld m_limitOld;
	string m_nodeSource;
	
	public Node_Namespace(
	Node_Identifier @name,
	Node_LimitOld @limitOld,
	string @nodeSource ) {
		m_name = @name;
		m_limitOld = @limitOld;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_LimitOld @limitOld {
		get { return m_limitOld; }
	}

	public string typeName {
		get { return "namespace"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_limitOld );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Labeled : INode_Expression {
	Node_Identifier m_label;
	INode_Expression m_child;
	string m_nodeSource;
	
	public Node_Labeled(
	Node_Identifier @label,
	INode_Expression @child,
	string @nodeSource ) {
		m_label = @label;
		m_child = @child;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Hidable : INode {
	Node_Boolean m_hidden;
	INode_IdentikeySpecialNew m_declaration;
	string m_nodeSource;
	
	public Node_Hidable(
	Node_Boolean @hidden,
	INode_IdentikeySpecialNew @declaration,
	string @nodeSource ) {
		m_hidden = @hidden;
		m_declaration = @declaration;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Boolean @hidden {
		get { return m_hidden; }
	}

	public INode_IdentikeySpecialNew @declaration {
		get { return m_declaration; }
	}

	public string typeName {
		get { return "hidable"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_hidden,
				m_declaration );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Call : INode_Expression {
	INode_Expression m_receiver;
	IList<Node_Argument> m_arguments;
	string m_nodeSource;
	
	public Node_Call(
	INode_Expression @receiver,
	IList<Node_Argument> @arguments,
	string @nodeSource ) {
		m_receiver = @receiver;
		m_arguments = @arguments;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Implements : INode_Expression {
	INode_Expression m_value;
	INode_Expression m_interface;
	string m_nodeSource;
	
	public Node_Implements(
	INode_Expression @value,
	INode_Expression @interface,
	string @nodeSource ) {
		m_value = @value;
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_FunctionInterface : INode_Expression {
	INode_Expression m_templateArgumentCount;
	IList<Node_ParameterInfo> m_parameterInfos;
	Node_NullableType m_returnInfo;
	string m_nodeSource;
	
	public Node_FunctionInterface(
	INode_Expression @templateArgumentCount,
	IList<Node_ParameterInfo> @parameterInfos,
	Node_NullableType @returnInfo,
	string @nodeSource ) {
		m_templateArgumentCount = @templateArgumentCount;
		m_parameterInfos = @parameterInfos;
		m_returnInfo = @returnInfo;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Import : INode {
	Node_Identifier m_alias;
	IList<Node_ImportAttempt> m_importAttempts;
	string m_nodeSource;
	
	public Node_Import(
	Node_Identifier @alias,
	IList<Node_ImportAttempt> @importAttempts,
	string @nodeSource ) {
		m_alias = @alias;
		m_importAttempts = @importAttempts;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @alias {
		get { return m_alias; }
	}

	public IList<Node_ImportAttempt> @importAttempts {
		get { return m_importAttempts; }
	}

	public string typeName {
		get { return "import"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_alias,
				m_importAttempts );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Method : INode_InterfaceMember {
	Node_Identifier m_name;
	INode_Expression m_interface;
	string m_nodeSource;
	
	public Node_Method(
	Node_Identifier @name,
	INode_Expression @interface,
	string @nodeSource ) {
		m_name = @name;
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_NamespacedValueIdentikey : INode_Expression {
	IList<Node_Identifier> m_namespaces;
	Node_Identifier m_identikeyName;
	string m_nodeSource;
	
	public Node_NamespacedValueIdentikey(
	IList<Node_Identifier> @namespaces,
	Node_Identifier @identikeyName,
	string @nodeSource ) {
		m_namespaces = @namespaces;
		m_identikeyName = @identikeyName;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Case : INode {
	IList<INode_Expression> m_values;
	INode_Expression m_result;
	string m_nodeSource;
	
	public Node_Case(
	IList<INode_Expression> @values,
	INode_Expression @result,
	string @nodeSource ) {
		m_values = @values;
		m_result = @result;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Xor : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	string m_nodeSource;
	
	public Node_Xor(
	INode_Expression @first,
	INode_Expression @second,
	string @nodeSource ) {
		m_first = @first;
		m_second = @second;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Return : INode_Expression {
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_Return(
	INode_Expression @value,
	string @nodeSource ) {
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Continue : INode_Expression {
	Node_Identifier m_label;
	string m_nodeSource;
	
	public Node_Continue(
	Node_Identifier @label,
	string @nodeSource ) {
		m_label = @label;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Dictionary : INode_Expression {
	Node_NullableType m_keyType;
	Node_NullableType m_valueType;
	IList<Node_DictionaryEntry> m_dictionaryEntrys;
	string m_nodeSource;
	
	public Node_Dictionary(
	Node_NullableType @keyType,
	Node_NullableType @valueType,
	IList<Node_DictionaryEntry> @dictionaryEntrys,
	string @nodeSource ) {
		m_keyType = @keyType;
		m_valueType = @valueType;
		m_dictionaryEntrys = @dictionaryEntrys;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Callee : INode_InterfaceMember {
	IList<Node_ParameterInfo> m_parameterInfos;
	Node_NullableType m_returnInfo;
	string m_nodeSource;
	
	public Node_Callee(
	IList<Node_ParameterInfo> @parameterInfos,
	Node_NullableType @returnInfo,
	string @nodeSource ) {
		m_parameterInfos = @parameterInfos;
		m_returnInfo = @returnInfo;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Expose : INode_IdentikeySpecialOld {
	IList<Node_Identifier> m_identifiers;
	string m_nodeSource;
	
	public Node_Expose(
	IList<Node_Identifier> @identifiers,
	string @nodeSource ) {
		m_identifiers = @identifiers;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_DeclareEmpty : INode_Expression {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	string m_nodeSource;
	
	public Node_DeclareEmpty(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	string @nodeSource ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Worker : INode {
	INode_Expression m_face;
	IList<Node_Worker> m_childs;
	IList<Node_MemberImplementation> m_memberImplementations;
	string m_nodeSource;
	
	public Node_Worker(
	INode_Expression @face,
	IList<Node_Worker> @childs,
	IList<Node_MemberImplementation> @memberImplementations,
	string @nodeSource ) {
		m_face = @face;
		m_childs = @childs;
		m_memberImplementations = @memberImplementations;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Break : INode_Expression {
	Node_Identifier m_label;
	string m_nodeSource;
	
	public Node_Break(
	Node_Identifier @label,
	string @nodeSource ) {
		m_label = @label;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Nand : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	string m_nodeSource;
	
	public Node_Nand(
	INode_Expression @first,
	INode_Expression @second,
	string @nodeSource ) {
		m_first = @first;
		m_second = @second;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Enum : INode_Expression {
	Node_NullableType m_nullableType;
	IList<Node_EnumEntry> m_enumEntrys;
	string m_nodeSource;
	
	public Node_Enum(
	Node_NullableType @nullableType,
	IList<Node_EnumEntry> @enumEntrys,
	string @nodeSource ) {
		m_nullableType = @nullableType;
		m_enumEntrys = @enumEntrys;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Curry : INode_Expression {
	INode_Expression m_function;
	IList<Node_Argument> m_arguments;
	Node_Boolean m_call;
	string m_nodeSource;
	
	public Node_Curry(
	INode_Expression @function,
	IList<Node_Argument> @arguments,
	Node_Boolean @call,
	string @nodeSource ) {
		m_function = @function;
		m_arguments = @arguments;
		m_call = @call;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_LimitOld : INode_IdentikeySpecialNew {
	IList<INode_IdentikeySpecialOld> m_declarations;
	IList<Node_Hidable> m_hidables;
	string m_nodeSource;
	
	public Node_LimitOld(
	IList<INode_IdentikeySpecialOld> @declarations,
	IList<Node_Hidable> @hidables,
	string @nodeSource ) {
		m_declarations = @declarations;
		m_hidables = @hidables;
		m_nodeSource = @nodeSource;
	}
	
	public IList<INode_IdentikeySpecialOld> @declarations {
		get { return m_declarations; }
	}

	public IList<Node_Hidable> @hidables {
		get { return m_hidables; }
	}

	public string typeName {
		get { return "limit-old"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_declarations,
				m_hidables );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_NullableType : INode {
	INode_Expression m_interface;
	Node_Boolean m_nullable;
	string m_nodeSource;
	
	public Node_NullableType(
	INode_Expression @interface,
	Node_Boolean @nullable,
	string @nodeSource ) {
		m_interface = @interface;
		m_nullable = @nullable;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_EnumeratorLoop : INode_Expression {
	INode_Expression m_container;
	IList<Node_Receiver> m_receivers;
	INode_Expression m_test;
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_EnumeratorLoop(
	INode_Expression @container,
	IList<Node_Receiver> @receivers,
	INode_Expression @test,
	INode_Expression @body,
	string @nodeSource ) {
		m_container = @container;
		m_receivers = @receivers;
		m_test = @test;
		m_body = @body;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Compound : INode_Expression {
	IList<INode_IdentikeySpecialOld> m_oldDeclarations;
	IList<INode_IdentikeySpecialNew> m_newDeclarations;
	IList<INode_Expression> m_members;
	string m_nodeSource;
	
	public Node_Compound(
	IList<INode_IdentikeySpecialOld> @oldDeclarations,
	IList<INode_IdentikeySpecialNew> @newDeclarations,
	IList<INode_Expression> @members,
	string @nodeSource ) {
		m_oldDeclarations = @oldDeclarations;
		m_newDeclarations = @newDeclarations;
		m_members = @members;
		m_nodeSource = @nodeSource;
	}
	
	public IList<INode_IdentikeySpecialOld> @oldDeclarations {
		get { return m_oldDeclarations; }
	}

	public IList<INode_IdentikeySpecialNew> @newDeclarations {
		get { return m_newDeclarations; }
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
				m_oldDeclarations,
				m_newDeclarations,
				m_members );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Interface : INode_Expression {
	IList<INode_Expression> m_inheritees;
	IList<INode_InterfaceMember> m_members;
	string m_nodeSource;
	
	public Node_Interface(
	IList<INode_Expression> @inheritees,
	IList<INode_InterfaceMember> @members,
	string @nodeSource ) {
		m_inheritees = @inheritees;
		m_members = @members;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Using : INode_IdentikeySpecialOld {
	IList<Node_Identifier> m_targets;
	Node_Identifier m_name;
	string m_nodeSource;
	
	public Node_Using(
	IList<Node_Identifier> @targets,
	Node_Identifier @name,
	string @nodeSource ) {
		m_targets = @targets;
		m_name = @name;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_SetProperty : INode_Expression {
	INode_Expression m_source;
	Node_Identifier m_propertyName;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_SetProperty(
	INode_Expression @source,
	Node_Identifier @propertyName,
	INode_Expression @value,
	string @nodeSource ) {
		m_source = @source;
		m_propertyName = @propertyName;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_ExtractMember : INode_Expression {
	INode_Expression m_source;
	Node_Identifier m_memberName;
	string m_nodeSource;
	
	public Node_ExtractMember(
	INode_Expression @source,
	Node_Identifier @memberName,
	string @nodeSource ) {
		m_source = @source;
		m_memberName = @memberName;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Throw : INode_Expression {
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_Throw(
	INode_Expression @value,
	string @nodeSource ) {
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Nor : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	string m_nodeSource;
	
	public Node_Nor(
	INode_Expression @first,
	INode_Expression @second,
	string @nodeSource ) {
		m_first = @first;
		m_second = @second;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_IgnoreMember : INode {
	Node_String m_name;
	Node_Integer m_depth;
	string m_nodeSource;
	
	public Node_IgnoreMember(
	Node_String @name,
	Node_Integer @depth,
	string @nodeSource ) {
		m_name = @name;
		m_depth = @depth;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Property : INode_InterfaceMember {
	Node_Identifier m_name;
	Node_Boolean m_writable;
	Node_NullableType m_nullableType;
	string m_nodeSource;
	
	public Node_Property(
	Node_Identifier @name,
	Node_Boolean @writable,
	Node_NullableType @nullableType,
	string @nodeSource ) {
		m_name = @name;
		m_writable = @writable;
		m_nullableType = @nullableType;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Object : INode_Expression {
	IList<Node_Worker> m_workers;
	string m_nodeSource;
	
	public Node_Object(
	IList<Node_Worker> @workers,
	string @nodeSource ) {
		m_workers = @workers;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_TryCatch : INode_Expression {
	INode_Expression m_try;
	IList<Node_ExceptionHandler> m_exceptionHandlers;
	INode_Expression m_else;
	INode_Expression m_finally;
	string m_nodeSource;
	
	public Node_TryCatch(
	INode_Expression @try,
	IList<Node_ExceptionHandler> @exceptionHandlers,
	INode_Expression @else,
	INode_Expression @finally,
	string @nodeSource ) {
		m_try = @try;
		m_exceptionHandlers = @exceptionHandlers;
		m_else = @else;
		m_finally = @finally;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_ParameterInfo : INode {
	Node_Direction m_direction;
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	Node_Boolean m_hasDefaultValue;
	string m_nodeSource;
	
	public Node_ParameterInfo(
	Node_Direction @direction,
	Node_NullableType @nullableType,
	Node_Identifier @name,
	Node_Boolean @hasDefaultValue,
	string @nodeSource ) {
		m_direction = @direction;
		m_nullableType = @nullableType;
		m_name = @name;
		m_hasDefaultValue = @hasDefaultValue;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Caller : INode_Expression {
	INode_Expression m_interface;
	Node_Identifier m_methodName;
	string m_nodeSource;
	
	public Node_Caller(
	INode_Expression @interface,
	Node_Identifier @methodName,
	string @nodeSource ) {
		m_interface = @interface;
		m_methodName = @methodName;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Yield : INode_Expression {
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_Yield(
	INode_Expression @value,
	string @nodeSource ) {
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Or : INode_Expression {
	INode_Expression m_first;
	INode_Expression m_second;
	string m_nodeSource;
	
	public Node_Or(
	INode_Expression @first,
	INode_Expression @second,
	string @nodeSource ) {
		m_first = @first;
		m_second = @second;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_MemberImplementation : INode {
	Node_MemberIdentification m_memberIdentification;
	INode_Expression m_function;
	string m_nodeSource;
	
	public Node_MemberImplementation(
	Node_MemberIdentification @memberIdentification,
	INode_Expression @function,
	string @nodeSource ) {
		m_memberIdentification = @memberIdentification;
		m_function = @function;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_DeclareAssign : INode_Expression {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	Node_Boolean m_breed;
	Node_Boolean m_inferInterface;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_DeclareAssign(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	Node_Boolean @breed,
	Node_Boolean @inferInterface,
	INode_Expression @value,
	string @nodeSource ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_breed = @breed;
		m_inferInterface = @inferInterface;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_IdentikeyType : INode {
	Node_IdentikeyCategory m_identikeyCategory;
	Node_NullableType m_nullableType;
	string m_nodeSource;
	
	public Node_IdentikeyType(
	Node_IdentikeyCategory @identikeyCategory,
	Node_NullableType @nullableType,
	string @nodeSource ) {
		m_identikeyCategory = @identikeyCategory;
		m_nullableType = @nullableType;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_EnumEntry : INode {
	Node_Identifier m_name;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_EnumEntry(
	Node_Identifier @name,
	INode_Expression @value,
	string @nodeSource ) {
		m_name = @name;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Receiver : INode {
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	string m_nodeSource;
	
	public Node_Receiver(
	Node_NullableType @nullableType,
	Node_Identifier @name,
	string @nodeSource ) {
		m_nullableType = @nullableType;
		m_name = @name;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Breeder : INode_InterfaceMember {
	INode_Expression m_interface;
	string m_nodeSource;
	
	public Node_Breeder(
	INode_Expression @interface,
	string @nodeSource ) {
		m_interface = @interface;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_UnconditionalLoop : INode_Expression {
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_UnconditionalLoop(
	INode_Expression @body,
	string @nodeSource ) {
		m_body = @body;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

class Node_Assign : INode_Expression {
	Node_Identifier m_name;
	Node_Boolean m_breed;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_Assign(
	Node_Identifier @name,
	Node_Boolean @breed,
	INode_Expression @value,
	string @nodeSource ) {
		m_name = @name;
		m_breed = @breed;
		m_value = @value;
		m_nodeSource = @nodeSource;
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
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

