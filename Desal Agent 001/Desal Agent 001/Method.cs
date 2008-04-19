//corresponds to Desal "method" nodes

using System.Diagnostics;

class Method {
	Identifier _name;
	IInterface _face;

	public Method(Identifier name, IInterface face) {
		Debug.Assert(name != null);
		Debug.Assert(face != null);
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
