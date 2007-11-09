//instance of a client class
//contains: scope, object id, reference to class that created object (XXX)

using System.Collections.Generic;
/*
class Client_Object : IObject<Scope> {
	Client_Class _class;
	public Scope scope;
	
	public Client_Object( IList<ClassMember> members, Client_Class clientClass ) {
	/*
		scope = new Scope();
		foreach( ClassMember member in members ) {
			if( member.type is IFunctionInterface ) {
				scope.declareFunctionIdentifier(
					member.name, member.type as IFunctionInterface );
			}
			else {
				scope.declareObjectIdentifier(
					member.name, member.type );
			}
		}
		_class = class_;
		* /
	}
	
	public long ID {
		get { throw new Error_Unimplemented(); }
	}
	
	public Class associatedClass {
		get { return _class; }
	}
} */