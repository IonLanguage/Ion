using System;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Create the expression.
            Expr expr = new Expr();

            // Capture explicit value token.
            Token valueToken = stream.Next();
            LLVMValueRef? value = null;

            // Attempt to identify the value.
            TokenType? identifiedType = TokenIdentifier.Identify(valueToken.Value);

            // The value's token type was successfully identified.
            if (identifiedType.HasValue)
            {
                // TODO
            }
            else
            {
                throw new Exception("Unable to identify token type from provided value");
            }

            // Gather contextual info from upcoming token.
            Token nextToken = stream.Peek();

            // Expression is simply a single literal.
            if (nextToken.Type == SyntaxAnalysis.TokenType.SymbolSemiColon)
            {
                expr.ExplicitValue = value;

                return expr;
            }

            // TODO: Add support for complex expressions.

            return expr;
        }
    }
}
