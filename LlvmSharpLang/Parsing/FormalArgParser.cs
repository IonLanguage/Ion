using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;
using LlvmSharpLang.CognitiveServices;

namespace LlvmSharpLang.Parsing
{
    public class FormalArgParser : IParser<FormalArg>
    {
        public FormalArg Parse(TokenStream stream)
        {
            // Initialize the type value.
            string typeValue;

            // Check if the next token is a type
            if (TokenIdentifier.IsType(stream.Peek()))
            {
                // Capture argument type value.
                typeValue = stream.Next().Value;
            }
            else
            {
                // TODO: Better error lol.
                throw new System.Exception("Oops you need a type!");
            }

            // Create the arg's type.
            CodeGeneration.Type type = new CodeGeneration.Type(typeValue);

            // Create the formal argument entity.
            FormalArg arg = new FormalArg(type);

            // Capture the arg's name.
            string name = stream.Next(TokenType.Identifier).Value;

            // Assign the arg's name.
            arg.SetName(name);

            return arg;
        }
    }
}
