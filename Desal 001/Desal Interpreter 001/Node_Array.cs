using System.Collections.Generic;

class Node_Array : INode_Expression {
	IList<INode_Expression> _elements;

	public Node_Array( IList<INode_Expression> elements ) {
		_elements = elements;
	}
	
	public IValue execute(Scope scope) {
		throw new Error_Unimplemented();
	}
	
	public void getInfo(out string name, out object children) {
		name = "array";
		children = _elements;
	}
}