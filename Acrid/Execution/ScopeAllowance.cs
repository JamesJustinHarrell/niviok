using System;
using Acrid.NodeTypes;

namespace Acrid.Execution {

public class ScopeAllowance {
	bool _can_remit;
	bool _can_yield;
	
	public ScopeAllowance(bool can_remit, bool can_yield) {
		_can_remit = can_remit;
		_can_yield = can_yield;
	}
	
	public bool can_remit {
		get { return _can_remit; }
	}
	
	public bool can_yield {
		get { return _can_yield; }
	}
}

} //namespace
