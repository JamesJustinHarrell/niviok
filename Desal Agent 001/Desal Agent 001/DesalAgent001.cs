using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml;

class DesalAgent001 {
	
	//entry point
	//IList<Argument> have the form: -key=value
	static int Main(string[] arguments) {
		Debug.Listeners.Add(
			new TextWriterTraceListener(
				Console.Error));
	
		DesalAgent001 program = new DesalAgent001();
		Bridge bridge = new Bridge();	
		
		IDictionary<string,string> args =
			new Dictionary<string,string>();
		
		args.Add("print-tree", "false");
		args.Add("run", "true");
		args.Add("dextr-parser", "Coco/R");
		args.Add("desible-warn-unhandled", "true");
		args.Add("desible-warn-allNS", "true");
		args.Add("test-desible-serializer", "false");
		args.Add("output-desible", "false");
		
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
		
		string representation = args["representation"];
		
		try {
			if( representation == "desexp" ) {
				bundle = Desexp.DesexpParser.parseFile(bridge, path);
			}
			else if( representation == "desible" ) {		
				bool warnUnhandled = bool.Parse(args["desible-warn-unhandled"]);
				bool warnAllNS = bool.Parse(args["desible-warn-allNS"]);
				bundle = DesibleParser.parseDocument(
					bridge, path, warnUnhandled, warnAllNS);
			}
			else if( representation == "dextr" ) {
				string parserName = args["dextr-parser"];
				bundle = Dextr.Custom.ParserManager.parseDocument(
					bridge, path, parserName);
				if( parserName == "token-displayer" || parserName == "token-info-displayer" )
					return 0;
			}
			else {
				bridge.error(
					String.Format(
						"Unknown representation '{0}'.",
						representation));
				return 1;
			}
		}
		catch(Exception e) {
			bridge.error(
				String.Format(
					"Unable to parse {0} document\n"+
					"at '{1}'\n" +
					"because: {2}",
					representation, path, e));
			return 1;
		}
		
		if( bool.Parse(args["print-tree"]) ) {
			program.printTree(bundle);
		}
		
		if(	bool.Parse(args["test-desible-serializer"]) ) {
			//serialize bundle and then reparse
			bundle = DesibleParser.parseDocument(
				bridge,
				DesibleSerializer.serializeToDocument(bundle),
				true,
				true);
			bridge.println("Desible serializer seems to be working okay.");
		}
		
		if( bool.Parse(args["output-desible"]) ) {
			string outputPath = args["desible-output-path"];
			XmlDocument doc = DesibleSerializer.serializeToDocument(bundle);
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
			using(XmlWriter writer = XmlWriter.Create(outputPath, settings)) {
				doc.WriteTo(writer);
			}
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
		IList<ParameterImpl> printParameters = new ParameterImpl[] {
			new ParameterImpl(
				Direction.IN, NullableType.dyn,
				new Identifier("text"), null)
		};
		IWorker printFunctor = Client_Function.wrap(new Function_Native(
			printParameters, null, printFunctionNative, scope));
		
		scope.declareAssign(
			new Identifier("println"), printFunctor);
				
		scope.declareAssign(
			new Identifier("true"), Bridge.wrapBoolean(true));
		
		scope.declareAssign(
			new Identifier("false"), Bridge.wrapBoolean(false));
		
		scope.declareAssign(
			new Identifier("Bool"),	Bridge.wrapInterface(Bridge.faceBool));
		
		scope.declareAssign(
			new Identifier("Int"), Bridge.wrapInterface(Bridge.faceInt));
		
		scope.declareAssign(
			new Identifier("Object"), Bridge.wrapInterface(Bridge.faceObject));

		scope.declareAssign(
			new Identifier("String"), Bridge.wrapInterface(Bridge.faceString));

		return scope;
	}
	
	/* Dextr interface notation:
	func println( Stringable text )
	note: typing is not yet implemented */
	IWorker printFunctionNative(Scope args) {
		Bridge bridge = args.bridge;
		IWorker arg = args.evaluateLocalIdentifier( new Identifier("text") );
		
		/* xxx
		should use String convertee of argument
		bridge.output(
			Bridge.unwrapString(
				arg.convert(Bridge.String) ))
		Haven't yet implemented convertors.
		*/
		
		if( arg.face == Bridge.faceString )
			bridge.output( Bridge.unwrapString(arg) );
		else if( arg.face == Bridge.faceInt )
			bridge.output( Bridge.unwrapInteger(arg).ToString() );
		else if( arg.face == Bridge.faceBool )
			bridge.output( Bridge.unwrapBoolean(arg).ToString() );
		else if( arg.face == Bridge.faceRat )
			bridge.output( Bridge.unwrapRational(arg).ToString() );
		else if( arg.face == Bridge.faceObject )
			bridge.output( "object" );
		else if( arg is Null )
			bridge.output( "null" );
		else
			throw new ClientException("unknown type cannot be converted to string");
		
		return new Null();
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