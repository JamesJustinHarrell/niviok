/*
This class was created because the MSDN and Mono documentation doesn't
say anything about the Equals and GetHashCode methods of the List<T> class,
and I need them to work by value.
*/

using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Acrid.NodeTypes;

public class IdentifierSequence : IEnumerable<Identifier> {
	IList<Identifier> _idents;
	
	public IdentifierSequence(params Identifier[] idents) {
		_idents = idents.Clone() as IList<Identifier>;
		Debug.Assert( _idents.Count > 0 );
	}
	
	public IdentifierSequence(IEnumerable<Identifier> idents) {
		_idents = new List<Identifier>(idents);
		Debug.Assert( _idents.Count > 0 );
	}
	
	public int Count {
		get { return _idents.Count; }
	}
	
	System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
		return _idents.GetEnumerator();
	}
	
	public IEnumerator<Identifier> GetEnumerator() {
		return _idents.GetEnumerator();
	}
	
	public override bool Equals(object o) {
		return (o is IdentifierSequence) &&
		(o as IdentifierSequence).ToString() == ToString();
	}
	
	public override int GetHashCode() {
		return ToString().GetHashCode();
	}
	
	public override string ToString() {
		IList<string> temp1 = new List<string>();
		foreach( Identifier ident in _idents )
			temp1.Add(ident.ToString());
		string[] temp2 = new string[_idents.Count];
		temp1.CopyTo(temp2, 0);
		return System.String.Join(":", temp2);
	}
}
