//implements the client Interface interface

using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class Interface_Interface : IInterface {
	//----- static
	
	public static IInterface _instance = new Interface_Interface();
	
	public static IInterface instance {
		get { return _instance; }
	}

	//----- instance
	
	IWorker _worker;
	
	//make constructor private
	Interface_Interface(){}

	public IList<IInterface> inheritees {
		get { return new IInterface[]{ Bridge.stdn_Object }; }
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
