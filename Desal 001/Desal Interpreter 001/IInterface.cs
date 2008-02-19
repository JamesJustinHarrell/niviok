//a Desal interface

using System.Collections.Generic;

interface IInterface {
	IList<IInterface> inheritees { get; }
	IDictionary<Identifier, PropertyInfo> properties { get; }
	//xxx IList<PropertyInfo> properties { get; }
	IDictionary<Identifier, IList<MethodInfo>> methods { get; }
	//xxx IList<MethodInfo> methods {	get; }
	IValue value { get; }
}


//xxx move elsewhere

enum Access {
	GET, SET, GET_SET
}

class PropertyInfo {
	public Identifier name;
	public NullableType type;
	public Access access; //null if private
	//xxx default values
	
	public PropertyInfo(){}
	
	public PropertyInfo(Identifier name, NullableType type, Access access) {
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