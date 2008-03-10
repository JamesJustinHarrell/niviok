using System.Collections.Generic;

class Node_Function : INode_Expression {
	//xxx enable IList<Node_Identifier> _templateParameters;
	IList<Node_Parameter> _parameters;
	Node_NullableType _returnType; //will be null for void functions
	INode_Expression _body;

	public Node_Function(
	IList<Node_Parameter> parameters,
	Node_NullableType returnType, INode_Expression body) {
		_parameters = parameters;
		_returnType = returnType;
		_body = body;
	}
	
	//creates a function; does not evaluate the function
	public IValue execute(Scope scope) {
		//evaluate parameters
		IList<Parameter> evaledParams = new List<Parameter>();
		foreach( Node_Parameter paramNode in _parameters ) {
			evaledParams.Add( paramNode.evaluateParameter(scope) );
		}
		
		NullableType returnType = ( _returnType == null ?
			null :
			_returnType.evaluateType(scope) );
		
		IFunction function = new Client_Function(
			evaledParams, returnType, _body,
			scope.createClosure(identikeyDependencies) );
		
		return FunctionWrapper.wrap(function);
	}
	
	public string typeName {
		get { return "function"; }
	}
	
	public ICollection<INode> children {
		get { return G.collect<INode>(_parameters, _returnType, _body); }
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents =
				G.depends( _parameters, _returnType, _body );
			foreach( Node_Parameter param in _parameters )
				idents.Remove( param.name );
			return idents;
		}
	}
}