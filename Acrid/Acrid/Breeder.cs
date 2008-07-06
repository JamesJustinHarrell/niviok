//corresponds to Niviok "breeder" nodes

class Breeder {
	IType _type;
	
	public Breeder(IType type) {
		_type = type;
	}
	
	public IType type {
		get { return _type; }
	}
}
