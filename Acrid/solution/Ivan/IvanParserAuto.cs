//THIS FILE WAS GENERATED PROGRAMMATICALLY.
//DO NOT EDIT THIS FILE DIRECTLY.
//CHANGES TO THIS FILE WILL BE OVERRIDDEN.

using System;
using System.Collections.Generic;
using Acrid.NodeTypes;
using Acrid.Ivan.SableCC.node;

namespace Acrid.Ivan {

public abstract class IvanParserAuto : IvanParserBase {
	protected virtual Node_And parseAnd(AAnd node) {
		return new Node_And(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFirst()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSecond()),
			getSource(node));
	}

	protected abstract Node_MemberStatus parseMemberStatus(PMemberstatus node);

	protected virtual Node_DeclareFirst parseDeclareFirst(ADeclarefirst node) {
		return new Node_DeclareFirst(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetOverload()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetBreed()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Conditional parseConditional(AConditional node) {
		return new Node_Conditional(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetTest()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetResult()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetElse()),
			getSource(node));
	}

	protected virtual Node_Argument parseArgument(AArgument node) {
		return new Node_Argument(
			parseOpt<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetParametername()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Module parseModule(AModule node) {
		return new Node_Module(
			parseOne<TInteger,Node_Integer>(parseInteger, (TInteger)node.GetNiviokmajorversionnumber()),
			parseOne<TInteger,Node_Integer>(parseInteger, (TInteger)node.GetNiviokminorversionnumber()),
			parseMult0<AImport,Node_Import>(parseImport, node.GetImport()),
			parseOne<ASieve,Node_Sieve>(parseSieve, (ASieve)node.GetSieve()),
			getSource(node));
	}

	protected virtual Node_Integer parseInteger(TInteger node) {
		return new Node_Integer(
			node.Text,
			getSource(node));
	}

	protected abstract Node_Boolean parseBoolean(PBoolean node);

	protected virtual Node_DictionaryEntry parseDictionaryEntry(ADictionaryentry node) {
		return new Node_DictionaryEntry(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetKey()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Property parseProperty(AProperty node) {
		return new Node_Property(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetWritable()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			getSource(node));
	}

	protected abstract Node_MemberType parseMemberType(PMembertype node);

	protected virtual Node_ImportAttempt parseImportAttempt(AImportattempt node) {
		return new Node_ImportAttempt(
			parseOne<TString,Node_String>(parseString, (TString)node.GetScheme()),
			parseOne<TString,Node_String>(parseString, (TString)node.GetBody()),
			getSource(node));
	}

	protected virtual Node_Select parseSelect(ASelect node) {
		return new Node_Select(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetInputvalue()),
			parseMult0<ACase,Node_Case>(parseCase, node.GetCase()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetElse()),
			getSource(node));
	}

	protected virtual Node_String parseString(TString node) {
		return new Node_String(
			node.Text,
			getSource(node));
	}

	protected virtual Node_GenericInterface parseGenericInterface(AGenericinterface node) {
		return new Node_GenericInterface(
			parseMult1<AParameterinfo,Node_ParameterInfo>(parseParameterInfo, node.GetParameter()),
			parseOne<AInterface,Node_Interface>(parseInterface, (AInterface)node.GetInterface()),
			getSource(node));
	}

	protected virtual Node_ParameterImpl parseParameterImpl(AParameterimpl node) {
		return new Node_ParameterImpl(
			parseOne<PDirection,Node_Direction>(parseDirection, (PDirection)node.GetDirection()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetDefaultvalue()),
			getSource(node));
	}

	protected virtual Node_Xor parseXor(AXor node) {
		return new Node_Xor(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFirst()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSecond()),
			getSource(node));
	}

	protected virtual Node_Raise parseRaise(ARaise node) {
		return new Node_Raise(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Generator parseGenerator(AGenerator node) {
		return new Node_Generator(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetBody()),
			getSource(node));
	}

	protected virtual Node_GenericFunction parseGenericFunction(AGenericfunction node) {
		return new Node_GenericFunction(
			parseMult1<AParameterinfo,Node_ParameterInfo>(parseParameterInfo, node.GetParameter()),
			parseOne<AFunction,Node_Function>(parseFunction, (AFunction)node.GetFunction()),
			getSource(node));
	}

	protected virtual Node_Breed parseBreed(ABreed node) {
		return new Node_Breed(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetParent()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetInterface()),
			getSource(node));
	}

	protected virtual Node_Nand parseNand(ANand node) {
		return new Node_Nand(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFirst()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSecond()),
			getSource(node));
	}

	protected virtual Node_Hidable parseHidable(AHidable node) {
		return new Node_Hidable(
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetHidden()),
			parseOne<PStatementdeclaration,INode_StatementDeclaration>(parseStatementDeclaration, (PStatementdeclaration)node.GetDeclaration()),
			getSource(node));
	}

	protected virtual Node_Call parseCall(ACall node) {
		return new Node_Call(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetReceiver()),
			parseMult0<AArgument,Node_Argument>(parseArgument, node.GetArgument()),
			getSource(node));
	}

