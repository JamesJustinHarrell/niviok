//xxx use bignum

class Client_Rational {
	double _value;

	public Client_Rational(double value) {
		_value = value;
	}
	
	public double value {
		get { return _value; }
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
