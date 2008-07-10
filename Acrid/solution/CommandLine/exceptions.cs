using System;

namespace Acrid.ComandLine {

//errors caused by the user, but not in client code
//e.g. invalid program arguments
class UserError : Exception {
	public UserError()
		:base() {}
	public UserError(string message)
		:base(message) {}
	public UserError(string message, Exception innerException)
		:base(message, innerException) {}
}

} //namespace
