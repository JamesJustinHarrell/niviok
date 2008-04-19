using System;
using System.Collections.Generic;

abstract class WorkerBase {
	protected IWorker _face;
	protected IObject _owner;
	protected IList<IWorker> _children;
	protected IDictionary<IInterface, IFunction> _callees;
	protected IDictionary<Identifier, IFunction> _propGetters;
	protected IDictionary<Identifier, IFunction> _propSetters;
	protected IDictionary<Identifier, IDictionary<IInterface, IFunction>> _methods;
	
	public WorkerBase() {}
	
	public WorkerBase(WorkerBase data) {
		_face = data._face;
		_owner = data._owner;
		_children = data._children;
		_callees = data._callees;
		_propGetters = data._propGetters;
		_propSetters = data._propSetters;
		_methods = data._methods;
	}
}