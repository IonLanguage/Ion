using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class GlobalVarParser : IParser<GlobalVar>
    {
        public GlobalVar Parse(ParserContext context)
        {
            // Consume type.
            string typeValue = context.Stream.Get().Value;

            // Create type.
            Type type = new Type(typeValue);

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
