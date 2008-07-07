//implements the Rat interface
//xxx should support arbitrary precision

using Acrid.NodeTypes;

namespace Acrid.Execution {

public class Client_Rational {
	public static IWorker wrap(double value) {
		Client_Rational o = new Client_Rational(value);
		NObject obj = new NObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.stdn_Rat, obj, new IWorker[]{} );

		builder.addBreeder(
			Bridge.stdn_String,
			delegate(){
				return Bridge.toClientString(
					value.ToString().ToLower());
			});
			
		builder.addMethod(
			new Identifier("add"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NType(Bridge.stdn_Rat),
						new Identifier("value"),
						null )
				},
				new NType(Bridge.stdn_Rat),
				delegate(IScope args) {
					return Bridge.toClientRational(
							o.add(
								Bridge.toNativeRational(
									GE.evalIdent(args, "value"))));
				},
				null ));

		builder.addMethod(
			new Identifier("lessThan?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NType(Bridge.stdn_Rat),
						new Identifier("value"),
						null )
				},
				new NType(Bridge.stdn_Bool),
				delegate(IScope args) {
					return Bridge.toClientBoolean(
							o.lessThan(
								Bridge.toNativeRational(
									GE.evalIdent(args, "value"))));
				},
				null ));

		IWorker rv = builder.compile();
		rv.nativeObject = value;
		obj.rootWorker = rv;
		return rv;
	}

	double _value;

	public Client_Rational(double value) {
		_value = value;
	}

	//comparison
	public bool lessThan(double value) {
		return _value < value;
	}
	public bool lessThanOrEqual(double value) {
		return _value <= value;
	}
	public bool equal(double value) {
		return _value == value;
	}
	public bool greaterThanOrEqual(double value) {
		return _value >= value;
	}
	public bool greaterThan(double value) {
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
	public double add(double value) {
		return _value + value;
	}
	public double subtract(double value) {
		return _value - value;
	}
	public double multiply(double value) {
		return _value * value;
	}
	public double divide(double value) {
		return _value / value;
	}
	public double absolute {
		get { return (_value < 0) ? (_value * -1) : _value; }
	}

	//mutating math operations
	public void add1(double value) {
		_value += value;
	}
	public void subtract1(double value) {
		_value -= value;
	}
	public void multiply1(double value) {
		_value *= value;
	}
	public void floorDivide1(double value) {
		_value /= value;
	}

	//convert
	public double toBuiltin() {
		return _value;
	}
	public string toString() {
		return _value.ToString();
	}
}

} //namespace
