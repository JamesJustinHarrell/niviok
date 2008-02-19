using System.Collections.Generic;

class Node_Possibility : INode_Expression {
	INode_Expression _testNode;
	INode_Expression _resultNode;

	public Node_Possibility(INode_Expression testNode, INode_Expression resultNode) {
		_testNode = testNode;
		_resultNode = resultNode;
	}
	
	public IValue execute(Scope scope) {
		IValue testVal = executeTest(scope);

		if( testVal.activeInterface != Bridge.Bool )
			throw new ClientException("test must be a Bool");
		
		return ( Bridge.unwrapBoolean(testVal) ?
			executeResult(scope) :
			new NullValue() );
	}
	
	//used for conditional node
	public IValue executeTest(Scope scope) {
		return _testNode.execute(scope);
	}
	public IValue executeResult(Scope scope) {
		return _resultNode.execute(scope);
	}

	public void getInfo(out string name, out object objs) {
		name = "possibility";
		objs = new object[]{ _testNode, _resultNode };
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return Help.getIdentRefs( _testNode, _resultNode ); }
	}
}