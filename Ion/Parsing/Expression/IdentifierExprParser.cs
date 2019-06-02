using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Invoke path parser.
            PathResult path = new PathParser().Parse(context);

            // Variable reference.
            if (context.Stream.Current.Type != SyntaxAnalysis.TokenType.SymbolParenthesesL)
            {
                // TODO: Should be done by an independent parser (VariableExprParser)?

                // Create and return the variable expression.
                return new VariableExpr(path);
            }

            // Otherwise, it's a function call. Invoke the function call parser.
            CallExpr callExpr = new CallExprParser(path).Parse(context);

            // Return the function call entity.
            return callExpr;
        }
    }
}
