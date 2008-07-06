using System;
using System.Collections.Generic;
using System.Diagnostics;

class DerefResults {
	IWorker _worker;
	IList<IWorker> _workerList;

	public DerefResults( IWorker worker, IList<IWorker> workerList ) {
		_worker = worker;
		_workerList = workerList;
		Debug.Assert( _worker == null || _workerList == null );
		Debug.Assert( _workerList == null || _workerList.Count > 0 );
	}
	
	public void Add( DerefResults other ) {
		if(other.worker != null) {
			if(_worker != null)
				throw new Exception("cannot add worker because already have worker");
			if(_workerList != null)
				throw new Exception("cannot add worker because already have workerList");
			_worker = other.worker;
		}
		if(other.workerList != null) {
			if(_worker != null)
				throw new Exception("cannot add workerList because already have worker");
			if(_workerList == null)
				_workerList = other.workerList;
			else
				G.addEach(_workerList, other.workerList);
		}
	}

	public IWorker worker {
		get { return _worker; }
	}
	
	public IList<IWorker> workerList {
		get { return _workerList; }
	}
}
