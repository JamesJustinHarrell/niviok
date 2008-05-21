
//This file was generated programmatically, so
//don't edit this file directly.

using System;
using System.Collections.Generic;
static partial class Executor {
	public static IWorker executeAny(INode_Expression node, Scope scope) {
		switch(node.typeName) {
			case "conditional-loop":
				return execute((Node_ConditionalLoop)node, scope);
			case "enumerator-loop":
				return execute((Node_EnumeratorLoop)node, scope);
			case "unconditional-loop":
				return execute((Node_UnconditionalLoop)node, scope);
			case "break":
				return execute((Node_Break)node, scope);
			case "continue":
				return execute((Node_Continue)node, scope);
			case "return":
				return execute((Node_Return)node, scope);
			case "throw":
				return execute((Node_Throw)node, scope);
			case "yield":
				return execute((Node_Yield)node, scope);
			case "declare-empty":
				return execute((Node_DeclareEmpty)node, scope);
			case "null":
				return execute((Node_Null)node, scope);
			case "assign":
				return execute((Node_Assign)node, scope);
			case "call":
				return execute((Node_Call)node, scope);
			case "cast":
				return execute((Node_Cast)node, scope);
			case "compound":
				return execute((Node_Compound)node, scope);
			case "conditional":
				return execute((Node_Conditional)node, scope);
			case "curry":
				return execute((Node_Curry)node, scope);
			case "declare-assign":
				return execute((Node_DeclareAssign)node, scope);
			case "declare-first":
				return execute((Node_DeclareFirst)node, scope);
			case "identifier":
				return execute((Node_Identifier)node, scope);
			case "ignore":
				return execute((Node_Ignore)node, scope);
			case "labeled":
				return execute((Node_Labeled)node, scope);
			case "namespaced-value-identikey":
				return execute((Node_NamespacedValueIdentikey)node, scope);
			case "select":
				return execute((Node_Select)node, scope);
			case "try-catch":
				return execute((Node_TryCatch)node, scope);
			case "and":
				return execute((Node_And)node, scope);
			case "nand":
				return execute((Node_Nand)node, scope);
			case "or":
				return execute((Node_Or)node, scope);
			case "nor":
				return execute((Node_Nor)node, scope);
			case "xor":
				return execute((Node_Xor)node, scope);
			case "xnor":
				return execute((Node_Xnor)node, scope);
			case "breed":
				return execute((Node_Breed)node, scope);
			case "caller":
				return execute((Node_Caller)node, scope);
			case "object":
				return execute((Node_Object)node, scope);
			case "dictionary":
				return execute((Node_Dictionary)node, scope);
			case "enum":
				return execute((Node_Enum)node, scope);
			case "extract-member":
				return execute((Node_ExtractMember)node, scope);
			case "function":
				return execute((Node_Function)node, scope);
			case "function-interface":
				return execute((Node_FunctionInterface)node, scope);
			case "generator":
				return execute((Node_Generator)node, scope);
			case "generic-function":
				return execute((Node_GenericFunction)node, scope);
			case "generic-interface":
				return execute((Node_GenericInterface)node, scope);
			case "instantiate-generic":
				return execute((Node_InstantiateGeneric)node, scope);
			case "integer":
				return execute((Node_Integer)node, scope);
			case "interface":
				return execute((Node_Interface)node, scope);
			case "implements":
				return execute((Node_Implements)node, scope);
			case "rational":
				return execute((Node_Rational)node, scope);
			case "set-property":
				return execute((Node_SetProperty)node, scope);
			case "string":
				return execute((Node_String)node, scope);
			default:
				throw new ApplicationException(String.Format(
					"can't execute node of type '{0}'",
					node.typeName));
		}
	}
}