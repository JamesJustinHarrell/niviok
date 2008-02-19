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
		args.Add("representation", "desible");
		
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
		
		if( args["representation"] == "dextr" ) {
			DextrParser.parseDocument(args["path"]);
		}
		else {
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
		}
		
		return 0;
	}
	
	Scope createGlobalScope(Bridge bridge) {
		Scope scope = new Scope();
		
		//func println(dyn value)
		IList<Parameter> printParameters = new Parameter[] {
			new Parameter(NullableType.dyn, new Identifier("text"))
		};
		//xxx use better binding stuff
		IFunction printFunction = new NativeFunction(
			bridge, printFunctionNative, printParameters, null, scope );
		IValue printFunctor =
			FunctionWrapper.wrap(printFunction);
		scope.declareAssign(
			new Identifier("println"),
			printFunctor );
		//xxx new NullableType( ReferenceCategory.FUNCTION, null ),
				
		//true
		scope.declareAssign(
			new Identifier("true"),
			//xxx new NullableType( ReferenceCategory.VALUE, Bridge.Bool ),
			//xxx true,
			Bridge.wrapBoolean(true) );
		
		//false
		scope.declareAssign(
			new Identifier("false"),
			//xxx new NullableType( ReferenceCategory.VALUE, Bridge.Bool ),
			//xxx true,
			Bridge.wrapBoolean(false) );
		
		//Bool
		scope.declareAssign(
			new Identifier("Bool"),
			Bridge.wrapInterface(Bridge.Bool) );

		return scope;
	}
	
	/* Dextr interface notation:
	func println( Stringable text )
	note: typing is not yet implemented */
	void printFunctionNative(Bridge bridge, Scope args) {
		IValue arg = args.evaluateLocalIdentifier( new Identifier("text") );
		
		/* xxx
		should use String convertee of argument
		bridge.output(
			Bridge.unwrapString(
				arg.convert(Bridge.String) ))
		*/
		
		if( arg.activeInterface == Bridge.String )
			bridge.output( Bridge.unwrapString(arg) );
		else if( arg.activeInterface == Bridge.Int )
			bridge.output( Bridge.unwrapInteger(arg).ToString() );
		else if( arg.activeInterface == Bridge.Bool )
			bridge.output( Bridge.unwrapBoolean(arg).ToString() );
		else if( arg.activeInterface == Bridge.Rat )
			bridge.output( Bridge.unwrapRational(arg).ToString() );
		else if( arg is NullValue )
			bridge.output( "null" );
		else
			throw new ClientException("unknown type cannot be converted to string");
	}
	
	void printNode(int level, INode node) {
		string name;
		object children;	
		printTabs(level);
		node.getInfo(out name, out children); //xxx should this use bridge?
		Console.Write(name);

/* */
		Console.Write(" (");
		bool first = true;
		foreach( Identifier ident in node.identikeyDependencies ) {
			if( first ) first = false;
			else Console.Write(", ");
			Console.Write( ident.str );
		}
		Console.Write(")");
/* */

		Console.WriteLine("");
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