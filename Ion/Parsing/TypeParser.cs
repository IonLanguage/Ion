using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class TypeParser : IParser<Type>
    {
        public Type Parse(TokenStream stream)
        {
            // Consume type value.
            string value = stream.Next().Value;

            // Create the type.
            return new Type(value);
        }
    }
}
