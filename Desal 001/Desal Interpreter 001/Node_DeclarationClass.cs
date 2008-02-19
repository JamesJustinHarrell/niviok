//a declaration-class node, which combines a declaration node and an access node

using System.Collections.Generic;

class Node_DeclarationClass : INode_DeclarationAny {
	INode_DeclarationAny _declaration;

	public Node_DeclarationClass() {
	}
	
	public INode_DeclarationAny decl {
		get { return _declaration; }
	}
	
	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
	}
	
	public void getInfo(out string name, out object children) {
		throw new Error_Unimplemented();
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}