using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using Type = Ion.CodeGeneration.Type;

namespace Ion.Parsing
{
    public class FormalArgParser : IParser<FormalArg>
    {
        public FormalArg Parse(TokenStream stream)
        {
            // Parse the type.
            Type type = new TypeParser().Parse(stream);

            // Create the formal argument entity.
            FormalArg arg = new FormalArg(type);

            // Capture the argument's name.
            string name = stream.Get(TokenType.Identifier).Value;

            // Skip the identifier token.
            stream.Skip();

            // Assign the argument's name.
            arg.SetName(name);

            // Return the argument.
            return arg;
        }
    }
}
