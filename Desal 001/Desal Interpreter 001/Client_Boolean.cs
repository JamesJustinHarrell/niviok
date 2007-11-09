class Client_Boolean {
	bool _val;
	
	public Client_Boolean(bool val) {
		_val = val;
	}
	
	public bool val {
		get { return _val; }
	}
	
	public bool equals(bool val) {
		return _val == val;
	}
}