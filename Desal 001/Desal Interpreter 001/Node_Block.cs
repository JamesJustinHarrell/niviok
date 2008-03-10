using System.Collections.Generic;

class Node_Block : INode_Expression {
	IList<INode_Expression> _members;
	
	/* xxx Could something refer to an outer identifier before declaring its own?
	func foo() {
		println( something ) //outer
		decl something = 123
		println( something ) //inner
	}
	I'm thinking no, but this needs to be speced. However, this would be okay:
	func foo() {
		println( outer:something ) //outer
		decl something = 123
		println( something ) //inner
	}
	*/
	HashSet<Identifier> declaredIdentikeys {
		get {
			HashSet<Identifier> idents = new HashSet<Identifier>();
			foreach( INode_Expression member in _members ) {
				if( member is Node_DeclareEmpty )
					idents.Add( (member as Node_DeclareEmpty).name );
				else if( member is Node_DeclareAssign )
					idents.Add( (member as Node_DeclareAssign).name );
				else if( member is Node_DeclareFirst )
					idents.Add( (member as Node_DeclareFirst).name );
			}
			return idents;
		}
	}

	public Node_Block(IList<INode_Expression> members) {
		_members = members;
	}
	
	public IValue execute(Scope scope) {
		Scope innerScope = new Scope(scope);
		
		//reserve identikeys, so the closures will include
		//the identikeys from other declare-first nodes
		foreach( INode_Expression member in _members )
			if( member is Node_DeclareFirst )
				scope.reserveDeclareFirst( (member as Node_DeclareFirst).name );

		IValue rv = new NullValue();
		foreach( INode_Expression expr in _members ) {
			rv = expr.execute(innerScope);
		}
		return rv;
	}
	
	public string typeName {
		get { return "block"; }
	}
	
	public ICollection<INode> children {
		get { return G.collect<INode>(_members); }
	}
	
	//xxx remove references to identikeys that are defined in this node (besides function identikeys)
	//xxx also, need to always remove references to identikeys that are global (do I?)
	public HashSet<Identifier> identikeyDependencies {
		get {
			HashSet<Identifier> idents = G.depends(_members);
			idents.ExceptWith(declaredIdentikeys);
			return idents;
		}
	}
}