class Client_Integer {
	//xxx replace with BigInt
	long _val;
	
	public Client_Integer(long val) {
		_val = val;
	}
	
	public long val {
		get { return _val; }
	}
	
	//comparison
	public bool lessThan(long val) {
		return _val < val;
	}
	public bool lessThanOrEqual(long val) {
		return _val <= val;
	}
	public bool equal(long val) {
		return _val == val;
	}
	public bool greaterThanOrEqual(long val) {
		return _val >= val;
	}
	public bool greaterThan(long val) {
		return _val > val;
	}
	
	//information
	public bool positive {
		get { return _val > 0; }
	}
	public bool negative {
		get { return _val < 0; }
	}
	
	//math operations
	public long add(long val) {
		return _val + val;
	}
	public double add(double val) {
		return _val + val;
	}
	public long subtract(long val) {
		return _val - val;
	}
	public double subtract(double val) {
		return _val - val;
	}
	public long multiply(long val) {
		return _val * val;
	}
	public double multiply(double val) {
		return _val * val;
	}
	public double divide(long val) {
		return _val / val;
	}
	public double divide(double val) {
		return _val / val;
	}
	public long absolute {
		get { return (_val < 0) ? (_val * -1) : _val; }
	}

	//mutating math operations
	public void add1(long val) {
		_val += val;
	}
	public void subtract1(long val) {
		_val -= val;
	}
	public void multiply1(long val) {
		_val *= val;
	}
	public void floorDivide1(long val) {
		_val /= val;
	}

	//convert
	public long toBuiltin() {
		return _val;
	}
	public string toString() {
		return _val.ToString();
	}
	//xxx func toString(IntNotationMethod) String
}
