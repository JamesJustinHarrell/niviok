//corresponds to Desal "breeder" nodes

class Breeder {
	IInterface _face;
	
	public Breeder(IInterface face) {
		_face = face;
	}
	
	public IInterface face {
		get { return _face; }
	}
}
