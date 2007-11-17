using System.Collections.Generic;

class Node_MethodCall : INode_Expression {
	INode_Expression _value;
	Node_Identifier _methodName;
	IList<INode_Expression> _arguments;
	//xxx IList<Node_LabeledArgument> _labeledArguments;
	
	public Node_MethodCall(
	INode_Expression val, Node_Identifier methodName, IList<INode_Expression> arguments) {
		_value = val;
		_methodName = methodName;
		_arguments = arguments;
	}
	
	public IValue evaluate(Scope scope) {
		try {
			return _value
				.evaluate(scope)
				.evaluateMethod(
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
	
	public void execute(Scope scope) {
		try {
			_value
				.evaluate(scope)
				.executeMethod(
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

	public void getInfo(out string name, out object objs) {
		name = "method-call";
		objs = new object[]{
			_value,
			_methodName,
			_arguments
			/* xxx _labeledArguments */ };
	}
	
	IList<IValue> evaluateArguments(Scope scope) {
		IList<IValue> evaledArgs = new List<IValue>();
		foreach( INode_Expression argument in _arguments ) {
			evaledArgs.Add( argument.evaluate(scope) );
		}
		return evaledArgs;
	}

	IDictionary<Identifier, IValue> evaluateLabeledArguments() {
		return new Dictionary<Identifier, IValue>();
	}
}