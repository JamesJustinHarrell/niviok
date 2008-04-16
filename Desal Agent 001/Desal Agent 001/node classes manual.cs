class Node_Access : TerminalNode<Access, Node_Access>, INode {
	public Node_Access(Access value) :base(value) {}
	public Node_Access(string str) :base(G.parseEnum<Access>(str)) {}

	public string typeName {
		get { return "access"; }
	}
}

class Node_Boolean : TerminalNode<bool, Node_Boolean>, INode {
	public Node_Boolean(bool value) :base(value) {}
	public Node_Boolean(string str) :base(System.Boolean.Parse(str)) {}
	
	public string typeName {
		get { return "boolean"; }
	}
}

class Node_Direction : TerminalNode<Direction, Node_Direction>, INode {
	public Node_Direction(Direction value) :base(value) {}
	public Node_Direction(string str) :base(G.parseEnum<Direction>(str)) {}

	public string typeName {
		get { return "direction"; }
	}
}

class Node_Identifier : TerminalNode<Identifier, Node_Identifier>, INode_Expression {
	public Node_Identifier(Identifier value) : base(value) {}
	public Node_Identifier(string str) :base(new Identifier(str)) {}
	
	public string typeName {
		get { return "identifier"; }
	}
}

class Node_IdentikeyCategory : TerminalNode<IdentikeyCategory, Node_IdentikeyCategory>, INode {
	public Node_IdentikeyCategory(IdentikeyCategory value) :base(value) {}
	public Node_IdentikeyCategory(string str) :base(G.parseEnum<IdentikeyCategory>(str)) {}
	
	public string typeName {
		get { return "identikey-category"; }
	}
}

//xxx use BigInt
class Node_Integer : TerminalNode<long, Node_Integer>, INode_Expression {
	public Node_Integer(long value) :base(value) {}
	public Node_Integer(string str) :base(System.Int64.Parse(str)) {}

	public string typeName {
		get { return "integer"; }
	}
}

class Node_MemberType : TerminalNode<MemberType, Node_MemberType>, INode {
	public Node_MemberType(MemberType value) :base(value) {}
	public Node_MemberType(string str) :base(G.parseEnum<MemberType>(str)) {}
	
	public string typeName {
		get { return "member-type"; }
	}
}

//xxx use BigNum library
class Node_Rational : TerminalNode<double, Node_Rational>, INode_Expression {
	public Node_Rational(double value) :base(value) {}
	public Node_Rational(string str) :base(System.Double.Parse(str)) {}
	
	public string typeName {
		get { return "rational"; }
	}
}

//xxx support Unicode, including astral characters
//xxx make sure no code points are surrogates
//xxx ensure all strings are valid and meaningful, i.e. can't start with a combining character
class Node_String : TerminalNode<string, Node_String>, INode_Expression {
	public Node_String(string value) :base(value) {}
	
	public string typeName {
		get { return "string"; }
	}
}