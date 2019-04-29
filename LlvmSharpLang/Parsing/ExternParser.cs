using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ExternParser : IParser<Prototype>
    {
        public Prototype Parse(TokenStream stream)
        {
            // Consume extern keyword.
            stream.Skip(TokenType.KeywordExternal);

            // Invoke and return the prototype parser.
            return new PrototypeParser().Parse(stream);
        }
    }
}
