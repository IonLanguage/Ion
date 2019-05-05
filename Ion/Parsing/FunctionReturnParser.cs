using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class FunctionReturnParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Ensure current return keyword.
            stream.EnsureCurrent(TokenType.KeywordReturn);

            // Skip over the return keyword.
            stream.Skip();

            // Capture the current token.
            Token token = stream.Get();

            // There is no return expression.
            if (token.Type == TokenType.SymbolSemiColon)
            {
                // Skip the semi-colon.
                stream.Skip();

                // TODO: Should return void? Investigate.
                // Return null as no expression/return value was captured.
                return null;
            }

            // Otherwise, invoke the expression parser.
            Expr expr = new ExprParser().Parse(stream);

            // Ensure current is a semi-colon.
            stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip the semi-colon.
            stream.Skip();

            // Return the expression.
            return expr;
        }
    }
}
