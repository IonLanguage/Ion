using System.Collections.Generic;
using Ion.Engine.Llvm;

namespace Ion.Tracking.Symbols
{
    public class StructSymbol : NamedSymbol<LlvmType>
    {
        public Dictionary<string, LlvmType> Properties { get; }

        public StructSymbol(string identifier, LlvmType value, Dictionary<string, LlvmType> properties) : base(identifier, value)
        {
            this.Properties = properties;
        }
    }
}
