using System;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Struct : Named, ITopLevelPipe
    {
        public StructPrototype Prototype { get; }

        public Struct(string name, StructPrototype prototype)
        {
            this.SetName(name);
            this.Prototype = prototype;
        }

        public LLVMValueRef Emit(PipeContext<Module> context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
