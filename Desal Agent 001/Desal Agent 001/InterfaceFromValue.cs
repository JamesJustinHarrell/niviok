//makes a Desal object that implements the Interface interface look like an IInterface

/*
Remember that an object can have
multiple implementations of the same interface.

Example:

interface Foo
	inherit interface Interface

singleton bar
	interface Interface
		...
	interface Foo
		interface Interface
			...

Interface a = (bar as Interface)
Foo b = (bar as Foo)
Interface c = (b as Interface)

Identifiers a and c are both bound to the same object,
and both object references have Interface as the active interface,
but each object reference refers to a different implementation of the Interface interface. 

The object reference should keep track of which implementation is active.
*/

using System.Collections.Generic;

class InterfaceFromValue : IInterface {
	public static IInterface wrap(IWorker val) {
		return new InterfaceFromValue(val);
	}
	
	IWorker _value;
	
	InterfaceFromValue(IWorker val) {
		_value = val;
	}

	public IList<IInterface> inheritees {
		get { throw new Error_Unimplemented(); }
	}
	
	public IList<CalleeInfo> callees {
		get { throw new Error_Unimplemented(); }
	}
	
	public IDictionary<Identifier, PropertyInfo> properties {
		get { throw new Error_Unimplemented(); }
	}
	
	public IDictionary<Identifier, IList<MethodInfo>> methods {
		get { throw new Error_Unimplemented(); }
	}
	
	public IWorker value {
		get { return _value; }
	}
}