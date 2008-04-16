//wrapping of IFunction (whether native or client) to
//create an IObject object and appropriate IWorker objects

using System;
using System.Collections.Generic;

class Client_Function {
	public static IWorker wrap(IFunction func) {
		Client_Function o = new Client_Function(func);
		IObject obj = new DesalObject(func);
		WorkerBuilder builder = new WorkerBuilder(
			func.face, obj, new IWorker[]{} );
		
		builder.addCallee(func);
		
		return builder.compile();
	}

	IFunction _func;
	
	Client_Function(IFunction func) {
		_func = func;
	}
}
