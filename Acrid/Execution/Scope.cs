using System;
using System.Diagnostics;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class Scope : IScope {
	IScope _parent;
	HashSet<IDerefable> _exposes;
	IDictionary<Identifier, NsScidentre> _nsScidentres;
	IDictionary<Identifier, IWoScidentre> _woScidentres;
	ScopeAllowance _allowance;

	public Scope( IScope parent, ScopeAllowance allowance ) {
		_parent = parent;
		_exposes = new HashSet<IDerefable>();
		_nsScidentres = new Dictionary<Identifier,NsScidentre>();
		_woScidentres = new Dictionary<Identifier,IWoScidentre>();
		_allowance = allowance;
		if(_parent == null && _allowance == null)
			throw new Exception("parent and allowance can't both be null");
	}
	
	public void expose(IDerefable d) {
		_exposes.Add(d);
	}
	
	public void bindNamespace( Identifier i, IDerefable d ) {
		if( ! _nsScidentres.ContainsKey(i) )
			_nsScidentres.Add(i, new NsScidentre());
		_nsScidentres[i].bind(d);
	}
	
	//this function is very similar to Sieve::reserveWoScidentre
	public IWoScidentre reserveWoScidentre( Identifier name, WoScidentreCategory cat ) {
		if(_nsScidentres.ContainsKey(name) )
			throw new Exception(String.Format(
				"scidentre '{0}' declared as worker and namespace", name));
		if(cat != WoScidentreCategory.FUNCTION)
			if(_woScidentres.ContainsKey(name))
				throw new Exception(String.Format(
					"non-function wo-scidentre '{0}' declared " +
					"multiple times in same scope", name));
		if(_woScidentres.ContainsKey(name)) {
			//note that here we know that the category is FUNCTION
			IWoScidentre ws = _woScidentres[name];
			if( !(ws is FunctionScidentre) )
				throw new Exception(String.Format(
					"wo-scidentre '{0}' in same scope declared " +
					"as function and non-function", name));
			(ws as FunctionScidentre).incrementRequiredCount();
		}
		else
			_woScidentres.Add(name, (
				cat == WoScidentreCategory.FUNCTION ? new FunctionScidentre() as IWoScidentre :
				cat == WoScidentreCategory.CONSTANT ? new ConstantScidentre() as IWoScidentre :
				cat == WoScidentreCategory.VARIABLE ? new VariableScidentre() as IWoScidentre :
				null /* xxx throw statement not allowed here */ ));
		return _woScidentres[name];
	}

	public void activateWoScidentre(Identifier name, NType type, IWorker worker) {
		_woScidentres[name].type = type;
		_woScidentres[name].assign(worker);
	}
	
	public void assign(Identifier name, IWorker worker) {
		if( _woScidentres.ContainsKey(name) )
			_woScidentres[name].assign(worker);
		else if( _parent != null )
			_parent.assign(name, worker);
		else
			throw new Exception(String.Format(
				"no wo-scidentre found named '{0}'", name));
	}
	
	public DerefResults upDeref(IdentifierSequence idents) {
		return GE.commonDeref(
			idents, _woScidentres, _nsScidentres, _exposes, _parent);
	}
	
	public HashSet<IWoScidentre> upFindEmptyWoScidentres(IdentifierSequence idents) { 
		return GE.commonFindEmptyWoScidentres(
			idents, _woScidentres, _nsScidentres, _exposes, _parent);
	}
	
	public ScopeAllowance allowance {
		get { return (_allowance == null ? _parent.allowance : _allowance); }
	}
}

} //namespace
