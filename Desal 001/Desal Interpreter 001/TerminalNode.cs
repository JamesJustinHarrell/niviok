using System.Collections.Generic;

/*
A base class for terminal nodes.
Uses the curiously recurring template pattern,
although C# doesn't support it very well.
*/

abstract class TerminalNode<T, U> where U : class, INode {
	T _value;
	
	public TerminalNode(T value) {
		_value = value;
	}
	
	public T value {
		get { return _value; }
	}
	
	public ICollection<INode> childNodes {
		get { return new INode[]{}; }
	}
	
	public virtual HashSet<Identifier> identikeyDependencies {
		get { return Depends.depends(this as U); }
	}
	
	public override string ToString () {
		return _value.ToString();
	}
}
