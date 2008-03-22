class NullableType {
	public static NullableType dyn;
	public static NullableType nulldyn;
	
	static NullableType() {
		dyn = new NullableType(null, false);
		nulldyn = new NullableType(null, true);
	}

	IInterface _face;
	bool _nullable;

	public NullableType( IInterface face, bool nullable ) {
		_face = face;
		_nullable = nullable;
	}
	
	public IInterface face {
		get { return _face; }
	}
	
	public bool nullable {
		get { return _nullable; }
	}
}
