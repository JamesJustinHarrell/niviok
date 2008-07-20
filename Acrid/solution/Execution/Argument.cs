//corresponds to "argument" nodes

using System.Collections.Generic;
using System.Diagnostics;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public class Argument {

	//create the scope to be used by a function body
	//assumes the arguments have already been matched to an appropriate function
	public static IScope setupArguments(
	IList<ParameterImpl> parameters, IList<Argument> arguments, IScope outerScope) {
		Debug.Assert(outerScope != null);
		IScope innerScope = new Scope(outerScope, null);
		
		for( int i = 0; i < arguments.Count; i++ ) {
			GE.declareAssign(
				parameters[i].name,
				ScidentreCategory.VARIABLE,
				new NType(),
				arguments[i].value,
					/* xxx downcast(arguments[i].value, parameters[i].type) */
				innerScope
			);
		}

		return innerScope;
	}

	Identifier _name;
	IWorker _value;
	
	public Argument(Identifier name, IWorker value) {
		_name = name;
		_value = value;
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public IWorker value {
		get { return _value; }
	}
}

} //namespace
