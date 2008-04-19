using System;
using System.Collections.Generic;
using System.Diagnostics;

class Worker : WorkerBase, IWorker {	
	//check members to make sure this is a valid implementation
	void checkImplementation() {
		IInterface bareFace = Bridge.unwrapInterface(_face);
	
		//check methods
		foreach( IList<Method> methList in bareFace.methods.Values )
			foreach( Method meth in methList ) {
				Debug.Assert(meth.name != null);
				Debug.Assert(meth.face != null);
				if(!(
					_methods.ContainsKey(meth.name) &&
					_methods[meth.name].ContainsKey(meth.face) ))
				{
					throw new ApplicationException(
						"method with name '" + meth.name + "' not implemented");
				}
			}
		
		//ensure FaceImpl doesn't implement anything not defined in Face
		foreach( Identifier ident in _methods.Keys ) {
			foreach(IInterface faceImpl in _methods[ident].Keys) {
				bool found = false;
				foreach( Identifier ident2 in bareFace.methods.Keys ) {
					foreach( Method meth in bareFace.methods[ident2] )
						if( meth.name == ident && meth.face == faceImpl ) {
							found = true;
							break;
						}
				}
				if( ! found )
					throw new ApplicationException(
						"implemented method '" + ident + "' not defined");
			}
		}
	}

/*xxx
	//xxx
	bool isMatch(IInterface face, IList<Argument> args) {
		IList<ParameterInfo> parameters = face.callees[0].parameters;
		if( parameters.Count != args.Count )
			return false;
		//xxx consider varargs and types
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
			throw new ApplicationException("no matches found");
		if( matches.Count > 1 )
			throw new ApplicationException("multiple matches");
		return matches[0];
	}
*/
	public Worker(WorkerBase members)
	: base(members)
	{
		//xxxx checkImplementation();
	}

	public IObject owner {
		get { return _owner; }
	}
	
	public IList<IWorker> children {
		get { return _children; }
	}

	public IWorker face {
		get { return _face; }
	}
	
	public IWorker breed(IInterface face) {
		throw new NotImplementedException();
	}
	
	public IWorker call(IList<Argument> arguments) {
		//xxx use arguments to determine which callee to call
		foreach( IFunction func in _callees.Values )
			return func.call(arguments);
		throw new ClientException("attempted to call worker with no callees");
	}
	
	public IWorker extractMember(Identifier name) {
		if( _propGetters.ContainsKey(name) )
			return _propGetters[name].call(new Argument[]{});
		if( _methods.ContainsKey(name) ) {
			//xxx merge functions instead of just using first
			foreach( IFunction func in _methods[name].Values )
				return Client_Function.wrap(func);
		}
		throw new NotImplementedException();
	}
	
	public void setProperty(Identifier propName, IWorker worker) {
		_propSetters[propName].call(
			new Argument[]{
				new Argument(null, worker)
			});
	}
}