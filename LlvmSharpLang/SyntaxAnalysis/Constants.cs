using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using TokenTypeMap = System.Collections.Generic.Dictionary<string, LlvmSharpLang.SyntaxAnalysis.TokenType>;
using ComplexTokenTypeMap = System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, LlvmSharpLang.SyntaxAnalysis.TokenType>;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public static class Constants
    {
        public static string mainFunctionName = "main";

        public static readonly TokenTypeMap keywords = new TokenTypeMap {
            {"fn", TokenType.KeywordFunction},
            {"exit", TokenType.KeywordExit},
            {"return", TokenType.KeywordReturn}
        };

        public static readonly TokenTypeMap symbols = new TokenTypeMap {
            {"@", TokenType.SymbolAt},
            {"(", TokenType.SymbolBlockL},
            {")", TokenType.SymbolBlockR},
            {"{", TokenType.SymbolParenthesesL},
            {"}", TokenType.SymbolParenthesesR},
            {":", TokenType.SymbolColon},
            {";", TokenType.SymbolSemiColon},
            {"=>", TokenType.SymbolArrow}
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
            {"\\", TokenType.OperatorEscape}
        }.SortByKeyLength();

        public static readonly ComplexTokenTypeMap complexTokenTypes = new ComplexTokenTypeMap
        {
            {Util.CreateRegex(@"[_a-z]+[_a-z0-9]*"), TokenType.Id},
            {Util.CreateRegex(@"""(\\.|[^\""\\])*"""), TokenType.LiteralString},
            {Util.CreateRegex(@"[0-9]+\.[0-9]+"), TokenType.LiteralDecimal},
            {Util.CreateRegex(@"[0-9]+"), TokenType.LiteralInteger},
            {Util.CreateRegex(@"'([^'\\\n]|\\.)'"), TokenType.LiteralCharacter}
        };
    }
}
