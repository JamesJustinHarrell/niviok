using System.Collections.Generic;

class Node_DeclareFirst : INode_DeclarationAny {
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

	public void getInfo(out string name, out object objs) {
		name = "declare-first";
		objs = new object[]{ _nameNode, _typeNode, _valueNode };
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = Help.getIdentRefs( _typeNode, _valueNode );
			if( _typeNode.category != IdentikeyCategory.FUNCTION )
				idents.Remove( _nameNode.identifier );
			return idents;
		}
	}
}
