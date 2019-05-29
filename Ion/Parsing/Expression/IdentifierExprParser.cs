using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // TODO: PathResult should either by passed into VariableExprParser and FunctionCallExprParser, or
            // TODO: the seperate parsers should do it themeselves, which then forces THIS parser to
            // TODO: figure out if it is a VariableExpr or a FunctionCallExpr without first
            // TODO: skipping over the subject.

            // Invoke path parser.
            PathResult path = new PathParser().Parse(context);

            // Shift the stream back to account for the path parser skipping the final identifier.
            context.Stream.Back();

            // Variable reference.
            if (context.Stream.Peek().Type != SyntaxAnalysis.TokenType.SymbolParenthesesL)
            {
                // TODO: Should be done by an independent parser (VariableExprParser)?
                // Skip identifier.
                context.Stream.Skip();

                // Create and return the variable expression.
                return new VariableExpr(path);
            }

            // Otherwise, it's a function call. Invoke the function call parser.
            FunctionCallExpr functionCallExpr = new FunctionCallExprParser().Parse(context);

            // Return the function call entity.
            return functionCallExpr;
        }
    }
}
