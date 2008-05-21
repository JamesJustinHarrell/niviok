using System;
using System.Diagnostics;
using System.Collections.Generic;

class Scope {
	Bridge _bridge; //null if _parent is not null
	Scope _parent; //null if _bridge is not null
	IDictionary<Identifier, Identikey> _identikeys;
	IDictionary<Identifier, Scope> _namespaces;
	bool _isYieldClosure;
	IWorker _yieldValue;
	
	Scope(Bridge bridge, Scope parentScope) {
		_bridge = bridge;
		_parent = parentScope;
		_identikeys = new Dictionary<Identifier, Identikey>();
		_namespaces = new Dictionary<Identifier, Scope>();
		_isYieldClosure = false;
		
		if( _bridge == null && _parent == null )
			throw new NullReferenceException();
	}
	
	public Scope(Bridge bridge) : this(bridge, null) {}
	
	public Scope(Scope parentScope) : this(null, parentScope) {}
	
	public Bridge bridge {
		get { return ( _bridge != null ? _bridge : _parent.bridge ); }
	}
	
	//null (not to be confused with Null) means the last value was accounted for 
	public IWorker yieldValue {
		get {
			if( _isYieldClosure )
				return _yieldValue;
			if( _parent != null )
				return _parent.yieldValue;
			throw new Exception("scope is not a yield closure");
		}
		set {
			if( _isYieldClosure )
				_yieldValue = value;
			else if( _parent != null )
				_parent.yieldValue = value;
			else
				throw new Exception("no yield closure to set value on");
		}
	}
	
	//xxx
	public void printIdentikeys() {
		foreach( Identifier name in _identikeys.Keys )
			Console.WriteLine(name);
	}
	
	//create copy that only includes identikeys specifiedy by @wantedIdents
	public Scope createClosure(ICollection<Identifier> wantedIdents) {
		Scope scope = new Scope(bridge);
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
	
	public Scope createGeneratorClosure(ICollection<Identifier> wantedIdents) {
		Scope rv = createClosure(wantedIdents);
		rv._isYieldClosure = true;
		return rv;
	}
	
	public void declareNamespace(Identifier name, Scope scope) {
		_namespaces.Add(name, scope);
	}
	
	public void expose(IList<Identifier> nsNames) {
		if( nsNames.Count != 1 )
			throw new NotImplementedException();
		Scope ns = _namespaces[nsNames[0]];
		foreach( Identifier name in ns._identikeys.Keys )
			_identikeys.Add(name, ns._identikeys[name]);				
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
			throw new ApplicationException(
				String.Format(
					"declare-first identikey with name '{0}' has not been reserved",
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
		
		#if DEBUG && false
		bridge.print("(note: identikey '" + ident + "' not found in scope with identikeys: "); 
		foreach( Identifier haveIdent in _identikeys.Keys )
			bridge.print(haveIdent + " ");
		bridge.println(")");
		#endif
		
		if( _parent != null )
			return _parent.evaluateIdentifier(ident);
			
		throw new ClientException(
			String.Format(
				"identifier '{0}' is undefined in scope",
				ident));
	}
	
	//xxx remove?
	public IWorker evaluateLocalIdentifier(Identifier ident) {
		if( _identikeys.ContainsKey(ident) )
			return _identikeys[ident].value;
		throw new ClientException("identifier '" + ident + "' is undefined");
	}
}