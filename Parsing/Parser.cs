using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing {
    // TODO: Constraint T to LLVM base type(s) (Value?).
    public interface IParser<T>
    {
        T Parse(TokenStream stream);
    }
}
