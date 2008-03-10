//a declaration-class node, which combines a declaration node and an access node

using System.Collections.Generic;

class Node_DeclareClass : INode_DeclareAny {
	INode_DeclareAny _declaration;

	public Node_DeclareClass() {
	}
	
	public INode_DeclareAny decl {
		get { return _declaration; }
	}
	
	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
	}
	
	public string typeName {
		get { return "declare-class"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _declaration }; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}