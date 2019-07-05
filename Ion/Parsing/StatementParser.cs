using Ion.CognitiveServices;
using Ion.Parsing;
using Ion.Syntax;

namespace Ion.Generation
{
    public class StatementParser : IParser<Construct>
    {
        public Construct Parse(ParserContext context)
        {
            // Capture the current token's type.
            Token token = context.Stream.Current;

            // Variable declaration expression.
            if (TokenIdentifier.IsType(token, context))
            {
                return new VarDeclareExprParser().Parse(context);
            }
            // If expression.
            else if (token.Type == TokenType.KeywordIf)
            {
                return new IfParser().Parse(context);
            }

            // Otherwise, delegate to the expression parser.
            Construct expr = new ExprParser().Parse(context);

            // Return the parsed expression.
            return expr;
        }
    }
}
