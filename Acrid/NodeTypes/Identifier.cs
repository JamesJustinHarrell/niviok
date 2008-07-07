namespace Acrid.NodeTypes {

public class Identifier {
	string _str;
	
	//xxx do checking to ensure validity
	public Identifier(string str) {
		_str = str;
	}

	public override bool Equals(object o) {
		return this.ToString() == o.ToString();
	}
	
	public override int GetHashCode() {
		return _str.GetHashCode();
	}
	
	public override string ToString() {
		return _str;
	}
}

} //namespace
