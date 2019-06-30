using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Invoke path parser.
            PathResult path = new PathParser().Parse(context);

            // Variable reference.
            if (context.Stream.Current.Type != TokenType.SymbolParenthesesL)
            {
                // TODO: Should be done by an independent parser (VariableExprParser)?

                // Create and return the variable expression.
                return new Variable(path);
            }

            // Otherwise, it's a function call. Invoke the function call parser.
            Call callExpr = new CallExprParser(path).Parse(context);

            // Return the function call entity.
            return callExpr;
        }
    }
}
