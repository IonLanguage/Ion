using LlvmSharpLang.CodeGen;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
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
