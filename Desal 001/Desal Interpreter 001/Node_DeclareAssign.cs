using System.Collections.Generic;

class Node_DeclareAssign : INode_DeclarationAny, INode_Expression {
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
	
	public void getInfo(out string name, out object children) {
		name = "declare-assign";
		children = new object[]{ _name, _type, _value };
	}

	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = Help.getIdentRefs( _type, _value );
			if( _type.category != IdentikeyCategory.FUNCTION )
				idents.Remove( _name.identifier );
			return idents;
		}
	}
}