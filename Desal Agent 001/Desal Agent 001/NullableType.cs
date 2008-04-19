//corresponds to Desal "nullable-type" nodes

using System.Diagnostics;

class NullableType {
	public static NullableType dyn;
	public static NullableType dyn_nullable;
	
	static NullableType() {
		dyn = new NullableType( (IWorker)null, false );
		dyn_nullable = new NullableType( (IWorker)null, true );
	}

	IWorker _face;
	bool _nullable;

	public NullableType( IWorker face, bool nullable ) {
		if(face is Null)
			throw new ClientException(
				"nullable-type face can't be null (only absent)");
		_face = face;
		_nullable = nullable;
	}
	
	//xxx convenience function -- remove?
	public NullableType( IInterface face, bool nullable ) {
		_face = Bridge.wrapInterface(face);
		_nullable = nullable;
	}

	//null means the face wasn't provided
	//Null is an error
	public IWorker face {
		get { return _face; }
	}
	
	public bool nullable {
		get { return _nullable; }
	}
}
