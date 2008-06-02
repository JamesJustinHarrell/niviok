using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml;

class Acrid {
	//entry point
	//arguments have the form: -key=value
	static int Main(string[] programArgs) {
		//Debug.Assert and Trace.Assert don't output anything by default
		Debug.Listeners.Add(new TextWriterTraceListener(Console.Error));
	
		//xxx XmlDocument::Load has threading issues
		//this seems to prevent the bug from being triggered
		System.Threading.Thread.Sleep(100);
		
		Acrid program = new Acrid();
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
	
	int run(string[] bareArgs, Bridge bridge) {
		IDictionary<string,string> args = parseArguments(bareArgs);
		string path = args["path"];
		string repr = args["representation"];
		Node_Module module;
		
		try {
			if( repr == "toy" ) {
				module = Toy.ToyParser.parseFile(bridge, path);
			}
			else if( repr == "desible" ) {
				bool warnUnhandled = bool.Parse(args["desible-warn-unhandled"]);
				bool warnAllNS = bool.Parse(args["desible-warn-allNS"]);
				module = DesibleParser.parseDocument(
					bridge, path, warnUnhandled, warnAllNS);
			}
			else if( repr == "fujin" ) {
				string parserName = args["fujin-parser"];
				module = Fujin.Custom.ParserManager.parseDocument(
					bridge, path, parserName);
			}
			else {
				throw new UserError(
					String.Format(
						"unknown representation '{0}'.",
						repr));
			}
		}
		catch(XmlException e) {
			throw new ParseError(
				"XmlException : " + e.Message,
				String.Format("{0} : {1}", repr, path),
				e);
		}
		
		if( bool.Parse(args["print-tree"]) ) {
			printTree(module);
		}
		
		if( bool.Parse(args["output-desible"]) ) {
			string outputPath = args["desible-output-path"];
			XmlDocument doc = DesibleSerializer.serializeToDocument(module);
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
			return Executor.executeProgram(module, bridge);
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
		defaultArgs.Add("fujin-parser", "Coco/R");
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

	void printTree(Node_Module root) {
		printNode(0, root);
	}
}