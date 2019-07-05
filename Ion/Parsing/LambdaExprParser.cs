using Ion.Generation;
using Ion.Misc;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class LambdaExprParser : IParser<Lambda>
    {
        public Lambda Parse(ParserContext context)
        {
            // Create a lambda expression.
            Lambda lambda = new Lambda();

            // Parse the formal arguments.
            FormalArgs args = new FormalArgsParser().Parse(context);

            // Assign the parsed arguments to the lambda.
            lambda.Arguments = args;

            // Create the type buffer, defaulting to void.
            Type type = PrimitiveTypeFactory.Void();

            // Return type is explicitly specified, parse and use it instead of the default.
            if (context.Stream.Current.Type == TokenType.SymbolColon)
            {
                // Ensure current type is symbol colon.
                context.Stream.EnsureCurrent(TokenType.SymbolColon);

                // Skip symbol colon token.
                context.Stream.Skip();

                // Parse the return type.
                type = new TypeParser().Parse(context);
            }

            // Assign the parsed type to the return type.
            lambda.ReturnType = type;

            // Ensure current token is symbol arrow.
            context.Stream.EnsureCurrent(TokenType.SymbolArrow);

            // Skip arrow symbol token.
            context.Stream.Skip();

            // Parse the body block.
            Block body = new BlockParser().Parse(context);

            // Assign the block to the lambda.
            lambda.Body = body;

            // Return the resulting expression.
            return lambda;
        }
    }
}
