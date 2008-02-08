class Client_Boolean {
	bool _value;
	
	public Client_Boolean(bool value) {
		_value = value;
	}
	
	public bool value {
		get { return _value; }
	}
	
	public bool equals(bool value) {
		return _value == value;
	}
}