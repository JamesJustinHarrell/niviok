using System.Collections.Generic;

namespace Acrid.NodeTypes {

//xxx would it be better to generate some of this automatically?

//any node
public interface INode {
	//e.g. foo-bar-baz
	string typeName {get;}
	
	//null if and only if the type never contains any children
	//empty if all children are optional and absent
	ICollection<INode> childNodes {get;}
	
	//the location in a concrete representation that represents this abstract node
	//whatever this node came from
	string nodeSource {get;}
}

public interface INode_Expression : INode {}

public interface INode_StatementDeclaration : INode {}

public interface INode_InterfaceMember : INode {}

} //namespace
