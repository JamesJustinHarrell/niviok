//wrapping of IFunction (whether native or client) to
//create an IObject object and appropriate IWorker objects

using System;
using System.Collections.Generic;

class Client_Function {
	public static IWorker wrap(IFunction func) {
		Client_Function o = new Client_Function(func);
		DesalObject obj = new DesalObject();
		WorkerBuilder builder = new WorkerBuilder(
			func.face, obj, new IWorker[]{} );
		
		builder.addCallee(func);
		
		builder.addPropertyGetter(
			new Identifier("parameterCount"),
			delegate(){ return Bridge.toClientInteger(o.parameterCount); });
		
		builder.addMethod(new Identifier("call"), func);
		
		IWorker rv = builder.compile();
		rv.nativeObject = func;
		obj.rootWorker = rv;
		return rv;
	}

	IFunction _func;
	
	Client_Function(IFunction func) {
		_func = func;
	}
	
	//xxx temporary
	public int parameterCount {
		get {
			IEnumerator<Callee> e = _func.face.callees.GetEnumerator();
			e.MoveNext();
			return e.Current.parameters.Count;
		}
	}
}
