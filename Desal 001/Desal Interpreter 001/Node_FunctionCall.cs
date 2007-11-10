using System.Collections.Generic;

class Node_FunctionCall : INode_Expression {
	INode_Expression _function;
	IList<INode_Expression> _arguments;

	public Node_FunctionCall(INode_Expression function, IList<INode_Expression> arguments) {
		_function = function;
		_arguments = arguments;
	}
	
	public IValue evaluate(Scope scope) {
		return _function
			.evaluate(scope)
			.evaluateCall( new Arguments(
				evaluateArguments(scope),
				evaluateLabeledArguments() ) );
	}
	
	public void execute(Scope scope) {
		_function
			.evaluate(scope)
			.executeCall( new Arguments(
				evaluateArguments(scope),
				evaluateLabeledArguments() ) );
	}

	public void getInfo(out string name, out object objs) {
		name = "function-call";
		objs = new object[]{ _function, _arguments };
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