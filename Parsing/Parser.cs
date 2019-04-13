using LLVMSharp;

namespace LlvmSharpLang {
    // TODO: Constraint T to LLVM base type(s) (Value?).
    public interface IParser<T>
    {
        T Parse(TokenStream stream);
    }
}
