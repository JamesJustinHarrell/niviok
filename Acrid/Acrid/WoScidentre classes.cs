using System;
using System.Collections.Generic;

class __WoScidentreBase {
	IType _type;
	
	public IType type {
		get { return _type; }
		set {
			if(value == null) { throw new NullReferenceException(); }
			if(_type != null) { throw new Exception("type already set"); }
			_type = value;
		}
	}
}

class __SingleWoScidentreBase : __WoScidentreBase {
	protected IWorker _worker;
	
	protected bool ready() {
		return _worker != null;
	}
	
	public DerefResults deref() {
		if(! ready()) { throw new Exception("single wo-scidentre not ready"); }
		return new DerefResults(_worker, null);
	}
}

class FunctionScidentre : __WoScidentreBase, IWoScidentre {
	int _requiredCount;
	IList<IWorker> _workers;
	
	public FunctionScidentre() {
		_requiredCount = 1;
		_workers = new List<IWorker>();
	}
	
	bool ready() {
		return _workers.Count == _requiredCount;
	}
	
	public void incrementRequiredCount() {
		if(_workers.Count != 0) { throw new Exception("workers already added"); }
		_requiredCount++;
	}
	
	public void assign(IWorker w) {
		if(w == null) { throw new Exception(); }
		if(ready()) { throw new Exception("function scidentre full"); }
		_workers.Add( w ); //xxx downcast(w, _type) );
	}
	
	public DerefResults deref()  {
		if(! ready()) { throw new Exception("function scidentre not ready"); }
		return new DerefResults(null, _workers);
	}
}

class ConstantScidentre : __SingleWoScidentreBase, IWoScidentre {
	public void assign(IWorker worker) {
		if(worker == null) { throw new Exception("argument @worker is null"); }
		if(ready()) { throw new Exception("constant scidentre already has value"); }
		_worker = worker;
	}
}

class VariableScidentre : __SingleWoScidentreBase, IWoScidentre {
	public void assign(IWorker worker) {
		if(worker == null) { throw new Exception(); }
		_worker = worker;
	}
}
