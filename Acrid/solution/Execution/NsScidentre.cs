using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public class NsScidentre {
	IList<IDerefable> _list;
	
	public NsScidentre() {
		_list = new List<IDerefable>();
	}
	
	public void bind(IDerefable d) {
		_list.Add(d);
	}
	
	public DerefResults deref(IdentifierSequence idents) {
		return GE.commonDeref(idents, null, null, _list, null);
	}
	
	public HashSet<IWoScidentre> findEmptyWoScidentres(IdentifierSequence idents) {
		HashSet<IWoScidentre> rv = new HashSet<IWoScidentre>();
		foreach( IDerefable d in _list )
			rv.UnionWith(d.findEmptyWoScidentres(idents));
		return rv;
	}
}

} //namespace
