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
            // Initialize the type value.
            string typeValue;

            // Check if the next token is a type.
            if (TokenIdentifier.IsType(stream.Peek()))
            {
                // Capture argument type value.
                typeValue = stream.Next().Value;
            }
            else
            {
                // TODO: Better error lol.
                throw new Exception("Oops you need a type!");
            }

            // Create the argument's type.
            Type type = new Type(typeValue);

            // Create the formal argument entity.
            FormalArg arg = new FormalArg(type);

            // Capture the argument's name.
            string name = stream.Next(TokenType.Identifier).Value;

            // Assign the argument's name.
            arg.SetName(name);

            return arg;
        }
    }
}
