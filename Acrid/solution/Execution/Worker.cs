using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class Worker : WorkerBase, IWorker {	
	//check members to make sure this is a valid implementation
	void checkImplementation() {	
		//check methods
		foreach( IList<Method> methList in _face.methods.Values )
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
				foreach( Identifier ident2 in _face.methods.Keys ) {
					foreach( Method meth in _face.methods[ident2] )
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
	
	public IList<IWorker> childWorkers {
		get { return _childWorkers; }
	}

	public IInterface face {
		get { return _face; }
	}
	
	public IWorker breed(IInterface face) {
	/* xxx enable when Bridge.getBreederFace is implemented
		IInterface breederFace = Bridge.getBreederFace(face);
		if( _face == breederFace )
			return _breeders[face].call(new Argument[]{});
		if( GE.inherits(_face, breederFace) )
			return GE.castDown(this, breederFace).breed(face);
		throw new ClientException(
			"no breeder for the specified interface");
	*/
		if( _breeders.ContainsKey(face) )
			return _breeders[face].call(new Argument[]{});
		throw new ClientException(
			"no breeder for the specified interface");
	}
	
	public IWorker call(IList<Argument> arguments) {
		if( _callees.Count == 0 )
			throw new ClientException("attempted to call worker that has no callees");
		
		//xxx use arguments to determine which callee to call
		IFunction chosenCallee = G.first(_callees.Values);
		try {
			return chosenCallee.call(arguments);
		}
		catch(ArgumentOutOfRangeException e) {
			throw new ClientException(String.Format(
				"wrong number of arguments ({0}) provided",
				arguments.Count
				));
		}
	}
	
	public IWorker extractMember(Identifier name) {
		if( _propGetters.ContainsKey(name) )
			return _propGetters[name].call(new Argument[]{});
		if( _methods.ContainsKey(name) ) {
			//xxx merge functions instead of just using first
			foreach( IFunction func in _methods[name].Values )
				return Client_Function.wrap(func);
		}
		throw new ClientException("no member with that name");
	}
	
	public void setProperty(Identifier propName, IWorker worker) {
		_propSetters[propName].call(
			new Argument[]{
				new Argument(null, worker)
			});
	}
}

} //namespace
