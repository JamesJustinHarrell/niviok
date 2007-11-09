using System.Collections.Generic;

class Scope {
	IDictionary<Identifier, Reference> _binds;
	Scope _parent;
	
	public Scope() {
		_binds = new Dictionary<Identifier, Reference>();
	}
	
	public Scope(ref Scope parentScope) {
		_binds = new Dictionary<Identifier, Reference>();
		_parent = parentScope;
	}

	public void declare(Identifier ident, ReferenceType type) {
		throw new Error_Unimplemented();
	}

	public void declareBind(
	Identifier ident, ReferenceType type, bool constant, IValue val ) {
		_binds.Add( ident, new Reference(type, constant, val) );
	}
	
	public void declarePervasive(
	Identifier ident, ReferenceType type, IValue val) {
		_binds.Add( ident, new Reference(type, val) );
	}

	public void bind(Identifier ident, IValue val) {
		_binds[ident].setValue(val);
	}

	public IValue evaluateIdentifier(Identifier ident) {
		if( _binds.ContainsKey(ident) )
			return _binds[ident].@value;
		if( _parent != null )
			return _parent.evaluateIdentifier(ident);
		throw new ClientException( "identifier '" + ident.str + "' is undefined" );
	}
	
	public IValue evaluateLocalIdentifier(Identifier ident) {
		if( _binds.ContainsKey(ident) )
			return _binds[ident].@value;
		throw new ClientException( "identifier '" + ident.str + "' is undefined" );
	}
	
	//xxx not used yet
	public void reserve(Identifier ident, IdentifierCategory cat) {
		//reserve an identifier for a namespace or alias
		throw new Error_Unimplemented();
	}
	
	public void printIdentifiers() {
		foreach( Identifier ident in _binds.Keys ) {
			System.Console.WriteLine("'" + ident.str + "'");
		}
	}
}