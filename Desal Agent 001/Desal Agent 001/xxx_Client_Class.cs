//class defined by client code
//a Client_Class instance is created during evaluation of class nodes

using System.Collections.Generic;
/* 
class Client_Class : IClass {
/* xxx
	IList<PrivateClassMember> _privateMembers;
	IList<Client_InterfaceImplementation> _implementations;
	Client_InterfaceImplementation _defaultImp;
	IList<IFunction> _constructors;
* /
	public Client_Class(
	IList<PrivateClassMember> privateMembers,
	IList<InterfaceImplementationInfo> implementationInfos,
	InterfaceImplementationInfo defaultImp,
	IList<IFunction> constructors ) {
	/* xxx
		_privateMembers = privateMembers;
		_implementations = new List<Client_InterfaceImplementation>();
		foreach( InterfaceImplementationInfo info in implementationInfos ) {
			InterfaceImplementation imp = new InterfaceImplementation(
				this, info.interface_, info.callees,
				info.getters, info.setters, info.methods );
			if( defaultImp.Equals(info) ) //xxx will this ever be true?
				_defaultImp = imp;
			_implementations.Add(imp);
		}
		_constructors = constructors;
* /
	}
	
	public IWorker instantiate(IList<IWorker> IList<Argument>) {
	/* xxx
		Client_Object obj = new Client_Object(_members, this);
		//xxx call appropriate constructor with object and IList<Argument>
		return new Client_Value(obj, _defaultImp);
	* /
	}
}

class PrivateClassMember {
	public Identifier name;
	public IInterface type;
}
*/