using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class GlobalVarParser : IParser<GlobalVar>
    {
        public GlobalVar Parse(TokenStream stream)
        {
            // Consume type.
            var typeValue = stream.Next().Value;

            // Create type.
            var type = new Type(typeValue);

            // Skip global variable prefix.
            stream.Skip(TokenType.SymbolAt);

            // Consume name.
            var name = stream.Next(TokenType.Identifier).Value;

            // Create the global variable.
            var globalVar = new GlobalVar(type);

            // Assign name.
            globalVar.SetName(name);

            return globalVar;
        }
    }
}