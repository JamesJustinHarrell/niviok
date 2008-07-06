using System;
using System.Collections.Generic;
using System.Diagnostics;

class NsScidentre {
	IList<IDerefable> _list;
	
	public NsScidentre() {
		_list = new List<IDerefable>();
	}
	
	public void bind(IDerefable d) {
		_list.Add(d);
	}
	
	public DerefResults deref(IdentifierSequence idents) {
		return G.commonDeref(idents, null, null, _list, null);
	}
	
	public HashSet<IWoScidentre> findEmptyWoScidentres(IdentifierSequence idents) {
		HashSet<IWoScidentre> rv = new HashSet<IWoScidentre>();
		foreach( IDerefable d in _list )
			rv.UnionWith(d.findEmptyWoScidentres(idents));
		return rv;
	}
}


/* xxx remove
a single NsScidentre should be able to refer to mulitple namespaces (ISieve objects)
example:
namespace Foo
	...
namespace Foo
	...
* /

class NsScidentre {
	ISieve _ns;

	public NsScidentre(ISieve ns) {
		_ns = ns;
	}

	public ISieve evalNamespaceIdent(Identifier ident) {
		Console.Error.WriteLine("returning null in evaluateNamespaceIdentifier");
		return null;
	}
	
	public IWorker evalWorkerIdent(Identifier ident) {
		return G.evalIdent(_ns, ident);
		/* xxx remove
		if( _scope.isReserved(ident) )
			throw new ApplicationException(
				"declare-first identikey hasn't been assigned to yet");
		return _scope.evaluateLocalIdentifier(ident);
		* /
	}
}
*/