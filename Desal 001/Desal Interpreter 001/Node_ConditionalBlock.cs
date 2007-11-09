class Node_ConditionalBlock : INode_Statement {
	INode_Expression _testNode;
	Node_Block _actionNode;

	public Node_ConditionalBlock(INode_Expression testNode, Node_Block actionNode) {
		_testNode = testNode;
		_actionNode = actionNode;
	}
	
	public void execute(ref Scope scope) {
		IValue testVal = _testNode.evaluate(ref scope);

		if( testVal.activeInterface == Wrapper.Bool ) {
			bool test = Wrapper.unwrapBoolean(testVal);
			if( test )
				_actionNode.execute(ref scope);
		}
		else {
			throw new ClientException("test must be a boolean");
		}
	}

	public void getInfo(out string name, out object objs) {
		name = "conditional-block";
		objs = new object[]{ _testNode, _actionNode };
	}
}