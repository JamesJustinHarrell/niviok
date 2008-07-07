//implements the Int interface
//xxx should support abitrary precision

using Acrid.NodeTypes;

namespace Acrid.Execution {

public class Client_Integer {
	public static IWorker wrap(long value) {
		Client_Integer o = new Client_Integer(value);
		NObject obj = new NObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.stdn_Int, obj, new IWorker[]{});
		
		builder.addBreeder(
			Bridge.stdn_String,
			delegate(){ return Bridge.toClientString(value.ToString()); });
			
		builder.addMethod(
			new Identifier("add"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NType(Bridge.stdn_Int),
						new Identifier("value"),
						null )
				},
				new NType(Bridge.stdn_Int),
				delegate(IScope args) {
					return Bridge.toClientInteger(
							o.add(
								Bridge.toNativeInteger(
									GE.evalIdent(args, "value"))));
				},
				null ));

		builder.addMethod(
			new Identifier("lessThan?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NType(Bridge.stdn_Int),
						new Identifier("value"),
						null )
				},
				new NType(Bridge.stdn_Bool),
				delegate(IScope args) {
					return Bridge.toClientBoolean(
							o.lessThan(
								Bridge.toNativeInteger(
									GE.evalIdent(args, "value"))));
				},
				null ));
			
		IWorker rv = builder.compile();
		rv.nativeObject = value;
		obj.rootWorker = rv;
		return rv;
	}

	long _value;
	
	public Client_Integer(long value) {
		_value = value;
	}
	
	//comparison
	public bool lessThan(long value) {
		return _value < value;
	}
	public bool lessThanOrEqual(long value) {
		return _value <= value;
	}
	public bool equal(long value) {
		return _value == value;
	}
	public bool greaterThanOrEqual(long value) {
		return _value >= value;
	}
	public bool greaterThan(long value) {
		return _value > value;
	}
	
	//information
	public bool positive {
		get { return _value > 0; }
	}
	public bool negative {
		get { return _value < 0; }
	}
	
	//math operations
	public long add(long value) {
		return _value + value;
	}
	public double add(double value) {
		return _value + value;
	}
	public long subtract(long value) {
		return _value - value;
	}
	public double subtract(double value) {
		return _value - value;
	}
	public long multiply(long value) {
		return _value * value;
	}
	public double multiply(double value) {
		return _value * value;
	}
	public double divide(long value) {
		return _value / value;
	}
	public double divide(double value) {
		return _value / value;
	}
	public long absolute {
		get { return (_value < 0) ? (_value * -1) : _value; }
	}

	//mutating math operations
	public void add1(long value) {
		_value += value;
	}
	public void subtract1(long value) {
		_value -= value;
	}
	public void multiply1(long value) {
		_value *= value;
	}
	public void floorDivide1(long value) {
		_value /= value;
	}
}

} //namespace
