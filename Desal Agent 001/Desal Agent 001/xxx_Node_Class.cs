/*xxx
class Node_Class : INode_Expression {
	public IWorker execute(Scope scope) {
		Scope staticScope = new Scope(scope);
			
		//Add instance constructors as free functions to STATIC_SCOPE.
	
		//Add instance constructors as callees to CLASS_INTERFACE.
		Interface staticInterface = new Interface();
		InterfaceImplBuilder impl = new InterfaceImplBuilder();
		foreach( Node_Function func in _instanceConstructors ) {
			impl.addCallee(func);
		}

		//declaration-pervasive
		foreach( Node_DeclareClass declClass in _staticDeclares ) {
			if( declClass.declaration is Node_DeclareFirst )
				declClass.execute(staticScope);
		}
	
		//not declaration-const-empty
		foreach( Node_DeclareClass declClass in _staticDeclares ) {
			if( !(declClass.declaration is Node_DeclareConstEmpty) )
				declClass.execute(staticScope);
		}
		
		//declaration-const-empty
		foreach( Node_DeclareClass declClass in _staticDeclares ) {
			if( declClass.declaration is Node_DeclareConstEmpty )
				declClass.execute(staticScope);
		}
		
		//xxx static callees as free functions
		
		//xxx add static callees and properties to classInterface
		
		if( _staticConstructor != null )
			Executor.execute(_staticConstructor, staticScope);
		
		//xxx staticScope.seal();
	}
}
*/

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
	//xxx public old_Node_Class classNode;
	public Scope staticScope;
	public IList<INode_Declaration> instanceDeclares;
	public ClassInterfaceImplementation defaultInterfaceImplementation;
}

class ClassValue : IWorker {
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
		get { throw new NotImplementedException(); }
	}
	public long objectID {
		get { throw new NotImplementedException(); }
	}
	public IWorker cast(IInterface aInterface) {
		throw new NotImplementedException();
	}
	public void executeCall(IList<Argument> arguments) {
		throw new NotImplementedException();
	}
	public IWorker evaluateCall(IList<Argument> arguments) {
		return _interfaceImpl.evaluateCall(_state, IList<Argument>);
	}
	public IWorker getProperty(Identifier name) {
		throw new NotImplementedException();
	}
	public void setProperty(Identifier propName, IWorker aValue) {
		throw new NotImplementedException();
	}
	public void executeMethod(Identifier name, IList<Argument> arguments) {
		throw new NotImplementedException();
	}
	public IWorker evaluateMethod(Identifier name, IList<Argument> arguments) {
		throw new NotImplementedException();
	}
}

class ClassInterfaceImplementation {
	ClassInterfaceImplementation _parent;
	IList<ClassInterfaceImplementation> _children;
	Node_Function _callee;
	
	public ClassInterfaceImplementation(Node_Function callee) {
		_callee = callee;
	}
	
	public IWorker evaluateCall(ClassState state, IList<Argument> args) {
		Scope instanceScope = new Scope(state.staticScope);
		foreach( INode_Declaration decl in state.instanceDeclares ) {
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
	
	public IWorker instantiate(IList<Argument> arguments) {
		Scope instanceScope = new Scope(staticScope);
		//xxx execute declarations in instanceScope
		func.evaluate(instanceScope).executeCall(IList<Argument>);
		return Client_Value(
			Client_Object(instanceScope),
			thisClass );
	}
}
*/