	protected virtual Node_Nor parseNor(ANor node) {
		return new Node_Nor(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFirst()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSecond()),
			getSource(node));
	}

	protected virtual Node_Rational parseRational(TRational node) {
		return new Node_Rational(
			node.Text,
			getSource(node));
	}

	protected virtual Node_TypeCase parseTypeCase(ATypecase node) {
		return new Node_TypeCase(
			parseMult1<PExpression,INode_Expression>(parseExpression, node.GetTesttype()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetResult()),
			getSource(node));
	}

	protected virtual Node_Import parseImport(AImport node) {
		return new Node_Import(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetAlias()),
			parseMult1<AImportattempt,Node_ImportAttempt>(parseImportAttempt, node.GetImportattempt()),
			getSource(node));
	}

	protected virtual Node_Method parseMethod(AMethod node) {
		return new Node_Method(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetInterface()),
			getSource(node));
	}

	protected virtual Node_Function parseFunction(AFunction node) {
		return new Node_Function(
			parseMult0<AParameterimpl,Node_ParameterImpl>(parseParameterImpl, node.GetParameterimpl()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetReturntype()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetBody()),
			getSource(node));
	}

	protected virtual Node_Xnor parseXnor(AXnor node) {
		return new Node_Xnor(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFirst()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSecond()),
			getSource(node));
	}

	protected abstract Node_Direction parseDirection(PDirection node);

	protected virtual Node_Object parseObject(AObject node) {
		return new Node_Object(
			parseMult1<AWorker,Node_Worker>(parseWorker, node.GetWorker()),
			getSource(node));
	}

	protected virtual Node_Curry parseCurry(ACurry node) {
		return new Node_Curry(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFunction()),
			parseMult0<AArgument,Node_Argument>(parseArgument, node.GetArgument()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetCall()),
			getSource(node));
	}

	protected virtual Node_Dictionary parseDictionary(ADictionary node) {
		return new Node_Dictionary(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetKeytype()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValuetype()),
			parseMult0<ADictionaryentry,Node_DictionaryEntry>(parseDictionaryEntry, node.GetDictionaryentry()),
			getSource(node));
	}

	protected virtual Node_Callee parseCallee(ACallee node) {
		return new Node_Callee(
			parseMult0<AParameterinfo,Node_ParameterInfo>(parseParameterInfo, node.GetParameterinfo()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetReturntype()),
			getSource(node));
	}

	protected virtual Node_InstantiateGeneric parseInstantiateGeneric(AInstantiategeneric node) {
		return new Node_InstantiateGeneric(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetGeneric()),
			parseMult1<AArgument,Node_Argument>(parseArgument, node.GetArgument()),
			getSource(node));
	}

	protected virtual Node_DeclareEmpty parseDeclareEmpty(ADeclareempty node) {
		return new Node_DeclareEmpty(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			getSource(node));
	}

	protected virtual Node_Worker parseWorker(AWorker node) {
		return new Node_Worker(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFace()),
			parseMult0<AWorker,Node_Worker>(parseWorker, node.GetChildworker()),
			parseMult0<AMemberimplementation,Node_MemberImplementation>(parseMemberImplementation, node.GetMemberimplementation()),
			getSource(node));
	}

	protected virtual Node_FunctionInterface parseFunctionInterface(AFunctioninterface node) {
		return new Node_FunctionInterface(
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetTemplateargumentcount()),
			parseMult0<AParameterinfo,Node_ParameterInfo>(parseParameterInfo, node.GetParameterinfo()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetReturntype()),
			getSource(node));
	}

	protected virtual Node_Enum parseEnum(AEnum node) {
		return new Node_Enum(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseMult1<AEnumentry,Node_EnumEntry>(parseEnumEntry, node.GetEnumentry()),
			getSource(node));
	}

	protected virtual Node_DeclareAssign parseDeclareAssign(ADeclareassign node) {
		return new Node_DeclareAssign(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetConstant()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetBreed()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Compound parseCompound(ACompound node) {
		return new Node_Compound(
			parseMult0<PExpression,INode_Expression>(parseExpression, node.GetExpose()),
			parseMult0<PStatementdeclaration,INode_StatementDeclaration>(parseStatementDeclaration, node.GetDeclaration()),
			parseMult1<PExpression,INode_Expression>(parseExpression, node.GetMember()),
			getSource(node));
	}

	protected virtual Node_Interface parseInterface(AInterface node) {
		return new Node_Interface(
			parseMult0<PExpression,INode_Expression>(parseExpression, node.GetInheritee()),
			parseMult0<AStatusedmember,Node_StatusedMember>(parseStatusedMember, node.GetMember()),
			getSource(node));
	}

	protected virtual INode_StatementDeclaration parseStatementDeclaration(PStatementdeclaration node) {
		if( node is ADeclarefirstStatementdeclaration )
			return parseDeclareFirst( (ADeclarefirst)(node as ADeclarefirstStatementdeclaration).GetDeclarefirst() );
		if( node is ASieveStatementdeclaration )
			return parseSieve( (ASieve)(node as ASieveStatementdeclaration).GetSieve() );
		else
			throw new Exception("unknown node type: " + node);
	}

	protected virtual Node_SetProperty parseSetProperty(ASetproperty node) {
		return new Node_SetProperty(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSource()),
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetPropertyname()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Sieve parseSieve(ASieve node) {
		return new Node_Sieve(
			parseMult0<PExpression,INode_Expression>(parseExpression, node.GetExpose()),
			parseMult0<AHidable,Node_Hidable>(parseHidable, node.GetHidable()),
			getSource(node));
	}

	protected virtual Node_ExtractMember parseExtractMember(AExtractmember node) {
		return new Node_ExtractMember(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSource()),
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetMembername()),
			getSource(node));
	}

	protected virtual Node_TypeSelect parseTypeSelect(ATypeselect node) {
		return new Node_TypeSelect(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetInputvalue()),
			parseOpt<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetCastedname()),
			parseOpt<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetRequirematch()),
			parseMult0<ATypecase,Node_TypeCase>(parseTypeCase, node.GetTypecase()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetElse()),
			getSource(node));
	}

	protected virtual Node_Case parseCase(ACase node) {
		return new Node_Case(
			parseMult1<PExpression,INode_Expression>(parseExpression, node.GetTestvalue()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetResult()),
			getSource(node));
	}

	protected virtual Node_Catcher parseCatcher(ACatcher node) {
		return new Node_Catcher(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseOpt<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetTest()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetResult()),
			getSource(node));
	}

	protected virtual Node_TryCatch parseTryCatch(ATrycatch node) {
		return new Node_TryCatch(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetTry()),
			parseMult0<ACatcher,Node_Catcher>(parseCatcher, node.GetCatcher()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetOnsuccess()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFinally()),
			getSource(node));
	}

	protected virtual Node_ParameterInfo parseParameterInfo(AParameterinfo node) {
		return new Node_ParameterInfo(
			parseOne<PDirection,Node_Direction>(parseDirection, (PDirection)node.GetDirection()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetHasdefaultvalue()),
			getSource(node));
	}

	protected virtual Node_Caller parseCaller(ACaller node) {
		return new Node_Caller(
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetInterface()),
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetMethodname()),
			getSource(node));
	}

	protected virtual Node_Or parseOr(AOr node) {
		return new Node_Or(
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFirst()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetSecond()),
			getSource(node));
	}

	protected virtual Node_MemberImplementation parseMemberImplementation(AMemberimplementation node) {
		return new Node_MemberImplementation(
			parseOne<PMembertype,Node_MemberType>(parseMemberType, (PMembertype)node.GetMembertype()),
			parseOpt<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetInterface()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetFunction()),
			getSource(node));
	}

	protected virtual Node_StatusedMember parseStatusedMember(AStatusedmember node) {
		return new Node_StatusedMember(
			parseOne<PMemberstatus,Node_MemberStatus>(parseMemberStatus, (PMemberstatus)node.GetMemberstatus()),
			parseOne<PInterfacemember,INode_InterfaceMember>(parseInterfaceMember, (PInterfacemember)node.GetMember()),
			getSource(node));
	}

	protected virtual INode_Expression parseExpression(PExpression node) {
		if( node is ADeclareemptyExpression )
			return parseDeclareEmpty( (ADeclareempty)(node as ADeclareemptyExpression).GetDeclareempty() );
		if( node is ARaiseExpression )
			return parseRaise( (ARaise)(node as ARaiseExpression).GetRaise() );
		if( node is AAssignExpression )
			return parseAssign( (AAssign)(node as AAssignExpression).GetAssign() );
		if( node is ACallExpression )
			return parseCall( (ACall)(node as ACallExpression).GetCall() );
		if( node is ACompoundExpression )
			return parseCompound( (ACompound)(node as ACompoundExpression).GetCompound() );
		if( node is AConditionalExpression )
			return parseConditional( (AConditional)(node as AConditionalExpression).GetConditional() );
		if( node is ACurryExpression )
			return parseCurry( (ACurry)(node as ACurryExpression).GetCurry() );
		if( node is ADeclareassignExpression )
			return parseDeclareAssign( (ADeclareassign)(node as ADeclareassignExpression).GetDeclareassign() );
		if( node is AIdentifierExpression )
			return parseIdentifier( (node as AIdentifierExpression).GetIdentifier() );
		if( node is ASelectExpression )
			return parseSelect( (ASelect)(node as ASelectExpression).GetSelect() );
		if( node is ASetpropertyExpression )
			return parseSetProperty( (ASetproperty)(node as ASetpropertyExpression).GetSetproperty() );
		if( node is ATrycatchExpression )
			return parseTryCatch( (ATrycatch)(node as ATrycatchExpression).GetTrycatch() );
		if( node is ATypeselectExpression )
			return parseTypeSelect( (ATypeselect)(node as ATypeselectExpression).GetTypeselect() );
		if( node is AAndExpression )
			return parseAnd( (AAnd)(node as AAndExpression).GetAnd() );
		if( node is ANandExpression )
			return parseNand( (ANand)(node as ANandExpression).GetNand() );
		if( node is AOrExpression )
			return parseOr( (AOr)(node as AOrExpression).GetOr() );
		if( node is ANorExpression )
			return parseNor( (ANor)(node as ANorExpression).GetNor() );
		if( node is AXorExpression )
			return parseXor( (AXor)(node as AXorExpression).GetXor() );
		if( node is AXnorExpression )
			return parseXnor( (AXnor)(node as AXnorExpression).GetXnor() );
		if( node is ABreedExpression )
			return parseBreed( (ABreed)(node as ABreedExpression).GetBreed() );
		if( node is ACallerExpression )
			return parseCaller( (ACaller)(node as ACallerExpression).GetCaller() );
		if( node is AObjectExpression )
			return parseObject( (AObject)(node as AObjectExpression).GetObject() );
		if( node is ADictionaryExpression )
			return parseDictionary( (ADictionary)(node as ADictionaryExpression).GetDictionary() );
		if( node is AEnumExpression )
			return parseEnum( (AEnum)(node as AEnumExpression).GetEnum() );
		if( node is AExtractmemberExpression )
			return parseExtractMember( (AExtractmember)(node as AExtractmemberExpression).GetExtractmember() );
		if( node is AFunctionExpression )
			return parseFunction( (AFunction)(node as AFunctionExpression).GetFunction() );
		if( node is AFunctioninterfaceExpression )
			return parseFunctionInterface( (AFunctioninterface)(node as AFunctioninterfaceExpression).GetFunctioninterface() );
		if( node is AGeneratorExpression )
			return parseGenerator( (AGenerator)(node as AGeneratorExpression).GetGenerator() );
		if( node is AGenericfunctionExpression )
			return parseGenericFunction( (AGenericfunction)(node as AGenericfunctionExpression).GetGenericfunction() );
		if( node is AGenericinterfaceExpression )
			return parseGenericInterface( (AGenericinterface)(node as AGenericinterfaceExpression).GetGenericinterface() );
		if( node is AInstantiategenericExpression )
			return parseInstantiateGeneric( (AInstantiategeneric)(node as AInstantiategenericExpression).GetInstantiategeneric() );
		if( node is AIntegerExpression )
			return parseInteger( (node as AIntegerExpression).GetInteger() );
		if( node is AInterfaceExpression )
			return parseInterface( (AInterface)(node as AInterfaceExpression).GetInterface() );
		if( node is ARationalExpression )
			return parseRational( (node as ARationalExpression).GetRational() );
		if( node is AStringExpression )
			return parseString( (node as AStringExpression).GetString() );
		else
			throw new Exception("unknown node type: " + node);
	}

	protected virtual Node_EnumEntry parseEnumEntry(AEnumentry node) {
		return new Node_EnumEntry(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}

	protected virtual Node_Breeder parseBreeder(ABreeder node) {
		return new Node_Breeder(
			parseOpt<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetType()),
			getSource(node));
	}

	protected virtual Node_Identifier parseIdentifier(TIdentifier node) {
		return new Node_Identifier(
			node.Text,
			getSource(node));
	}

	protected virtual INode_InterfaceMember parseInterfaceMember(PInterfacemember node) {
		if( node is ABreederInterfacemember )
			return parseBreeder( (ABreeder)(node as ABreederInterfacemember).GetBreeder() );
		if( node is ACalleeInterfacemember )
			return parseCallee( (ACallee)(node as ACalleeInterfacemember).GetCallee() );
		if( node is APropertyInterfacemember )
			return parseProperty( (AProperty)(node as APropertyInterfacemember).GetProperty() );
		if( node is AMethodInterfacemember )
			return parseMethod( (AMethod)(node as AMethodInterfacemember).GetMethod() );
		else
			throw new Exception("unknown node type: " + node);
	}

	protected virtual Node_Assign parseAssign(AAssign node) {
		return new Node_Assign(
			parseOne<TIdentifier,Node_Identifier>(parseIdentifier, (TIdentifier)node.GetName()),
			parseOne<PBoolean,Node_Boolean>(parseBoolean, (PBoolean)node.GetBreed()),
			parseOne<PExpression,INode_Expression>(parseExpression, (PExpression)node.GetValue()),
			getSource(node));
	}
}

} //namespace

