using Ion.CognitiveServices;
using Ion.Parsing;
using Ion.SyntaxAnalysis;

namespace Ion.CodeGeneration
{
    public class StatementParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            TokenType currentTokenType = context.Stream.Get().Type;

            // Variable declaration expression.
            if (TokenIdentifier.IsType(currentTokenType))
            {
                return new VarDeclareExprParser().Parse(context);
            }

            // Otherwise, delegate to the primary expression parser.
            Expr expr = new PrimaryExprParser().Parse(context);

            // Return the parsed expression.
            return expr;
        }
    }
}