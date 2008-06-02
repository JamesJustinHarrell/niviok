//implements the Interface interface

class Client_Interface {
	public static IWorker wrap(IInterface face) {
		Client_Interface o = new Client_Interface(face);
		NiviokObject obj = new NiviokObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.std_Interface, obj, new IWorker[]{} );

		builder.addMethod(
			new Identifier("equals?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_Interface, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.std_Interface, false),
				delegate(Scope args) {
					return Bridge.toClientBoolean(
						o.equals(
							Bridge.toNativeInterface(
								args.evaluateLocalIdentifier(
									new Identifier("value"))) ));
				},
				Bridge.debugScope ));
		
		IWorker rv = builder.compile();
		rv.nativeObject = face;
		obj.rootWorker = rv;
		return rv;
	}

	IInterface _face;
	
	public Client_Interface( IInterface face ) {
		_face = face;
	}
	
	public bool equals(IInterface face) {
		//xxx there are other situations where this should true
		return _face == face;
	}
}