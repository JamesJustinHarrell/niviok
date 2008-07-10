//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;

namespace Acrid.NodeTypes {

public class Node_And : INode_Expression {
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

public class Node_DeclareFirst : INode_StatementDeclaration {
	Node_Identifier m_name;
	Node_Boolean m_overload;
	INode_Expression m_type;
	Node_Boolean m_breed;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_DeclareFirst(
	Node_Identifier @name,
	Node_Boolean @overload,
	INode_Expression @type,
	Node_Boolean @breed,
	INode_Expression @value,
	string @nodeSource ) {
		m_name = @name;
		m_overload = @overload;
		m_type = @type;
		m_breed = @breed;
		m_value = @value;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Boolean @overload {
		get { return m_overload; }
	}

	public INode_Expression @type {
		get { return m_type; }
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
				m_overload,
				m_type,
				m_breed,
				m_value );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_InstantiateGeneric : INode_Expression {
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

public class Node_Xnor : INode_Expression {
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

public class Node_Conditional : INode_Expression {
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

public class Node_Argument : INode {
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

public class Node_Module : INode {
	Node_Integer m_niviokMajorVersionNumber;
	Node_Integer m_niviokMinorVersionNumber;
	IList<Node_Import> m_imports;
	Node_Sieve m_sieve;
	string m_nodeSource;
	
	public Node_Module(
	Node_Integer @niviokMajorVersionNumber,
	Node_Integer @niviokMinorVersionNumber,
	IList<Node_Import> @imports,
	Node_Sieve @sieve,
	string @nodeSource ) {
		m_niviokMajorVersionNumber = @niviokMajorVersionNumber;
		m_niviokMinorVersionNumber = @niviokMinorVersionNumber;
		m_imports = @imports;
		m_sieve = @sieve;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Integer @niviokMajorVersionNumber {
		get { return m_niviokMajorVersionNumber; }
	}

	public Node_Integer @niviokMinorVersionNumber {
		get { return m_niviokMinorVersionNumber; }
	}

	public IList<Node_Import> @imports {
		get { return m_imports; }
	}

	public Node_Sieve @sieve {
		get { return m_sieve; }
	}

	public string typeName {
		get { return "module"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_niviokMajorVersionNumber,
				m_niviokMinorVersionNumber,
				m_imports,
				m_sieve );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Yield : INode_Expression {
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

public class Node_Property : INode_InterfaceMember {
	Node_Identifier m_name;
	Node_Boolean m_writable;
	INode_Expression m_type;
	string m_nodeSource;
	
	public Node_Property(
	Node_Identifier @name,
	Node_Boolean @writable,
	INode_Expression @type,
	string @nodeSource ) {
		m_name = @name;
		m_writable = @writable;
		m_type = @type;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Boolean @writable {
		get { return m_writable; }
	}

	public INode_Expression @type {
		get { return m_type; }
	}

	public string typeName {
		get { return "property"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_writable,
				m_type );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_ImportAttempt : INode {
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

public class Node_Select : INode_Expression {
	INode_Expression m_inputValue;
	IList<Node_Case> m_cases;
	INode_Expression m_else;
	string m_nodeSource;
	
	public Node_Select(
	INode_Expression @inputValue,
	IList<Node_Case> @cases,
	INode_Expression @else,
	string @nodeSource ) {
		m_inputValue = @inputValue;
		m_cases = @cases;
		m_else = @else;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @inputValue {
		get { return m_inputValue; }
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
				m_inputValue,
				m_cases,
				m_else );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_GenericInterface : INode_Expression {
	IList<Node_ParameterInfo> m_parameters;
	Node_Interface m_interface;
	string m_nodeSource;
	
	public Node_GenericInterface(
	IList<Node_ParameterInfo> @parameters,
	Node_Interface @interface,
	string @nodeSource ) {
		m_parameters = @parameters;
		m_interface = @interface;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_ParameterInfo> @parameters {
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

public class Node_ParameterImpl : INode {
	Node_Direction m_direction;
	INode_Expression m_type;
	Node_Identifier m_name;
	INode_Expression m_defaultValue;
	string m_nodeSource;
	
	public Node_ParameterImpl(
	Node_Direction @direction,
	INode_Expression @type,
	Node_Identifier @name,
	INode_Expression @defaultValue,
	string @nodeSource ) {
		m_direction = @direction;
		m_type = @type;
		m_name = @name;
		m_defaultValue = @defaultValue;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Direction @direction {
		get { return m_direction; }
	}

	public INode_Expression @type {
		get { return m_type; }
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
				m_type,
				m_name,
				m_defaultValue );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Xor : INode_Expression {
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

public class Node_Generator : INode_Expression {
	INode_Expression m_type;
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_Generator(
	INode_Expression @type,
	INode_Expression @body,
	string @nodeSource ) {
		m_type = @type;
		m_body = @body;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @type {
		get { return m_type; }
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
				m_type,
				m_body );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_GenericFunction : INode_Expression {
	IList<Node_ParameterInfo> m_parameters;
	Node_Function m_function;
	string m_nodeSource;
	
	public Node_GenericFunction(
	IList<Node_ParameterInfo> @parameters,
	Node_Function @function,
	string @nodeSource ) {
		m_parameters = @parameters;
		m_function = @function;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_ParameterInfo> @parameters {
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

public class Node_Breed : INode_Expression {
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

public class Node_Namespace : INode_StatementDeclaration {
	Node_Identifier m_name;
	Node_Sieve m_sieve;
	string m_nodeSource;
	
	public Node_Namespace(
	Node_Identifier @name,
	Node_Sieve @sieve,
	string @nodeSource ) {
		m_name = @name;
		m_sieve = @sieve;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Sieve @sieve {
		get { return m_sieve; }
	}

	public string typeName {
		get { return "namespace"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_sieve );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Nand : INode_Expression {
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

public class Node_Hidable : INode {
	Node_Boolean m_hidden;
	INode_StatementDeclaration m_declaration;
	string m_nodeSource;
	
	public Node_Hidable(
	Node_Boolean @hidden,
	INode_StatementDeclaration @declaration,
	string @nodeSource ) {
		m_hidden = @hidden;
		m_declaration = @declaration;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Boolean @hidden {
		get { return m_hidden; }
	}

	public INode_StatementDeclaration @declaration {
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

public class Node_Call : INode_Expression {
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

public class Node_Nor : INode_Expression {
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

public class Node_TypeCase : INode {
	IList<INode_Expression> m_testTypes;
	INode_Expression m_result;
	string m_nodeSource;
	
	public Node_TypeCase(
	IList<INode_Expression> @testTypes,
	INode_Expression @result,
	string @nodeSource ) {
		m_testTypes = @testTypes;
		m_result = @result;
		m_nodeSource = @nodeSource;
	}
	
	public IList<INode_Expression> @testTypes {
		get { return m_testTypes; }
	}

	public INode_Expression @result {
		get { return m_result; }
	}

	public string typeName {
		get { return "type-case"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_testTypes,
				m_result );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Import : INode {
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

public class Node_Method : INode_InterfaceMember {
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

public class Node_Function : INode_Expression {
	IList<Node_ParameterImpl> m_parameterImpls;
	INode_Expression m_returnType;
	INode_Expression m_body;
	string m_nodeSource;
	
	public Node_Function(
	IList<Node_ParameterImpl> @parameterImpls,
	INode_Expression @returnType,
	INode_Expression @body,
	string @nodeSource ) {
		m_parameterImpls = @parameterImpls;
		m_returnType = @returnType;
		m_body = @body;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_ParameterImpl> @parameterImpls {
		get { return m_parameterImpls; }
	}

	public INode_Expression @returnType {
		get { return m_returnType; }
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
				m_returnType,
				m_body );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_NamespacedWoScidentre : INode_Expression {
	IList<Node_Identifier> m_namespaces;
	Node_Identifier m_identikeyName;
	string m_nodeSource;
	
	public Node_NamespacedWoScidentre(
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
		get { return "namespaced-wo-scidentre"; }
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

public class Node_Object : INode_Expression {
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

public class Node_Curry : INode_Expression {
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

public class Node_Dictionary : INode_Expression {
	INode_Expression m_keyType;
	INode_Expression m_valueType;
	IList<Node_DictionaryEntry> m_dictionaryEntrys;
	string m_nodeSource;
	
	public Node_Dictionary(
	INode_Expression @keyType,
	INode_Expression @valueType,
	IList<Node_DictionaryEntry> @dictionaryEntrys,
	string @nodeSource ) {
		m_keyType = @keyType;
		m_valueType = @valueType;
		m_dictionaryEntrys = @dictionaryEntrys;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @keyType {
		get { return m_keyType; }
	}

	public INode_Expression @valueType {
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

public class Node_Callee : INode_InterfaceMember {
	IList<Node_ParameterInfo> m_parameterInfos;
	INode_Expression m_returnType;
	string m_nodeSource;
	
	public Node_Callee(
	IList<Node_ParameterInfo> @parameterInfos,
	INode_Expression @returnType,
	string @nodeSource ) {
		m_parameterInfos = @parameterInfos;
		m_returnType = @returnType;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_ParameterInfo> @parameterInfos {
		get { return m_parameterInfos; }
	}

	public INode_Expression @returnType {
		get { return m_returnType; }
	}

	public string typeName {
		get { return "callee"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameterInfos,
				m_returnType );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Expose : INode {
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

public class Node_DeclareEmpty : INode_Expression {
	Node_Identifier m_name;
	INode_Expression m_type;
	string m_nodeSource;
	
	public Node_DeclareEmpty(
	Node_Identifier @name,
	INode_Expression @type,
	string @nodeSource ) {
		m_name = @name;
		m_type = @type;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @type {
		get { return m_type; }
	}

	public string typeName {
		get { return "declare-empty"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_type );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Worker : INode {
	INode_Expression m_face;
	IList<Node_Worker> m_childWorkers;
	IList<Node_MemberImplementation> m_memberImplementations;
	string m_nodeSource;
	
	public Node_Worker(
	INode_Expression @face,
	IList<Node_Worker> @childWorkers,
	IList<Node_MemberImplementation> @memberImplementations,
	string @nodeSource ) {
		m_face = @face;
		m_childWorkers = @childWorkers;
		m_memberImplementations = @memberImplementations;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @face {
		get { return m_face; }
	}

	public IList<Node_Worker> @childWorkers {
		get { return m_childWorkers; }
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
				m_childWorkers,
				m_memberImplementations );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_FunctionInterface : INode_Expression {
	INode_Expression m_templateArgumentCount;
	IList<Node_ParameterInfo> m_parameterInfos;
	INode_Expression m_returnType;
	string m_nodeSource;
	
	public Node_FunctionInterface(
	INode_Expression @templateArgumentCount,
	IList<Node_ParameterInfo> @parameterInfos,
	INode_Expression @returnType,
	string @nodeSource ) {
		m_templateArgumentCount = @templateArgumentCount;
		m_parameterInfos = @parameterInfos;
		m_returnType = @returnType;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @templateArgumentCount {
		get { return m_templateArgumentCount; }
	}

	public IList<Node_ParameterInfo> @parameterInfos {
		get { return m_parameterInfos; }
	}

	public INode_Expression @returnType {
		get { return m_returnType; }
	}

	public string typeName {
		get { return "function-interface"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_templateArgumentCount,
				m_parameterInfos,
				m_returnType );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Enum : INode_Expression {
	INode_Expression m_type;
	IList<Node_EnumEntry> m_enumEntrys;
	string m_nodeSource;
	
	public Node_Enum(
	INode_Expression @type,
	IList<Node_EnumEntry> @enumEntrys,
	string @nodeSource ) {
		m_type = @type;
		m_enumEntrys = @enumEntrys;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @type {
		get { return m_type; }
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
				m_type,
				m_enumEntrys );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_DeclareAssign : INode_Expression {
	Node_Identifier m_name;
	Node_Boolean m_constant;
	INode_Expression m_type;
	Node_Boolean m_breed;
	INode_Expression m_value;
	string m_nodeSource;
	
	public Node_DeclareAssign(
	Node_Identifier @name,
	Node_Boolean @constant,
	INode_Expression @type,
	Node_Boolean @breed,
	INode_Expression @value,
	string @nodeSource ) {
		m_name = @name;
		m_constant = @constant;
		m_type = @type;
		m_breed = @breed;
		m_value = @value;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Boolean @constant {
		get { return m_constant; }
	}

	public INode_Expression @type {
		get { return m_type; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
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
				m_constant,
				m_type,
				m_breed,
				m_value );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Compound : INode_Expression {
	IList<Node_Expose> m_exposes;
	IList<Node_Using> m_usings;
	IList<INode_StatementDeclaration> m_declarations;
	IList<INode_Expression> m_members;
	string m_nodeSource;
	
	public Node_Compound(
	IList<Node_Expose> @exposes,
	IList<Node_Using> @usings,
	IList<INode_StatementDeclaration> @declarations,
	IList<INode_Expression> @members,
	string @nodeSource ) {
		m_exposes = @exposes;
		m_usings = @usings;
		m_declarations = @declarations;
		m_members = @members;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_Expose> @exposes {
		get { return m_exposes; }
	}

	public IList<Node_Using> @usings {
		get { return m_usings; }
	}

	public IList<INode_StatementDeclaration> @declarations {
		get { return m_declarations; }
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
				m_exposes,
				m_usings,
				m_declarations,
				m_members );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Interface : INode_Expression {
	IList<INode_Expression> m_inheritees;
	IList<Node_StatusedMember> m_members;
	string m_nodeSource;
	
	public Node_Interface(
	IList<INode_Expression> @inheritees,
	IList<Node_StatusedMember> @members,
	string @nodeSource ) {
		m_inheritees = @inheritees;
		m_members = @members;
		m_nodeSource = @nodeSource;
	}
	
	public IList<INode_Expression> @inheritees {
		get { return m_inheritees; }
	}

	public IList<Node_StatusedMember> @members {
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

public class Node_Using : INode {
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

public class Node_SetProperty : INode_Expression {
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

public class Node_Sieve : INode_StatementDeclaration {
	IList<Node_Expose> m_exposes;
	IList<Node_Using> m_usings;
	IList<Node_Hidable> m_hidables;
	string m_nodeSource;
	
	public Node_Sieve(
	IList<Node_Expose> @exposes,
	IList<Node_Using> @usings,
	IList<Node_Hidable> @hidables,
	string @nodeSource ) {
		m_exposes = @exposes;
		m_usings = @usings;
		m_hidables = @hidables;
		m_nodeSource = @nodeSource;
	}
	
	public IList<Node_Expose> @exposes {
		get { return m_exposes; }
	}

	public IList<Node_Using> @usings {
		get { return m_usings; }
	}

	public IList<Node_Hidable> @hidables {
		get { return m_hidables; }
	}

	public string typeName {
		get { return "sieve"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_exposes,
				m_usings,
				m_hidables );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_ExtractMember : INode_Expression {
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

public class Node_Throw : INode_Expression {
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

public class Node_TypeSelect : INode_Expression {
	INode_Expression m_inputValue;
	Node_Identifier m_castedName;
	Node_Boolean m_requireMatch;
	IList<Node_TypeCase> m_typeCases;
	INode_Expression m_else;
	string m_nodeSource;
	
	public Node_TypeSelect(
	INode_Expression @inputValue,
	Node_Identifier @castedName,
	Node_Boolean @requireMatch,
	IList<Node_TypeCase> @typeCases,
	INode_Expression @else,
	string @nodeSource ) {
		m_inputValue = @inputValue;
		m_castedName = @castedName;
		m_requireMatch = @requireMatch;
		m_typeCases = @typeCases;
		m_else = @else;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @inputValue {
		get { return m_inputValue; }
	}

	public Node_Identifier @castedName {
		get { return m_castedName; }
	}

	public Node_Boolean @requireMatch {
		get { return m_requireMatch; }
	}

	public IList<Node_TypeCase> @typeCases {
		get { return m_typeCases; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}

	public string typeName {
		get { return "type-select"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_inputValue,
				m_castedName,
				m_requireMatch,
				m_typeCases,
				m_else );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Case : INode {
	IList<INode_Expression> m_testValues;
	INode_Expression m_result;
	string m_nodeSource;
	
	public Node_Case(
	IList<INode_Expression> @testValues,
	INode_Expression @result,
	string @nodeSource ) {
		m_testValues = @testValues;
		m_result = @result;
		m_nodeSource = @nodeSource;
	}
	
	public IList<INode_Expression> @testValues {
		get { return m_testValues; }
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
				m_testValues,
				m_result );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_DictionaryEntry : INode {
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

public class Node_Catcher : INode {
	INode_Expression m_type;
	Node_Identifier m_name;
	INode_Expression m_test;
	INode_Expression m_result;
	string m_nodeSource;
	
	public Node_Catcher(
	INode_Expression @type,
	Node_Identifier @name,
	INode_Expression @test,
	INode_Expression @result,
	string @nodeSource ) {
		m_type = @type;
		m_name = @name;
		m_test = @test;
		m_result = @result;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @type {
		get { return m_type; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @test {
		get { return m_test; }
	}

	public INode_Expression @result {
		get { return m_result; }
	}

	public string typeName {
		get { return "catcher"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_type,
				m_name,
				m_test,
				m_result );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_TryCatch : INode_Expression {
	INode_Expression m_try;
	IList<Node_Catcher> m_catchers;
	INode_Expression m_onSuccess;
	INode_Expression m_finally;
	string m_nodeSource;
	
	public Node_TryCatch(
	INode_Expression @try,
	IList<Node_Catcher> @catchers,
	INode_Expression @onSuccess,
	INode_Expression @finally,
	string @nodeSource ) {
		m_try = @try;
		m_catchers = @catchers;
		m_onSuccess = @onSuccess;
		m_finally = @finally;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @try {
		get { return m_try; }
	}

	public IList<Node_Catcher> @catchers {
		get { return m_catchers; }
	}

	public INode_Expression @onSuccess {
		get { return m_onSuccess; }
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
				m_catchers,
				m_onSuccess,
				m_finally );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_ParameterInfo : INode {
	Node_Direction m_direction;
	INode_Expression m_type;
	Node_Identifier m_name;
	Node_Boolean m_hasDefaultValue;
	string m_nodeSource;
	
	public Node_ParameterInfo(
	Node_Direction @direction,
	INode_Expression @type,
	Node_Identifier @name,
	Node_Boolean @hasDefaultValue,
	string @nodeSource ) {
		m_direction = @direction;
		m_type = @type;
		m_name = @name;
		m_hasDefaultValue = @hasDefaultValue;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Direction @direction {
		get { return m_direction; }
	}

	public INode_Expression @type {
		get { return m_type; }
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
				m_type,
				m_name,
				m_hasDefaultValue );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Caller : INode_Expression {
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

public class Node_Remit : INode_Expression {
	Node_Identifier m_label;
	string m_nodeSource;
	
	public Node_Remit(
	Node_Identifier @label,
	string @nodeSource ) {
		m_label = @label;
		m_nodeSource = @nodeSource;
	}
	
	public Node_Identifier @label {
		get { return m_label; }
	}

	public string typeName {
		get { return "remit"; }
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

public class Node_Or : INode_Expression {
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

public class Node_MemberImplementation : INode {
	Node_MemberType m_memberType;
	Node_Identifier m_name;
	INode_Expression m_interface;
	INode_Expression m_function;
	string m_nodeSource;
	
	public Node_MemberImplementation(
	Node_MemberType @memberType,
	Node_Identifier @name,
	INode_Expression @interface,
	INode_Expression @function,
	string @nodeSource ) {
		m_memberType = @memberType;
		m_name = @name;
		m_interface = @interface;
		m_function = @function;
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

	public INode_Expression @function {
		get { return m_function; }
	}

	public string typeName {
		get { return "member-implementation"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_memberType,
				m_name,
				m_interface,
				m_function );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_StatusedMember : INode {
	Node_MemberStatus m_memberStatus;
	INode_InterfaceMember m_member;
	string m_nodeSource;
	
	public Node_StatusedMember(
	Node_MemberStatus @memberStatus,
	INode_InterfaceMember @member,
	string @nodeSource ) {
		m_memberStatus = @memberStatus;
		m_member = @member;
		m_nodeSource = @nodeSource;
	}
	
	public Node_MemberStatus @memberStatus {
		get { return m_memberStatus; }
	}

	public INode_InterfaceMember @member {
		get { return m_member; }
	}

	public string typeName {
		get { return "statused-member"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_memberStatus,
				m_member );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_EnumEntry : INode {
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

public class Node_Breeder : INode_InterfaceMember {
	INode_Expression m_type;
	string m_nodeSource;
	
	public Node_Breeder(
	INode_Expression @type,
	string @nodeSource ) {
		m_type = @type;
		m_nodeSource = @nodeSource;
	}
	
	public INode_Expression @type {
		get { return m_type; }
	}

	public string typeName {
		get { return "breeder"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_type );
		}
	}
	
	public string @nodeSource {
		get { return m_nodeSource; }
	}
}

public class Node_Assign : INode_Expression {
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

} //namespace

