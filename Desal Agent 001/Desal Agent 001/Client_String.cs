//implements the String interface

using System;
using System.Collections.Generic;

class Client_String {
	public static IWorker wrap(IList<uint> codePoints) {
		Client_String o = new Client_String(codePoints);
		IObject obj = new DesalObject(codePoints);
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.faceString, obj, new IWorker[]{});

		builder.addPropertyGetter(
			new Identifier("length"),
			delegate(){ return Bridge.wrapInteger(o.length); });

		builder.addMethod(
			new Identifier("concat"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceString, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.faceString, false),
				delegate(Scope args) {
					return Bridge.wrapCodePoints(
						o.concat(
							Bridge.unwrapCodePoints(
								args.evaluateIdentifier(
									new Identifier("value"))) ));
				},
				Bridge.universalScope ));
			
		builder.addMethod(
			new Identifier("concat!"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceString, false),
						new Identifier("value"),
						null )
				},
				null,
				delegate(Scope args) {
					o.concat0(
						Bridge.unwrapCodePoints(
							args.evaluateIdentifier(
								new Identifier("value"))) );
					return Bridge.wrapCodePoints(o._codePoints);
				},
				Bridge.universalScope ));

		builder.addMethod(
			new Identifier("substring"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceInt, false),
						new Identifier("start"),
						null )
				},
				new NullableType(Bridge.faceString, false),
				delegate(Scope args) {
					return Bridge.wrapCodePoints(
							o.substring(
								Bridge.unwrapInteger(
									args.evaluateLocalIdentifier(
										new Identifier("start"))) ));
				},
				Bridge.universalScope ));

		builder.addMethod(
			new Identifier("substring"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceInt, false),
						new Identifier("start"),
						null ),
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceInt, false),
						new Identifier("limit"),
						null )
				},
				new NullableType(Bridge.faceString, false),
				delegate(Scope args) {
					return Bridge.wrapCodePoints(
							o.substring(
								Bridge.unwrapInteger(
									args.evaluateLocalIdentifier(
										new Identifier("start"))),
								Bridge.unwrapInteger(
									args.evaluateLocalIdentifier(
										new Identifier("limit"))) ));
				},
				Bridge.universalScope ));
		
		return builder.compile();
	}

	List<uint> _codePoints;

	public Client_String(IList<uint> codePoints) {
		_codePoints = new List<uint>(codePoints);
	}

	public long length {
		get { return _codePoints.Count; }
	}

	public IList<uint> concat(IList<uint> val) {
		List<uint> rv = new List<uint>();
		rv.AddRange(_codePoints);
		rv.AddRange(val);
		return rv;
	}
	
	public void concat0(IList<uint> val) {
		_codePoints.AddRange(val);
	}
	
	public IList<uint> substring(long a) {
		throw new NotImplementedException();
	}
	
	public IList<uint> substring(long a, long b) {
		throw new NotImplementedException();
	}
}