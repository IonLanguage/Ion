using Ion.CodeGeneration.Structure;
using System.Collections.Generic;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Namespace : IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public readonly List<string> path;

        public Namespace(List<string> path)
        {
            this.path = path;
        }

        public void Emit(LLVMModuleRef context)
        {
            // TODO
            LLVM.
        }
    }
}
