//implements a non-generic version of the Generator interface

/*
A is the main thread
B is a generator thread

B is suspended
A is running
A calls yield() on a generator
resume B
A waits until B suspends or ends
get current yield value from B
resume A
*/

using System;
using System.Threading;

class Client_Generator {
	//xxx automate wrapping
	public static IWorker wrap(INode_Expression body, Scope scope) {
		Client_Generator o = new Client_Generator(body, scope);
		IObject obj = new DesalObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.faceGenerator, obj, new IWorker[]{} );

		builder.addMethod(
			new Identifier("yield"),
			new Function_Native(
				new ParameterImpl[]{},
				NullableType.dyn_nullable,
				delegate(Scope args) {
					return o.@yield();
				},
				Bridge.universalScope ));
		
		return builder.compile();
	}

	Thread _thread;
	Scope _closure;
	
	Client_Generator(INode_Expression body, Scope scope) {
		_closure = scope.createGeneratorClosure(Depends.depends(body));
		_thread = new Thread(new ThreadStart(delegate() {
			Executor.executeAny(body, _closure);
		}));
		_thread.IsBackground = true;
	}
	
	~Client_Generator() {
		try { _thread.Abort(); }
		catch(ThreadStateException) { _thread.Resume(); }
	}
	
	bool isRunning() {
		return
			(! isState(ThreadState.Stopped)) &&
			(! isState(ThreadState.Suspended)) &&
			(! isState(ThreadState.WaitSleepJoin)) ;
	}
	
	bool isState(ThreadState state) {
		#if GEN_DEBUG
		Console.WriteLine("current state: " + _thread.ThreadState);
		Console.WriteLine("wanted state: " + state);
		#endif
	
		return (_thread.ThreadState & state) == state;
	}
	
	void outputState(string location) {
		Console.WriteLine(
			String.Format(
				"{0} ; thread state is {1} ; yieldValue is {2}",
				location,
				_thread.ThreadState,
				_closure.yieldValue == null ? "null" :
					Bridge.unwrapInteger(_closure.yieldValue).ToString()));
		Console.Out.Flush();
	}
	
	IWorker @yield() {
		#if GEN_DEBUG
		outputState("yield called");
		#endif
	
		if( isState(ThreadState.Unstarted) )
			_thread.Start();
		else if( isState(ThreadState.WaitSleepJoin) ) {
			#if GEN_DEBUG
			Console.WriteLine("fixin to call Interrupt");
			Console.Out.Flush();
			#endif
			_thread.Interrupt();
		}
		else if( isState(ThreadState.Stopped) )
			throw new ClientException("generator exhausted");
		else
			throw new ApplicationException(
				"unexpected thread state: " + _thread.ThreadState);
	
		#if GEN_DEBUG
		outputState("before while");
		#endif
	
		while( (isState(ThreadState.WaitSleepJoin) || isRunning()) &&
		_closure.yieldValue == null )
			Thread.Sleep(1);
	
		#if GEN_DEBUG
		outputState("after while");
		#endif
		
		//wait for generator thread to end or put itself to sleep
		while( isRunning() )
			Thread.Sleep(1);
		
		#if GEN_DEBUG
		outputState("after wait for sleep");
		#endif
		
		if( isState(ThreadState.WaitSleepJoin) ) {
			#if GEN_DEBUG
			outputState("one");
			#endif
			IWorker yieldValue = _closure.yieldValue;
			_closure.yieldValue = null;
			#if GEN_DEBUG
			outputState("two");
			#endif
			return yieldValue;
		}

		throw new ClientException("generator exhausted");
	}
}
