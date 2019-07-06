using Ion.Engine.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.Generation
{
    public class StructDefProperty : Named, IOneWayPipe<LLVMTypeRef>
    {
        public Type Type { get; }

        public StructDefProperty(Type type, string name)
        {
            this.Type = type;
            this.SetName(name);
        }
    }
}
