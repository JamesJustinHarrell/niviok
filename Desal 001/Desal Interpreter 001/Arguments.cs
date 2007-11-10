using System.Collections.Generic;

/*
rational: IList<IValue> isn't good enough for arguments
consider this: doStuff(500, "foo", bam=34, smort="blah")

xxx should this be renamed something like "ArgumentList"?
type names aren't usually plural

xxx consider passing arguments in such a way that a class like this isn't needed
*/

class Arguments {
	IList<IValue> _unlabeled;
	IDictionary<Identifier, IValue> _labeled;
	
	public Arguments( IList<IValue> unlabeled, IDictionary<Identifier, IValue> labeled ) {
		if( unlabeled == null || labeled == null )
			throw new System.Exception("null arguments to Arguments");
		_unlabeled = unlabeled;
		_labeled = labeled;
	}
	
	public Scope setup(IList<Parameter> parameters) {
		Scope innerScope = new Scope();
		//xxxx
		innerScope.declarePervasive(
			new Identifier("value"), null, _unlabeled[0] );
		return innerScope;
	}
	
	public Scope setup(IList<Parameter> parameters, Scope outerScope) {
		if( _labeled.Count != 0 )
			throw new Error_Unimplemented();
		if( _unlabeled.Count != parameters.Count )
			throw new System.ApplicationException("argument count");

		Scope innerScope = new Scope(outerScope);
		
		for( int i = 0; i < parameters.Count; i++ ) {
			if( parameters[i].type.category == ReferenceCategory.VALUE &&
			parameters[i].type.face != _unlabeled[i].activeInterface )
				throw new System.ApplicationException("interface mismatch");
		
			innerScope.declarePervasive(
				parameters[i].name,
				parameters[i].type,
				_unlabeled[i] );
		}

		return innerScope;
	}
	
	public long count {
		get { return _unlabeled.Count + _labeled.Count; }
	}
}