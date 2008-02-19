using System.Collections.Generic;

/*
A collection of arguments being passed during a call.

XXX Note that this class is extremely outdated,
and can't be updated until arguments have been speced out better.

The value receiving these arguments will need access to the arguments
to determine which callee should be used.
*/

class Arguments {
	IList<IValue> _unlabeled;
	IDictionary<Identifier, IValue> _labeled;
	
	public Arguments( IList<IValue> unlabeled, IDictionary<Identifier, IValue> labeled ) {
		Bridge.checkNull(new object[]{ unlabeled, labeled });
		_unlabeled = unlabeled;
		_labeled = labeled;
	}
	
	//create the scope to be used by the function body
	//assumes the arguments have been matched to an appropriate function
	public Scope setup(IList<Parameter> parameters, Scope outerScope) {
		Scope innerScope = new Scope(outerScope);
		
		/* xxx
		Should reserve identikeys with Scope::reserveDeclareFirst()
		and then assign the arguments with Scope::declareFirst().
		Then finalize the scope before using.
		That will ensure no arguments have names not specified by
		the parameters, and that no parameters go without values.
		*/
		
		//bind unlabled arguments
		for( int i = 0; i < parameters.Count; i++ )
			innerScope.declareAssign( parameters[i].name, _unlabeled[i] );
		
		//bind labled arguments
		foreach( Identifier name in _labeled.Keys )
			innerScope.declareAssign( name, _labeled[name] );

		return innerScope;
	}
	
	//currently all that is used to match up arguments with correct callee
	public long count {
		get { return _unlabeled.Count + _labeled.Count; }
	}
}