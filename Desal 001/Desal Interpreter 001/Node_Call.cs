using System.Collections.Generic;

class Node_Call : INode_Expression {
	INode_Expression _value;
	Node_Identifier _methodName; //optional
	IList<INode_Expression> _arguments;
	
	public Node_Call(
	INode_Expression val, IList<INode_Expression> arguments)
	: this(val, null, arguments)
	{}
	
	public Node_Call(
	INode_Expression val, Node_Identifier methodName, IList<INode_Expression> arguments) {
		_value = val;
		_methodName = methodName;
		_arguments = arguments;
	}
	
	public IValue execute(Scope scope) {
		return ( _methodName == null ?
			executeFunction(scope) :
			executeMethod(scope) );
	}

	public void getInfo(out string name, out object objs) {
		name = "call";
		objs = new object[]{
			_value,
			_methodName,
			_arguments
		};
	}
	
	IValue executeFunction(Scope scope) {
		try {
			return _value
				.execute(scope)
				.call(
					new Arguments(
						evaluateArguments(scope),
						evaluateLabeledArguments() ) );
			
		}
		catch(ClientException e) {
			e.pushFunc(
				_value is Node_Identifier ?
				((Node_Identifier)_value).identifier.str :
				"(anonymous)" );
			throw e;
		}
	}

	IValue executeMethod(Scope scope) {
		try {
			return _value
				.execute(scope)
				.callMethod(
					_methodName.identifier,
					new Arguments(
						evaluateArguments(scope),
						evaluateLabeledArguments() ) );
		}
		catch(ClientException e) {
			e.pushFunc(_methodName.identifier.str);
			throw e;
		}
	}
	
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
}