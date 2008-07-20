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
	
	public void expose(IWorker worker) {
		if( _visible )
			throw new NotImplementedException();
		else
			_sieve.expose(worker);
	}

	public void addSieve(ISieve sieve) {
		_sieve.addSieve(_visible, sieve);
	}
	
	public IScidentre reserveScidentre(
	Identifier name, ScidentreCategory cat ) {
		return _sieve.reserveScidentre( _visible, name, cat );
	}
	
	public void activateScidentre(Identifier name, NType type, IWorker worker) {
		throw new NotImplementedException();
	}
	
	public void assign(Identifier name, IWorker worker) {
		throw new NotImplementedException();
	}
	
	public DerefResults upDeref(Identifier idents) {
		return _sieve.upDeref(idents);
	}
	
	public HashSet<IScidentre> upFindEmptyScidentres(Identifier idents) {
		return _sieve.upFindEmptyScidentres(idents);
	}
	
	public ScopeAllowance allowance {
		get { return _parent.allowance; }
	}
}

public class Sieve : ISieve {
	IScope _parent;
	HashSet<IWorker> _exposes; //all hidden
	HashSet<ISieve> _visibleSieves;
	HashSet<ISieve> _hiddenSieves;
	IDictionary<Identifier,IScidentre> _visibleScidentres;
	IDictionary<Identifier,IScidentre> _hiddenScidentres;
	SieveScope _visibleScope;
	SieveScope _hiddenScope;

	public Sieve(IScope parent) {
		_parent = parent;
		_exposes = new HashSet<IWorker>();
		_visibleSieves = new HashSet<ISieve>();
		_hiddenSieves = new HashSet<ISieve>();
		_visibleScidentres = new Dictionary<Identifier,IScidentre>();
		_hiddenScidentres = new Dictionary<Identifier,IScidentre>();
		_hiddenScope = new SieveScope(_parent, this, false);
		_visibleScope = new SieveScope(_parent, this, true);
	}
	
	private void isInEither<T>(
	IDictionary<Identifier,T> a,
	IDictionary<Identifier,T> b,
	Identifier name,
	string message) {
		if(a.ContainsKey(name) || b.ContainsKey(name))
			throw new Exception(message);
	}
	
	private IDictionary<Identifier,IScidentre> addScidentres() {
		IDictionary<Identifier,IScidentre> scidentres =
			new Dictionary<Identifier,IScidentre>(_hiddenScidentres);
		foreach(Identifier name in _visibleScidentres.Keys) {
			IScidentre visibleWS = _visibleScidentres[name];
			if( (visibleWS is OverloadScidentre) && scidentres.ContainsKey(name) ) {
				OverloadScidentre allWS = new OverloadScidentre();
				IEnumerable<IWorker> workerList =
					G.join(
						visibleWS.deref().workerList,
						scidentres[name].deref().workerList);
				foreach(IWorker worker in workerList) {
					allWS.incrementRequiredCount();
					allWS.assign(worker);
				}
				scidentres[name] = allWS;
			}
			else
				scidentres.Add(name, visibleWS);
		}
		return scidentres;
	}
	
	private HashSet<ISieve> addSieves() {
		HashSet<ISieve> rv = new HashSet<ISieve>();
		rv.UnionWith(_visibleSieves);
		rv.UnionWith(_hiddenSieves);
		return rv;
	}
	
	//this function is very similar to Scope::reserveScidentre
	private IScidentre reserveScidentre(
	IDictionary<Identifier,IScidentre> dict, Identifier name, ScidentreCategory cat ) {
		if( dict.ContainsKey(name) ) {
			//note that here we know that the category is FUNCTION
			IScidentre ws = dict[name];
			(ws as OverloadScidentre).incrementRequiredCount();
		}
		else
			dict.Add(name, (
				cat == ScidentreCategory.CONSTANT ? new ConstantScidentre() as IScidentre :
				cat == ScidentreCategory.OVERLOAD ? new OverloadScidentre() as IScidentre :
				cat == ScidentreCategory.VARIABLE ? new VariableScidentre() as IScidentre :
				null /* xxx throw statement not allowed here */ ));
		return dict[name];
	}

	public IScope visible {
		get { return _visibleScope; }
	}
	
	public IScope hidden {
		get { return _hiddenScope; }
	}
	
	//----- related to the IScope interface
	
	public void expose(IWorker worker) {
		_exposes.Add(worker);
	}
	
	public void addSieve(bool visible, ISieve sieve) {
		(visible ? _visibleSieves : _hiddenSieves).Add(sieve);
	}
	
	public IScidentre reserveScidentre(
	bool visible, Identifier name, ScidentreCategory cat ) {
		if(cat == ScidentreCategory.OVERLOAD)
			if(
			( _hiddenScidentres.ContainsKey(name) &&
			!(_hiddenScidentres[name] is OverloadScidentre) ) ||
			( _visibleScidentres.ContainsKey(name) &&
			!(_visibleScidentres[name] is OverloadScidentre) ) )
				throw new Exception(
					"scidentre in same scope declared as overload and non-overload");
		else
			isInEither<IScidentre>(
				_visibleScidentres,
				_hiddenScidentres,
				name,
				"non-overload scidentre already declared");
		return reserveScidentre(
			visible ? _visibleScidentres : _hiddenScidentres, name, cat );
	}
	
	public DerefResults upDeref(Identifier name) {
		return GE.commonDeref(
			name, addScidentres(), addSieves(), _exposes, _parent);
	}
	
	public HashSet<IScidentre> upFindEmptyScidentres(Identifier name) {
		return GE.commonFindEmptyScidentres(
			name, addScidentres(), addSieves(), _parent);
	}
	
	//----- members of the ISieve interface
	
	public DerefResults deref(Identifier name) {
		return GE.commonDeref(
			name, _visibleScidentres, _visibleSieves, null, null);
	}
	
	public HashSet<IScidentre> findEmptyScidentres(Identifier name) {
		return GE.commonFindEmptyScidentres(
			name, _visibleScidentres, _visibleSieves, null);
	}
	
	public HashSet<Identifier> visibleScidentreNames {
		get {
			HashSet<Identifier> names = new HashSet<Identifier>();
			names.UnionWith(_visibleScidentres.Keys);
			foreach( ISieve sieve in _visibleSieves )
				names.UnionWith( sieve.visibleScidentreNames );
			return names;
		}
	}
}

} //namespace
