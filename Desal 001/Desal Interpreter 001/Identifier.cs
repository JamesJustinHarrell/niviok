class Identifier {
	string _str;
	
	//xxx do checking to ensure validity
	public Identifier(string str) {
		_str = str;
	}

	public override bool Equals(object o) {
		return (
			o != null &&
			o is Identifier &&
			((Identifier)o)._str.Equals(_str) );
	}
	
	public override int GetHashCode() {
		return _str.GetHashCode();
	}
	
	public override string ToString() {
		return _str;
	}
};