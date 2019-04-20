using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using TokenTypeMap = System.Collections.Generic.Dictionary<string, LlvmSharpLang.SyntaxAnalysis.TokenType>;
using ComplexTokenTypeMap = System.Collections.Generic.Dictionary<System.Text.RegularExpressions.Regex, LlvmSharpLang.SyntaxAnalysis.TokenType>;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.SyntaxAnalysis
{

    public static class TypeMapExtensions
    {

        public static bool Some(this TokenTypeMap map, Func<string, bool> func)
        {
            foreach (string str in map.Keys)
            {
                if (func(str))
                {
                    return true;
                }
            }

            return false;

        }

        public static TokenTypeMap SortByKeyLength(this TokenTypeMap map)
        {
            var keys = new string[map.Count];
            map.Keys.CopyTo(keys, 0);
            List<String> keyList = new List<string>(keys);
            keyList.Sort((a, b) =>
            {
                if (a.Length > b.Length) return -1;
                else if (b.Length > a.Length) return 1;
                else return 0;
            });
            TokenTypeMap @new = new Dictionary<string, TokenType>();
            foreach (var item in keyList)
            {
                @new[item] = map[item];
            }

            return @new;
        }

    }

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
