class Client_Interface {
	IInterface _face;
	
	public Client_Interface( IInterface face ) {
		_face = face;
	}
	
	public IInterface value {
		get { return _face; }
	}
	
	public bool equals( IInterface face) {
		//xxx also could be same if same members and same interface ID
		return _face == face;
	}
}