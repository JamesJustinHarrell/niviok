using System.Collections.Generic;

class Node_Method : INode {
	Node_Identifier _name;
	INode_Expression _iface;

	public Node_Method(Node_Identifier name, INode_Expression iface) {
		_name = name;
		_iface = iface;
	}
	
	public MethodInfo evaluateMethodInfo(Scope scope) {
		return new MethodInfo(
			_name.identifier,
			null ); //InterfaceFromValue.wrap( _iface.evaluate(scope) ) );
	}
	
	public string typeName {
		get { return "method"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _name, _iface }; }
	}

	public HashSet<Identifier> identikeyDependencies {
		get { return _iface.identikeyDependencies; }
	}
}