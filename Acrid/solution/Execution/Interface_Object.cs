//implements the client Object interface

using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class Interface_Object : IInterface {
	//----- static
	
	public static IInterface _instance = new Interface_Object();
	
	public static IInterface instance {
		get { return _instance; }
	}

	//----- instance
	
	IWorker _worker;
	
	//make constructor private
	Interface_Object(){}

	public IList<IInterface> inheritees {
		get { return new List<IInterface>(); }
	}
	
	public IList<Breeder> breeders {
		get { return new List<Breeder>(); }
	}
	
	public IList<Callee> callees {
		get { return new List<Callee>(); }
	}
	
	public IDictionary<Identifier, Property> properties {
		get { return new Dictionary<Identifier, Property>(); }
	}
	
	public IDictionary<Identifier, IList<Method>> methods {
		get { return new Dictionary<Identifier, IList<Method>>(); }
	}
	
	public IWorker worker {
		get {
			if( _worker == null )
				_worker = Client_Interface.wrap(this);
			return _worker;
		}
	}
}

} //namespace
