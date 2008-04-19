//refers to an identikey that has been reserved
//by a declare-first node, but not yet assigned to
//it is necessary for cyclical references

using System;
using System.Collections.Generic;

class FutureWorker : IWorker {
	Identikey _key;
	IWorker _worker;

	public FutureWorker(Identikey key) {
		if( key.category != IdentikeyCategory.CONSTANT &&
		key.category != IdentikeyCategory.FUNCTION )
			throw new ArgumentException("invalid category of identikey");
		_key = key;
	}

	void requireWorker() {
		if( _key.value == null )
			throw new Exception(
				"identikey created by declare-first not yet bound to a value");
		_worker = _key.value;
	}

	public IObject owner {
		get {
			requireWorker();
			return _worker.owner;
		}
	}
	
	public IList<IWorker> children {
		get {
			requireWorker();
			return _worker.children;
		}
	}
	
	public IWorker face {
		get {
			if(_worker != null)
				return _worker.face;
			if(_key.type.face != null)
				return _key.type.face;
			requireWorker();
			return _worker.face;
		}
	}
	
	public IWorker breed(IInterface face) {
		requireWorker();
		return _worker.breed(face);
	}
	
	public IWorker call(IList<Argument> arguments) {
		requireWorker();
		return _worker.call(arguments);
	}
	
	public IWorker extractMember(Identifier name) {
		requireWorker();
		return _worker.extractMember(name);
	}		
	
	public void setProperty(Identifier propName, IWorker worker) {
		requireWorker();
		_worker.setProperty(propName, worker);
	}
}
