using System.Collections.Generic;

class Node_Call : INode_Expression {
	INode_Expression _value;
	IList<INode_Expression> _arguments;
	
	IList<IValue> evaluateArguments(Scope scope) {
		IList<IValue> evaledArgs = new List<IValue>();
		foreach( INode_Expression argument in _arguments ) {
			evaledArgs.Add( argument.execute(scope) );
		}
		return evaledArgs;
	}

	IDictionary<Identifier, IValue> evaluateLabeledArguments() {
		return new Dictionary<Identifier, IValue>();
	}
	
	public Node_Call(
	INode_Expression val, IList<INode_Expression> arguments) {
		_value = val;
		_arguments = arguments;
	}
	
	public IValue execute(Scope scope) {
		Arguments args = new Arguments(
			evaluateArguments(scope),
			evaluateLabeledArguments() );
		IValue func = _value.execute(scope);
	
		try {
			return func.call(args);
		}
		catch(ClientException e) {
			e.pushFunc(
				( _value is Node_Identifier ?
					(_value as Node_Identifier).identifier.ToString():
					"(anonymous)" ));
			throw e;
		}
	}
	
	public string typeName {
		get { return "call"; }
	}
	
	public ICollection<INode> children {
		get { return G.collect<INode>(_value, _arguments); }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends( _value, _arguments ); }
	}
}