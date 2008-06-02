//implements the Bool interface

class Client_Boolean {
	//xxx automate wrapping
	public static IWorker wrap(bool value) {
		Client_Boolean o = new Client_Boolean(value);
		NiviokObject obj = new NiviokObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.std_Bool, obj, new IWorker[]{} );
		
		builder.addBreeder(
			Bridge.std_String,
			delegate(){
				return Bridge.toClientString(
					value.ToString().ToLower());
			});

		builder.addMethod(
			new Identifier("equals?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_Bool, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.std_Bool, false),
				delegate(Scope args) {
					return Bridge.toClientBoolean(
						o.equals(
							Bridge.toNativeBoolean(
								args.evaluateLocalIdentifier(
									new Identifier("value"))) ));
				},
				Bridge.debugScope ));
		
		IWorker rv = builder.compile();
		rv.nativeObject = value;
		obj.rootWorker = rv;
		return rv;
	}

	bool _value;
	
	Client_Boolean(bool value) {
		_value = value;
	}
	
	bool equals(bool value) {
		return _value == value;
	}
}