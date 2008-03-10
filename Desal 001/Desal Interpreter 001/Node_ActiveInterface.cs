using System.Collections.Generic;

class Node_ActiveInterface : INode_Expression {
	INode_Expression _val;

	public Node_ActiveInterface(INode_Expression val) {
		_val = val;
	}
	
	public IValue execute(Scope scope) {
		return _val.execute(scope).activeInterface.value;
	}
	
	public string typeName {
		get { return "active-interface"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _val }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return _val.identikeyDependencies; }
	}
}
