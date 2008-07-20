using System;
using System.Collections.Generic;
using Acrid.NodeTypes;

namespace Acrid.Execution {

class LibraryWrapper {
	public static IWorker wrap(ISieve sieve) {
		IList<Property> props = new List<Property>();
		foreach( Identifier name in sieve.visibleScidentreNames )
			props.Add(new Property(name, false, new NType()));
		IInterface face = new Interface(
			new IInterface[]{},
			new Callee[]{},
			new Breeder[]{},
			props,
			new Method[]{});
		NObject o = new NObject();
		WorkerBuilder b = new WorkerBuilder(face, o, new IWorker[]{});
		foreach( Identifier name in sieve.visibleScidentreNames ) {
			//The same 'name' variable is used through each iteration.
			//If the delegate refers to 'name', it will always get the value that 'name' refered to last.
			Identifier name2 = name;
			b.addPropertyGetter( name, delegate() {
				return GE.evalIdent(sieve, name2);
			});
		}
		return b.compile();
	}
}

}
