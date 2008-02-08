class Node_Parameter : INode {
	Node_Identifier _name;
	Node_ReferenceType _type;
	INode_Expression _defaultValue;
	Node_Bool _nullable;

	public Node_Parameter(
	Node_Identifier name, Node_ReferenceType type,
	INode_Expression defaultValue, Node_Bool nullable ) {
		_name = name;
		_type = type;
		_defaultValue = defaultValue;
		_nullable = nullable;
	}
	
	public Parameter evaluateParameter(Scope scope) {
		return new Parameter(
			_name.identifier,
			null, //xxx _type.evaluateType(scope),
			( _defaultValue == null ? null : _defaultValue.execute(scope) ),
			_nullable.val );
	}
	
	public void getInfo(out string name, out object objs) {
		name = "parameter";
		objs = new object[]{ _name, _type, _defaultValue, _nullable };
	}
}