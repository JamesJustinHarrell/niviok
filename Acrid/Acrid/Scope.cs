using System;
using System.Diagnostics;
using System.Collections.Generic;

class Scope {
	Bridge _bridge; //null if _parent is not null
	Scope _parent; //null if _bridge is not null
	IDictionary<Identifier, Identikey> _identikeys;
	IDictionary<Identifier, Namespace> _namespaces;
	bool _isYieldClosure;
	IWorker _yieldValue;
	
	Scope(Bridge bridge, Scope parentScope) {
		_bridge = bridge;
		_parent = parentScope;
		_identikeys = new Dictionary<Identifier, Identikey>();
		_namespaces = new Dictionary<Identifier, Namespace>();
		_isYieldClosure = false;
		
		if( _bridge == null && _parent == null )
			throw new NullReferenceException();
	}
	
	public Scope(Bridge bridge) : this(bridge, null) {}
	
	public Scope(Scope parentScope) : this(null, parentScope) {}
	
	public Bridge bridge {
		get { return ( _bridge != null ? _bridge : _parent.bridge ); }
	}
	
	public Scope parent {
		get { return _parent; }
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
				if( wantedIdents.Contains(oldIdent) && ! scope._identikeys.ContainsKey(oldIdent) )
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
	
	public void declareNamespace(Identifier name, Namespace ns) {
		_namespaces.Add(name, ns);
	}
	
	public void expose(IList<Identifier> nsNames) {
		if( nsNames.Count != 1 )
			throw new NotImplementedException();
		
		//get namespace
		Namespace ns = null;
		{
			Identifier nsName = nsNames[0];
			Scope current = this;
			while( current != null ) {
				if( current._namespaces.ContainsKey(nsName) ) {
		 			ns = current._namespaces[nsName];
		 			break;
		 		}
				current = current._parent;
			}
			if( ns == null ) {
				throw new ClientException(
					String.Format(
						"no namespace named {0} in this scope",
						nsName));
			}
		}

		foreach( Identifier name in ns.scope._identikeys.Keys )
			_identikeys.Add(name, ns.scope._identikeys[name]);				
	}

	//used by "assign" nodes
	public void assign(Identifier ident, IWorker val) {
		if( _identikeys.ContainsKey(ident) )
			_identikeys[ident].value = val;
		else if( _parent != null )
			_parent.assign(ident, val);
		else
			throw new ClientException(String.Format(
				"no identikey with name '{0}'", ident));
	}

	//used by "declare-empty" nodes
	public void declareEmpty(
	Identifier ident, IdentikeyCategory category, NullableType type) {
		_identikeys.Add(
			ident,
			new Identikey(
				category,
				type,
				new Null(type.face)));
	}

	//used by "declare-assign" nodes
	public void declareAssign(
	Identifier ident, IdentikeyCategory category, NullableType type, IWorker val) {
		_identikeys.Add(
			ident,
			new Identikey(
				category,
				type,
				val));
	}
	
	//value expressions in "declare-first" nodes
	//may reference each other, so all identikeys must be created
	//before any of the value expressions are executed
	public void reserveDeclareFirst(Identifier ident, IdentikeyCategory category) {
		if( category == IdentikeyCategory.CONSTANT ) {
			if( _identikeys.ContainsKey(ident) )
				throw new ClientException(
					String.Format(
						"constant identikey with name '{0}' declared multiple " +
						"times in same scope",
						ident));
		
			_identikeys.Add(ident, new Identikey(category, null, null));
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
				_identikeys.Add(ident, new Identikey(category, null, null));
			}
		}
		else {
			throw new ClientException(
				"declare-first nodes must declare constant or function identikeys");
		}
	}
	
	public void setType(Identifier ident, NullableType type) {
		Identikey key = _identikeys[ident];
		if( key.type != null )
			throw new ApplicationException(
				"attempt to set nullable-type of identikey more than once");
		key.type = type;
	}
	
	//used by "declare-first" nodes
	public void declareFirst(Identifier ident, IWorker val) {
		if( ! _identikeys.ContainsKey(ident) )
			throw new ApplicationException(
				String.Format(
					"declare-first identikey with name '{0}' has not been reserved",
					ident ));
		_identikeys[ident].value = val;
	}

	public IWorker evaluateIdentifier(Identifier ident) {
		//xxx duplication -- for now keep synchonized with evaluateLocalIdentifier
		if( _identikeys.ContainsKey(ident) ) {
			Identikey key = _identikeys[ident];
			if( key.value == null ) {
				throw new ApplicationException(
					"evaluating declare-first identikey that hasn't been assigned to yet");
			}
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
	
	public IWorker evaluateLocalIdentifier(Identifier ident) {
		//xxx duplication -- for now keep synchonized with evaluateLocalIdentifier
		if( _identikeys.ContainsKey(ident) ) {
			Identikey key = _identikeys[ident];
			if( key.value == null ) {
				throw new ApplicationException(
					"evaluating declare-first identikey that hasn't been assigned to yet");
			}
			return key.value;
		}

		throw new ClientException("identifier '" + ident + "' is undefined");
	}
	
	public bool isReserved(Identifier ident) {
		return _identikeys.ContainsKey(ident) && _identikeys[ident].value == null;
	}
	
	public Identikey getReservedIdentikey(Identifier ident) {
		Debug.Assert(isReserved(ident));
		return _identikeys[ident];
	}
}