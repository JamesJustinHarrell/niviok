using System; //for System.Console
using System.Collections.Generic;

class DesalInterpreter001 {
	static string testPath = "/media/files/Desal/Desal 001/test.desible";

	static int Main(string[] arguments) {
		//arguments have the form: -key=value -key=value
		
		IDictionary<string,string> args =
			new Dictionary<string,string>();
		
		args.Add("path", testPath);
		args.Add("unhandled-warn-level", "0");
		args.Add("print-tree", "false");
		args.Add("run", "true");
		
		foreach( string arg in arguments ) {
			if( arg[0] == '-' ) {
				string[] parts = arg.Split(new char[]{'='}, 2);
				if( parts.Length != 2 )
					throw new Exception();
				string key = parts[0].Substring(1);
				string val = parts[1];
				args.Remove(key);
				args.Add(key, val);
			}
			else throw new Exception();
		}
		
		DesalInterpreter001 program = new DesalInterpreter001();
		
		DesibleParser parser = new DesibleParser();
		parser.unhandledWarnLevel = Int32.Parse(args["unhandled-warn-level"]);
		Node_Bundle bundleNode = parser.parsePath(args["path"]);
		bundleNode.setup( program.createGlobalScope() );
		
		if( Boolean.Parse(args["print-tree"]) ) {
			program.printTree(bundleNode);
		}
		
		if( Boolean.Parse(args["run"]) ) {
			try {
				return bundleNode.run();
			}
			catch(ClientException e) {
				System.Console.WriteLine(e.clientMessage);
				//xxx return ERROR_CODE;
			}
		}
		
		return 0;
	}
	
	Scope createGlobalScope() {
		Scope scope = new Scope();

		//func print(dyn value)
		IList<Parameter> printParameters = new Parameter[] {
			new Parameter( new Identifier("value"), ReferenceCategory.DYN )
		};
		//xxx use better binding stuff
		IFunction printFunction = new NativeFunction(
			printFunctionNative, printParameters, null, scope );
		IValue printFunctor =
			FunctionWrapper.wrap(printFunction);
		scope.declarePervasive(
			new Identifier("println"),
			new ReferenceType( ReferenceCategory.FUNCTION, null ),
			printFunctor );
		
		//true
		scope.declareBind(
			new Identifier("true"),
			new ReferenceType( ReferenceCategory.VALUE,	Wrapper.Bool ),
			true,
			Wrapper.wrapBoolean(true) );
		
		//false
		scope.declareBind(
			new Identifier("false"),
			new ReferenceType( ReferenceCategory.VALUE,	Wrapper.Bool ),
			true,
			Wrapper.wrapBoolean(false) );

		return scope;
	}
	
	void printFunctionNative(Scope args) {
		IValue arg = args.evaluateLocalIdentifier( new Identifier("value") );
		
		if( arg.activeInterface == Wrapper.String )
			Console.WriteLine( Wrapper.unwrapString(arg) );
		else if( arg.activeInterface == Wrapper.Int )
			Console.WriteLine( Wrapper.unwrapInteger(arg) );
		else if( arg.activeInterface == Wrapper.Bool )
			Console.WriteLine( Wrapper.unwrapBoolean(arg) );
		else
			throw new Error_Unimplemented();
	}
	
	void printNode(int level, INode node) {
		string name;
		object children;	
		printTabs(level);
		node.getInfo(out name, out children);
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