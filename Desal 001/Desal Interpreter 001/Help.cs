//Contains static methods that should be global free functions.
//C# doesn't allow free functions.

using System.Collections;
using System.Collections.Generic;

static class Help {
	public static HashSet<Identifier> getIdentRefs( params object[] args ) {
		return getIdentRefs__(args);
	}
	
	private static HashSet<Identifier> getIdentRefs__( ICollection args ) {
		HashSet<Identifier> idents = new HashSet<Identifier>();
		foreach( object o in args ) {
			if( o == null )
				continue;
			if( o is INode )
				idents.UnionWith( (o as INode).identikeyDependencies );
			else if( o is ICollection )
				idents.UnionWith( getIdentRefs__(o as ICollection) );
			else
				throw new System.Exception("unknown object type: " + o.ToString());
		}
		return idents;
	}
}
