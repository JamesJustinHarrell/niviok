//corresponds to "direction" nodes
enum Direction {
	IN, OUT, INOUT
}

//corresponds to "wo-scidentre-category" nodes
enum WoScidentreCategory {
	CONSTANT, FUNCTION, VARIABLE
}

//corresponds to "member-status" nodes
enum MemberStatus {
	NEW, NORMAL, DEPRECATED
}

//corresponds to "member-type" nodes
enum MemberType {
	BREEDER, CALLEE, PROPERTY_GETTER, PROPERTY_SETTER, METHOD
}
