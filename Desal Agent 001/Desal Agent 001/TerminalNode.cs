/*
A base class for terminal nodes.
Uses the curiously recurring template pattern,
although C# doesn't support it very well.
*/

using System.Collections.Generic;

abstract class TerminalNode<T, U> where U : class, INode {
	T _value;
	
	public TerminalNode(T value) {
		_value = value;
	}
	
	public T value {
		get { return _value; }
	}
	
	//note: this should be null for types that can never have child nodes
	//(and empty for types that can)
	public ICollection<INode> childNodes {
		get { return null; }
	}

	public override string ToString() {
		return _value.ToString();
	}
}
