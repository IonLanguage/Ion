using LLVMSharp;

namespace Ion.Tracking.Symbols
{
    public class StructSymbol : NamedSymbol<LLVMTypeRef>
    {
        public StructSymbol(string name, LLVMTypeRef value) : base(name, value)
        {
            //
        }
    }
}
