using System.Collections.Generic;

class Node_ClassProperty : INode {
	public Node_ClassProperty() {
		throw new Error_Unimplemented();
	}

	public string typeName {
		get { return "class-property"; }
	}
	
	public ICollection<INode> children {
		get { throw new Error_Unimplemented(); }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}