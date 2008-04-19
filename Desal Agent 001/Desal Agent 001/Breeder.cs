//corresponds to Desal "breeder" nodes

class Breeder {
	IWorker _face;
	
	public Breeder(IWorker face) {
		_face = face;
	}
	
	public IWorker face {
		get { return _face; }
	}
}
