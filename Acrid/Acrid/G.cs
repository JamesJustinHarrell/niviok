//functions that should be global, but
//can't due to limitations in C#

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

static class G {
	public delegate TR Function0<TR>();
	public delegate TR Function1<T1,TR>(T1 t1);
	public delegate TR Function2<T1,T2,TR>(T1 t1, T2 t2);

	static ICollection<T> collect__<T>( ICollection args ) {	
		ICollection<T> rv = new LinkedList<T>();
		foreach( object o in args ) {
			if( o == null )
				continue;
			if( o is T )
				rv.Add( ((T)o) );
			else if( o is ICollection )
				//foreach( T t in collect__<T>(o as ICollection) )
				foreach( T t in (o as ICollection) )
					rv.Add(t);
			else
				throw new ArgumentException("unknown object type for: " + o.ToString());
		}
		return rv;
	}
	
	public static ICollection<T> collect<T>( params object[] args ) {
		return collect__<T>(args);
	}
	
	public static T parseEnum<T>(string text) {
		return (T)Enum.Parse(typeof(T), text, true);
	}

	//xxx should use bridge for output
	public static void printSableccToken( Fujin.Sablecc.node.Token token ) {
		if( token is Fujin.Sablecc.node.TIndentOpen )
			Console.Write("INDENTPOPEN");
		else if( token is Fujin.Sablecc.node.TIndentClose )
			Console.Write("INDENTCLOSE");
		else if( token is Fujin.Sablecc.node.TNewline )
			Console.Write("NEWLINE");
		else
			Console.Write(token.Text);
	}
	
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
	
	//create the scope to be used by a function body
	//assumes the IList<Argument> have been matched to an appropriate function
	public static IScope setupArguments(
	IList<ParameterImpl> parameters, IList<Argument> arguments, IScope outerScope) {
		Debug.Assert(outerScope != null);
		IScope innerScope = new Scope(outerScope, null);
		
		for( int i = 0; i < arguments.Count; i++ ) {
			G.declareAssign(
				parameters[i].name,
				WoScidentreCategory.VARIABLE,
				new NType(),
				arguments[i].value,
					/* xxx downcast(arguments[i].value, parameters[i].type) */
				innerScope
			);
		}

		return innerScope;
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
		return G.inherits(
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
	
	public static T first<T>( IEnumerable<T> collection ) {
		IEnumerator<T> enumerator = collection.GetEnumerator();
		enumerator.MoveNext();
		return enumerator.Current;
	}
	
	public static IEnumerable<T> rest<T>( IEnumerable<T> collection ) {
		List<T> rv = new List<T>(collection);
		rv.RemoveAt(0);
		return rv;
	}
	
	public static T last<T>( IEnumerable<T> collection ) {
		LinkedList<T> temp = new LinkedList<T>(collection);
		return temp.Last.Value;
	}
	
	public static IList<T> join<T>( IEnumerable<T> a, IEnumerable<T> b ) {
		IList<T> rv = new List<T>(a);
		foreach( T t in b )
			rv.Add(t);
		return rv;
	}
	
	//xxx what should the return value be when the collection is empty?
	public static bool any_false<T>( IEnumerable<T> collection, Function1<T,bool> callback ) {
		foreach( T t in collection )
			if( callback(t) == false )
				return true;
		return false;
	}
	
	//xxx what should the return value be when the collection is empty?
	public static bool all_true<T>( IEnumerable<T> collection, Function1<T,bool> callback ) {
		return ! any_false(collection, callback);
	}
	
	public static void addEach<T>( ICollection<T> outCollection, IEnumerable<T> inCollection ) {
		foreach( T t in inCollection )
			outCollection.Add(t);
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
			if( woScidentres != null && woScidentres.ContainsKey(first(idents)) ) {
				results.Add( woScidentres[ first(idents) ].deref() );
			}
		}
		else if(nsScidentres != null && nsScidentres.ContainsKey(first(idents))) {
			results.Add( nsScidentres[first(idents)].deref(
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
			if(woScidentres.ContainsKey( first(idents) ))
				results.Add( woScidentres[ first(idents) ] );
		else if(nsScidentres.ContainsKey( first(idents) ))
			results.UnionWith(
				nsScidentres[first(idents)].findEmptyWoScidentres(
					new IdentifierSequence(rest(idents))) );
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
