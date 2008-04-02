class Client_Integer {
	//xxx replace with BigInt
	long _value;
	
	public Client_Integer(long value) {
		_value = value;
	}
	
	public long value {
		get { return _value; }
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

	//convert
	public long toBuiltin() {
		return _value;
	}
	public string toString() {
		return _value.ToString();
	}
	//xxx func toString(IntNotationMethod) String
}
