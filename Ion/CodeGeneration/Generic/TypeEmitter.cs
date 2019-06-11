using Ion.Engine.CodeGeneration.Helpers;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public interface ITypeEmitter : IOneWayPipe<LLVMTypeRef>
    {
        bool IsVoid { get; }
    }
}
