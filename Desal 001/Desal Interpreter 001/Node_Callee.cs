using System.Collections.Generic;

class Node_Callee : INode {
	IList<Node_Parameter> _parameters;
	
	public Node_Callee(IList<Node_Parameter> parameters) {
		_parameters = parameters;
	}
	
	public IList<Node_Parameter> parameters {
		get { return _parameters; }
	}
	
	public string typeName {
		get { return "callee"; }
	}
	
	public ICollection<INode> children {
		get { return G.collect<INode>(_parameters); }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> dependencies = new HashSet<Identifier>();
			foreach( INode node in _parameters ) {
				dependencies.UnionWith(node.identikeyDependencies);
			}
			return dependencies;
		}
	}
}