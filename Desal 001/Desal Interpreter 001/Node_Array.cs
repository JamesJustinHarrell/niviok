using System.Collections.Generic;

class Node_Array : INode_Expression {
	IList<INode_Expression> _elements;

	public Node_Array( IList<INode_Expression> elements ) {
		_elements = elements;
	}
	
	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
	}
	
	public string typeName {
		get { return "array"; }
	}
	
	public ICollection<INode> children {
		get { return G.collect<INode>(_elements); }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return G.depends(_elements); }
	}
}
