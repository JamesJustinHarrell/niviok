using System.Collections.Generic;

/*
Note: The spec defines something like this:
	class Identikey { Identifier, List<Value> }
	class Scope { Collection<Identikey> }
But this agent does this:
	class Identikey { List<Value> }
	class Scope { Dictionary<Identifier, Identikey> }
The effect should be the same, but internally it's slightly different.
*/

class Identikey {
	/*
	if this is a function identikey,
	the active interface of this value will be the sum
	of the active interfaces of the functions bound to this identikey
	*/
	IValue _value;
	
	public Identikey( IValue val ) {
		_value = val;
	}
	
	public IValue val {
		get { return _value; }
		set { _value = value; }
	}
}

class Scope {
	IDictionary<Identifier, Identikey> _identikeys;
	Bridge _bridge;
	Scope _parent;
	
	public Scope(Bridge bridge) {
		_identikeys = new Dictionary<Identifier, Identikey>();
		_bridge = bridge;
	}
	
	public Scope(Scope parentScope) {
		_identikeys = new Dictionary<Identifier, Identikey>();
		_parent = parentScope;
	}
	
	//create copy that only includes identikeys specifiedy by @idents
	public Scope createClosure(ICollection<Identifier> wantedIdents) {
		Scope scope = new Scope(_bridge);
		Scope currentOld = this;
		while( currentOld != null ) {
			foreach( Identifier oldIdent in currentOld._identikeys.Keys ) {
				if( wantedIdents.Contains(oldIdent) )
					scope._identikeys.Add( oldIdent, currentOld._identikeys[oldIdent] );
			}
			currentOld = currentOld._parent;
		}
		return scope;
	}

	public void assign(Identifier ident, IValue val) {
		_identikeys[ident].val = val;
	}

	public void declareEmpty(Identifier ident) {
		_identikeys.Add( ident, new Identikey(null) );
	}

	public void declareAssign(
	Identifier ident, IValue val ) {
		_identikeys.Add( ident, new Identikey(val) );
	}
	
	//all declare-first identikeys in a node must be declared
	public void reserveDeclareFirst(Identifier ident) {
		_identikeys.Add( ident, new Identikey(null) );
	}
	
	//xxx need to do checking to make sure this is being assigned to a declare-first node
	//also need to ensure and check lots of other stuff
	public void declareFirst(Identifier ident, IValue val) {
		if( ! _identikeys.ContainsKey(ident) )
			System.Console.WriteLine(
				"WARNING: scope does not contain declare-first identikey named {0}",
				ident.ToString() );
		_identikeys[ident].val = val;
	}

	public IValue evaluateIdentifier(Identifier ident) {
		if( _identikeys.ContainsKey(ident) )
			return _identikeys[ident].val;
		if( _parent != null )
			return _parent.evaluateIdentifier(ident);
		throw new ClientException( "identifier '" + ident.ToString() + "' is undefined" );
	}
	
	public IValue evaluateLocalIdentifier(Identifier ident) {
		if( _identikeys.ContainsKey(ident) )
			return _identikeys[ident].val;
		throw new ClientException( "identifier '" + ident.ToString() + "' is undefined" );
	}
	
	/* xxx not used yet
	public void reserve(Identifier ident, IdentifierCategory cat) {
		//reserve an identifier for a namespace or alias
		throw new Error_Unimplemented();
	}
	
	//xxx should this use bridge?
	public void printIdentifiers() {
		foreach( Identifier ident in _binds.Keys ) {
			System.Console.WriteLine("'" + ident.str + "'");
		}
	}
	*/
	
	public Bridge bridge {
		get { return ( _bridge != null ? _bridge : _parent.bridge ); }
	}
}