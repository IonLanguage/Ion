using LLVMSharp;

namespace LlvmSharpLang {
    // TODO: Constraint T to LLVM base type(s) (Value?).
    public interface IParser<T>
    {
        public abstract T Parse(TokenStream stream);
    }
}
