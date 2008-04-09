//functions that should be global, but
//can't due to limitations in C#

using System;
using System.Collections;
using System.Collections.Generic;

static class G {
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
				throw new System.Exception("unknown object type for: " + o.ToString());
		}
		return rv;
	}
	
	public static ICollection<T> collect<T>( params object[] args ) {
		return collect__<T>(args);
	}
	
	public static T parseEnum<T>(string text) {
		return (T)Enum.Parse(typeof(T), text, true);
	}

	public static void printSableccToken( Dextr.Sablecc.node.Token token ) {
		if( token is Dextr.Sablecc.node.TIndentOpen )
			Console.Write("INDENTPOPEN");
		else if( token is Dextr.Sablecc.node.TIndentClose )
			Console.Write("INDENTCLOSE");
		else if( token is Dextr.Sablecc.node.TNewline )
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
}
