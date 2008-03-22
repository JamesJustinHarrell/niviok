
//This file was generated programmatically, so
//don't edit this file directly.

using System.Collections.Generic;

class Node_Possibility : INode_Expression {
	INode_Expression m_test;
	INode_Expression m_result;
	
	public Node_Possibility(
	INode_Expression @test,
	INode_Expression @result ) {
		m_test = @test;
		m_result = @result;
	}
	
	public INode_Expression @test {
		get { return m_test; }
	}

	public INode_Expression @result {
		get { return m_result; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "possibility"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_test,
				m_result );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_DeclareFirst : INode_Declaration {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	INode_Expression m_value;
	Node_Boolean m_breed;
	
	public Node_DeclareFirst(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	INode_Expression @value,
	Node_Boolean @breed ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_value = @value;
		m_breed = @breed;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_IdentikeyType @identikeyType {
		get { return m_identikeyType; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "declare-first"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_identikeyType,
				m_value,
				m_breed );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Unassign : INode_Expression {
	Node_Identifier m_identifier;
	
	public Node_Unassign(
	Node_Identifier @identifier ) {
		m_identifier = @identifier;
	}
	
	public Node_Identifier @identifier {
		get { return m_identifier; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "unassign"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_identifier );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Chain : INode_Expression {
	Node_NullableType m_nullableType;
	IList<INode_Expression> m_element;
	
	public Node_Chain(
	Node_NullableType @nullableType,
	IList<INode_Expression> @element ) {
		m_nullableType = @nullableType;
		m_element = @element;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public IList<INode_Expression> @element {
		get { return m_element; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "chain"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_element );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_DeclareConstEmpty : INode_Declaration {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	
	public Node_DeclareConstEmpty(
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "declare-const-empty"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_identikeyType );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Conditional : INode_Expression {
	IList<Node_Possibility> m_possibility;
	INode_Expression m_else;
	
	public Node_Conditional(
	IList<Node_Possibility> @possibility,
	INode_Expression @else ) {
		m_possibility = @possibility;
		m_else = @else;
	}
	
	public IList<Node_Possibility> @possibility {
		get { return m_possibility; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "conditional"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_possibility,
				m_else );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_DeclareAssign : INode_Declaration {
	Node_Identifier m_name;
	Node_IdentikeyType m_identikeyType;
	INode_Expression m_value;
	Node_Boolean m_breed;
	
	public Node_DeclareAssign(
	Node_Identifier @name,
	Node_IdentikeyType @identikeyType,
	INode_Expression @value,
	Node_Boolean @breed ) {
		m_name = @name;
		m_identikeyType = @identikeyType;
		m_value = @value;
		m_breed = @breed;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_IdentikeyType @identikeyType {
		get { return m_identikeyType; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "declare-assign"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_identikeyType,
				m_value,
				m_breed );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_StaticMember : INode {
	INode_Declaration m_declaration;
	Node_Access m_access;
	
	public Node_StaticMember(
	INode_Declaration @declaration,
	Node_Access @access ) {
		m_declaration = @declaration;
		m_access = @access;
	}
	
	public INode_Declaration @declaration {
		get { return m_declaration; }
	}

	public Node_Access @access {
		get { return m_access; }
	}
	
	public string typeName {
		get { return "static-member"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_declaration,
				m_access );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_ForManual : INode_Expression {
	IList<INode_Expression> m_initializers;
	INode_Expression m_test;
	IList<INode_Expression> m_postActions;
	Node_Block m_action;
	
	public Node_ForManual(
	IList<INode_Expression> @initializers,
	INode_Expression @test,
	IList<INode_Expression> @postActions,
	Node_Block @action ) {
		m_initializers = @initializers;
		m_test = @test;
		m_postActions = @postActions;
		m_action = @action;
	}
	
	public IList<INode_Expression> @initializers {
		get { return m_initializers; }
	}

	public INode_Expression @test {
		get { return m_test; }
	}

	public IList<INode_Expression> @postActions {
		get { return m_postActions; }
	}

	public Node_Block @action {
		get { return m_action; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "for-manual"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_initializers,
				m_test,
				m_postActions,
				m_action );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_DoWhile : INode_Expression {
	Node_Block m_action;
	INode_Expression m_test;
	
	public Node_DoWhile(
	Node_Block @action,
	INode_Expression @test ) {
		m_action = @action;
		m_test = @test;
	}
	
	public Node_Block @action {
		get { return m_action; }
	}

	public INode_Expression @test {
		get { return m_test; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "do-while"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_action,
				m_test );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_ClassProperty : INode {
	Node_Identifier m_identifier;
	Node_IdentikeyType m_identikeyType;
	Node_Function m_getter;
	Node_Function m_setter;
	
	public Node_ClassProperty(
	Node_Identifier @identifier,
	Node_IdentikeyType @identikeyType,
	Node_Function @getter,
	Node_Function @setter ) {
		m_identifier = @identifier;
		m_identikeyType = @identikeyType;
		m_getter = @getter;
		m_setter = @setter;
	}
	
	public Node_Identifier @identifier {
		get { return m_identifier; }
	}

	public Node_IdentikeyType @identikeyType {
		get { return m_identikeyType; }
	}

	public Node_Function @getter {
		get { return m_getter; }
	}

	public Node_Function @setter {
		get { return m_setter; }
	}
	
	public string typeName {
		get { return "class-property"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_identifier,
				m_identikeyType,
				m_getter,
				m_setter );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_DoTimes : INode_Expression {
	INode_Expression m_times;
	Node_Block m_action;
	INode_Expression m_test;
	
	public Node_DoTimes(
	INode_Expression @times,
	Node_Block @action,
	INode_Expression @test ) {
		m_times = @times;
		m_action = @action;
		m_test = @test;
	}
	
	public INode_Expression @times {
		get { return m_times; }
	}

	public Node_Block @action {
		get { return m_action; }
	}

	public INode_Expression @test {
		get { return m_test; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "do-times"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_times,
				m_action,
				m_test );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_ForPair : INode_Expression {
	INode_Expression m_container;
	INode_Expression m_keyInterface;
	Node_Identifier m_keyName;
	INode_Expression m_valueInterface;
	Node_Identifier m_valueName;
	Node_Block m_action;
	
	public Node_ForPair(
	INode_Expression @container,
	INode_Expression @keyInterface,
	Node_Identifier @keyName,
	INode_Expression @valueInterface,
	Node_Identifier @valueName,
	Node_Block @action ) {
		m_container = @container;
		m_keyInterface = @keyInterface;
		m_keyName = @keyName;
		m_valueInterface = @valueInterface;
		m_valueName = @valueName;
		m_action = @action;
	}
	
	public INode_Expression @container {
		get { return m_container; }
	}

	public INode_Expression @keyInterface {
		get { return m_keyInterface; }
	}

	public Node_Identifier @keyName {
		get { return m_keyName; }
	}

	public INode_Expression @valueInterface {
		get { return m_valueInterface; }
	}

	public Node_Identifier @valueName {
		get { return m_valueName; }
	}

	public Node_Block @action {
		get { return m_action; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "for-pair"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_container,
				m_keyInterface,
				m_keyName,
				m_valueInterface,
				m_valueName,
				m_action );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Array : INode_Expression {
	Node_NullableType m_nullableType;
	IList<INode_Expression> m_element;
	
	public Node_Array(
	Node_NullableType @nullableType,
	IList<INode_Expression> @element ) {
		m_nullableType = @nullableType;
		m_element = @element;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public IList<INode_Expression> @element {
		get { return m_element; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "array"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_element );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Select : INode_Expression {
	INode_Expression m_value;
	IList<Node_Case> m_case;
	INode_Expression m_else;
	
	public Node_Select(
	INode_Expression @value,
	IList<Node_Case> @case,
	INode_Expression @else ) {
		m_value = @value;
		m_case = @case;
		m_else = @else;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public IList<Node_Case> @case {
		get { return m_case; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "select"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value,
				m_case,
				m_else );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Generator : INode_Expression {
	Node_NullableType m_nullableType;
	INode_Expression m_expression;
	
	public Node_Generator(
	Node_NullableType @nullableType,
	INode_Expression @expression ) {
		m_nullableType = @nullableType;
		m_expression = @expression;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public INode_Expression @expression {
		get { return m_expression; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "generator"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_expression );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_ForRange : INode_Expression {
	Node_Identifier m_name;
	INode_Expression m_start;
	INode_Expression m_limit;
	Node_Boolean m_inclusive;
	INode_Expression m_test;
	Node_Block m_action;
	
	public Node_ForRange(
	Node_Identifier @name,
	INode_Expression @start,
	INode_Expression @limit,
	Node_Boolean @inclusive,
	INode_Expression @test,
	Node_Block @action ) {
		m_name = @name;
		m_start = @start;
		m_limit = @limit;
		m_inclusive = @inclusive;
		m_test = @test;
		m_action = @action;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @start {
		get { return m_start; }
	}

	public INode_Expression @limit {
		get { return m_limit; }
	}

	public Node_Boolean @inclusive {
		get { return m_inclusive; }
	}

	public INode_Expression @test {
		get { return m_test; }
	}

	public Node_Block @action {
		get { return m_action; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "for-range"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_start,
				m_limit,
				m_inclusive,
				m_test,
				m_action );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_NamedFunction : INode {
	Node_Identifier m_name;
	Node_Function m_function;
	
	public Node_NamedFunction(
	Node_Identifier @name,
	Node_Function @function ) {
		m_name = @name;
		m_function = @function;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Function @function {
		get { return m_function; }
	}
	
	public string typeName {
		get { return "named-function"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_function );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Call : INode_Expression {
	INode_Expression m_value;
	IList<Node_Argument> m_argument;
	
	public Node_Call(
	INode_Expression @value,
	IList<Node_Argument> @argument ) {
		m_value = @value;
		m_argument = @argument;
	}
	
	public INode_Expression @value {
		get { return m_value; }
	}

	public IList<Node_Argument> @argument {
		get { return m_argument; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "call"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_value,
				m_argument );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_FunctionInterface : INode_Expression {
	INode_Expression m_templateArgumentCount;
	IList<Node_Parameter> m_parameter;
	Node_NullableType m_returnInfo;
	
	public Node_FunctionInterface(
	INode_Expression @templateArgumentCount,
	IList<Node_Parameter> @parameter,
	Node_NullableType @returnInfo ) {
		m_templateArgumentCount = @templateArgumentCount;
		m_parameter = @parameter;
		m_returnInfo = @returnInfo;
	}
	
	public INode_Expression @templateArgumentCount {
		get { return m_templateArgumentCount; }
	}

	public IList<Node_Parameter> @parameter {
		get { return m_parameter; }
	}

	public Node_NullableType @returnInfo {
		get { return m_returnInfo; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "function-interface"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_templateArgumentCount,
				m_parameter,
				m_returnInfo );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Import : INode_ScopeAlteration {
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Parameter : INode {
	Node_Direction m_direction;
	Node_NullableType m_nullableType;
	Node_Identifier m_name;
	Node_Boolean m_hasDefaultValue;
	INode_Expression m_defaultValue;
	
	public Node_Parameter(
	Node_Direction @direction,
	Node_NullableType @nullableType,
	Node_Identifier @name,
	Node_Boolean @hasDefaultValue,
	INode_Expression @defaultValue ) {
		m_direction = @direction;
		m_nullableType = @nullableType;
		m_name = @name;
		m_hasDefaultValue = @hasDefaultValue;
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

	public Node_Boolean @hasDefaultValue {
		get { return m_hasDefaultValue; }
	}

	public INode_Expression @defaultValue {
		get { return m_defaultValue; }
	}
	
	public string typeName {
		get { return "parameter"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_direction,
				m_nullableType,
				m_name,
				m_hasDefaultValue,
				m_defaultValue );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Method : INode {
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Function : INode_Expression {
	IList<Node_Parameter> m_parameter;
	Node_NullableType m_returnInfo;
	INode_Expression m_body;
	
	public Node_Function(
	IList<Node_Parameter> @parameter,
	Node_NullableType @returnInfo,
	INode_Expression @body ) {
		m_parameter = @parameter;
		m_returnInfo = @returnInfo;
		m_body = @body;
	}
	
	public IList<Node_Parameter> @parameter {
		get { return m_parameter; }
	}

	public Node_NullableType @returnInfo {
		get { return m_returnInfo; }
	}

	public INode_Expression @body {
		get { return m_body; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "function"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_parameter,
				m_returnInfo,
				m_body );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Return : INode_Expression {
	INode_Expression m_expression;
	
	public Node_Return(
	INode_Expression @expression ) {
		m_expression = @expression;
	}
	
	public INode_Expression @expression {
		get { return m_expression; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "return"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_expression );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Dictionary : INode_Expression {
	Node_NullableType m_keyType;
	Node_NullableType m_valueType;
	IList<Node_DictionaryEntry> m_dictionaryEntry;
	
	public Node_Dictionary(
	Node_NullableType @keyType,
	Node_NullableType @valueType,
	IList<Node_DictionaryEntry> @dictionaryEntry ) {
		m_keyType = @keyType;
		m_valueType = @valueType;
		m_dictionaryEntry = @dictionaryEntry;
	}
	
	public Node_NullableType @keyType {
		get { return m_keyType; }
	}

	public Node_NullableType @valueType {
		get { return m_valueType; }
	}

	public IList<Node_DictionaryEntry> @dictionaryEntry {
		get { return m_dictionaryEntry; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "dictionary"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_keyType,
				m_valueType,
				m_dictionaryEntry );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_InterfaceImplementation : INode {
	IList<Node_InterfaceImplementation> m_children;
	INode_Expression m_interface;
	IList<Node_Function> m_callee;
	IList<Node_NamedFunction> m_getter;
	IList<Node_NamedFunction> m_setter;
	IList<Node_NamedFunction> m_method;
	Node_Boolean m_default;
	
	public Node_InterfaceImplementation(
	IList<Node_InterfaceImplementation> @children,
	INode_Expression @interface,
	IList<Node_Function> @callee,
	IList<Node_NamedFunction> @getter,
	IList<Node_NamedFunction> @setter,
	IList<Node_NamedFunction> @method,
	Node_Boolean @default ) {
		m_children = @children;
		m_interface = @interface;
		m_callee = @callee;
		m_getter = @getter;
		m_setter = @setter;
		m_method = @method;
		m_default = @default;
	}
	
	public IList<Node_InterfaceImplementation> @children {
		get { return m_children; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}

	public IList<Node_Function> @callee {
		get { return m_callee; }
	}

	public IList<Node_NamedFunction> @getter {
		get { return m_getter; }
	}

	public IList<Node_NamedFunction> @setter {
		get { return m_setter; }
	}

	public IList<Node_NamedFunction> @method {
		get { return m_method; }
	}

	public Node_Boolean @default {
		get { return m_default; }
	}
	
	public string typeName {
		get { return "interface-implementation"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_children,
				m_interface,
				m_callee,
				m_getter,
				m_setter,
				m_method,
				m_default );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Expose : INode_ScopeAlteration {
	IList<Node_Identifier> m_identifier;
	
	public Node_Expose(
	IList<Node_Identifier> @identifier ) {
		m_identifier = @identifier;
	}
	
	public IList<Node_Identifier> @identifier {
		get { return m_identifier; }
	}
	
	public string typeName {
		get { return "expose"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_identifier );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_DeclareEmpty : INode_Declaration {
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Enum : INode_Expression {
	Node_NullableType m_nullableType;
	IList<Node_EnumEntry> m_enumEntry;
	
	public Node_Enum(
	Node_NullableType @nullableType,
	IList<Node_EnumEntry> @enumEntry ) {
		m_nullableType = @nullableType;
		m_enumEntry = @enumEntry;
	}
	
	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public IList<Node_EnumEntry> @enumEntry {
		get { return m_enumEntry; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "enum"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_nullableType,
				m_enumEntry );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_InstantiateGeneric : INode_Expression {
	INode_Expression m_generic;
	IList<Node_Argument> m_argument;
	
	public Node_InstantiateGeneric(
	INode_Expression @generic,
	IList<Node_Argument> @argument ) {
		m_generic = @generic;
		m_argument = @argument;
	}
	
	public INode_Expression @generic {
		get { return m_generic; }
	}

	public IList<Node_Argument> @argument {
		get { return m_argument; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "instantiate-generic"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_generic,
				m_argument );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_ForKey : INode_Expression {
	INode_Expression m_container;
	INode_Expression m_keyInterface;
	Node_Identifier m_name;
	Node_Block m_action;
	
	public Node_ForKey(
	INode_Expression @container,
	INode_Expression @keyInterface,
	Node_Identifier @name,
	Node_Block @action ) {
		m_container = @container;
		m_keyInterface = @keyInterface;
		m_name = @name;
		m_action = @action;
	}
	
	public INode_Expression @container {
		get { return m_container; }
	}

	public INode_Expression @keyInterface {
		get { return m_keyInterface; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Block @action {
		get { return m_action; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "for-key"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_container,
				m_keyInterface,
				m_name,
				m_action );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Using : INode_ScopeAlteration {
	IList<Node_Identifier> m_target;
	Node_Identifier m_name;
	
	public Node_Using(
	IList<Node_Identifier> @target,
	Node_Identifier @name ) {
		m_target = @target;
		m_name = @name;
	}
	
	public IList<Node_Identifier> @target {
		get { return m_target; }
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
				m_target,
				m_name );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Throw : INode_Expression {
	INode_Expression m_expression;
	
	public Node_Throw(
	INode_Expression @expression ) {
		m_expression = @expression;
	}
	
	public INode_Expression @expression {
		get { return m_expression; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "throw"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_expression );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Property : INode {
	Node_Identifier m_name;
	Node_NullableType m_nullableType;
	Node_Access m_access;
	
	public Node_Property(
	Node_Identifier @name,
	Node_NullableType @nullableType,
	Node_Access @access ) {
		m_name = @name;
		m_nullableType = @nullableType;
		m_access = @access;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public Node_Access @access {
		get { return m_access; }
	}
	
	public string typeName {
		get { return "property"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_nullableType,
				m_access );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_TryCatch : INode_Expression {
	INode_Expression m_try;
	IList<Node_ExceptionHandler> m_exceptionHandler;
	INode_Expression m_else;
	INode_Expression m_finally;
	
	public Node_TryCatch(
	INode_Expression @try,
	IList<Node_ExceptionHandler> @exceptionHandler,
	INode_Expression @else,
	INode_Expression @finally ) {
		m_try = @try;
		m_exceptionHandler = @exceptionHandler;
		m_else = @else;
		m_finally = @finally;
	}
	
	public INode_Expression @try {
		get { return m_try; }
	}

	public IList<Node_ExceptionHandler> @exceptionHandler {
		get { return m_exceptionHandler; }
	}

	public INode_Expression @else {
		get { return m_else; }
	}

	public INode_Expression @finally {
		get { return m_finally; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "try-catch"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_try,
				m_exceptionHandler,
				m_else,
				m_finally );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Callee : INode {
	IList<Node_Parameter> m_parameter;
	Node_NullableType m_returnInfo;
	
	public Node_Callee(
	IList<Node_Parameter> @parameter,
	Node_NullableType @returnInfo ) {
		m_parameter = @parameter;
		m_returnInfo = @returnInfo;
	}
	
	public IList<Node_Parameter> @parameter {
		get { return m_parameter; }
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
				m_parameter,
				m_returnInfo );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Yield : INode_Expression {
	INode_Expression m_expression;
	
	public Node_Yield(
	INode_Expression @expression ) {
		m_expression = @expression;
	}
	
	public INode_Expression @expression {
		get { return m_expression; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "yield"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_expression );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Ignore : INode_Expression {
	INode_Expression m_content;
	IList<Node_IgnoreMember> m_ignoreMember;
	
	public Node_Ignore(
	INode_Expression @content,
	IList<Node_IgnoreMember> @ignoreMember ) {
		m_content = @content;
		m_ignoreMember = @ignoreMember;
	}
	
	public INode_Expression @content {
		get { return m_content; }
	}

	public IList<Node_IgnoreMember> @ignoreMember {
		get { return m_ignoreMember; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "ignore"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_content,
				m_ignoreMember );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Cast : INode_Expression {
	INode_Expression m_object;
	INode_Expression m_interface;
	
	public Node_Cast(
	INode_Expression @object,
	INode_Expression @interface ) {
		m_object = @object;
		m_interface = @interface;
	}
	
	public INode_Expression @object {
		get { return m_object; }
	}

	public INode_Expression @interface {
		get { return m_interface; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "cast"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_object,
				m_interface );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_While : INode_Expression {
	INode_Expression m_test;
	Node_Block m_block;
	
	public Node_While(
	INode_Expression @test,
	Node_Block @block ) {
		m_test = @test;
		m_block = @block;
	}
	
	public INode_Expression @test {
		get { return m_test; }
	}

	public Node_Block @block {
		get { return m_block; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "while"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_test,
				m_block );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_IdentikeyType : INode {
	Node_IdentikeyCategory m_identikeyCategory;
	Node_NullableType m_nullableType;
	Node_Boolean m_constant;
	
	public Node_IdentikeyType(
	Node_IdentikeyCategory @identikeyCategory,
	Node_NullableType @nullableType,
	Node_Boolean @constant ) {
		m_identikeyCategory = @identikeyCategory;
		m_nullableType = @nullableType;
		m_constant = @constant;
	}
	
	public Node_IdentikeyCategory @identikeyCategory {
		get { return m_identikeyCategory; }
	}

	public Node_NullableType @nullableType {
		get { return m_nullableType; }
	}

	public Node_Boolean @constant {
		get { return m_constant; }
	}
	
	public string typeName {
		get { return "identikey-type"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_identikeyCategory,
				m_nullableType,
				m_constant );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_ForValue : INode_Expression {
	INode_Expression m_container;
	INode_Expression m_valueInterface;
	Node_Identifier m_name;
	Node_Block m_action;
	
	public Node_ForValue(
	INode_Expression @container,
	INode_Expression @valueInterface,
	Node_Identifier @name,
	Node_Block @action ) {
		m_container = @container;
		m_valueInterface = @valueInterface;
		m_name = @name;
		m_action = @action;
	}
	
	public INode_Expression @container {
		get { return m_container; }
	}

	public INode_Expression @valueInterface {
		get { return m_valueInterface; }
	}

	public Node_Identifier @name {
		get { return m_name; }
	}

	public Node_Block @action {
		get { return m_action; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "for-value"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_container,
				m_valueInterface,
				m_name,
				m_action );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Block : INode_Expression {
	IList<INode_ScopeAlteration> m_scopeAlteration;
	IList<INode_Expression> m_members;
	
	public Node_Block(
	IList<INode_ScopeAlteration> @scopeAlteration,
	IList<INode_Expression> @members ) {
		m_scopeAlteration = @scopeAlteration;
		m_members = @members;
	}
	
	public IList<INode_ScopeAlteration> @scopeAlteration {
		get { return m_scopeAlteration; }
	}

	public IList<INode_Expression> @members {
		get { return m_members; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "block"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_scopeAlteration,
				m_members );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
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
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Convertor : INode {
	INode_Expression m_interface;
	
	public Node_Convertor(
	INode_Expression @interface ) {
		m_interface = @interface;
	}
	
	public INode_Expression @interface {
		get { return m_interface; }
	}
	
	public string typeName {
		get { return "convertor"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_interface );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Curry : INode_Expression {
	INode_Expression m_function;
	IList<Node_Argument> m_argument;
	Node_Boolean m_call;
	
	public Node_Curry(
	INode_Expression @function,
	IList<Node_Argument> @argument,
	Node_Boolean @call ) {
		m_function = @function;
		m_argument = @argument;
		m_call = @call;
	}
	
	public INode_Expression @function {
		get { return m_function; }
	}

	public IList<Node_Argument> @argument {
		get { return m_argument; }
	}

	public Node_Boolean @call {
		get { return m_call; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "curry"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_function,
				m_argument,
				m_call );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Assign : INode_Expression {
	Node_Identifier m_name;
	INode_Expression m_value;
	Node_Boolean m_breed;
	
	public Node_Assign(
	Node_Identifier @name,
	INode_Expression @value,
	Node_Boolean @breed ) {
		m_name = @name;
		m_value = @value;
		m_breed = @breed;
	}
	
	public Node_Identifier @name {
		get { return m_name; }
	}

	public INode_Expression @value {
		get { return m_value; }
	}

	public Node_Boolean @breed {
		get { return m_breed; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "assign"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_name,
				m_value,
				m_breed );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

class Node_Loop : INode_Expression {
	Node_Block m_block;
	
	public Node_Loop(
	Node_Block @block ) {
		m_block = @block;
	}
	
	public Node_Block @block {
		get { return m_block; }
	}
	
	public IValue execute(Scope scope) {
		return Interpreter.execute(this, scope);
	}

	public string typeName {
		get { return "loop"; }
	}
	
	public ICollection<INode> childNodes {
		get {
			return G.collect<INode>(
				m_block );
		}
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this); }
	}
}

