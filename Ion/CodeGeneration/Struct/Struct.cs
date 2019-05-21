using System;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Struct : Named, IPipe<Module, LLVMTypeRef>
    {
        public StructPrototype Prototype { get; }

        public Struct(string name, StructPrototype prototype)
        {
            this.SetName(name);
            this.Prototype = prototype;
        }

        public LLVMTypeRef Emit(PipeContext<Module> context)
        {
            // TODO: Finish implementing.
            LLVMTypeRef @struct = LLVM.StructCreateNamed(LLVM.GetGlobalContext(), this.Name);

            LLVM.StructSetBody(@struct, new LLVMTypeRef[] { }, false);

            return @struct;
        }
    }
}
