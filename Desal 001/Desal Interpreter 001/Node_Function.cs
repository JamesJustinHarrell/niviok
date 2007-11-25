using System.Collections.Generic;

class Node_Function : INode_Expression {
	//xxx enable IList<Node_Identifier> _templateParameters;
	Bridge _bridge;
	IList<Node_Parameter> _parameters;
	Node_ReferenceType _returnType; //will be null for void functions
	Node_Block _block;

	public Node_Function(
	Bridge bridge,
	IList<Node_Parameter> parameters,
	Node_ReferenceType returnType, Node_Block block) {
		_bridge = bridge;
		_parameters = parameters;
		_returnType = returnType;
		_block = block;
	}
	
	//creates a function; does not evaluate the function
	public IValue evaluate(Scope scope) {
		//evaluate parameters
		IList<Parameter> evaledParams = new List<Parameter>();
		foreach( Node_Parameter paramNode in _parameters ) {
			evaledParams.Add( paramNode.evaluateParameter(scope) );
		}
		
		ReferenceType returnType = ( _returnType == null ?
			null :
			_returnType.evaluateType(scope) );
		
		IFunction function = new Client_Function(
			evaledParams, returnType, _block, scope );		
		
		return FunctionWrapper.wrap(function);
	}
	
	public void execute(Scope scope) {
		_bridge.warning("executing a function node has no effect (it's not the same as executing a function)");
	}
	
	public void getInfo(out string name, out object children) {
		name = "function";
		children = new object[]{ _parameters, _returnType, _block };
	}
}