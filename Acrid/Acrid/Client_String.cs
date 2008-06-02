//implements the String interface

using System;
using System.Collections.Generic;

class Client_String {
	public static IList<uint> unwrap(IWorker worker) {
		object native = worker.nativeObject;
		try {
			return (IList<uint>)native;
		}
		catch(InvalidCastException e) {
			throw new ClientException(
				String.Format(
					"this object is not a builtin string ({0})",
					native));
		}
	}

	public static IWorker wrap(IList<uint> codePoints) {
		Client_String o = new Client_String(codePoints);
		NiviokObject obj = new NiviokObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.std_String, obj, new IWorker[]{});

		builder.addBreeder(
			Bridge.std_String,
			delegate(){ return wrap(codePoints); });

		builder.addPropertyGetter(
			new Identifier("length"),
			delegate(){ return Bridge.toClientInteger(o.length); });

		builder.addMethod(
			new Identifier("concat"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_String, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.std_String, false),
				delegate(Scope args) {
					return wrap(
						o.concat(
							unwrap(
								args.evaluateIdentifier(
									new Identifier("value"))) ));
				},
				Bridge.debugScope ));
			
		builder.addMethod(
			new Identifier("concat!"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_String, false),
						new Identifier("value"),
						null )
				},
				null,
				delegate(Scope args) {
					o.concat0(
						unwrap(
							args.evaluateIdentifier(
								new Identifier("value"))) );
					return wrap(o._codePoints);
				},
				Bridge.debugScope ));

		builder.addMethod(
			new Identifier("substring"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_Int, false),
						new Identifier("start"),
						null )
				},
				new NullableType(Bridge.std_String, false),
				delegate(Scope args) {
					return wrap(
							o.substring(
								Bridge.toNativeInteger(
									args.evaluateLocalIdentifier(
										new Identifier("start"))) ));
				},
				Bridge.debugScope ));

		builder.addMethod(
			new Identifier("substring"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_Int, false),
						new Identifier("start"),
						null ),
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_Int, false),
						new Identifier("limit"),
						null )
				},
				new NullableType(Bridge.std_String, false),
				delegate(Scope args) {
					return wrap(
							o.substring(
								Bridge.toNativeInteger(
									args.evaluateLocalIdentifier(
										new Identifier("start"))),
								Bridge.toNativeInteger(
									args.evaluateLocalIdentifier(
										new Identifier("limit"))) ));
				},
				Bridge.debugScope ));
		
		IWorker rv = builder.compile();
		rv.nativeObject = codePoints;
		obj.rootWorker = rv;
		return rv;
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