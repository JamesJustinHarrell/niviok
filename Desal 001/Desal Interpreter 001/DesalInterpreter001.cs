using System; //for System.Console
using System.Collections.Generic;

class DesalInterpreter001 {
	static int Main(string[] arguments) {
		//arguments have the form: -key=value -key=value
		
		IDictionary<string,string> args =
			new Dictionary<string,string>();
		
		args.Add("unhandled-warn-level", "2");
		args.Add("print-tree", "false");
		args.Add("run", "true");
		
		foreach( string arg in arguments ) {
			if( arg[0] == '-' ) {
				string[] parts = arg.Split('=');
				if( parts.Length != 2 )
					throw new Exception("bad argument: " + arg);
				string key = parts[0].Substring(1);
				string val = parts[1];
				args.Remove(key);
				args.Add(key, val);
			}
			else throw new Exception("bad argument: " + arg);
		}
		
		DesalInterpreter001 program = new DesalInterpreter001();
		
		Bridge bridge = new Bridge();
		DesibleParser parser = new DesibleParser();
		parser.unhandledWarnLevel = Int32.Parse(args["unhandled-warn-level"]);
		
		if( args.ContainsKey("path") ) {
			Node_Bundle bundleNode = parser.parsePath(bridge, args["path"]);
			bundleNode.setup( program.createGlobalScope(bridge) );
			
			if( Boolean.Parse(args["print-tree"]) ) {
				program.printTree(bundleNode);
			}
			
			if( Boolean.Parse(args["run"]) ) {
				try {
					return bundleNode.run();
				}
				catch(ClientException e) {
					bridge.error(e.clientMessage);
					//xxx return ERROR_CODE;
				}
			}
		}
		else {
			bridge.warning("no path set");
		}
		
		return 0;
	}
	
	Scope createGlobalScope(Bridge bridge) {
		Scope scope = new Scope();

		//func print(dyn value)
		IList<Parameter> printParameters = new Parameter[] {
			new Parameter( new Identifier("value"), ReferenceCategory.DYN )
		};
		//xxx use better binding stuff
		IFunction printFunction = new NativeFunction(
			bridge, printFunctionNative, printParameters, null, scope );
		IValue printFunctor =
			FunctionWrapper.wrap(printFunction);
		scope.declarePervasive(
			new Identifier("println"),
			new ReferenceType( ReferenceCategory.FUNCTION, null ),
			printFunctor );
		
		//true
		scope.declareBind(
			new Identifier("true"),
			new ReferenceType( ReferenceCategory.VALUE,	Bridge.Bool ),
			true,
			Bridge.wrapBoolean(true) );
		
		//false
		scope.declareBind(
			new Identifier("false"),
			new ReferenceType( ReferenceCategory.VALUE,	Bridge.Bool ),
			true,
			Bridge.wrapBoolean(false) );

		return scope;
	}
	
	void printFunctionNative(Bridge bridge, Scope args) {
		IValue arg = args.evaluateLocalIdentifier( new Identifier("value") );
		
		if( arg.activeInterface == Bridge.String )
			bridge.output( Bridge.unwrapString(arg).ToString() );
		else if( arg.activeInterface == Bridge.Int )
			bridge.output( Bridge.unwrapInteger(arg).ToString() );
		else if( arg.activeInterface == Bridge.Bool )
			bridge.output( Bridge.unwrapBoolean(arg).ToString() );
		else
			throw new Error_Unimplemented();
	}
	
	void printNode(int level, INode node) {
		string name;
		object children;	
		printTabs(level);
		node.getInfo(out name, out children); //xxx should this use bridge?
		Console.WriteLine(name);
		printObject(level+1, children);
	}
	
	void printObject(int level, object obj) {
		if( obj == null)
			return;
		if( obj is INode ) {
			printNode( level, (INode)obj );
		}
		else if( obj is string ) {
			printTabs(level);
			Console.WriteLine(
				'"' +
				((string)obj).Replace(@"\", @"\\").Replace("\"", "\\\"")
				+ '"' );
		}
		else if( obj is System.Collections.IEnumerable ) {
			foreach( object o in (System.Collections.IEnumerable)obj )
				printObject(level, o);
		}
		else {
			printTabs(level);
			Console.WriteLine(obj);
		}
	}
	
	void printTabs(int count) {
		for( int i = 0; i < count; i++ )
			Console.Write("    ");
	}

	void printTree(Node_Bundle root) {
		printNode(0, root);
	}
}