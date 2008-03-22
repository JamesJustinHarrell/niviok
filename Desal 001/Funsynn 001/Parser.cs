using System.Collections.Generic;

namespace Funsynn {

/*
use self and internal rules to parse a Funsynn grammar,
then use that grammar to parse a file
*/

	class Factor {
		public string type;
		public string literal;
	}	
	
	class Term {
		public IList<Factor> factors;
	}
	
	class Expression {
		public IList<Term> terms;
	}
	
	
	class Grammar {
		/*
		Funsynn syntax consists of rules.
		Each rule consists of 1 identifier and 1 expression.
		Each expression consists of multiple terms.
		Each term consists of multiple factors.
		Each factor is one of a thing.
		*/		
	
		IDictionary<Identifier, Expression> rules;

		//grammar of Funsynn
		public Grammar() {
			rules = new Dictionary<Identifier, Expression>();
			
			Expression documentExpression = new Expression();
			Term documentTerm1 = new Term();
			Factor documentFactor1_1 = new Factor();
			documentFactor1_1.type = "literal";
			documentFactor1_1.type = "Document = \"foo\" ;";
			documentTerm1.factors.Add(documentFactor1_1);
			documentExpression.Add(documentTerm1);			
			rules["Document"] = documentExpression;
		}
	
		//grammar described by Funsynn
		public Grammar(string filePath) {
			StreamReader file = File.OpenText(filePath);
			string line = file.ReadLine();
			while( line != null ) {
				Console.WriteLine(line);
				file.Read();
				line = file.ReadLine();
			}
			file.Close();
		}
	}

	public class Parser	{
		public Parser(Grammar grammar) {
		}
	
		public void parseFile(string filePath) {
		}
	}
	
	class Choice : IExpression {
}

class Series : IExpression {
}

class Exclusion : IExpression {
}

class Optional : IExpression {
}

class Repetition : IExpression {
	int minimum;
	int maximum; //or negative for infinity
}

class Terminal : IExpression {
}





parser.discardMatches("Comment");
parser.discardMatches("Whitespace");
parser.discardMatches("Fill");
}