using System;
using System.Collections.Generic;

class Scope {
	IDictionary<Identifier, Identikey> _identikeys;
	Bridge _bridge; //null if _parent is not null
	Scope _parent; //null if _bridge is not null
	
	public Scope(Bridge bridge) {
		_identikeys = new Dictionary<Identifier, Identikey>();
		_bridge = bridge;
	}
	
	public Scope(Scope parentScope) {
		_identikeys = new Dictionary<Identifier, Identikey>();
		_parent = parentScope;
	}
	
	public Bridge bridge {
		get { return ( _bridge != null ? _bridge : _parent.bridge ); }
	}
	
	//create copy that only includes identikeys specifiedy by @wantedIdents
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

	//used by Desal "assign" nodes
	public void assign(Identifier ident, IWorker val) {
		if( _identikeys.ContainsKey(ident) )
			_identikeys[ident].value = val;
		else if( _parent != null )
			_parent.assign(ident, val);
		else
			throw new ClientException(String.Format(
				"no identikey with name '{0}'", ident));
	}

	//used by Desal "declare-empty" nodes
	public void declareEmpty(
	Identifier ident, IdentikeyCategory category, NullableType type) {
		_identikeys.Add(
			ident,
			new Identikey(
				category,
				type,
				new Null(type.face)));
	}

	//used by Desal "declare-assign" nodes
	public void declareAssign(
	Identifier ident, IdentikeyCategory category, NullableType type, IWorker val) {
		_identikeys.Add(
			ident,
			new Identikey(
				category,
				type,
				val));
	}
	
	//value expressions in Desal "declare-first" nodes
	//may reference each other, so all identikeys must be created
	//before any of the value expressions are executed
	public void reserveDeclareFirst(
	Identifier ident, IdentikeyCategory category, NullableType type) {
		if( category == IdentikeyCategory.CONSTANT ) {
			if( _identikeys.ContainsKey(ident) )
				throw new ClientException(
					String.Format(
						"identikey with name '{0}' declared multiple " +
						"times in same scope",
						ident));
		
			_identikeys.Add(
				ident,
				new Identikey(
					category,
					type,
					null));
		}
		else if( category == IdentikeyCategory.FUNCTION ) {
			if( _identikeys.ContainsKey(ident) ) {
				if( _identikeys[ident].category != IdentikeyCategory.FUNCTION )
					throw new ClientException(
						String.Format(
							"identifier '{0}' declared as function identikey " +
							"and as constant identikey",
							ident));
			}
			else {
				_identikeys.Add(
					ident,
					new Identikey(
						category,
						null,
						null));
			}
		}
		else {
			throw new ClientException(
				"declare-first nodes must declare constant or function identikeys");
		}
	}
	
	public void setType(Identifier ident, NullableType type) {
		_identikeys[ident].type = type;
	}
	
	//used by Desal "declare-first" nodes
	public void declareFirst(
	Identifier ident, IdentikeyCategory category, NullableType type, IWorker val) {
		if( ! _identikeys.ContainsKey(ident) )
			bridge.printlnWarning(
				String.Format(
					"scope does not contain declare-first identikey named '{0}'",
					ident ));
		_identikeys[ident].value = val;
	}

	public IWorker evaluateIdentifier(Identifier ident) {
		if( _identikeys.ContainsKey(ident) ) {
			Identikey key = _identikeys[ident];
			if( key.value == null )
				return new FutureWorker(key);
			return key.value;
		}
		if( _parent != null )
			return _parent.evaluateIdentifier(ident);
		throw new ClientException("identifier '" + ident + "' is undefined");
	}
	
	//xxx remove?
	public IWorker evaluateLocalIdentifier(Identifier ident) {
		if( _identikeys.ContainsKey(ident) )
			return _identikeys[ident].value;
		throw new ClientException("identifier '" + ident + "' is undefined");
	}
}