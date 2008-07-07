//corresponds to Niviok "breeder" nodes

namespace Acrid.Execution {

public class Breeder {
	IType _type;
	
	public Breeder(IType type) {
		_type = type;
	}
	
	public IType type {
		get { return _type; }
	}
}

} //namespace
