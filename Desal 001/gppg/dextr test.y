%{
	public Bridge bridge;
	public Bundle bundle;
	public Plane plane;
	public bool shouldParseBundle;
%}

%start DextrDocument

%token DOCUMENT_OPEN
%token DOCUMENT_CLOSE
%token INDENT_OPEN
%token INDENT_CLOSE
%token NEWLINE
%token FREE
%token INTEGER
%token RATIONAL
%token RESERVED
%token STRING

%%

Expression
	: Simple
	| Add
	| BooleanLogic
	| DeclareFirst
	| IfElse
	| Test ;

Identifier
	: [ "$" ] FREE ;

Primary
	: Identifier
	| INTEGER
	| RATIONAL
	| STRING
	| Parenthetical ;

Parenthetical
	: '(' Expression ')' ;

Simple
	: Block
	| Call
	| ExtractMember
	| Primary ;

Call
	: Simple "(" ArgumentList ")" ;

ArgumentList
	: [ Expression { "," Expression } ] ;

ExtractMember
	: Simple "." Identifier ;

Mult
	: Simple ("*" | "/") Simple ;

Add
	: Mult ("+" | "-") Mult ;

BooleanLogic
	: Simple ("and" | "nand" | "or" | "nor" | "xor" | "xnor") Simple ;

DeclareFirst
	: DeclareFirstNormal
	| FunctionDeclaration ;

DeclareFirstNormal
	: "decl" Identifier "=" Expression ;

FunctionDeclaration
	: "func" Identifier "(" ParameterList ")" Block ;

ParameterList
	: [ Parameter { "," Parameter } ] ;

Parameter
	: Identifier ;

Block
	: BraceBlock
	| TabBlock
	| BraceTabBlock ;

//to ensure this starts and ends on the same line,
//test lineNumber of "{" and "}" tokens
//this can't be easily stated in BNF
BraceBlock
	: "{" [ Expression { "," Expression } ] "}" ;

//the tokenizer ensures a newline comes before every indent open
//the newline is declared optional to allow, e.g.: NEWLINE TabBlock
TabBlock
	: [ NEWLINE ] INDENT_OPEN { Expression NEWLINE } INDENT_CLOSE ;

BraceTabBlock
	: "{" TabBlock "}" ;

Test
	: Simple ("lt" | "lte" | "eql" | "gte" | "gt" | "dne") Simple ;

IfElse
	: "if" (Simple | Test) Block [ { "elif" (Simple | Test) Block } "else" Block ] ;

BundleDocument
	:
		DOCUMENT_OPEN
		{ "expose" Identifier | NEWLINE }
		{ DeclareFirst | NEWLINE }
		DOCUMENT_CLOSE
	;
	
PlaneDocument
	:
		DOCUMENT_OPEN
		{ DeclareFirst | NEWLINE }
		DOCUMENT_CLOSE
	;

//xxx should choose between PlaneDocument and BundleDocument based on shouldParseBundle
DextrDocument
	: BundleDocument ;











list    :   /*empty */
        |   list stat '\n'
        |   list error '\n'
                {
                    yyerrok();
                }
        |   list '\n' stat '\n' { /* skip */ }
        ;


stat    :   expr
                {
                    System.Console.WriteLine($1);
                }
        |   LETTER '=' expr
                {
                    regs[$1] = $3;
                }
        ;

expr    :   '(' expr ')'
                {
                    $$ = $2;
                }
        |   expr '*' expr
                {
                    $$ = $1 * $3;
                }
        |   expr '/' expr
                {
                    $$ = $1 / $3;
                }
        |   expr '%' expr
                {
                    $$ = $1 % $3;
                }
        |   expr '+' expr
                {
                    $$ = $1 + $3;
                }
        |   expr '-' expr
                {
                    $$ = $1 - $3;
                }
        |   expr '&' expr
                {
                    $$ = $1 & $3;
                }
        |   expr '|' expr
                {
                    $$ = $1 | $3;
                }
        |   '-' expr %prec UMINUS
                {
                    $$ = -$2;
                }
        |   LETTER
                {
                    $$ = regs[$1];
                }
        |   number
        ;

number  :   DIGIT
                {
                    $$ = $1;
                    _base = ($1==0) ? 8 : 10;
                }
        |   number DIGIT
                {
                    $$ = _base * $1 + $2;
                }
        ;

%%

static void Main(string[] args)
{
    Parser parser = new Parser();
    
    System.IO.TextReader reader;
    if (args.Length > 0)
        reader = new System.IO.StreamReader(args[0]);
    else
        reader = System.Console.In;
        
    parser.scanner = new Scanner(reader);
    //parser.Trace = true;
    
    parser.Parse();
}

class Scanner: gppg.IScanner<int,LexLocation>
{
    private System.IO.TextReader reader;

    public Scanner(System.IO.TextReader reader)
    {
        this.reader = reader;
    }

    public override int yylex()
    {
        char ch = (char) reader.Read();

        if (ch == '\n')
            return ch;
        else if (char.IsWhiteSpace(ch))
            return yylex();
        else if (char.IsDigit(ch))
        {
            yylval = ch - '0';
            return (int)Tokens.DIGIT;
        }
        else if (char.IsLetter(ch))
        {
            yylval = char.ToLower(ch) - 'a';
            return (int)Tokens.LETTER;
        }
        else
            switch (ch)
            {
                case '+':
                case '-':
                case '*':
                case '/':
                case '(':
                case ')':
                case '%':
                case '|':
                case '&':
                case '=':
                    return ch;
                default:
                    Console.Error.WriteLine("Illegal character '{0}'", ch);
                    return yylex();
            }
    }

    public override void yyerror(string format, params object[] args)
    {
        Console.Error.WriteLine(format, args);
    }
}
