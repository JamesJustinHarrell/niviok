using System.Collections.Generic;

class Node_ExtractMember : INode_Expression {
	INode_Expression _sourceNode;
	Node_Identifier _memberNameNode;

	public Node_ExtractMember(
	INode_Expression sourceNode, Node_Identifier memberNameNode) {
		_sourceNode = sourceNode;
		_memberNameNode = memberNameNode;
	}
	
	public IValue execute(Scope scope) {
		IValue source = _sourceNode.execute(scope);
		Identifier memberName = _memberNameNode.identifier;
		return source.extractNamedMember(memberName);
	}
	
	public string typeName {
		get { return "extract-member"; }
	}
	
	public ICollection<INode> children {
		get { return new INode[]{ _sourceNode, _memberNameNode }; }
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { return _sourceNode.identikeyDependencies; }
	}
}