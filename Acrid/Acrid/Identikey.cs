using System;

class Identikey {
	IdentikeyCategory _category;
	NullableType _type;
	IWorker _value;
	
	public Identikey( IdentikeyCategory category, NullableType type, IWorker value ) {
		_category = category;
		_type = type;
		_value = value;
	}
	
	public IdentikeyCategory category {
		get { return _category; }
	}
	
	public NullableType type {
		get { return _type; }
		set {
			if( _value != null )
				throw new ApplicationException(
					"only declare-first identikeys can " +
					"have their nullable types changed");
			_type = value;
		}
	}
	
	public IWorker value {
		get { return _value; }
		set {
			if( _category == IdentikeyCategory.FUNCTION && ! (_value == null) )
				throw new NotImplementedException("function merging not supported yet");
			_value = value;
		}
	}
}
