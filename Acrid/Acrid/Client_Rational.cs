//implements the Rat interface
//xxx should support arbitrary precision

class Client_Rational {
	public static IWorker wrap(double value) {
		Client_Rational o = new Client_Rational(value);
		NiviokObject obj = new NiviokObject();
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.std_Rat, obj, new IWorker[]{} );

		builder.addBreeder(
			Bridge.std_String,
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
						new NullableType(Bridge.std_Rat, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.std_Rat, false),
				delegate(Scope args) {
					return Bridge.toClientRational(
							o.add(
								Bridge.toNativeRational(
									args.evaluateLocalIdentifier(
										new Identifier("value"))) ));
				},
				Bridge.debugScope ));

		builder.addMethod(
			new Identifier("lessThan?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.std_Rat, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.std_Bool, false),
				delegate(Scope args) {
					return Bridge.toClientBoolean(
							o.lessThan(
								Bridge.toNativeRational(
									args.evaluateLocalIdentifier(
										new Identifier("value"))) ));
				},
				Bridge.debugScope ));

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
