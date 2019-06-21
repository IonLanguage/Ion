using System.Collections.Generic;
using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class StructExprParser : IParser<StructExpr>
    {
        public StructExpr Parse(ParserContext context)
        {
            // Ensure current token is keyword new.
            context.Stream.EnsureCurrent(TokenType.KeywordNew);

            // Skip new keyword token.
            context.Stream.Skip();

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Invoke struct body parser.
            List<StructProperty> body = new StructBodyParser().Parse(context);

            // Create the resulting struct.
            StructExpr @struct = new StructExpr(identifier, body);

            // Return the resulting struct.
            return @struct;
        }
    }
}
