using System;

class Node_Boolean : TerminalNode<bool, Node_Boolean>, INode {
	public Node_Boolean(bool value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_Boolean(string str, string nodeSource)
		: base(Boolean.Parse(str), nodeSource) {}
	
	public string typeName {
		get { return "boolean"; }
	}
}

class Node_Direction : TerminalNode<Direction, Node_Direction>, INode {
	public Node_Direction(Direction value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_Direction(string str, string nodeSource)
		: base(G.parseEnum<Direction>(str), nodeSource) {}

	public string typeName {
		get { return "direction"; }
	}
}

class Node_Identifier : TerminalNode<Identifier, Node_Identifier>, INode_Expression {
	public Node_Identifier(Identifier value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_Identifier(string str, string nodeSource)
		: base(new Identifier(str), nodeSource) {}
	
	public string typeName {
		get { return "identifier"; }
	}
}

class Node_MemberStatus : TerminalNode<MemberStatus, Node_MemberStatus>, INode {
	public Node_MemberStatus(MemberStatus value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_MemberStatus(string str, string nodeSource)
		: base(G.parseEnum<MemberStatus>(str), nodeSource) {}
	
	public string typeName {
		get { return "member-status"; }
	}
}

class Node_WoScidentreCategory : TerminalNode<WoScidentreCategory, Node_WoScidentreCategory>, INode {
	public Node_WoScidentreCategory(WoScidentreCategory value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_WoScidentreCategory(string str, string nodeSource)
		: base(G.parseEnum<WoScidentreCategory>(str), nodeSource) {}
	
	public string typeName {
		get { return "wo-scidentre-category"; }
	}
}

//xxx use BigInt
class Node_Integer : TerminalNode<long, Node_Integer>, INode_Expression {
	public Node_Integer(long value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_Integer(string str, string nodeSource)
		: base(Int64.Parse(str), nodeSource) {}

	public string typeName {
		get { return "integer"; }
	}
}

class Node_MemberType : TerminalNode<MemberType, Node_MemberType>, INode {
	public Node_MemberType(MemberType value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_MemberType(string str, string nodeSource)
		: base(G.parseEnum<MemberType>(str), nodeSource) {}
	
	public string typeName {
		get { return "member-type"; }
	}
}

//xxx use BigNum library
class Node_Rational : TerminalNode<double, Node_Rational>, INode_Expression {
	public Node_Rational(double value, string nodeSource)
		: base(value, nodeSource) {}
	public Node_Rational(string str, string nodeSource)
		: base(Double.Parse(str), nodeSource) {}
	
	public string typeName {
		get { return "rational"; }
	}
}

//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates
//xxx ensure all strings are valid and meaningful, i.e. can't start with a combining character
class Node_String : TerminalNode<string, Node_String>, INode_Expression {
	public Node_String(string value, string nodeSource)
		: base(value, nodeSource) {}
	
	public string typeName {
		get { return "string"; }
	}
}