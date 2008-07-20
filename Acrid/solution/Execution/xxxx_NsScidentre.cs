/*
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public class NsScidentre {
	IList<ISieve> _list;
	
	public NsScidentre() {
		_list = new List<ISieve>();
	}
	
	public void bind(ISieve d) {
		_list.Add(d);
	}
	
	public DerefResults deref(Identifier idents) {
		return GE.commonDeref(idents, null, null, _list, null);
	}
	
	public HashSet<IScidentre> findEmptyScidentres(Identifier idents) {
		HashSet<IScidentre> rv = new HashSet<IScidentre>();
		foreach( ISieve d in _list )
			rv.UnionWith(d.findEmptyScidentres(idents));
		return rv;
	}
}

} //namespace
*/