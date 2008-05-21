using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml;

class DesalAgent001 {
	//entry point
	//arguments have the form: -key=value
	static int Main(string[] programArgs) {
		//Debug.Assert and Trace.Assert don't output anything by default
		Debug.Listeners.Add(new TextWriterTraceListener(Console.Error));
	
		//xxx XmlDocument::Load has threading issues
		//this seems to prevent the bug from being triggered
		System.Threading.Thread.Sleep(100);
		
		DesalAgent001 program = new DesalAgent001();
		Bridge bridge = new Bridge();
		try {
			return program.run(programArgs, bridge);
		}
		catch(UserError e) {
			bridge.printlnError("User input: " + e.Message);
			return 1;
		}
		catch(ParseError e) {
			bridge.printlnError("Parsing: " + e.Message);
			return 1;
		}
	}
	
	//exception is client code caught
	//implementation and user exceptions not caught
	int run(string[] bareArgs, Bridge bridge) {
		try {
			return throwingRun(bareArgs, bridge);
		}
		catch(ClientException e) {
			bridge.printlnError("uncaught exception: " + e.clientMessage);
			return 1;
		}
	}
	
	int throwingRun(string[] bareArgs, Bridge bridge) {
		IDictionary<string,string> args = parseArguments(bareArgs);
		string path = args["path"];
		string repr = args["representation"];
		Node_Bundle bundle;
		
		try {
			if( repr == "desexp" ) {
				bundle = Desexp.DesexpParser.parseFile(bridge, path);
			}
			else if( repr == "desible" ) {
				bool warnUnhandled = bool.Parse(args["desible-warn-unhandled"]);
				bool warnAllNS = bool.Parse(args["desible-warn-allNS"]);
				bundle = DesibleParser.parseDocument(
					bridge, path, warnUnhandled, warnAllNS);
			}
			else if( repr == "dextr" ) {
				string parserName = args["dextr-parser"];
				bundle = Dextr.Custom.ParserManager.parseDocument(
					bridge, path, parserName);
				if( parserName == "token-displayer" || parserName == "token-info-displayer" )
					return 0;
			}
			else {
				throw new UserError(
					String.Format(
						"unknown representation '{0}'.",
						repr));
			}
		}
		catch(Exception e) {
			if( e is ParseError )
				throw;
			throw new ParseError(
				String.Format(
					"unable to parse {0} document at '{1}' because: {2}",
					repr, path, e),
				e);
		}
		
		if( bool.Parse(args["print-tree"]) ) {
			printTree(bundle);
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
		
		//xxx the desired behavior recently changed (and has not been implemented)
		//see "executing module.txt" for details
		if( bool.Parse(args["run"]) ) {
			//all exceptions in client code should be caught, and not bubble up here
			IWorker result = Executor.execute(bundle, bridge);
			//xxx execution of bundle should always produce a non-null Int worker
			return ( result is Null ? 0 : (int)Bridge.unwrapInteger(result) );
		}
		
		return 0;
	}
	
	IDictionary<string,string> parseArguments(string[] bareArgs) {
		IDictionary<string,string> args = new Dictionary<string,string>();
		
		foreach( string arg in bareArgs ) {
			string[] parts = arg.Split('=');
			if( arg[0] != '-' || parts.Length != 2 )
				throw new UserError("bad argument: " + arg);
			string key = parts[0].Substring(1);
			string val = parts[1];
			if( args.ContainsKey(key) )
				throw new UserError("duplicate key: " + key);
			args.Add(key, val);
		}
		
		IDictionary<string,string> defaultArgs = new Dictionary<string,string>();
		defaultArgs.Add("print-tree", "false");
		defaultArgs.Add("run", "true");
		defaultArgs.Add("dextr-parser", "Coco/R");
		defaultArgs.Add("desible-warn-unhandled", "true");
		defaultArgs.Add("desible-warn-allNS", "true");
		defaultArgs.Add("test-desible-serializer", "false");
		defaultArgs.Add("output-desible", "false");
		foreach( string key in defaultArgs.Keys ) {
			if( ! args.ContainsKey(key) )
				args.Add(key, defaultArgs[key]);
		}
		
		if( ! args.ContainsKey("path") )
			throw new UserError("No path given.");
		if( ! args.ContainsKey("representation") )
			throw new UserError("Representation not specified.");
		
		return args;
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