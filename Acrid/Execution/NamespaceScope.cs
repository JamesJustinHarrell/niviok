//searches parent for members of the associated namespace before searching parent scope normally

using Acrid.NodeTypes;

namespace Acrid.Execution {

class NamespaceScope : IScope {
	IScope _parent;
	Identifier _name;
	
	public NamespaceScope(IScope parent, Identifier name) {
		_parent = parent;
		_name = name;
	}
	
	public void expose( IDerefable d ) {
		_parent.expose(d);
	}
	
	public void bindNamespace( Identifier name, IDerefable d) {
		_parent.bindNamespace(name, d);
	}
	
	public IWoScidentre reserveWoScidentre( Identifier name, WoScidentreCategory cat) {
		return _parent.reserveWoScidentre(name, cat);
	}
	
	public void activateWoScidentre(Identifier name, NType type, IWorker worker) {
		_parent.activateWoScidentre(name, type, worker);
	}

	public void assign(Identifier name, IWorker worker) {
		_parent.assign(name, worker);
	}
	
	public DerefResults upDeref(IdentifierSequence idents) {
		DerefResults results = new DerefResults(null, null);
		results.Add(
			_parent.upDeref(
				new IdentifierSequence(
					G.join(new Identifier[]{_name}, idents))));
		results.Add( _parent.upDeref(idents) );
		return results;
	}
	
	public HashSet<IWoScidentre> upFindEmptyWoScidentres(IdentifierSequence idents) {
		HashSet<IWoScidentre> results = new HashSet<IWoScidentre>();
		results.UnionWith(
			_parent.upFindEmptyWoScidentres(
				new IdentifierSequence(
					G.join(new Identifier[]{_name}, idents))));
		results.UnionWith( _parent.upFindEmptyWoScidentres(idents) );
		return results;
	}
	
	public ScopeAllowance allowance {
		get { return _parent.allowance; }
	}
}

} //namespace
