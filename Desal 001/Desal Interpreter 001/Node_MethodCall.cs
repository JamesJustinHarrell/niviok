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
	
	public IValue evaluate(ref Scope scope) {
		return _value
			.evaluate(ref scope)
			.evaluateMethod(
				_methodName.identifier,
				new Arguments(
					evaluateArguments(ref scope),
					evaluateLabeledArguments() ) );
	}
	
	public void execute(ref Scope scope) {
		_value
			.evaluate(ref scope)
			.executeMethod(
				_methodName.identifier,
				new Arguments(
					evaluateArguments(ref scope),
					evaluateLabeledArguments() ) );
	}

	public void getInfo(out string name, out object objs) {
		name = "method-call";
		objs = new object[]{
			_value,
			_methodName,
			_arguments
			/* xxx _labeledArguments */ };
	}
	
	IList<IValue> evaluateArguments(ref Scope scope) {
		IList<IValue> evaledArgs = new List<IValue>();
		foreach( INode_Expression argument in _arguments ) {
			evaledArgs.Add( argument.evaluate(ref scope) );
		}
		return evaledArgs;
	}

	IDictionary<Identifier, IValue> evaluateLabeledArguments() {
		return new Dictionary<Identifier, IValue>();
	}
}