//implements the Interface interface

class Client_Interface {
	public static IWorker wrap(IInterface face) {
		Client_Interface o = new Client_Interface(face);
		IObject obj = new DesalObject(face);
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.faceInterface, obj, new IWorker[]{} );

		builder.addMethod(
			new Identifier("equals?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceInterface, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.faceInterface, false),
				delegate(Scope args) {
					return Bridge.wrapBoolean(
						o.equals(
							Bridge.unwrapInterface(
								args.evaluateLocalIdentifier(
									new Identifier("value"))) ));
				},
				null ));
		
		return builder.compile();
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