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
using System.Collections.Generic;
using System.Threading;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public class Client_Generator {
	static IDictionary<Thread,IWorker> _yieldValues = new Dictionary<Thread,IWorker>();
	
	public static void setYieldValue(Thread thread, IWorker yieldValue) {
		_yieldValues[thread] = yieldValue;
	}
	
	public static bool hasNullYieldValue(Thread thread) {
		return _yieldValues[thread] == null;
	}

	//xxx automate wrapping
	public static IWorker wrap(INode_Expression body, IScope scope) {
		Client_Generator o = new Client_Generator(body, scope);
		IObject obj = new NObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.stdn_Generator, obj, new IWorker[]{} );

		builder.addMethod(
			new Identifier("yield"),
			new Function_Native(
				new ParameterImpl[]{},
				Bridge.stdn_Nullable_any,
				delegate(IScope args) {
					return o.@yield();
				},
				null ));
		
		return builder.compile();
	}

	Thread _thread;
	IScope _closure;
	ClientException _exception;
	
	Client_Generator(INode_Expression body, IScope scope) {
		_closure = GE.createClosure(Depends.depends(body), scope);
		_thread = new Thread(new ThreadStart(delegate() {
			try {
				Executor.executeAny(body, _closure);
			}
			catch(ClientException e) {
				_exception = e;
			}
		}));
		_thread.IsBackground = true;
		_yieldValues.Add(_thread, null);
	}
	
	~Client_Generator() {
		_thread.Abort();
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
				_yieldValues[_thread] == null ? "null" :
					Bridge.toNativeInteger(_yieldValues[_thread]).ToString()));
		Console.Out.Flush();
	}
	
	IWorker @yield() {
		#if GEN_DEBUG
		outputState("yield called");
		#endif
	
		if( _exception != null )
			throw _exception;
	
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
		_yieldValues[_thread] == null )
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
			IWorker yieldValue = _yieldValues[_thread];
			//setting to null required for workaround used in execute(Node_Yield, IScope)
			_yieldValues[_thread] = null;
			#if GEN_DEBUG
			outputState("two");
			#endif
			return yieldValue;
		}

		throw new ClientException("generator exhausted");
	}
}

} //namespace



/*
xxxx yield node replaced with yield function

	//yield
	//highly coupled with Client_Generator
	public static IWorker execute(Node_Yield node, IScope scope) {
		IWorker yieldValue = executeAny(node.value, scope);
		#if GEN_DEBUG
		Console.WriteLine("fixin to yield " + Bridge.toNativeInteger(yieldValue));
		Console.Out.Flush();
		#endif
		
		Client_Generator.setYieldValue(Thread.CurrentThread, yieldValue);
		
		//xxx for each call to Interrupt, Mono throws ThreadInterruptedException twice
		//the while loop is a workaround which would otherwise not be required
		while( ! Client_Generator.hasNullYieldValue(Thread.CurrentThread) ) {
			try {
				Thread.Sleep(Timeout.Infinite);
			}
			catch(ThreadInterruptedException e) {
				#if GEN_DEBUG
				Console.WriteLine("generator awoken");
				Console.Out.Flush();
				#endif
			}
		}
		
		return new Null(); //note: in future, may add something like Python's "send" method
	}
*/

