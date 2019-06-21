using Ion.Engine.CodeGeneration.Helpers;
using LLVMSharp;

namespace Ion.Generation
{
    public interface ITypeEmitter : IOneWayPipe<LLVMTypeRef>
    {
        bool IsVoid { get; }
    }
}
