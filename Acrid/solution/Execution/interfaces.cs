using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

//a collection of scidentres
//used by e.g. namespaces, sieves, and libraries
public interface IDerefable {
	DerefResults deref(IdentifierSequence idents);
	HashSet<IWoScidentre> findEmptyWoScidentres(IdentifierSequence idents);
}

public interface IFunction {
	IInterface face {get;}
	IWorker call(IList<Argument> arguments);
}

public interface IInterface {
	IList<IInterface> inheritees {get;}
	IList<Breeder> breeders {get;}
	IList<Callee> callees {get;}
	IDictionary<Identifier, Property> properties {get;}
	IDictionary<Identifier, IList<Method>> methods {get;}
	IWorker worker {get;}
}

public interface IObject {
	IWorker rootWorker {get;}
	bool sameObject(IObject o);
}

public interface IScope {
	void expose(IDerefable d);
	void bindNamespace(Identifier name, IDerefable d);
	IWoScidentre reserveWoScidentre(Identifier name, WoScidentreCategory cat);

	//scidentre reserved by the above function
	//used by declare-assign and declare-first nodes
	//@worker can be Null (but not null)
	void activateWoScidentre(Identifier name, NType type, IWorker worker);
	
	void assign(Identifier name, IWorker worker);
	
	//searches up a chain of scopes
	DerefResults upDeref(IdentifierSequence idents);
	
	/*
	returns every wo-scidentre that:
		* can be referenced by the specified Identifier sequence, and
		* is in the EMPTY state
	May also return others. All that is guaranteed is that the wanted ones are returned.
	*/
	HashSet<IWoScidentre> upFindEmptyWoScidentres(IdentifierSequence idents);
	
	ScopeAllowance allowance {get;}
}

public interface IWorker {
	IObject owner {get;}
	IList<IWorker> childWorkers {get;}
	IInterface face {get;}
	IWorker breed(IInterface face);
	IWorker call(IList<Argument> arguments);
	IWorker extractMember(Identifier name);
	void setProperty(Identifier propName, IWorker worker);
	object nativeObject {get;set;}
}

public interface IWoScidentre {
	IType type {get;set;}
	void assign(IWorker worker);
	DerefResults deref();
}

} //namespace
