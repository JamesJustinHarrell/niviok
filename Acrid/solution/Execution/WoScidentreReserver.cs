using System;
using Acrid.NodeTypes;

namespace Acrid.Execution {

static class WoScidentreReserver {
	public static void reserve( Node_Call node, IScope scope ) {
		reserveAny(node.receiver, scope);
		foreach(Node_Argument a in node.arguments)
			if(a.value != null)
				reserveAny(a.value, scope);
	}
	
	public static void reserve( Node_Compound node, IScope scope ) {
		foreach(INode_Expression child in node.members)
			reserveAny(child, scope);
	}
	
	public static void reserve( Node_DeclareAssign node, IScope scope ) {
		scope.reserveWoScidentre(
			node.name.value,
			node.constant.value ? WoScidentreCategory.CONSTANT : WoScidentreCategory.VARIABLE);
	}

	public static void reserve( Node_DeclareEmpty node, IScope scope ) {
		scope.reserveWoScidentre( node.name.value, WoScidentreCategory.VARIABLE );
	}
	
	public static void reserveAny( INode_Expression node, IScope scope ) {
		switch(node.typeName) {
		
		//these nodes have scope
		//xxx automate
		case "conditional" :
		case "function" :
		case "object" :
		case "try-catch" :
			break;
		
		//these nodes cannot contain declare-* children
		//xxx automate
		case "identifier" :
		case "integer" :
		case "string" :
		case "rational" :
			break;
		
		//xxx these need to be moved into the below category
		case "and" :
		case "nand" :
		case "or" :
		case "nor" :
		case "xor" :
		case "xnor" :
			break;
		
		//can contain declare-* nodes, and don't have scope
		case "assign" :
			reserveAny( (node as Node_Assign).value, scope );
			break;
		case "call" :
			reserve(node as Node_Call, scope);
			break;
		case "compound" :
			reserve(node as Node_Compound, scope);
			break;
		case "declare-assign" :
			reserve(node as Node_DeclareAssign, scope);
			break;
		case "declare-empty" :
			reserve(node as Node_DeclareEmpty, scope);
			break;
		case "extract-member" :
			reserveAny( (node as Node_ExtractMember).source, scope );
			break;
		case "set-property" :
			reserveAny( (node as Node_SetProperty).source, scope );
			reserveAny( (node as Node_SetProperty).value, scope );
			break;
		case "yield" :
			reserveAny( (node as Node_Yield).value, scope );
			break;
		default :
			throw new Exception(String.Format("unrecognized node type '{0}'", node.typeName));
		}
	}
}

} //namespace
