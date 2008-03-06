using System.Collections.Generic;

class Node_Identifier : INode_Expression {
	Bridge _bridge;
	Identifier _identifier;

	public Node_Identifier(Bridge bridge, Identifier identifier) {
		_bridge = bridge;
		_identifier = identifier;
	}
	
	public Identifier identifier {
		get { return _identifier; }
	}
	
	public IValue execute(Scope scope) {
		return scope.evaluateIdentifier(_identifier);
	}
	
	public void getInfo(out string name, out object objs) {
		name = "identifier";
		objs = _identifier.str;
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = new HashSet<Identifier>();
			idents.Add(_identifier);
			return idents;
		}
	}
}