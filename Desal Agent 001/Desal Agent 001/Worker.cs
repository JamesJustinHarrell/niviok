using System;
using System.Collections.Generic;

class Worker : WorkerBase, IWorker {
	IInterface _face;
	
	//check members to make sure this is a valid implementation
	void checkImplementation() {
		/*
		
		//check methods
		foreach( IList<MethodInfo> methList in _face.methods.Values )
			foreach( MethodInfo meth in methList )
				if( ! (
					_methods.ContainsKey(meth.name) &&
					_methods[meth.name].ContainsKey(meth.iface) ) )
				{
					throw new System.Exception(
						"method with name '" + meth.name + "' not implemented");
				}
		
		//ensure FaceImpl doesn't implement anything not defined in Face
		foreach( Identifier ident in _voidMethods.Keys ) {
			foreach( IFunctionInterface faceImpl in _voidMethods[ident].Keys ) {
				bool found = false;
				foreach( MethodInfo meth in _face.methods ) {
					if( meth.name == ident && meth.iface == faceImpl ) {
						found = true;
						break;
					}
				}
				if( ! found )
					throw new System.Exception(
						"implemented method '" + ident.str + "' not defined");
			}
		}
		
		*/
	}

/*xxx
	//xxx does this belong here?
	bool isMatch(IInterface face, IList<Argument> args) {
		//xxx vararg
		if( args.count != face.parameters.Count ) {
			return false;
		}
		//xxx types
		return true;
	}

	KeyValuePair<IInterface, TFunc>
	getFunction<TFunc>(IDictionary<IInterface, TFunc> funcs, IList<Argument> args) {
		IList< KeyValuePair<IFunctionInterface, TFunc> > matches =
			new List< KeyValuePair<IFunctionInterface, TFunc> >();
		foreach( KeyValuePair<IFunctionInterface, TFunc> pair in funcs ) {
			if( isMatch(pair.Key, args) ) {
				matches.Add(pair);
			}
		}
		if( matches.Count == 0 )
			throw new System.Exception("no matches found");
		if( matches.Count > 1 )
			throw new System.Exception("multiple matches");
		return matches[0];
	}
*/
	public Worker( WorkerBase members, IInterface face )
	: base(members)
	{
		_face = face;
		checkImplementation();
	}

	public IObject owner {
		get { return _owner; }
	}
	
	public IList<IWorker> children {
		get { return _children; }
	}

	public IInterface face {
		get { return _face; }
	}
	
	public IWorker breed(IInterface face) {
		throw new Error_Unimplemented();
	}
	
	public IWorker call(IList<Argument> arguments) {
		//xxx use arguments to determine which callee to call
		foreach( IFunction func in _callees.Values )
			return func.call(arguments);
		throw new Exception("attempted to call worker with no callees");
	}
	
	public IWorker extractMember(Identifier name) {
		if( _propGetters.ContainsKey(name) )
			return _propGetters[name].call(new Argument[]{});
		if( _methods.ContainsKey(name) ) {
			//xxx merge functions instead of just using first
			foreach( IFunction func in _methods[name].Values )
				return Client_Function.wrap(func);
		}
		throw new Error_Unimplemented();
	}
	
	public void setProperty(Identifier propName, IWorker worker) {
		_propSetters[propName].call(
			new Argument[]{
				new Argument(null, worker)
			});
	}
}