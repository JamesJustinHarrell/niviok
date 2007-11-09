class Identifier {
	string _str;
	
	//xxx do checking to ensure validity
	public Identifier(string str) {
		_str = str;
	}
	
	public string str {
		get { return _str; }
	}
	
	public override bool Equals(object o) {
		return (
			o is Identifier &&
			((Identifier)o)._str.Equals(_str) );
	}
	
	public override int GetHashCode() {
		return _str.GetHashCode();
	}
};