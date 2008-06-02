using System.Collections.Generic;

//xxx would it be better to generate some of this automatically?

//any node
interface INode {
	//e.g. foo-bar-baz
	string typeName {get;}
	
	//null if and only if the type never contains any children
	//empty if all children are optional and absent
	ICollection<INode> childNodes {get;}
	
	//the location in a concrete representation that represents this abstract node
	//whatever this node came from
	string nodeSource {get;}
}

//xxx what's the deal with this?
//node that declares a value identikey
interface INode_Declaration : INode_Expression {
	Node_Identifier name {get;}
	Node_IdentikeyType identikeyType {get;}
}

//node that can be executed
interface INode_Expression : INode {}

interface INode_IdentikeySpecialNew : INode {}

interface INode_IdentikeySpecialOld : INode {}

interface INode_InterfaceMember : INode {}
