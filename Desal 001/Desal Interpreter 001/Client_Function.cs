//DEPRECATED because IFunction is deprecated
//function defined by client code
//produced through the evaluation of a function node

using System.Collections.Generic;

class Client_Function : IFunction {
	IList<Parameter> _parameters;
	NullableType _returnType;
	INode_Expression _body;
	Scope _scope;
	
	public Client_Function(
	IList<Parameter> parameters, NullableType returnType,
	INode_Expression body, Scope scope) {
		_body = body;
		_parameters = parameters;
		_returnType = returnType;
		_scope = scope;
	}
	
	//xxx replace with get interface
	public IList<Parameter> parameters {
		get { return _parameters; }
	}
	public NullableType returnType {
		get { return _returnType; }
	}
	
	public IValue call(Arguments arguments) {	
		//bind arguments to parameter identifiers
		Scope functionScope = arguments.setup( _parameters, _scope );
		
		//xxx try {
			return _body.execute(functionScope);
		/* xxx
		}
		catch(ReturnStatement statement) {
			return statement.returnValue;
		}
		catch(BreakStatement statement) {
			throw new ClientException("can't break here");
		}
		*/
	}
}