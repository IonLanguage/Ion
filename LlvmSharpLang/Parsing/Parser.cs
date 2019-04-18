using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public interface IParser<T>
    {
        T Parse(TokenStream stream);
    }
}
