using System.Collections.Generic;

class Node_Parameter : INode {
	/* xxx direction */
	Node_NullableType _nullableType;
	Node_Identifier _name;
	Node_Boolean _hasDefaultValue;
	INode_Expression _defaultValue; //nullable

	public Node_Parameter(
	Node_NullableType nullableType, Node_Identifier name,
	Node_Boolean hasDefaultValue, INode_Expression defaultValue) {
		_nullableType = nullableType;
		_name = name;
		_hasDefaultValue = hasDefaultValue;
		_defaultValue = defaultValue;
	}
	
	public Identifier name {
		get { return _name.identifier; }
	}
	
	public Parameter evaluateParameter(Scope scope) {
		return new Parameter(
			/* xxx direction */
			_nullableType.evaluateType(scope),
			_name.identifier,
			_hasDefaultValue.val,
			( _defaultValue == null ? null : _defaultValue.execute(scope) ));
	}
	
	public string typeName {
		get { return "parameter"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _nullableType, _name, _hasDefaultValue, _defaultValue }; }
	}

	//xxx take a closer look at this
	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _nullableType, _defaultValue ); }
	}
}