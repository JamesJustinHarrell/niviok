//used by Node_DeclareClass and Node_Class

interface INode_Declaration : INode_Expression {
	Node_Identifier name {get;}
}

//xxx not used anymore
interface INode_DeclareAny : INode_Expression {}
