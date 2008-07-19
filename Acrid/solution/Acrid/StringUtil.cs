//converts between abstract Unicode and UTF-16

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Acrid {

public static class StringUtil {
	public static char replacement = (char)0xFFFD; //U+FFFD REPLACEMENT CHARACTER

	public static bool isHighSurrogate(uint codePoint) {
		return (0xD800 <= codePoint && codePoint <= 0xDBFF);
	}
	
	public static bool isLowSurrogate(uint codePoint) {
		return (0xDC00 <= codePoint && codePoint <= 0xDFFF);
	}

	public static bool isSurrogate(uint codePoint) {
		return (0xD800 <= codePoint && codePoint <= 0xDFFF);
	}
	
	public static uint fromSurrogatePair(uint high, uint low) {
		if( ! (isHighSurrogate(high) && isLowSurrogate(low)) )
			return replacement;
		return (high - 0xD800)*0x400 + (low - 0xDC00) + 0x10000;
	}

	public static char computeHighSurrogate(uint codePoint) {
		return (char)(
			((double)(codePoint - 0x10000)) /
			((double)(0x400 + 0xD800)) );
	}
	
	public static char computeLowSurrogate(uint codePoint) {
		return (char)( ((codePoint - 0x10000) % 0x400) + 0xDC00 );
	}

	public static IList<uint> toCodePoints(string str) {
		return toCodePoints(new StringReader(str));
	}
	
	public static IList<uint> toCodePoints(TextReader reader) {
		//key: H-high, L-low, N-normal
		//5 cases: HL, HN|HH, H<eof>, L, N
		IList<uint> codePoints = new List<uint>();
		while( reader.Peek() != -1 ) {
			char code1 = (char)reader.Read();
			if( isHighSurrogate(code1) ) {
				if( reader.Peek() != -1 ) {
					char code2 = (char)reader.Read();
					if( isLowSurrogate(code2) ) //HL
						codePoints.Add(fromSurrogatePair(code1, code2));
					else codePoints.Add(replacement); //HN|HH
				}
				else codePoints.Add(replacement); //H<eof>
			}
			else if( isLowSurrogate(code1) ) //L
				codePoints.Add(replacement);
			else codePoints.Add(code1); //N
		}
		return codePoints;
	}
	
	public static string stringFromCodePoints(IList<uint> codePoints) {
		StringBuilder sb = new StringBuilder();
		foreach( uint codePoint in codePoints ) {
			if( codePoint > 0x10FFFF || isSurrogate(codePoint) )
				sb.Append(replacement);
			if( codePoint >= 0x10000 ) {
				sb.Append(computeHighSurrogate(codePoint));
				sb.Append(computeLowSurrogate(codePoint));
			}
			else sb.Append( (char)codePoint );
		}
		return sb.ToString();
	}
}

} //namespace
