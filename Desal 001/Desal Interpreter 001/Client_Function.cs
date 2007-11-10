//DEPRECATED because IFunction is deprecated
//function defined by client code
//produced through the evaluation of a function node

using System.Collections.Generic;

class Client_Function : IFunction {
	IList<Parameter> _parameters;
	ReferenceType _returnType;
	Node_Block _block;
	Scope _scope;
	
	public Client_Function(
	IList<Parameter> parameters, ReferenceType returnType,
	Node_Block block, Scope scope) {
		_block = block;
		_parameters = parameters;
		_returnType = returnType;
		_scope = scope;
	}
	
	//xxx replace with get interface
	public IList<Parameter> parameters {
		get { return _parameters; }
	}
	public ReferenceType returnType {
		get { return _returnType; }
	}
	
	public void executeCall(Arguments arguments) {
		Scope functionScope = new Scope(_scope);
		//xxx bind arguments to parameter identifiers
		_block.execute(functionScope);
	}
	
	public IValue evaluateCall(Arguments arguments) {
		Scope functionScope = new Scope(_scope);
		
		//xxx bind arguments to parameter identifiers
		
		//xxx try {
			_block.execute(functionScope);
		/* xxx
		}
		catch(ReturnStatement statement) {
			return statement.returnValue;
		}
		catch(BreakStatement statement) {}
		*/
		
		throw new System.Exception("function didn't return anything");
	}
}