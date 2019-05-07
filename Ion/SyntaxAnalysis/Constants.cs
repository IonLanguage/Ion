using System.Collections.Generic;
using System.Linq;
using Ion.CognitiveServices;
using Ion.ErrorReporting;
using Ion.Misc;
using LLVMSharp;
using TokenTypeMap = System.Collections.Generic.Dictionary<string, Ion.SyntaxAnalysis.TokenType>;
using ComplexTokenTypeMap =
    System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, Ion.SyntaxAnalysis.TokenType>;

namespace Ion.SyntaxAnalysis
{
    public static class Constants
    {
        public delegate LLVMValueRef ConstantResolver();

        public delegate LLVMValueRef SimpleMathBuilderInvoker(LLVMBuilderRef builder, LLVMValueRef leftSide, LLVMValueRef rightSide, string name);

        public static readonly TokenTypeMap keywords = new TokenTypeMap
        {
            {"exit", TokenType.KeywordExit},
            {"return", TokenType.KeywordReturn},
            {"if", TokenType.KeywordIf},
            {"extern", TokenType.KeywordExternal},
            {"true", TokenType.KeywordTrue},
            {"false", TokenType.KeywordFalse}
        };

        public static readonly TokenTypeMap symbols = new TokenTypeMap
        {
            {"@", TokenType.SymbolAt},
            {"(", TokenType.SymbolParenthesesL},
            {")", TokenType.SymbolParenthesesR},
            {"{", TokenType.SymbolBlockL},
            {"}", TokenType.SymbolBlockR},
            {":", TokenType.SymbolColon},
            {";", TokenType.SymbolSemiColon},
            {"=>", TokenType.SymbolArrow},
            {"...", TokenType.SymbolContinuous},
            {",", TokenType.SymbolComma}
        }.SortByKeyLength();

        public static readonly TokenTypeMap operators = new TokenTypeMap
        {
            {"==", TokenType.OperatorEquality},
            {"+", TokenType.OperatorAddition},
            {"-", TokenType.OperatorSubtraction},
            {"*", TokenType.OperatorMultiplication},
            {"/", TokenType.OperatorDivision},
            {"%", TokenType.OperatorModulo},
            {"^", TokenType.OperatorExponent},
            {"=", TokenType.OperatorAssignment},
            {"|", TokenType.OperatorPipe},
            {"&", TokenType.OperatorAddressOf},
            {"\\", TokenType.OperatorEscape},
            {"<", TokenType.OperatorLessThan},
            {">", TokenType.OperatorGreaterThan},
            {"and", TokenType.OperatorAnd},
            {"or", TokenType.OperatorOr},
            {"!", TokenType.OperatorNot}
        }.SortByKeyLength();

        public static readonly TokenTypeMap types = new TokenTypeMap
        {
            {TypeName.Double, TokenType.TypeDouble},
            {TypeName.Int32, TokenType.TypeInt},
            {TypeName.Int64, TokenType.TypeLong},
            {TypeName.Boolean, TokenType.TypeBool},
            {TypeName.Void, TokenType.TypeVoid},
            {TypeName.Float, TokenType.TypeFloat},
            {TypeName.Character, TokenType.TypeChar},
            {TypeName.String, TokenType.TypeString}
        };

        /// <summary>
        /// A combination of the simple token type maps
        /// which include operators, symbols and keywords.
        /// </summary>
        public static readonly TokenTypeMap simpleTokenTypes = new[]
            {
                keywords,
                symbols,
                operators,
                types
            }
            .SelectMany(dictionary => dictionary)
            .ToLookup(pair => pair.Key, pair => pair.Value)
            .ToDictionary(group => group.Key, group => group.First())
            .SortByKeyLength();

        public static readonly ComplexTokenTypeMap complexTokenTypes = new ComplexTokenTypeMap
        {
            {Pattern.Identifier, TokenType.Identifier},
            {Pattern.String, TokenType.LiteralString},
            {Pattern.Decimal, TokenType.LiteralDecimal},
            {Pattern.Integer, TokenType.LiteralInteger},
            {Pattern.Character, TokenType.LiteralCharacter}
        };

        public static readonly ComplexTokenTypeMap commentTokenTypes = new ComplexTokenTypeMap
        {
            {Util.CreateRegex(@"//[^\r\n]+"), TokenType.SingleLineComment},
            {Util.CreateRegex(@"\/\*[^*/]+\*\/"), TokenType.MultiLineComment}
        };

        public static readonly Dictionary<TokenType, int> operatorPrecedence = new Dictionary<TokenType, int>
        {
            {TokenType.OperatorLessThan, 10},
            {TokenType.OperatorAddition, 20},
            {TokenType.OperatorSubtraction, 20},
            {TokenType.OperatorMultiplication, 40},
            {TokenType.OperatorDivision, 40},
            {TokenType.OperatorModulo, 40},
            {TokenType.OperatorExponent, 80}
        };

        public static Dictionary<ErrorType, string> errorTypeStrings = new Dictionary<ErrorType, string>
        {
            {ErrorType.Error, "Error"},
            {ErrorType.Fatal, "Fatal"},
            {ErrorType.Warning, "Warning"}
        };

        public static Dictionary<TokenType, SimpleMathBuilderInvoker> operatorBuilderMap = new Dictionary<TokenType, SimpleMathBuilderInvoker>
        {
            {TokenType.OperatorAddition, LLVM.BuildAdd},
            {TokenType.OperatorSubtraction, LLVM.BuildSub},
            {TokenType.OperatorMultiplication, LLVM.BuildMul},
            {TokenType.OperatorDivision, LLVM.BuildUDiv},
            {TokenType.OperatorModulo, LLVM.BuildURem}
        };
    }
}
