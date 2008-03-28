using System; //for System.Console
using System.Collections.Generic;

class DesalAgent001 {
	
	//entry point
	//arguments have the form: -key=value
	static int Main(string[] arguments) {
		DesalAgent001 program = new DesalAgent001();
		Bridge bridge = new Bridge();	
		
		IDictionary<string,string> args =
			new Dictionary<string,string>();
		
		args.Add("print-tree", "false");
		args.Add("run", "true");
		args.Add("dextr-parser", "Coco/R");
		args.Add("desible-warn-unhandled", "true");
		args.Add("desible-warn-allNS", "true");
		
		foreach( string arg in arguments ) {
			if( arg[0] == '-' ) {
				string[] parts = arg.Split('=');
				if( parts.Length != 2 ) {
					bridge.error("bad argument: " + arg);
					return 1;
				}
				string key = parts[0].Substring(1);
				string val = parts[1];
				args.Remove(key);
				args.Add(key, val);
			}
			else {
				bridge.error("bad argument: " + arg);
				return 1;
			}
		}
		
		if( ! args.ContainsKey("path") ) {
			bridge.error("No path given. Program terminating.");
			return 1;
		}
		if( ! args.ContainsKey("representation") ) {
			bridge.error("Representation not specified. Program terminating.");
			return 1;
		}
		
		string path = args["path"];
		
		Node_Bundle bundle;
		
		if( args["representation"] == "dextr" ) {
			string parserName = args["dextr-parser"];
			bundle = Dextr.Parser.parseDocument(
				bridge, path, parserName);
			if( parserName == "token-displayer" || parserName == "token-info-displayer" )
				return 0;
		}
		else if( args["representation"] == "desible" ) {		
			bool warnUnhandled = bool.Parse(args["desible-warn-unhandled"]);
			bool warnAllNS = bool.Parse(args["desible-warn-allNS"]);
			bundle = DesibleParser.parseDocument(
				bridge, path, warnUnhandled, warnAllNS);
			
			//xxxv serialize bundle and then reparse
			bundle = DesibleParser.parseDocument(
				bridge,
				DesibleSerializer.serializeToDocument(bundle),
				warnUnhandled,
				warnAllNS );
		}
		else {
			bridge.error("unknown representation");
			return 1;
		}
		
		if( bool.Parse(args["print-tree"]) ) {
			program.printTree(bundle);
		}
		
		if( bool.Parse(args["run"]) ) {
			try {
				return (int)Bridge.unwrapInteger(
					Executor.execute(
						bundle,
						program.createGlobalScope(bridge)));
			}
			catch(ClientException e) {
				bridge.error("UNCAUGHT EXCEPTION:\n" + e.clientMessage);
				return 1;
			}
			catch(Exception e) {
				bridge.error("IMPLEMENTATION: " + e.ToString());
				return 1;
			}
		}
		
		return 0;
	}
	
	Scope createGlobalScope(Bridge bridge) {
		Scope scope = new Scope(bridge);
		
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
				
		//true
		scope.declareAssign(
			new Identifier("true"),
			Bridge.wrapBoolean(true) );
		
		//false
		scope.declareAssign(
			new Identifier("false"),
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
		Haven't yet implemented convertors.
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
		printTabs(level);
		Console.Write(node.typeName);

		//write identikey dependencies
		ICollection<Identifier> depends = Depends.depends(node);
		if( depends.Count > 0 ) {
			Console.Write(" (");
			bool first = true;
			foreach( Identifier ident in depends ) {
				if( first ) first = false;
				else Console.Write(", ");
				Console.Write( ident.ToString() );
			}
			Console.Write(")");
		}

		Console.WriteLine("");
		
		//write children or contents
		ICollection<INode> children = node.childNodes;
		if( children == null ) {
			printTabs(level + 1);
			Console.WriteLine(
				'"' +
				node.ToString().Replace(@"\", @"\\").Replace("\"", "\\\"")
				+ '"' );
		}
		else {
			foreach( INode child in children )
				printNode( level+1, child );
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