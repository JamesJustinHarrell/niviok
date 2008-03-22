//functions that should be global, but
//can't due to limitations in C#

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
	
	static HashSet<Identifier> depends__( ICollection args ) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		foreach( object o in args ) {
			if( o == null )
				continue;
			if( o is INode )
				idents.UnionWith( (o as INode).identikeyDependencies );
			else if( o is ICollection )
				idents.UnionWith( depends__(o as ICollection) );
			else
				throw new System.Exception("unknown object type: " + o.ToString());
		}
		return idents;
	}
	
	//xxx remove
	public static HashSet<Identifier> depends( params object[] args ) {
		return depends__(args);
	}
}
