using System.Collections.Generic;

class Node_DeclareAssign : INode_DeclarationAny, INode_Expression {
	Node_Identifier _name;
	Node_ReferenceType _type;
	Node_Bool _const;
	INode_Expression _value;

	public Node_DeclareAssign(
	Node_Identifier name, Node_ReferenceType type,
	Node_Bool constant, INode_Expression val ) {		
		_name = name;
		_type = type;
		_const = constant;
		_value = val;
	}
	
	public IValue execute(Scope scope) {
		IValue val = _value.execute(scope);
		
		scope.declareAssign(
			_name.identifier,
			//xxx ( _type == null ? null : _type.evaluateType(scope) ),
			//xxx ( _const == null ? false : _const.val ),
			val );
		return val;
	}
	
	public void getInfo(out string name, out object children) {
		name = "declare-assign";
		children = new object[]{ _name, _type, _const, _value };
	}
}