using System;

namespace Acrid {

public class ParseError : Exception {
	public string source;
	public ParseError(string message, string aSource)
		: base(message) { source = aSource; }
	public ParseError(string message, string aSource, Exception innerException)
		: base(message, innerException) { source = aSource; }
}

} //namespace
