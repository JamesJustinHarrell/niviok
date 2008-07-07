/*
A base class for terminal nodes.
Uses the curiously recurring template pattern,
although C# doesn't support it very well.
*/

using System.Collections.Generic;

namespace Acrid.NodeTypes {

public abstract class TerminalNode<T, U> where U : class, INode {
	T _value;
	string _nodeSource;
	
	public TerminalNode(T value, string nodeSource) {
		_value = value;
		_nodeSource = nodeSource;
	}
	
	public T value {
		get { return _value; }
	}
	
	//note: this should be null for types that can never have child nodes
	//(and empty for types that can)
	public ICollection<INode> childNodes {
		get { return null; }
	}
	
	public string nodeSource {
		get { return _nodeSource; }
	}

	public override string ToString() {
		return _value.ToString();
	}
}

} //namespace
