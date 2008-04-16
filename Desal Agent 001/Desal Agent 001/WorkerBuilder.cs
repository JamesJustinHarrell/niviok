using System.Collections.Generic;

class WorkerBuilder : WorkerBase {
	public delegate IWorker PropGetFunc();

	public WorkerBuilder(IInterface face, IObject owner, IList<IWorker> children) {
		_face = face;
		_owner = owner;
		_children = children;
	
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
	
	public IWorker compile() {
		//xxx is this the best place for this?
		//it would probably be best to replace this with more general
		//code that automatically creates workers for empty interfaces
		if( _children.Count == 0 && _face != Bridge.faceObject ) {
			WorkerBuilder builder = new WorkerBuilder(
				Bridge.faceObject, _owner, new IWorker[]{});
			_children = new List<IWorker>(_children);
			_children.Add(builder.compile());
		}
		
		return new Worker(this, _face);
	}
	
	
	//----- convenience methods
	
	public void addPropertyGetter(Identifier ident, PropGetFunc func) {
		addPropertyGetter(
			ident,
			new Function_Native(
				new ParameterImpl[]{},
				new NullableType(null, true),
				delegate(Scope scope) {
					return func();
				},
				null));
	}
	
	public void addPropertyGetter(Identifier ident, IWorker func) {
		addPropertyGetter(ident, Bridge.unwrapFunction(func));
	}
	
	public void addPropertySetter(Identifier ident, IWorker func) {
		addPropertySetter(ident, Bridge.unwrapFunction(func));
	}
}
