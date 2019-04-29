using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class PrototypeParser : IParser<Prototype>
    {
        public Prototype Parse(TokenStream stream)
        {
            // Capture identifier.
            string identifier = stream.Next(TokenType.Identifier).Value;

            // Invoke the formal argument parser.
            FormalArgs args = new FormalArgsParser().Parse(stream);

            // Create the resulting prototype entity.
            Prototype prototype = new Prototype(identifier, args);

            // Return prototype.
            return prototype;
        }
    }
}
