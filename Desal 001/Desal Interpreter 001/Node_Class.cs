using System.Collections.Generic;

class Node_Class : INode_Expression {
	IList<Node_DeclarationClass> _staticDeclarations;
	Node_Block _staticConstructor;
	IList<Node_Function> _staticCallees;
	IList<Node_ClassProperty> _staticProperties;
	IList<Node_Function> _instanceConstructors;
	IList<INode_DeclarationAny> _instanceDeclarations;
	IList<Node_InterfaceImplementation> _interfaceImplementations;

	public Node_Class(
	IList<Node_DeclarationClass> staticDeclarations,
	Node_Block staticConstructor,
	IList<Node_Function> staticCallees,
	IList<Node_ClassProperty> staticProperties,
	IList<Node_Function> instanceConstructors,
	IList<INode_DeclarationAny> instanceDeclarations,
	IList<Node_InterfaceImplementation> interfaceImplementations ) {
		_staticDeclarations = staticDeclarations;
		_staticConstructor = staticConstructor;
		_staticProperties = staticProperties;
		_instanceConstructors = instanceConstructors;
		_instanceDeclarations = instanceDeclarations;
		_interfaceImplementations = interfaceImplementations;
	}	

	public IValue execute(Scope scope) {
		Scope staticScope = new Scope(scope);
			
		//xxx Add instance constructors as free functions to STATIC_SCOPE.
	
		/* Add instance constructors as callees to CLASS_INTERFACE.
		Interface staticInterface = new Interface();
		InterfaceImplBuilder impl = new InterfaceImplBuilder();
		foreach( Node_Function func in _instanceConstructors ) {
			impl.addCallee(func);
		}
		*/

		//declaration-pervasive
		foreach( Node_DeclarationClass declClass in _staticDeclarations ) {
			if( declClass.decl is Node_DeclareFirst )
				declClass.execute(staticScope);
		}
		
		//not declaration-const-empty
		foreach( Node_DeclarationClass declClass in _staticDeclarations ) {
			if( !(declClass.decl is Node_DeclarationConstEmpty) )
				declClass.execute(staticScope);
		}
		
		//declaration-const-empty
		foreach( Node_DeclarationClass declClass in _staticDeclarations ) {
			if( declClass.decl is Node_DeclarationConstEmpty )
				declClass.execute(staticScope);
		}
		
		//xxx static callees as free functions
		
		//xxx add static callees and properties to classInterface
		
		if( _staticConstructor != null )
			_staticConstructor.execute(staticScope);
		
		//xxx staticScope.seal();
	
		throw new Error_Unimplemented();
	}
	
	public void getInfo(out string name, out object objs) {
		name = "class";
		objs = new object[] {
			_staticDeclarations,
			_staticConstructor,
			_staticCallees,
			_staticProperties,
			_instanceConstructors,
			_instanceDeclarations,
			_interfaceImplementations };
	}
	
	public HashSet<Identifier> identikeyDependencies {
		get { throw new Error_Unimplemented(); }
	}
}


/*
value producted by evaluating class node
	object
		static scope
		class node
	interface implementation
		interface implementation (callee)
			function node
			ref to parent faceimpl
		interface implementation (callee)
			function node
			ref to parent faceimpl
		getters
		setters
		methods
* /

//xxx

//hold state of value produced when evaluating class node
class ClassState {
	//xxx public Node_Class classNode;
	public Scope staticScope;
	public IList<INode_DeclarationAny> instanceDeclarations;
	public ClassInterfaceImplementation defaultInterfaceImplementation;
}

class ClassValue : IValue {
	ClassState _state;
	ClassInterfaceImplementation _interfaceImpl;

	public ClassValue(
	Scope staticScope,
	//Node_Class classNode,
	ClassInterfaceImplementation faceImpl) {
		_state = new ClassState();
		_state.staticScope = staticScope;
		_interfaceImpl = faceImpl;
	}

	public IInterface activeInterface {
		get { throw new Error_Unimplemented(); }
	}
	public long objectID {
		get { throw new Error_Unimplemented(); }
	}
	public IValue cast(IInterface aInterface) {
		throw new Error_Unimplemented();
	}
	public void executeCall(Arguments arguments) {
		throw new Error_Unimplemented();
	}
	public IValue evaluateCall(Arguments arguments) {
		return _interfaceImpl.evaluateCall(_state, arguments);
	}
	public IValue getProperty(Identifier name) {
		throw new Error_Unimplemented();
	}
	public void setProperty(Identifier propName, IValue aValue) {
		throw new Error_Unimplemented();
	}
	public void executeMethod(Identifier name, Arguments arguments) {
		throw new Error_Unimplemented();
	}
	public IValue evaluateMethod(Identifier name, Arguments arguments) {
		throw new Error_Unimplemented();
	}
}

class ClassInterfaceImplementation {
	ClassInterfaceImplementation _parent;
	IList<ClassInterfaceImplementation> _children;
	Node_Function _callee;
	
	public ClassInterfaceImplementation(Node_Function callee) {
		_callee = callee;
	}
	
	public IValue evaluateCall(ClassState state, Arguments args) {
		Scope instanceScope = new Scope(state.staticScope);
		foreach( INode_DeclarationAny decl in state.instanceDeclarations ) {
			decl.execute(instanceScope);
		}
		_callee.evaluate(instanceScope).executeCall(args);
		return new ClassValue(state.staticScope, state.defaultInterfaceImplementation);
	}
}

/*
class InstanceConstructor {
	Scope _staticScope;
	Node_Function _func;

	public InstanceConstructor(	Scope staticScope, Node_Function func) {
		_staticScope = staticScope;
		_func = func;
	}
	
	public IValue instantiate(Arguments arguments) {
		Scope instanceScope = new Scope(staticScope);
		//xxx execute declarations in instanceScope
		func.evaluate(instanceScope).executeCall(arguments);
		return Client_Value(
			Client_Object(instanceScope),
			thisClass );
	}
}
*/