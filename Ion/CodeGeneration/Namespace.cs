using System;
using Ion.CodeGeneration.Structure;
using System.Collections.Generic;
using LLVMSharp;
using Ion.Misc;
using Ion.Parsing;

namespace Ion.CodeGeneration
{
    public class Namespace : IReaction<LLVMModuleRef>
    {
        public readonly PathResult path;

        public Namespace(PathResult path)
        {
            this.path = path;
        }

        public void Invoke(LLVMModuleRef context)
        {
            // Retrieve the corresponding path name.
            string name = this.path.ToString();

            // Set the module's name.
            LLVM.SetModuleIdentifier(context, name, name.Length);
        }
    }
}
