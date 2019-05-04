using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Parse the left side of the expression.
            Expr leftSide = new PrimaryExprParser().Parse(stream);

            // Ensure left side was successfully parsed, otherwise return null.
            if (leftSide == null)
            {
                return null;
            }

            // Invoke the binary expression parser.
            Expr expr = new BinaryOpRightSideParser(leftSide, 0).Parse(stream);

            // Return the parsed expression.
            return expr;
        }
    }
}
