using System;

class NamespaceReference : IDerefable {
	IdentifierSequence _idents;
	IScope _scope;
	 
	public NamespaceReference(IdentifierSequence idents, IScope scope) {
		_idents = idents;
		_scope = scope;
	}
	
	public DerefResults deref(IdentifierSequence idents) {	
		return ((SieveScope)_scope).xxxNsUpDeref( new IdentifierSequence(G.join(_idents, idents)) );
		//xxxx temporary
		return _scope.upDeref( new IdentifierSequence(G.join(_idents, idents)) );
	}

	public HashSet<IWoScidentre> findEmptyWoScidentres(IdentifierSequence idents) {
		return new HashSet<IWoScidentre>();
		//xxxx temporary
		return _scope.upFindEmptyWoScidentres( new IdentifierSequence(G.join(_idents, idents)) );
	}
}
