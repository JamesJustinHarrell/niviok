using System;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

//xxx if this is only used by Sieve, it should be renamed __SieveScope
//otherwise, it should be moved to its own file 
class SieveScope : IScope {
	IScope _parent;
	Sieve _sieve;
	bool _visible;
	
	public SieveScope(IScope parent, Sieve sieve, bool visible) {
		_parent = parent;
		_sieve = sieve;
		_visible = visible;
	}

	public void expose(IDerefable d) {
		if(_visible)
			throw new NotImplementedException();
		else
			_sieve.expose(d);
	}
	
	public void bindNamespace( Identifier name, IDerefable d ) {
		_sieve.bindNamespace( _visible, name, d );
	}
	
	public IWoScidentre reserveWoScidentre(
	Identifier name, WoScidentreCategory cat ) {
		return _sieve.reserveWoScidentre( _visible, name, cat );
	}
	
	public void activateWoScidentre(Identifier name, NType type, IWorker worker) {
		throw new NotImplementedException();
	}
	
	public void assign(Identifier name, IWorker worker) {
		throw new NotImplementedException();
	}
	
	public DerefResults xxxNsUpDeref(IdentifierSequence idents) {
		return _sieve.xxxNsUpDeref(idents);
	}
	
	public DerefResults upDeref(IdentifierSequence idents) {
		return _sieve.upDeref(idents);
	}
	
	public HashSet<IWoScidentre> upFindEmptyWoScidentres(IdentifierSequence idents) {
		return _sieve.upFindEmptyWoScidentres(idents);
	}
	
	public ScopeAllowance allowance {
		get { return _parent.allowance; }
	}
}

public class Sieve : IDerefable {
	IScope _parent;
	HashSet<IDerefable> _exposes; //all hidden
	IDictionary<Identifier,NsScidentre> _visibleNsScidentres;
	IDictionary<Identifier,NsScidentre> _allNsScidentres;
	IDictionary<Identifier,IWoScidentre> _visibleWoScidentres;
	IDictionary<Identifier,IWoScidentre> _hiddenWoScidentres;
	SieveScope _visibleScope;
	SieveScope _hiddenScope;

	public Sieve(IScope parent) {
		_parent = parent;
		_exposes = new HashSet<IDerefable>();
		_visibleNsScidentres = new Dictionary<Identifier,NsScidentre>();
		_allNsScidentres = new Dictionary<Identifier,NsScidentre>();
		_visibleWoScidentres = new Dictionary<Identifier,IWoScidentre>();
		_hiddenWoScidentres = new Dictionary<Identifier,IWoScidentre>();
		_hiddenScope = new SieveScope(_parent, this, false);
		_visibleScope = new SieveScope(_parent, this, true);
	}
	
	void isInEither<T>(
	IDictionary<Identifier,T> a,
	IDictionary<Identifier,T> b,
	Identifier name,
	string message) {
		if(a.ContainsKey(name) || b.ContainsKey(name))
			throw new Exception(message);
	}
	
	public void expose(IDerefable d) {
		_exposes.Add(d);
	}

	void bindNamespace(
	IDictionary<Identifier,NsScidentre> dict, Identifier name, IDerefable d ) {
		if(! dict.ContainsKey(name))
			dict.Add(name, new NsScidentre());
		dict[name].bind(d);
	}
	
	public void bindNamespace(bool visible, Identifier name, IDerefable d) {
		isInEither<IWoScidentre>(
			_visibleWoScidentres,
			_hiddenWoScidentres,
			name,
			"scidentre declared as worker and namespace");
		bindNamespace( _allNsScidentres, name, d );
		if(visible)
			bindNamespace( _visibleNsScidentres, name, d );
	}
	
	//this function is very similar to Scope::reserveWoScidentre
	private IWoScidentre reserveWoScidentre(
	IDictionary<Identifier,IWoScidentre> dict, Identifier name, WoScidentreCategory cat ) {
		if( dict.ContainsKey(name) ) {
			//note that here we know that the category is FUNCTION
			IWoScidentre ws = dict[name];
			(ws as OverloadScidentre).incrementRequiredCount();
		}
		else
			dict.Add(name, (
				cat == WoScidentreCategory.CONSTANT ? new ConstantScidentre() as IWoScidentre :
				cat == WoScidentreCategory.OVERLOAD ? new OverloadScidentre() as IWoScidentre :
				cat == WoScidentreCategory.VARIABLE ? new VariableScidentre() as IWoScidentre :
				null /* xxx throw statement not allowed here */ ));
		return dict[name];
	}
	
	public IWoScidentre reserveWoScidentre(
	bool visible, Identifier name, WoScidentreCategory cat ) {
		if(_allNsScidentres.ContainsKey(name))
			throw new Exception("scidentre declared as worker and namespace");
		if(cat == WoScidentreCategory.OVERLOAD)
			if(
			( _hiddenWoScidentres.ContainsKey(name) &&
			!(_hiddenWoScidentres[name] is OverloadScidentre) ) ||
			( _visibleWoScidentres.ContainsKey(name) &&
			!(_visibleWoScidentres[name] is OverloadScidentre) ) )
				throw new Exception(
					"scidentre in same scope declared as overload and non-overload");
		else
			isInEither<IWoScidentre>(
				_visibleWoScidentres,
				_hiddenWoScidentres,
				name,
				"non-function wo-scidentre declared multiple times in same scope");
		return reserveWoScidentre(
			visible ? _visibleWoScidentres : _hiddenWoScidentres, name, cat );
	}
	
	public DerefResults deref(IdentifierSequence idents) {
		return GE.commonDeref(
			idents, _visibleWoScidentres, _visibleNsScidentres, _exposes, null);
	}
	
	private IDictionary<Identifier,IWoScidentre> addWoScidentres() {
		IDictionary<Identifier,IWoScidentre> woScidentres =
			new Dictionary<Identifier,IWoScidentre>(_hiddenWoScidentres);
		foreach(Identifier name in _visibleWoScidentres.Keys) {
			IWoScidentre visibleWS = _visibleWoScidentres[name];
			if( (visibleWS is OverloadScidentre) && woScidentres.ContainsKey(name) ) {
				OverloadScidentre allWS = new OverloadScidentre();
				IEnumerable<IWorker> workerList =
					G.join(
						visibleWS.deref().workerList,
						woScidentres[name].deref().workerList);
				foreach(IWorker worker in workerList) {
					allWS.incrementRequiredCount();
					allWS.assign(worker);
				}
				woScidentres[name] = allWS;
			}
			else
				woScidentres.Add(name, visibleWS);
		}
		return woScidentres;
	}
	
	public DerefResults xxxNsUpDeref(IdentifierSequence idents) {
		return GE.commonDeref(
			idents, addWoScidentres(), _allNsScidentres, null, _parent);
	}
	
	public DerefResults upDeref(IdentifierSequence idents) {
		return GE.commonDeref(
			idents, addWoScidentres(), _allNsScidentres, _exposes, _parent);
	}
	
	public HashSet<IWoScidentre> findEmptyWoScidentres(IdentifierSequence idents) {
		return GE.commonFindEmptyWoScidentres(
			idents, _visibleWoScidentres, _visibleNsScidentres, null, null);
	}
	
	public HashSet<IWoScidentre> upFindEmptyWoScidentres(IdentifierSequence idents) {
		return GE.commonFindEmptyWoScidentres(
			idents, addWoScidentres(), _allNsScidentres, _exposes, _parent);
	}

	public IScope visible {
		get { return _visibleScope; }
	}
	
	public IScope hidden {
		get { return _hiddenScope; }
	}
}

} //namespace
