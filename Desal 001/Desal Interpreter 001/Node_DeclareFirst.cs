using System.Collections.Generic;

class Node_DeclareFirst : INode_DeclareAny {
	Node_Identifier _nameNode;
	Node_IdentikeyType _typeNode;
	INode_Expression _valueNode;
	
	public Node_DeclareFirst(
	Node_Identifier identNode, Node_IdentikeyType typeNode, INode_Expression valueNode) {
		_nameNode = identNode;
		_typeNode = typeNode;
		_valueNode = valueNode;
	}
	
	public Identifier name {
		get { return _nameNode.identifier; }
	}
	
	public IValue execute(Scope scope) {
		IValue val = _valueNode.execute(scope);
		scope.declareFirst(
			_nameNode.identifier,
			//xxx _typeNode.evaluateType(scope),
			val );
		return val;
	}
	
	public string typeName {
		get { return "declare-first"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _nameNode, _typeNode, _valueNode }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = G.depends( _typeNode, _valueNode );
			if( _typeNode.category != IdentikeyCategory.FUNCTION )
				idents.Remove( _nameNode.identifier );
			return idents;
		}
	}
}
