using LLVMSharp;

namespace LlvmSharpLang
{
    public class FormalArg : NamedEntity<LLVMValueRef>
    {
        public LLVMTypeRef Type { get; set; }
    }
}
