using System.Collections.Generic;
using System.Diagnostics;

class WorkerBuilder : WorkerBase {
	public delegate IWorker PropGetFunc();

	public WorkerBuilder(IInterface face, IObject owner, IList<IWorker> childWorkers) {
		Debug.Assert(face != null, "@face null in WorkerBuilder constructor");
		Debug.Assert(owner != null, "@owner null in WorkerBuilder constructor");
		Debug.Assert(childWorkers != null, "@childWorkers null in WorkerBuilder constructor");
	
		_face = face;
		_owner = owner;
		_childWorkers = childWorkers;
	
		_breeders = new Dictionary<IInterface, IFunction>();
		_callees = new Dictionary<IInterface, IFunction>();
		_propGetters = new Dictionary<Identifier, IFunction>();
		_propSetters = new Dictionary<Identifier, IFunction>();
		_methods = new Dictionary<Identifier, IDictionary<IInterface, IFunction>>();
	}
	
	public void addCallee(IFunction func) {
		_callees.Add(func.face, func);
	}
	
	public void addPropertyGetter(Identifier ident, IFunction func) {
		_propGetters.Add(ident, func);
	}

	public void addPropertySetter(Identifier ident, IFunction func) {
		_propSetters.Add(ident, func);
	}
	
	public void addMethod(Identifier ident, IFunction func) {
		if( ! _methods.ContainsKey(ident) )
			_methods.Add( ident, new Dictionary<IInterface, IFunction>() );
		_methods[ident].Add(func.face, func);
	}
	
	public void addBreeder(IInterface face, PropGetFunc func) {
		_breeders.Add(
			face,
			new Function_Native(
				new ParameterImpl[]{},
				new NType(face),
				delegate(IScope scope) {
					return func();
				},
				null));
	}
	
	public IWorker compile() {
		//xxx is this the best place for this?
		//it would probably be best to replace this with more general
		//code that automatically creates workers for empty interfaces
		if( _childWorkers.Count == 0 && _face != Bridge.stdn_Object ) {
			WorkerBuilder builder = new WorkerBuilder(
				Bridge.stdn_Object, _owner, new IWorker[]{});
			_childWorkers = new List<IWorker>(_childWorkers);
			_childWorkers.Add(builder.compile());
		}
		
		return new Worker(this);
	}
	
	
	//----- convenience methods
	
	public void addPropertyGetter(Identifier ident, PropGetFunc func) {
		addPropertyGetter(
			ident,
			new Function_Native(
				new ParameterImpl[]{},
				Bridge.stdn_Nullable_any,
				delegate(IScope scope) {
					return func();
				},
				null));
	}
	
	public void addPropertyGetter(Identifier ident, IWorker func) {
		addPropertyGetter(ident, Bridge.toNativeFunction(func));
	}
	
	public void addPropertySetter(Identifier ident, IWorker func) {
		addPropertySetter(ident, Bridge.toNativeFunction(func));
	}
}
