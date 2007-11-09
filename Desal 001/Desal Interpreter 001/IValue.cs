using System.Collections.Generic;

interface IValue {
	IInterface activeInterface { get; }
	long objectID { get; }
	IValue cast(IInterface aInterface);
	void executeCall(Arguments arguments);
	IValue evaluateCall(Arguments arguments);
	IValue getProperty(Identifier name);
	void setProperty(Identifier propName, IValue aValue);
	void executeMethod(Identifier name, Arguments arguments);
	IValue evaluateMethod(Identifier name, Arguments arguments);
}