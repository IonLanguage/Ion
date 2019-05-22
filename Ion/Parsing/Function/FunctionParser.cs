using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class FunctionParser : IParser<Function>
    {
        public Function Parse(ParserContext context)
        {
            // Parse the prototype from the stream, this captures the name, arguments and return type.
            Prototype prototype = new PrototypeParser().Parse(context);

            // Create the function.
            Function function = new Function();

            // Assign the function prototype to the parsed prototype.
            function.Prototype = prototype;

            // Parse the body.
            Block body = new BlockParser().Parse(context);

            // Set the name of the body block.
            body.SetNameEntry();

            // Assign the body.
            function.Body = body;

            // Return the function.
            return function;
        }
    }
}