//implements the Int interface
//xxx should support abitrary precision

class Client_Integer {
	public static IWorker wrap(long value) {
		Client_Integer o = new Client_Integer(value);
		IObject obj = new DesalObject(value);
		WorkerBuilder builder = new WorkerBuilder(
			Bridge.faceInt, obj, new IWorker[]{});

		builder.addMethod(
			new Identifier("add"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceInt, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.faceInt, false),
				delegate(Scope args) {
					return Bridge.wrapInteger(
							o.add(
								Bridge.unwrapInteger(
									args.evaluateLocalIdentifier(
										new Identifier("value"))) ));
				},
				Bridge.universalScope ));

		builder.addMethod(
			new Identifier("lessThan?"),
			new Function_Native(
				new ParameterImpl[]{
					new ParameterImpl(
						Direction.IN,
						new NullableType(Bridge.faceInt, false),
						new Identifier("value"),
						null )
				},
				new NullableType(Bridge.faceBool, false),
				delegate(Scope args) {
					return Bridge.wrapBoolean(
							o.lessThan(
								Bridge.unwrapInteger(
									args.evaluateLocalIdentifier(
										new Identifier("value"))) ));
				},
				Bridge.universalScope ));
			
		return builder.compile();
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
