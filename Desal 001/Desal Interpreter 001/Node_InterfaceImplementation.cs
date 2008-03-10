using System.Collections.Generic;

class Node_InterfaceImplementation : INode {
	public Node_InterfaceImplementation() {
		throw new Error_Unimplemented();
	}
	
	public string typeName {
		get { return "interface-implementation"; }
	}
	
	public ICollection<INode> children {
		get { throw new Error_Unimplemented(); }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}
