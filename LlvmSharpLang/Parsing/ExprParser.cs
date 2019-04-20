using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
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

            // Gather contextual info from upcoming token.
            Token nextToken = stream.Peek();

            // Attempt to identify the value.
            TokenType? identifiedType = TokenIdentifier.Identify(valueToken.Value);

            // The value's token type was successfully identified.
            if (identifiedType.HasValue)
            {
                switch (identifiedType.Value)
                {
                    // Possible function call or operation.
                    case TokenType.Id:
                        {
                            // Function call.
                            if (nextToken.Type == TokenType.SymbolParenthesesL)
                            {
                                expr.Type = ExprType.FunctionCall;
                                expr.FunctionCallTarget = valueToken.Value;
                            }
                            // Operation.
                            else if (TokenIdentifier.IsOperator(nextToken.Value))
                            {
                                // TODO
                            }

                            break;
                        }

                    default:
                        {
                            throw new Exception("Unexpected token; Expecting function call or operation");
                        }
                }
            }
            else
            {
                throw new Exception("Unable to identify token type from provided value");
            }

            // TODO: Review because of changes above.
            // Expression is singular.
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
