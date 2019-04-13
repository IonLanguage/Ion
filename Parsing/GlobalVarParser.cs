using LlvmSharpLang.CodeGen;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class GlobalVarParser : IParser<GlobalVar>
    {
        public GlobalVar Parse(TokenStream stream)
        {
            // Consume type.
            string typeValue = stream.Next().Value;

            // Create type.
            Type type = new Type(typeValue);

            // Skip global variable prefix.
            stream.Skip(TokenType.SymbolAt);

            // Consume name.
            string name = stream.Next(TokenType.Id).Value;

            // Create the global variable.
            GlobalVar globalVar = new GlobalVar(type);

            // Assign name.
            globalVar.SetName(name);

            return globalVar;
        }
    }
}
