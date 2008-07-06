class DependsResults {
	HashSet<IdentifierSequence> _executeDepends;
	HashSet<IdentifierSequence> _finishDepends;
	
	public DependsResults(
	HashSet<IdentifierSequence> executeDepends,
	HashSet<IdentifierSequence> finishDepends) {
		_executeDepends = executeDepends;
		if( _executeDepends == null )
			_executeDepends = new HashSet<IdentifierSequence>();
		_finishDepends = finishDepends;
		if( _finishDepends == null )
			_finishDepends = new HashSet<IdentifierSequence>();
	}
	
	public void UnionWith(DependsResults results) {
		_executeDepends.UnionWith(results.executeDepends);
		_finishDepends.UnionWith(results.finishDepends);
	}
	
	//adds to finish if node is a type that can be a finish dependency
	//otherwise adds to execute dependencies
	public void tryFinish(INode_Expression node) {
		if( node.typeName == "identifier" )
			_finishDepends.UnionWith(Depends.depends(node));
		else
			_executeDepends.UnionWith(Depends.depends(node));
	}

	public HashSet<IdentifierSequence> executeDepends {
		get { return _executeDepends; }
	}
	
	public HashSet<IdentifierSequence> finishDepends {
		get { return _finishDepends; }
	}
}
