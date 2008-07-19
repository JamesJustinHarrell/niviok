using System.Collections.Generic;
using System.Diagnostics;

namespace Acrid.Toy {

public class Sexp {
	SexpType _type;
	string _atom;
	LinkedList<Sexp> _list;
	int _line;
	int _column;
	
	public Sexp(
	SexpType type, string atom, LinkedList<Sexp> list,
	int line, int column) {
		if( type == SexpType.LIST )
			Debug.Assert( list != null );
		_type = type;
		_atom = atom;
		_list = list;
		_line = line;
		_column = column;
	}
	
	public SexpType type {
		get { return _type; }
	}
	
	public string atom {
		get { return _atom; }
	}
	
	public LinkedList<Sexp> list {
		get { return _list; }
	}
	
	public int line {
		get { return _line; }
	}
	
	public int column {
		get { return _column; }
	}
	
	public override string ToString() {
		if( _type == SexpType.LIST ) {
			string rv = "(";
			bool follow = false;
			foreach( Sexp s in _list ) {
				if(follow)
					rv += " ";
				else follow = true;
				rv += s.ToString();
			}
			rv += ")";
			return rv;
		}
		return _atom;
	}
}

} //namespace
