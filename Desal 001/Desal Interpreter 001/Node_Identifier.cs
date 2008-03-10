using System.Collections.Generic;

class Node_Identifier : INode_Expression {
	Identifier _identifier;

	public Node_Identifier(Identifier identifier) {
		_identifier = identifier;
	}
	
	public Identifier identifier {
		get { return _identifier; }
	}
	
	public IValue execute(Scope scope) {
		return scope.evaluateIdentifier(_identifier);
	}
	
	public string typeName {
		get { return "identifier"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{}; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = new HashSet<Identifier>();
			idents.Add(_identifier);
			return idents;
		}
	}
	
	public override string ToString() {
		return _identifier.ToString();
	}
}