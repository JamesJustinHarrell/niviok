using System.Collections.Generic;

interface IFunction {
	IInterface face {get;}
	IWorker call(IList<Argument> arguments);
}

interface IInterface {
	IList<IInterface> inheritees {get;}
	//xxx IList<BreederInfo> breeders {get;}
	IList<CalleeInfo> callees {get;}
	IDictionary<Identifier, PropertyInfo> properties {get;}
	IDictionary<Identifier, IList<MethodInfo>> methods {get;}
}

interface IObject {
	IWorker rootWorker {get;}
	bool sameObject(IObject o);
	object native {get;}
}

interface IWorker {
	IObject owner {get;}
	IList<IWorker> children {get;}
	IInterface face {get;}
	IWorker breed(IInterface face);
	IWorker call(IList<Argument> arguments);
	IWorker extractMember(Identifier name);
	void setProperty(Identifier propName, IWorker worker);
}
