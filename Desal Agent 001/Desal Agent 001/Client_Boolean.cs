//implements the Bool interface

class Client_Boolean {
	//xxx automate wrapping
	public static IWorker wrap(bool value) {
		Client_Boolean o = new Client_Boolean(value);
		IObject obj = new DesalObject(value);
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.faceBool, obj, new IWorker[]{} );

		builder.addMethod(
			new Identifier("equals?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceBool, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.faceBool, false),
				delegate(Scope args) {
					return Bridge.wrapBoolean(
						o.equals(
							Bridge.unwrapBoolean(
								args.evaluateLocalIdentifier(
									new Identifier("value"))) ));
				},
				null ));
		
		return builder.compile();
	}

	bool _value;
	
	Client_Boolean(bool value) {
		_value = value;
	}
	
	bool equals(bool value) {
		return _value == value;
	}
}