//an interface from a declare-first identikey that hasn't been assigned to yet

using System;
using System.Collections.Generic;

class ReservedType : IType {
	public static IType tryWrap( INode_Expression node, IScope scope ) {
		//xxx
		return new NType();
	}
}
