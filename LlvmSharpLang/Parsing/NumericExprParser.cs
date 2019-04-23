using System;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class NumericExprParser : IParser<NumericExpr>
    {
        public NumericExpr Parse(TokenStream stream)
        {
            // Consume numeric literal token.
            Token token = stream.Next();

            // Ensure captured token is numeric.
            if (!TokenIdentifier.IsNumeric(token))
            {
                throw new Exception($"Expected token to be classified as numeric, but got '{token.Type}'");
            }

            // Create the numeric expression entity.
            NumericExpr numericExpr = new NumericExpr(token.Type, Resolvers.TypeFromToken(token), token.Value);

            // Return the numeric expression entity.
            return numericExpr;
        }
    }
}
