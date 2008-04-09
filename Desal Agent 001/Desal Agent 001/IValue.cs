//xxx using System.Collections.Generic;

interface IValue {
	IInterface activeInterface { get; }
	IValue cast(IInterface aInterface);
	IValue call(Arguments arguments);
	IValue extractMember(Identifier name);
	void setProperty(Identifier propName, IValue aValue);
/*xxx
	long objectID { get; }
	IValue getProperty(Identifier name);
	IValue callMethod(Identifier name, Arguments arguments);
*/
}