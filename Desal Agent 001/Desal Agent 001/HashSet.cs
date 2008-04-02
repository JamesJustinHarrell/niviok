//.NET 3.5 defines a HashSet collection
//This is intended to be a subset of that.
//Hopefully the official HashSet will be a drop-in replacement for this class.

using System.Collections;
using System.Collections.Generic;

class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable {
	Dictionary<T, bool> _dict = new Dictionary<T, bool>();

	public HashSet() {}
	
	public void UnionWith(IEnumerable<T> other) {
		foreach( T item in other )
			Add(item);
	}
	
	//removes elements in this collection that are in @other
	public void ExceptWith(IEnumerable<T> other) {
		foreach( T item in other )
			Remove(item);
	}
	
	void ICollection<T>.Add(T item) {
		Add(item);
	}
	
	public bool Add(T item) {
		if( _dict.ContainsKey(item) )
			return false;
		_dict.Add(item, true);
		return true;
	}
	
	public void Clear() {
		_dict.Clear();
	}
	
	public bool Contains(T item) {
		return _dict.ContainsKey(item);
	}
	
	public void CopyTo(T[] array, int arrayIndex) {
		_dict.Keys.CopyTo(array, arrayIndex);
	}
	
	IEnumerator IEnumerable.GetEnumerator() {
		return _dict.Keys.GetEnumerator();
	}
	
	public IEnumerator<T> GetEnumerator() {
		return _dict.Keys.GetEnumerator();
	}
	
	public bool Remove(T item) {
		return _dict.Remove(item);
	}
	
	public int Count {
		get { return _dict.Count; }
	}
	
	public bool IsReadOnly {
		get { return false; }
	}
}
