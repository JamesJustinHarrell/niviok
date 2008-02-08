//a Desal interface

using System.Collections.Generic;

interface IInterface {
	IList<IInterface> inheritees { get; }
	IList<PropertyInfo> properties { get; }	
	IList<MethodInfo> methods {	get; }
	IValue value { get; }
}


//xxx move elsewhere

enum Access {
	GET, SET, GET_SET
}

class PropertyInfo {
	public Identifier name;
	public ReferenceType type;
	public Access access; //null if private
	//xxx default values
	
	public PropertyInfo(){}
	
	public PropertyInfo(Identifier name, ReferenceType type, Access access) {
		this.name = name;
		this.type = type;
		this.access = access;
	}
}

class MethodInfo {
	public Identifier name;
	public IFunctionInterface iface;
	
	public MethodInfo(){}
	
	public MethodInfo(Identifier name, IFunctionInterface iface) {
		this.name = name;
		this.iface = iface;
	}
}