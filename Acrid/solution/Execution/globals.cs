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

	public static IWorker evalIdent(IScope scope, IdentifierSequence idents) {
		DerefResults results = scope.upDeref(idents);
		if( results.worker != null )
			return results.worker;
		if( results.workerList != null ) {
			if( results.workerList.Count > 1 )
				throw new NotImplementedException();
			return results.workerList[0];
		}
		throw new ClientException(
			String.Format("wo-scidentre '{0}' not found", idents));
	}
	
	public static IWorker evalIdent(IScope scope, Identifier name) {
		DerefResults results = scope.upDeref(new IdentifierSequence(name));
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
	
	public static IWorker evalIdent(IDerefable d, Identifier name) {
		DerefResults results = d.deref(new IdentifierSequence(name));
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
	
	public static IWorker evalIdent(IDerefable d, string name) {
		return evalIdent(d, new Identifier(name));
	}
	
	public static IScope createClosure(
	HashSet<IdentifierSequence> idents, IScope scope ) {
		//xxx temp
		return scope;
	}

	public static DerefResults commonDeref(
	IdentifierSequence idents,
	IDictionary<Identifier,IWoScidentre> woScidentres,
	IDictionary<Identifier,NsScidentre> nsScidentres,
	IEnumerable<IDerefable> derefables,
	IScope scope ) {
		DerefResults results = new DerefResults(null, null);		
		if(idents.Count == 1) {
			if( woScidentres != null && woScidentres.ContainsKey(G.first(idents)) ) {
				results.Add( woScidentres[ G.first(idents) ].deref() );
			}
		}
		else if(nsScidentres != null && nsScidentres.ContainsKey(G.first(idents))) {
			results.Add( nsScidentres[G.first(idents)].deref(
				new IdentifierSequence(G.rest(idents))) );
		}
		if(derefables != null)
			foreach(IDerefable d in derefables)
				results.Add(d.deref(idents));
		if(scope != null && results.worker == null) {
			DerefResults scopeResults = scope.upDeref(idents);
			if(results.workerList == null || scopeResults.worker == null)
				results.Add(scopeResults);
		}
		return results;
	}

	public static HashSet<IWoScidentre> commonFindEmptyWoScidentres(
	IdentifierSequence idents,
	IDictionary<Identifier,IWoScidentre> woScidentres,
	IDictionary<Identifier,NsScidentre> nsScidentres,
	IEnumerable<IDerefable> derefables,
	IScope scope ) {
		HashSet<IWoScidentre> results = new HashSet<IWoScidentre>();
		if(idents.Count == 1)
			if(woScidentres.ContainsKey( G.first(idents) ))
				results.Add( woScidentres[ G.first(idents) ] );
		else if(nsScidentres.ContainsKey( G.first(idents) ))
			results.UnionWith(
				nsScidentres[G.first(idents)].findEmptyWoScidentres(
					new IdentifierSequence(G.rest(idents))) );
		foreach(IDerefable d in derefables)
			results.UnionWith( d.findEmptyWoScidentres(idents) );
		if(scope != null)
			results.UnionWith(scope.upFindEmptyWoScidentres(idents));
		return results;
	}

	public static void declareAssign (
	Identifier name, WoScidentreCategory cat,
	NType type, IWorker worker, IScope scope ) {
		IWoScidentre ws = scope.reserveWoScidentre(name, cat);
		ws.type = type;
		ws.assign(worker);
	}
	
	public static IList<Identifier> extractIdents( IList<Node_Identifier> identNodes ) {
		IList<Identifier> rv = new List<Identifier>();
		foreach( Node_Identifier identNode in identNodes )
			rv.Add( identNode.value );
		return rv;
	}
}

} //namespace
