using System.Collections.Generic;

class Node_DeclareEmpty : INode_DeclarationAny {
	Node_Identifier _name;
	Node_IdentikeyType _type;

	public Node_DeclareEmpty( Node_Identifier name, Node_IdentikeyType type ) {
		_name = name;
		_type = type;
	}
	
	public Identifier name {
		get { return _name.identifier; }
	}

	public IValue execute(Scope scope) {
		scope.declareEmpty(_name.identifier);
		return new NullValue();
	}
	
	public void getInfo(out string name, out object children) {
		name = "declare-empty";
		children = new object[]{ _name, _type };
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return Help.getIdentRefs( _name, _type ); }
	}
}