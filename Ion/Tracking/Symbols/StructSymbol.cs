using System.Collections.Generic;
using LLVMSharp;

namespace Ion.Tracking.Symbols
{
    public class StructSymbol : NamedSymbol<LLVMTypeRef>
    {
        public Dictionary<string, LLVMTypeRef> Properties { get; }

        public StructSymbol(string identifier, LLVMTypeRef value, Dictionary<string, LLVMTypeRef> properties) : base(identifier, value)
        {
            this.Properties = properties;
        }
    }
}
