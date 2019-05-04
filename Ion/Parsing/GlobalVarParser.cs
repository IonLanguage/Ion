using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class GlobalVarParser : IParser<GlobalVar>
    {
        public GlobalVar Parse(TokenStream stream)
        {
            // Consume type.
            string typeValue = stream.Get().Value;

            // Create type.
            Type type = new Type(typeValue);

            // Skip global variable prefix.
            stream.Skip(TokenType.SymbolAt);

            // Consume name.
            string name = stream.Next(TokenType.Identifier).Value;

            // Skip name.
            stream.Skip();

            // Create the global variable.
            GlobalVar globalVar = new GlobalVar(type);

            // Assign name.
            globalVar.SetName(name);

            // Return the global variable.
            return globalVar;
        }
    }
}
