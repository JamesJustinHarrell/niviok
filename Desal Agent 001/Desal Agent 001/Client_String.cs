using System.Collections.Generic;
using StringBuilder = System.Text.StringBuilder;

/*
in C#, this outputs 18:
System.Console.WriteLine( "🿰 Hello, World 🿱".Length );

in Desal, this outputs 16:
println( "🿰 Hello, World 🿱".length )

The difference is clear. C# strings are a pile of steaming shit.
*/

static class StringUtil {
	static bool isSurrogate(uint codePoint) {
		/*
		high surrogates: 0xD800 through 0xDBFF
			private use: 0xDB80 through 0xDBFF
		low surrogates: 0xDC00 through 0xDFFF */
		return (0xD800 <= codePoint && codePoint <= 0xDFFF);
	}

	public static IList<uint> codePointsFromString(string str) {
		IList<uint> codePoints = new List<uint>();

		for( int i = 0; i < str.Length; i++ ) { 
			uint code = str[i];

			if( 0xD800 <= code && code <= 0xDBff ) {
				i++;
				if( i > str.Length ) break;
				uint nextCode = str[i];
				if( nextCode >= 0xDC00 && nextCode <= 0xDFFF ) {
					codePoints.Add( (code - 0xD800)*0x400 + (nextCode-0xDC00) + 0x10000 );
				}
			}
			else {
				codePoints.Add(code);
			}
		}

		return codePoints;
	}
	
	public static string stringFromCodePoints(IList<uint> codePoints) {
		//List<char> UTF16 = new List<char>();
		StringBuilder sb = new StringBuilder();
		
		foreach( uint codePoint in codePoints ) {
			if( codePoint > 0x10FFFF ) {
				throw new System.ApplicationException(
					"illegal code point - above 0x110000");
			}
			if( isSurrogate(codePoint) ) {
				throw new System.ApplicationException(
					"surrogates are not meaningful code points");
			}
			if( codePoint >= 0x10000 ) {
				sb.Append(
					(char)System.Math.Floor(
						(double)(codePoint-0x10000)/0x400+0xD800 ));
				sb.Append( (char)(
					(codePoint-0x10000) % 0x400+0xDC00 ));
			}
			else {
				sb.Append( (char)codePoint );
			}
		}
		
		return sb.ToString();
	}
}

class Client_String {
	List<uint> _codePoints;

	public Client_String(IList<uint> codePoints) {
		if( codePoints is List<uint> ) {
			_codePoints = (List<uint>)codePoints;
		}
		else {
			_codePoints = new List<uint>();
			_codePoints.AddRange(codePoints);
		}
	}

	public IList<uint> codePoints {
		get { return _codePoints; }
	}
	
	//----- client interface
	
	public long length {
		get { return _codePoints.Count; }
	}

	public IList<uint> concat(IList<uint> val) {
		List<uint> rv = new List<uint>();
		rv.AddRange(_codePoints);
		rv.AddRange(val);
		return rv;
	}
	
	public void concat0(IList<uint> val) {
		_codePoints.AddRange(val);
	}
	
	public IList<uint> substring(long a) {
		throw new Error_Unimplemented();
	}
	
	public IList<uint> substring(long a, long b) {
		throw new Error_Unimplemented();
	}
}