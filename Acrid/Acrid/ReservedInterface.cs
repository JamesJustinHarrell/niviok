//an interface from a declare-first identikey that hasn't been assigned to yet

using System.Collections.Generic;

class ReservedInterface : IInterface {
	Identikey _key;
	
	public ReservedInterface(Identikey key) {
		_key = key;
	}
	
	public IList<IInterface> inheritees {
		get { return Bridge.toNativeInterface(_key.value).inheritees; }
	}
	
	public IList<Breeder> breeders {
		get { return Bridge.toNativeInterface(_key.value).breeders; }
	}
	
	public IList<Callee> callees {
		get { return Bridge.toNativeInterface(_key.value).callees; }
	}
	
	public IDictionary<Identifier, Property> properties {
		get { return Bridge.toNativeInterface(_key.value).properties; }
	}
	
	public IDictionary<Identifier, IList<Method>> methods {
		get { return Bridge.toNativeInterface(_key.value).methods; }
	}
	
	public IWorker worker {
		get { return _key.value; }
	}
}