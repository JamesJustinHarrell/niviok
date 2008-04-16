//corresponds to the method node
class MethodInfo {
	Identifier _name;
	IInterface _face;

	public MethodInfo(Identifier name, IInterface face) {
		_name = name;
		_face = face;
	}
	
	public Identifier name {
		get { return _name; }
	}
	
	public IInterface face {
		get { return _face; }
	}
}
