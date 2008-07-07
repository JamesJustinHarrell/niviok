using System;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

abstract class WorkerBase : Worker_NativeObject {
	protected IInterface _face;
	protected IObject _owner;
	protected IList<IWorker> _childWorkers;
	protected IDictionary<IInterface, IFunction> _breeders;
	protected IDictionary<IInterface, IFunction> _callees;
	protected IDictionary<Identifier, IFunction> _propGetters;
	protected IDictionary<Identifier, IFunction> _propSetters;
	protected IDictionary<Identifier, IDictionary<IInterface, IFunction>> _methods;
	
	public WorkerBase() {}
	
	public WorkerBase(WorkerBase data) {
		_face = data._face;
		_owner = data._owner;
		_childWorkers = data._childWorkers;
		_breeders = data._breeders;
		_callees = data._callees;
		_propGetters = data._propGetters;
		_propSetters = data._propSetters;
		_methods = data._methods;
	}
}

} //namespace
