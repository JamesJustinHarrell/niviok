using System.Collections.Generic;

interface IValue {
	IInterface activeInterface { get; }
	long objectID { get; }
	IValue cast(IInterface aInterface);
	IValue call(Arguments arguments);
	IValue getProperty(Identifier name);
	void setProperty(Identifier propName, IValue aValue);
	IValue callMethod(Identifier name, Arguments arguments);
}