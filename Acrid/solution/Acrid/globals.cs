using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Acrid {

//introduced in .NET framework 3.5
public delegate void Action();
public delegate void Action<T>(T obj);
public delegate void Action<T1,T2>(T1 arg1, T2 arg2);
public delegate void Action<T1,T2,T3>(T1 arg1, T2 arg2, T3 arg3);
public delegate void Action<T1,T2,T3,T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
public delegate TResult Func<TResult>();
public delegate TResult Func<T,TResult>(T arg);
public delegate TResult Func<T1,T2,TResult>(T1 arg1, T2 arg2);
public delegate TResult Func<T1,T2,T3,TResult>(T1 arg1, T2 arg2, T3 arg3);
public delegate TResult Func<T1,T2,T3,T4,TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

//These functions should be global, but
//can't due to limitations in C#.
//"G" is for "Global"
public static class G {
	
	public static void addEach<T>( ICollection<T> outCollection, IEnumerable<T> inElements ) {
		each(inElements, outCollection.Add);
	}
	
	public static bool allFalse<T>( IEnumerable<T> elements, Func<T,bool> callback ) {
		return ! anyTrue(elements, callback);
	}
	
	public static bool allTrue<T>( IEnumerable<T> elements, Func<T,bool> callback ) {
		return ! anyFalse(elements, callback);
	}
	
	public static bool anyFalse<T>( IEnumerable<T> elements, Func<T,bool> callback ) {
		//when not accounting for zero elements, return value is often surprising
		//so require 1+ elements
		if( empty(elements) )
			throw new Exception("no elements given");
		
		foreach( T element in elements )
			if( callback(element) == false )
				return true;
		return false;
	}
	
	public static bool anyTrue<T>( IEnumerable<T> elements, Func<T,bool> callback ) {
		//when not accounting for zero elements, return value is often surprising
		//so require 1+ elements
		if( empty(elements) )
			throw new Exception("no elements given");
		
		foreach( T element in elements )
			if( callback(element) )
				return true;
		return false;	
	}

	//search through @elements looking for objects of type T
	static ICollection<T> collect<T>( IEnumerable elements ) {
		ICollection<T> rv = new LinkedList<T>();
		foreach( object o in elements ) {
			if( o == null )
				continue;
			if( o is T )
				rv.Add( (T)o );
			else if( o is ICollection )
				each<T>( o as ICollection, rv.Add );
			else
				throw new ArgumentException("unknown object type for: " + o.ToString());
		}
		return rv;
	}
	
	public static ICollection<T> collect<T>( params object[] args ) {
		return collect<T>(args as ICollection);
	}
	
	//T must be specified when calling, but works with non-generic IEnumerable
	public static void each<T>( IEnumerable elements, Action<T> callback ) {
		foreach( T element in elements )
			callback(element);
	}
	
	//T can be inferred when calling, but doesn't work with non-generic IEnuerable
	public static void each<T>( IEnumerable<T> elements, Action<T> callback ) {
		foreach( T element in elements )
			callback(element);
	}
	public static void each<T,TR>( IEnumerable<T> elements, Func<T,TR> callback ) {
		foreach( T element in elements )
			callback(element);
	}
	
	public static bool empty( ICollection collection ) {
		return collection.Count == 0;
	}
	
	public static bool empty( IEnumerable elements ) {
		return ! elements.GetEnumerator().MoveNext();
	}
	
	public static IEnumerable<T> filter<T>( IEnumerable<T> elements, Func<T,bool> callback ) {
		foreach( T element in elements )
			if( callback(element) )
				yield return element;
	}
	
	public static Func<T,bool> invert<T>( Func<T,bool> function ) {
		return delegate(T argument) {
			return ! function(argument);
		};
	}
	
	public static T first<T>( IEnumerable<T> elements ) {
		IEnumerator<T> enumerator = elements.GetEnumerator();
		enumerator.MoveNext();
		return enumerator.Current;
	}
	
	public static IList<T> join<T>( IEnumerable<T> a, IEnumerable<T> b ) {
		IList<T> rv = new List<T>(a);
		each( b, rv.Add );
		return rv;
	}
	
	public static T last<T>( IEnumerable<T> elements ) {
		return new LinkedList<T>(elements).Last.Value;
	}
	
	public static IEnumerable<Tout> map<Tin,Tout>(
	IEnumerable<Tin> elements, Func<Tin,Tout> callback ) {
		foreach( Tin element in elements )
			yield return callback(element);
	}
	
	public static T parseEnum<T>(string text) {
		return (T)Enum.Parse(typeof(T), text, true);
	}
	
	public static IEnumerable<T> rest<T>( IEnumerable<T> elements ) {
		List<T> list = new List<T>(elements);
		list.RemoveAt(0);
		return list;
	}
	
	public static IEnumerable<T> typeEnumerable<T>( IEnumerable elements ) {
		foreach( T element in elements )
			yield return (T)element;
	}
	
}

} //namespace
