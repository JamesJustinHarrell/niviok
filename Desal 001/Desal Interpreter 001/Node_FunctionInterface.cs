using System.Collections.Generic;

class Node_FunctionInterface : INode_Expression {
	IList<Node_Parameter> _parameters;
	Node_NullableType _type;

	public Node_FunctionInterface(
	IList<Node_Parameter> parameters, Node_NullableType type ) {
		_parameters = parameters;
		_type = type;
	}
	
	public IFunctionInterface evaluateInterface(Scope scope) {
		IList<Parameter> parameters =
			new List<Parameter>();
		foreach( Node_Parameter paramNode in _parameters ) {
			parameters.Add( paramNode.evaluateParameter(scope) );
		}
		return FunctionInterface.getFuncFace(
			parameters,	_type.evaluateType(scope) );
	}
	
	public IValue execute(Scope scope) {
		scope.bridge.warning("Node_FunctionInterface.evaluate called; returning null");
		return new NullValue();
	}
	
	public string typeName {
		get { return "function-interface"; }
	}
	
	public ICollection<INode> children {
		get { return G.collect<INode>(_parameters, _type); }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _parameters, _type ); }
	}
}