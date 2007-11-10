class Node_DeclarationBind : INode_DeclarationAny, INode_Expression {
	Node_Identifier _name;
	Node_ReferenceType _type;
	Node_Bool _const; //optional
	INode_Expression _value;

	public Node_DeclarationBind(
	Node_Identifier name, Node_ReferenceType type,
	Node_Bool constant, INode_Expression val ) {
		_name = name;
		_type = type;
		_const = constant;
		_value = val;
	}

	public void execute(Scope scope) {
		evaluate(scope);
	}
	
	public IValue evaluate(Scope scope) {
		IValue val = _value.evaluate(scope);
		scope.declareBind(
			_name.identifier,
			_type.evaluateType(scope),
			( _const == null ? false : _const.val ),
			val );
		return val;
	}
	
	public void getInfo(out string name, out object children) {
		name = "declaration-bind";
		children = new object[]{ _name, _type, _const, _value };
	}
}