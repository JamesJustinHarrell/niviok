using System;
using System.Collections.Generic;
using System.Text;
using Acrid.NodeTypes;

namespace Acrid.Execution {

/* When executing module or compound node:
* first create the complete hierachy of scopes, complete with all scidentres
* along the way build up a queue of empty wo-scidentres
* execute entries in the queue, assigning to the associated wo-scidentres */
public class ScopeQueue {
	enum State {
		EMPTY, WAITING, READY
	}
	class Entry {
		public IScidentre target;
		public INode_Expression typeExpr;
		public INode_Expression valueExpr;
		public IScope scope;
		public HashSet<Entry> executeDependencies;
		public HashSet<Entry> finishDependencies;
		public State state;
		public HashSet<Entry> listeners;
		
		public Entry(
		IScidentre a,
		INode_Expression b,
		INode_Expression c,
		IScope d,
		HashSet<Entry> e,
		HashSet<Entry> f,
		State g,
		HashSet<Entry> h) {
			target = a;
			typeExpr = b;
			valueExpr = c;
			scope = d;
			executeDependencies = e;
			finishDependencies = f;
			state = g;
			listeners = h;
		}
		
		public string shortString() {
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("type: " + typeExpr);
			sb.AppendLine("    source: " + typeExpr.nodeSource);
			sb.AppendLine("value: " + valueExpr);
			sb.AppendLine("    source: " + valueExpr.nodeSource);
			sb.AppendLine("state: " + state);
			return sb.ToString();
		}	
		
		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			sb.Append(shortString());
			if( executeDependencies.Count > 0 ) {
				sb.AppendLine("--- execute dependencies:");
				foreach( Entry e in executeDependencies )
					sb.Append(e.shortString());
			}
			if( finishDependencies.Count > 0 ) {
				sb.AppendLine("--- finish dependencies:");
				foreach( Entry e in finishDependencies )
					sb.Append(e.shortString());
			}
			return sb.ToString();
		}
	}
	List<Entry> _entries; //needed to store order of entries
	IDictionary<IScidentre, Entry> _entryLookup;

	public ScopeQueue() {
		_entries = new List<Entry>();
		_entryLookup = new Dictionary<IScidentre,Entry>();
	}
	
	public void add(
	IScidentre ws, INode_Expression typeExpr, INode_Expression valueExpr, IScope scope ) {
		Entry entry = new Entry(
			ws,
			typeExpr, valueExpr,
			scope,
			null, null,
			State.EMPTY,
			new HashSet<Entry>() );
		_entries.Add(entry);
		_entryLookup.Add(ws, entry);
	}
	
	public void executeAll() {
		findAllDependencies();
		reduceQueue();
	}

	void findAllDependencies() {
		foreach(Entry e in _entries) {
			DependsResults results = Depends.dependsSplitAny(e.valueExpr);
			results.executeDepends.UnionWith(Depends.depends(e.typeExpr));
			e.executeDependencies = findDependencies(results.executeDepends, e.scope);
			e.finishDependencies = findDependencies(results.finishDepends, e.scope);
		}
	}
	
	HashSet<Entry> findDependencies( HashSet<Identifier> identseqs, IScope scope ) {
		HashSet<Entry> output = new HashSet<Entry>();
		foreach(Identifier identseq in identseqs) {
			foreach(IScidentre ws in scope.upFindEmptyScidentres(identseq))
				if(_entryLookup.ContainsKey(ws))
					output.Add(_entryLookup[ws]);
		}
		return output;
	}

	void reduceQueue() {
		foreach(Entry e in _entries) {
			setListeners( e, e.executeDependencies );
			setListeners( e, e.finishDependencies );
		}
		foreach(Entry e in _entries)
			tryAdvance(e);
		StringBuilder sb = new StringBuilder();
		foreach(Entry e in _entries)
			if(e.state != State.READY)
				sb.AppendLine(e.ToString());
		if( sb.Length > 0 )
			throw new Exception(
				"Some wo-scidentres, created by declare-first nodes, " +
				"could not be set to the READY state:\n" + sb.ToString());
	}
	
	void tryAdvance(Entry e) {
		if(e.state == State.EMPTY) {
			removeReadyDepends(e.executeDependencies);
			if(e.executeDependencies.Count == 0) {
				e.target.type = Evaluator.evaluateType(e.typeExpr, e.scope);
				e.target.assign(Executor.executeAny(e.valueExpr, e.scope));
				e.state = State.WAITING;
				#if SQDEBUG
					Console.WriteLine("EMPTY -> WAITING");
					Console.WriteLine(e);
				#endif
				checkListeners(e);
			}
		}
		if(e.state == State.WAITING) {
			removeReadyDepends(e.finishDependencies);
			if( cyclicalReady(e, new HashSet<Entry>()) ) {
				e.state = State.READY;
				#if SQDEBUG
					Console.WriteLine("WAITING -> READY");
					Console.WriteLine(e);
				#endif
				checkListeners(e);
			}
		}
	}
	
	void setListeners( Entry entry, HashSet<Entry> dependencies ) {
		foreach(Entry dep in dependencies)
			dep.listeners.Add(entry);
	}
	
	void checkListeners( Entry e ) {
		foreach(Entry e2 in e.listeners)
			tryAdvance(e2);
	}
	
	void removeReadyDepends( HashSet<Entry> depends ) {
		HashSet<Entry> toRemove = new HashSet<Entry>();
		foreach(Entry e in depends)
			if(e.state == State.READY)
				toRemove.Add(e);
		foreach(Entry e in toRemove)
			depends.Remove(e);
	}

	//tells if @entry is READY or WAITING with only cyclical dependencies
	bool cyclicalReady( Entry entry, HashSet<Entry> cleared ) {
		switch(entry.state) {
		case State.EMPTY :
			return false;
		case State.WAITING :
			if( cleared.Contains(entry) )
				return true;
			if( entry.finishDependencies.Count == 0 )
				return true;
			HashSet<Entry> cleared2 = new HashSet<Entry>();
			cleared2.UnionWith(cleared);
			cleared2.Add(entry);
			return G.allTrue<Entry>(
				entry.finishDependencies,
				delegate(Entry dep) { return cyclicalReady(dep, cleared2); });
		case State.READY :
			return true;
		default :
			throw new Exception();
		}
	}
}

} //namespace
