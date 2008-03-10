using System.Collections.Generic;

class Node_DeclareConstEmpty : INode_DeclareAny {
	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
	}
	
	public string typeName {
		get { return "declare-const-empty"; }
	}
	
	public ICollection<INode> children {
		get { throw new Error_Unimplemented(); }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}