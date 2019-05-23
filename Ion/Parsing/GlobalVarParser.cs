using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class GlobalVarParser : IParser<GlobalVar>
    {
        public GlobalVar Parse(ParserContext context)
        {
            System.Console.WriteLine($"Current: {context.Stream.Get()}");
            // Invoke type parser.
            Type type = new TypeParser().Parse(context);

            // Skip global variable prefix.
            context.Stream.Skip(TokenType.SymbolAt);

            // Consume name.
            string name = context.Stream.Next(TokenType.Identifier).Value;

            // Skip name.
            context.Stream.Skip();

            // Create the global variable.
            GlobalVar globalVar = new GlobalVar(type);

            // Assign name.
            globalVar.SetName(name);

            // Return the global variable.
            return globalVar;
        }
    }
}
