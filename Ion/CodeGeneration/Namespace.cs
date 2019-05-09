using System;
using Ion.CodeGeneration.Structure;
using System.Collections.Generic;
using LLVMSharp;
using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class Namespace : Named, IReaction<LLVMModuleRef>
    {
        public readonly List<string> path;

        public Namespace(List<string> path)
        {
            this.path = path;
        }

        public void React(LLVMModuleRef context)
        {
            // Ensure name is set.
            this.EnsureNameOrThrow();

            // Set the module's name.
            LLVM.SetModuleIdentifier(context, this.Name, this.Name.Length);
        }
    }
}
