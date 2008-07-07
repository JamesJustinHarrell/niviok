using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Acrid {

public delegate TR Function0<TR>();
public delegate TR Function1<T1,TR>(T1 t1);
public delegate TR Function2<T1,T2,TR>(T1 t1, T2 t2);

//These functions should be global, but
//can't due to limitations in C#.
//"G" is for "Global"
public static class G {
	
	public static void addEach<T>( ICollection<T> outCollection, IEnumerable<T> inCollection ) {
		foreach( T t in inCollection )
			outCollection.Add(t);
	}
	
	//xxx what should the return value be when the collection is empty?
	public static bool all_true<T>( IEnumerable<T> collection, Function1<T,bool> callback ) {
		return ! any_false(collection, callback);
	}
	
	//xxx what should the return value be when the collection is empty?
	public static bool any_false<T>( IEnumerable<T> collection, Function1<T,bool> callback ) {
		foreach( T t in collection )
			if( callback(t) == false )
				return true;
		return false;
	}

	static ICollection<T> collect<T>( ICollection args ) {	
		ICollection<T> rv = new LinkedList<T>();
		foreach( object o in args ) {
			if( o == null )
				continue;
			if( o is T )
				rv.Add( ((T)o) );
			else if( o is ICollection )
				foreach( T t in (o as ICollection) )
					rv.Add(t);
			else
				throw new ArgumentException("unknown object type for: " + o.ToString());
		}
		return rv;
	}
	
	public static ICollection<T> collect<T>( params object[] args ) {
		return collect<T>(args as ICollection);
	}
	
	public static T first<T>( IEnumerable<T> collection ) {
		IEnumerator<T> enumerator = collection.GetEnumerator();
		enumerator.MoveNext();
		return enumerator.Current;
	}
	
	public static IList<T> join<T>( IEnumerable<T> a, IEnumerable<T> b ) {
		IList<T> rv = new List<T>(a);
		foreach( T t in b )
			rv.Add(t);
		return rv;
	}
	
	public static T last<T>( IEnumerable<T> collection ) {
		LinkedList<T> temp = new LinkedList<T>(collection);
		return temp.Last.Value;
	}
	
	public static T parseEnum<T>(string text) {
		return (T)Enum.Parse(typeof(T), text, true);
	}
	
	public static IEnumerable<T> rest<T>( IEnumerable<T> collection ) {
		List<T> rv = new List<T>(collection);
		rv.RemoveAt(0);
		return rv;
	}
	
}

} //namespace
