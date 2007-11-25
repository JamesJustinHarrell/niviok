using System.Collections.Generic;

class Node_FunctionInterface : INode_Expression {
	Bridge _bridge;
	IList<Node_Parameter> _parameters;
	Node_ReferenceType _type;

	public Node_FunctionInterface(
	Bridge bridge,
	IList<Node_Parameter> parameters, Node_ReferenceType type ) {
		_bridge = bridge;
		_parameters = parameters;
		_type = type;
	}
	
	public IValue evaluate(Scope scope) {
		_bridge.warning("Node_FunctionInterface.evaluate called; returning null");
		return new NullValue(null);
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
	
	public void execute(Scope scope) {
		//xxx warning does nothing
	}

	public void getInfo(out string name, out object objs) {
		name = "function-interface";
		objs = new object[]{ _parameters, _type };
	}
}