using Acrid.NodeTypes;

namespace Acrid.Execution {

public class DependsResults {
	HashSet<Identifier> _executeDepends;
	HashSet<Identifier> _finishDepends;
	
	public DependsResults(
	HashSet<Identifier> executeDepends,
	HashSet<Identifier> finishDepends) {
		_executeDepends = executeDepends;
		if( _executeDepends == null )
			_executeDepends = new HashSet<Identifier>();
		_finishDepends = finishDepends;
		if( _finishDepends == null )
			_finishDepends = new HashSet<Identifier>();
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

	public HashSet<Identifier> executeDepends {
		get { return _executeDepends; }
	}
	
	public HashSet<Identifier> finishDepends {
		get { return _finishDepends; }
	}
}

} //namespace
