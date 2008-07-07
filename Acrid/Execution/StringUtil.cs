//converts between abstract Unicode and UTF-16
//used mainly by the Client_String class

using System;
using System.Collections.Generic;
using System.Text;
using Acrid.NodeTypes;

namespace Acrid.Execution {

static class StringUtil {
	static bool isHighSurrogate(uint codePoint) {
		//note that 0xDB80 through 0xDBFF are for private use
		return (0xD800 <= codePoint && codePoint <= 0xDBFF);
	}
	
	static bool isLowSurrogate(uint codePoint) {
		return (0xDC00 <= codePoint && codePoint <= 0xDFFF);
	}

	static bool isSurrogate(uint codePoint) {
		return (0xD800 <= codePoint && codePoint <= 0xDFFF);
	}
	
	static uint fromSurrogatePair(uint high, uint low) {
		return (high - 0xD800)*0x400 + (low - 0xDC00) + 0x10000;
	}

	static char computeHighSurrogate(uint codePoint) {
		return (char)Math.Floor(
			(double)(codePoint-0x10000)/0x400+0xD800 );
	}
	
	static char computeLowSurrogate(uint codePoint) {
		return (char)( (codePoint-0x10000) % 0x400+0xDC00 );
	}

	public static IList<uint> codePointsFromString(string str) {
		IList<uint> codePoints = new List<uint>();

		for( int i = 0; i < str.Length; i++ ) { 
			uint code = str[i];

			if( isHighSurrogate(code) ) {
				i++;
				if( i > str.Length )
					throw new ArgumentException(
						"string ends with high surrogate");
				uint nextCode = str[i];
				if( ! isLowSurrogate(nextCode) )
					throw new ArgumentException(
						"high surrogate not followed by low surrogate");
				codePoints.Add(fromSurrogatePair(code, nextCode));
			}
			else {
				codePoints.Add(code);
			}
		}

		return codePoints;
	}
	
	public static string stringFromCodePoints(IList<uint> codePoints) {
		StringBuilder sb = new StringBuilder();
		
		foreach( uint codePoint in codePoints ) {
			if( codePoint > 0x10FFFF ) {
				throw new ArgumentException(
					"illegal code point - above 0x110000");
			}
			if( isSurrogate(codePoint) ) {
				throw new ArgumentException(
					"surrogates are not meaningful code points");
			}
			if( codePoint >= 0x10000 ) {
				sb.Append(computeHighSurrogate(codePoint));
				sb.Append(computeLowSurrogate(codePoint));
			}
			else {
				sb.Append( (char)codePoint );
			}
		}
		
		return sb.ToString();
	}
}

} //namespace
