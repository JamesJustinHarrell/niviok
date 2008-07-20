using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Acrid.NodeTypes;

namespace Acrid.ComandLine {

class CommandLine {

	//entry point
	//arguments have the form: -key=value
	static int Main(string[] programArgs) {
		//Debug.Assert and Trace.Assert don't output anything by default
		Debug.Listeners.Add(new TextWriterTraceListener(Console.Error));
		
		CommandLine program = new CommandLine();
		try {
			return program.run(programArgs);
		}
		catch(UserError e) {
			Console.Error.WriteLine("User input: " + e.Message);
			return 1;
		}
		catch(ParseError e) {
			Console.Error.WriteLine("Parsing: " + e.Message + "\n" + e.source);
			#if DEBUG
			Console.Error.WriteLine("Inner exception:\n" + e.InnerException);
			#endif
			return 1;
		}
	}
	
	int run(string[] bareArgs) {
		IDictionary<string,string> args = parseArguments(bareArgs);
		string path = args["path"];
		string repr = args["representation"];
		Node_Module module;
		
		if( repr == "toy" ) {
			module = Toy.ToyParser.parseFile(path);
		}
		else if( repr == "desible" ) {
			bool warnUnhandled = bool.Parse(args["desible-warn-unhandled"]);
			bool warnAllNS = bool.Parse(args["desible-warn-allNS"]);
			module = Desible.DesibleParser.parseDocument(
				path, warnUnhandled, warnAllNS);
		}
		else if( repr == "fujin" ) {
			string parserName = args["fujin-parser"];
			module = Fujin.Custom.ParserManager.parseDocument(
				path, parserName);
		}
		else {
			throw new UserError(String.Format(
				"unknown representation '{0}'.",
				repr));
		}
		
		if( bool.Parse(args["print-tree"]) ) {
			printTree(module);
		}
		
		if( bool.Parse(args["output-desible"]) ) {
			string outputPath = args["desible-output-path"];
			XmlDocument doc = Desible.DesibleSerializer.serializeToDocument(module);
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.IndentChars = "\t";
			using(XmlWriter writer = XmlWriter.Create(outputPath, settings)) {
				doc.WriteTo(writer);
			}
		}
		
		if( bool.Parse(args["run"]) ) {
			//all exceptions in client code should be caught, and not bubble up here
			return Execution.Executor.executeProgramModule(
				module, Console.In, Console.Out, Console.Error);
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
		
		//default arguments are added second in order to catch duplicate user-supplied arguments
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
		HashSet<Identifier> depends = Execution.Depends.depends(node);
		if( depends.Count > 0 ) {
			Console.Write(" (");
			bool first = true;
			foreach( Identifier identseq in depends ) {
				if( first ) first = false;
				else Console.Write(", ");
				Console.Write( identseq.ToString() );
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

} //namespace
