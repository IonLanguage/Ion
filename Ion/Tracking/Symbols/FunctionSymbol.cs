using LLVMSharp;

namespace Ion.Tracking.Symbols
{
    public class FunctionSymbol : NamedSymbol<LLVMValueRef>
    {
        public int ArgumentCount { get; }

        public bool ContinuousArgs { get; }

        public FunctionSymbol(string identifier, LLVMValueRef value, bool continuousArgs = false) : base(identifier, value)
        {
            this.ArgumentCount = (int)LLVM.CountParams(value);
            this.ContinuousArgs = continuousArgs;
        }
    }
}
