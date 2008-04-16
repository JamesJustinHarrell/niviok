class Argument {
	Identifier _name;
	IWorker _value;
	
	public Argument(Identifier name, IWorker value) {
		_name = name;
		_value = value;
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public IWorker value {
		get { return _value; }
	}
}
