using System.Collections.Generic;
using LLVMSharp;

namespace Ion.Tracking.Symbols
{
    public class StructSymbol : NamedSymbol<LLVMTypeRef>
    {
        public Dictionary<string, LLVMTypeRef> Properties { get; }

        public StructSymbol(string name, LLVMTypeRef value, Dictionary<string, LLVMTypeRef> properties) : base(name, value)
        {
            this.Properties = properties;
        }
    }
}
