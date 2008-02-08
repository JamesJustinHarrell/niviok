class Node_ConditionalBlock : INode_Expression {
	INode_Expression _testNode;
	Node_Block _actionNode;

	public Node_ConditionalBlock(INode_Expression testNode, Node_Block actionNode) {
		_testNode = testNode;
		_actionNode = actionNode;
	}
	
	public IValue execute(Scope scope) {
		IValue testVal = _testNode.execute(scope);

		if( testVal.activeInterface == Bridge.Bool ) {
			return ( Bridge.unwrapBoolean(testVal) ?
				_actionNode.execute(scope) :
				new NullValue() );
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