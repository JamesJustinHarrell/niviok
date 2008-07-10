//implements the Bool interface

using Acrid.NodeTypes;

namespace Acrid.Execution {

public class Client_Boolean {
	//xxx automate wrapping
	//this method should only be called twice ever -- once each for std:true and std:false
	public static IWorker wrap(bool value) {
		Client_Boolean o = new Client_Boolean(value);
		NObject obj = new NObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.stdn_Bool, obj, new IWorker[]{} );
		
		builder.addBreeder(
			Bridge.stdn_String,
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
						new NType(Bridge.stdn_Bool),
						new Identifier("value"),
						null )
				},
				new NType(Bridge.stdn_Bool),
				delegate(IScope args) {
					return Bridge.toClientBoolean(
						o.equals(
							Bridge.toNativeBoolean(
								GE.evalIdent(args, "value"))));
				},
				null ));
		
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

} //namespace
