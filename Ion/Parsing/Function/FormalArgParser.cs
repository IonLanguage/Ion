using Ion.Syntax;
using Type = Ion.Generation.Type;

namespace Ion.Parsing
{
    public class FormalArgParser : IParser<(string, Type)>
    {
        public (string, Type) Parse(ParserContext context)
        {
            // Parse the type.
            Type type = new TypeParser().Parse(context);

            // Capture the argument's name.
            string name = context.Stream.Get(TokenType.Identifier).Value;

            // Create the formal argument entity.
            (string, Type) arg = (name, type);

            // Skip the identifier token.
            context.Stream.Skip();

            // Return the argument.
            return arg;
        }
    }
}
