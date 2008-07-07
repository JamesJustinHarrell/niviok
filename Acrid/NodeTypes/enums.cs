namespace Acrid.NodeTypes {

//corresponds to "direction" nodes
public enum Direction {
	IN, OUT, INOUT
}

//corresponds to "wo-scidentre-category" nodes
public enum WoScidentreCategory {
	CONSTANT, FUNCTION, VARIABLE
}

//corresponds to "member-status" nodes
public enum MemberStatus {
	NEW, NORMAL, DEPRECATED
}

//corresponds to "member-type" nodes
public enum MemberType {
	BREEDER, CALLEE, PROPERTY_GETTER, PROPERTY_SETTER, METHOD
}

} //namespace
