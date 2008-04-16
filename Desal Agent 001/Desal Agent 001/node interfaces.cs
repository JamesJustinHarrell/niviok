using System.Collections.Generic;

//xxx would it be better to generate some of this automatically?

//any Desal node
interface INode {
	//e.g. foo-bar-baz
	string typeName {get;}
	
	//null if and only if the type never contains any children
	//empty if all children are optional and absent
	ICollection<INode> childNodes {get;}
}

//e.g. import, using, expose
interface INode_ScopeAlteration : INode {}

//node that can be executed
interface INode_Expression : INode {}

//node that declares a value identikey
interface INode_Declaration : INode_Expression {
	Node_Identifier name {get;}
	Node_IdentikeyType identikeyType {get;}
}

interface INode_InterfaceMember : INode {}

