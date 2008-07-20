using System;
using System.Diagnostics;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class Scope : IScope {
	IScope _parent;
	HashSet<IWorker> _exposes;
	HashSet<ISieve> _sieves;
	IDictionary<Identifier, IScidentre> _scidentres;
	ScopeAllowance _allowance;

	public Scope( IScope parent, ScopeAllowance allowance ) {
		_parent = parent;
		_exposes = new HashSet<IWorker>();
		_sieves = new HashSet<ISieve>();
		_scidentres = new Dictionary<Identifier,IScidentre>();
		_allowance = allowance;
		if(_parent == null && _allowance == null)
			throw new Exception("parent and allowance can't both be null");
	}
	
	public void expose(IWorker worker) {
		_exposes.Add(worker);
	}
	
	public void addSieve(ISieve sieve) {
		_sieves.Add(sieve);
	}
	
	//this function is very similar to Sieve::reserveScidentre
	public IScidentre reserveScidentre( Identifier name, ScidentreCategory cat ) {
		if(cat != ScidentreCategory.OVERLOAD)
			if(_scidentres.ContainsKey(name))
				throw new Exception(String.Format(
					"non-overload wo-scidentre '{0}' declared " +
					"multiple times in same scope", name));
		if(_scidentres.ContainsKey(name)) {
			//note that here we know that the category is FUNCTION
			IScidentre ws = _scidentres[name];
			if( !(ws is OverloadScidentre) )
				throw new Exception(String.Format(
					"wo-scidentre '{0}' in same scope declared " +
					"as function and non-function", name));
			(ws as OverloadScidentre).incrementRequiredCount();
		}
		else
			_scidentres.Add(name, (
				cat == ScidentreCategory.CONSTANT ? new ConstantScidentre() as IScidentre :
				cat == ScidentreCategory.OVERLOAD ? new OverloadScidentre() as IScidentre :
				cat == ScidentreCategory.VARIABLE ? new VariableScidentre() as IScidentre :
				null /* xxx throw statement not allowed here */ ));
		return _scidentres[name];
	}

	public void activateScidentre(Identifier name, NType type, IWorker worker) {
		_scidentres[name].type = type;
		_scidentres[name].assign(worker);
	}
	
	public void assign(Identifier name, IWorker worker) {
		if( _scidentres.ContainsKey(name) )
			_scidentres[name].assign(worker);
		else if( _parent != null )
			_parent.assign(name, worker);
		else
			throw new Exception(String.Format(
				"no wo-scidentre found named '{0}'", name));
	}
	
	public DerefResults upDeref(Identifier idents) {
		return GE.commonDeref(
			idents, _scidentres, _sieves, _exposes, _parent);
	}
	
	public HashSet<IScidentre> upFindEmptyScidentres(Identifier idents) { 
		return GE.commonFindEmptyScidentres(
			idents, _scidentres, _sieves, _parent);
	}
	
	public ScopeAllowance allowance {
		get { return (_allowance == null ? _parent.allowance : _allowance); }
	}
}

} //namespace
