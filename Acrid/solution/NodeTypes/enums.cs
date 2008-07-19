namespace Acrid.NodeTypes {

//corresponds to "direction" nodes
public enum Direction {
	IN, OUT, INOUT
}

//corresponds to "member-status" nodes
public enum MemberStatus {
	NEW, NORMAL, DEPRECATED
}

//corresponds to "member-type" nodes
public enum MemberType {
	BREEDER, CALLEE, GETTER, SETTER, METHOD
}

} //namespace
