using LLVMSharp;

namespace LlvmSharpLang
{
    public class FormalArgParser : IParser<LLVMValueRef[]>
    {
        public LLVMValueRef[] Parse(TokenStream stream)
        {
            List<LLVMValueRef> args = new List<LLVMValueRef>();

            

            return args.ToArray();
        }
    }
}
