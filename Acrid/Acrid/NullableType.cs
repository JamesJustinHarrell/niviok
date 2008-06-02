//corresponds to "nullable-type" nodes

using System.Diagnostics;

class NullableType {
	public static NullableType dyn;
	public static NullableType dyn_nullable;
	
	static NullableType() {
		dyn = new NullableType( null, false );
		dyn_nullable = new NullableType( null, true );
	}

	IInterface _face;
	bool _nullable;

	public NullableType( IInterface face, bool nullable ) {
		_face = face;
		_nullable = nullable;
	}

	//null means the face wasn't provided
	//Null is an error
	public IInterface face {
		get { return _face; }
	}
	
	public bool nullable {
		get { return _nullable; }
	}
}
