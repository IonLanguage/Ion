using Ion.Engine.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class StructDefProperty : Named, IOneWayPipe<LLVMTypeRef>
    {
        public Type Type { get; }

        public StructDefProperty(Type type, string name)
        {
            this.Type = type;
            this.SetName(name);
        }

        public LLVMTypeRef Emit()
        {
            // TODO: Should register property along with its name on the symbol table somehow (name not being used).
            // Emit and return the associated type.
            return this.Type.Emit();
        }
    }
}
