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
	public static IInterface wrap(IValue val) {
		/* xxx
		if( val.object_ is InterfaceWrapper )
			return ((InterfaceWrapper)val.object_).unwrap();
		else
		*/
			return new InterfaceFromValue(val);
	}
	
	IValue _value;
	
	InterfaceFromValue(IValue val) {
		_value = val;
	}

	public IList<IInterface> inheritees {
		get { throw new Error_Unimplemented(); }
	}
	/* xxx
	public IList<Parameter> callee {
		get { throw new Error_Unimplemented(); }
	}
	
	public IInterface returnType {
		get { throw new Error_Unimplemented(); }
	}
	*/
	public IList<PropertyInfo> properties {
		get { throw new Error_Unimplemented(); }
	}
	
	public IList<MethodInfo> methods {
		get { throw new Error_Unimplemented(); }
	}
	
	public IValue value {
		get { return _value; }
	}
}