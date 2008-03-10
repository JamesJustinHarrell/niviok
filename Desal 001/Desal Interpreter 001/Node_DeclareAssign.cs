using System.Collections.Generic;

class Node_DeclareAssign : INode_DeclareAny, INode_Expression {
	Node_Identifier _name;
	Node_IdentikeyType _type;
	INode_Expression _value;

	public Node_DeclareAssign(
	Node_Identifier name, Node_IdentikeyType type, INode_Expression val ) {		
		_name = name;
		_type = type;
		_value = val;
	}
	
	public Identifier name {
		get { return _name.identifier; }
	}
	
	public IValue execute(Scope scope) {
		IValue val = _value.execute(scope);
		
		scope.declareAssign(
			_name.identifier,
			//xxx ( _type == null ? null : _type.evaluateType(scope) ),
			//xxx ( _const == null ? false : _const.val ),
			val );
		return val;
	}
	
	public string typeName {
		get { return "declare-assign"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _name, _type, _value }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = G.depends( _type, _value );
			if( _type.category != IdentikeyCategory.FUNCTION )
				idents.Remove( _name.identifier );
			return idents;
		}
	}
}