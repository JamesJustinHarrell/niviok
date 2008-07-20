//functions that should be global, but
//can't due to limitations in C#

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Acrid.NodeTypes;

namespace Acrid.Execution {

//"G" is for "Global"
//"E" is for "Execution"
public static class GE {
	
	//tells whether a inherits b or a == b
	//xxx think up better parameter names
	public static bool inheritsOrIs(IInterface a, IInterface b) {
		return (a == b) || inherits(a, b);
	}
	
	//tells whether a inherits b
	public static bool inherits(IInterface a, IInterface b) {
		foreach( IInterface c in a.inheritees )
			if( inheritsOrIs(c, b) )
				return true;
		return false;
	}
	
	public static IWorker combineWorkers(IList<IWorker> workers) {
		//xxxx merge instead of just returning first
		foreach( IWorker worker in workers )
			return worker;
		throw new NotImplementedException();
	}
	
	public static IWorker castDown(IWorker source, IInterface face) {
		Debug.Assert(inheritsOrIs(source.face, face));
		if( source.face == face )
			return source;
		foreach( IWorker child in source.childWorkers )
			if( inheritsOrIs(child.face, face) )
				return castDown(child, face);
		throw new ClientException("the object does not implement this interface");
	}
	
	public static IWorker cast(IWorker source, IInterface face) {
		if( inheritsOrIs(source.face, face) )
			return castDown(source, face);
		source = source.owner.rootWorker;
		if( inheritsOrIs(source.face, face) )
			return castDown(source, face);
		throw new ClientException("the object does not implement this interface");
	}
	
	/* xxx remove?
	public static IWorker cast(IWorker source, Type type) {
		if( source is Null && type.nullable == false )
			throw new ClientException("attempted to cast null to non-nullable type");
		return cast(source, type.face);
	}
	*/
	
	public static bool canBreed(IInterface face, IInterface targetFace) {
		return GE.inherits(
			face, Bridge.getBreederFace(Bridge.stdn_String));
	}
	
	public static IWorker evalIdent(IScope scope, Identifier name) {
		DerefResults results = scope.upDeref(name);
		if( results.worker != null )
			return results.worker;
		if( results.workerList != null ) {
			if( results.workerList.Count > 1 )
				throw new NotImplementedException();
			return results.workerList[0];
		}
		throw new ClientException(
			String.Format("wo-scidentre '{0}' not found", name));
	}
	
	public static IWorker evalIdent(IScope scope, string name) {
		return evalIdent(scope, new Identifier(name));
	}
	
	public static IWorker evalIdent(ISieve d, Identifier name) {
		DerefResults results = d.deref(name);
		if( results.worker != null )
			return results.worker;
		if( results.workerList != null ) {
			if( results.workerList.Count > 1 )
				throw new NotImplementedException();
			return results.workerList[0];
		}
		throw new ClientException(
			String.Format("scidentre '{0}' not found", name));
	}
	
	public static IWorker evalIdent(ISieve d, string name) {
		return evalIdent(d, new Identifier(name));
	}
	
	public static IScope createClosure(
	HashSet<Identifier> idents, IScope scope ) {
		//xxx temp
		return scope;
	}

	public static DerefResults commonDeref(
	Identifier name,
	IDictionary<Identifier,IScidentre> scidentres,
	IEnumerable<ISieve> sieves,
	IEnumerable<IWorker> exposes,
	IScope scope ) {
		DerefResults results = new DerefResults(null, null);		
		if( scidentres != null && scidentres.ContainsKey(name) )
			results.Add( scidentres[name].deref() );
		if(sieves != null)
			foreach(ISieve d in sieves)
				results.Add(d.deref(name));
		if(exposes != null && results.worker == null) {
			foreach(IWorker w in exposes) {
				DerefResults exposeResults = GE.deref(w, name);
				if(results.workerList == null || exposeResults.worker == null)
					results.Add(exposeResults);
			}
		}
		if(scope != null && results.worker == null) {
			DerefResults scopeResults = scope.upDeref(name);
			if(results.workerList == null || scopeResults.worker == null)
				results.Add(scopeResults);
		}
		return results;
	}

	public static HashSet<IScidentre> commonFindEmptyScidentres(
	Identifier name,
	IDictionary<Identifier,IScidentre> scidentres,
	IEnumerable<ISieve> sieves,
	IScope scope ) {
		HashSet<IScidentre> results = new HashSet<IScidentre>();
		if(scidentres.ContainsKey(name))
			results.Add( scidentres[name] );
		foreach(ISieve d in sieves)
			results.UnionWith( d.findEmptyScidentres(name) );
		if(scope != null)
			results.UnionWith(scope.upFindEmptyScidentres(name));
		return results;
	}

	public static void declareAssign (
	Identifier name, ScidentreCategory cat,
	NType type, IWorker worker, IScope scope ) {
		IScidentre ws = scope.reserveScidentre(name, cat);
		ws.type = type;
		ws.assign(worker);
	}
	
	public static DerefResults deref(IWorker worker, Identifier name) {
		return new DerefResults(
			worker.face.properties.ContainsKey(name) ?
				extractMember(worker, name) :
				null,
			worker.face.methods.ContainsKey(name) ?
				new IWorker[]{ extractMember(worker, name) } :
				null );
	}
	
	public static IList<Identifier> extractIdents( IList<Node_Identifier> identNodes ) {
		IList<Identifier> rv = new List<Identifier>();
		foreach( Node_Identifier identNode in identNodes )
			rv.Add( identNode.value );
		return rv;
	}
	
	public static IWorker extractMember( IWorker worker, string name ) {
		return extractMember(worker, new Identifier(name));
	}
	
	public static IWorker extractMember( IWorker worker, Identifier name ) {
		return worker.extractMember(name);
	}
}

} //namespace
