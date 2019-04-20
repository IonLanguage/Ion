using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FormalArgParser : IParser<FormalArg>
    {
        public FormalArg Parse(TokenStream stream)
        {
            // Capture argument type value.
            string typeValue = stream.Next().Value;

            // Create the arg's type.
            CodeGeneration.Type type = new CodeGeneration.Type(typeValue);

            // Create the arg.
            FormalArg arg = new FormalArg(type);

            // Capture the arg's name.
            string name = stream.Next(SyntaxAnalysis.TokenType.Id).Value;

            // Assign the arg's name.
            arg.SetName(name);

            return arg;
        }
    }
}
