using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using TokenTypeMap = System.Collections.Generic.Dictionary<string, LlvmSharpLang.SyntaxAnalysis.TokenType>;
using ComplexTokenTypeMap = System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, LlvmSharpLang.SyntaxAnalysis.TokenType>;
using LlvmSharpLang.Misc;
using LlvmSharpLang.CodeGeneration;
using LLVMSharp;
using System.Linq;
using LlvmSharpLang.ErrorReporting;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public static class Constants
    {
        public delegate LLVMValueRef ConstantResolver();

        public static readonly TokenTypeMap keywords = new TokenTypeMap {
            {"fn", TokenType.KeywordFunction},
            {"exit", TokenType.KeywordExit},
            {"return", TokenType.KeywordReturn},
            {"if", TokenType.KeywordIf},
            {"extern", TokenType.KeywordExternal}
        };

        public static readonly TokenTypeMap symbols = new TokenTypeMap {
            {"@", TokenType.SymbolAt},
            {"(", TokenType.SymbolParenthesesL},
            {")", TokenType.SymbolParenthesesR},
            {"{", TokenType.SymbolBlockL},
            {"}", TokenType.SymbolBlockR},
            {":", TokenType.SymbolColon},
            {";", TokenType.SymbolSemiColon},
            {"=>", TokenType.SymbolArrow},
            {"..", TokenType.SymbolContinuous},
            {",", TokenType.SymbolComma}
        }.SortByKeyLength();

        public static readonly TokenTypeMap operators = new TokenTypeMap {
            {"==", TokenType.OperatorEquality},
            {"+", TokenType.OperatorAddition},
            {"-", TokenType.OperatorSubstraction},
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

        /// <summary>
        /// A combination of the simple token type maps
        /// which include operators, symbols and keywords.
        /// </summary>
        public static readonly TokenTypeMap simpleTokenTypes = new TokenTypeMap[]
        {
            Constants.keywords,
            Constants.symbols,
            Constants.operators
        }
        .SelectMany((dictionary) => dictionary)
        .ToLookup((pair) => pair.Key, (pair) => pair.Value)
        .ToDictionary((group) => group.Key, (group) => group.First())
        .SortByKeyLength();

        public static readonly ComplexTokenTypeMap complexTokenTypes = new ComplexTokenTypeMap
        {
            {Util.CreateRegex(@"[_a-z]+[_a-z0-9]*"), TokenType.Identifier},
            {Util.CreateRegex(@"""(\\.|[^\""\\])*"""), TokenType.LiteralString},
            {Util.CreateRegex(@"-?[0-9]+\.[0-9]+"), TokenType.LiteralDecimal},
            {Util.CreateRegex(@"-?[0-9]+"), TokenType.LiteralInteger},
            {Util.CreateRegex(@"'([^'\\\n]|\\.)'"), TokenType.LiteralCharacter}
        };

        public static readonly ComplexTokenTypeMap commentTokenTypes = new ComplexTokenTypeMap {
            {Util.CreateRegex(@"//[^\r\n]+"), TokenType.SingleLineComment},
            {Util.CreateRegex(@"\/\*[^*/]+\*\/"), TokenType.MultiLineComment}
        };

        public static readonly Dictionary<TokenType, int> operatorPrecedence = new Dictionary<TokenType, int>
        {
            {TokenType.OperatorLessThan, 10},
            {TokenType.OperatorAddition, 20},
            {TokenType.OperatorSubstraction, 20},
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
    }
}
