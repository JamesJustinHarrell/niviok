//implements the Interface interface

class Client_Interface {
	public static IWorker wrap(IInterface face) {
		Client_Interface o = new Client_Interface(face);
		NObject obj = new NObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.stdn_Interface, obj, new IWorker[]{} );

		builder.addMethod(
			new Identifier("equals?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NType(Bridge.stdn_Interface),
						new Identifier("value"),
						null )
				},
				new NType(Bridge.stdn_Interface),
				delegate(IScope args) {
					return Bridge.toClientBoolean(
						o.equals(
							Bridge.toNativeInterface(
								G.evalIdent(args, "value"))));
				},
				null ));
		
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