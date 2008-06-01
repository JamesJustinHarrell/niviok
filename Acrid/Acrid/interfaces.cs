using System.Collections.Generic;

interface IFunction {
	IInterface face {get;}
	IWorker call(IList<Argument> arguments);
}

interface IInterface {
	IList<IInterface> inheritees {get;}
	IList<Breeder> breeders {get;}
	IList<Callee> callees {get;}
	IDictionary<Identifier, Property> properties {get;}
	IDictionary<Identifier, IList<Method>> methods {get;}
	IWorker worker {get;}
}

interface IObject {
	IWorker rootWorker {get;}
	bool sameObject(IObject o);
}

interface IWorker {
	IObject owner {get;}
	IList<IWorker> childWorkers {get;}
	IInterface face {get;}
	IWorker breed(IInterface face);
	IWorker call(IList<Argument> arguments);
	IWorker extractMember(Identifier name);
	void setProperty(Identifier propName, IWorker worker);
	object nativeObject {get;set;}
}
