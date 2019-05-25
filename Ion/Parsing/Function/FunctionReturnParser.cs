using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class FunctionReturnParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Ensure current return keyword.
            context.Stream.EnsureCurrent(SyntaxAnalysis.TokenType.KeywordReturn);

            // Skip over the return keyword.
            context.Stream.Skip();

            // Capture the current token.
            Token token = context.Stream.Get();

            // There is no return expression.
            if (token.Type == SyntaxAnalysis.TokenType.SymbolSemiColon)
            {
                // Skip the semi-colon.
                context.Stream.Skip();

                // TODO: Should return void? Investigate.
                // Return null as no expression/return value was captured.
                return null;
            }

            // Otherwise, invoke the expression parser.
            Expr expr = new ExprParser().Parse(context);

            // Ensure current is a semi-colon.
            context.Stream.EnsureCurrent(SyntaxAnalysis.TokenType.SymbolSemiColon);

            // Skip the semi-colon.
            context.Stream.Skip();

            // Return the expression.
            return expr;
        }
    }
}
