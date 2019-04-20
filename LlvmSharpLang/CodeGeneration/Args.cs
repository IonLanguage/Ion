using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
{
    public class Args : IUncontextedEntity<LLVMTypeRef[]>
    {
        public Type Type { get; set; }

        public List<Arg> Values { get; set; }

        public bool Continuous { get; set; }

        public Args()
        {
            this.Values = new List<Arg>();
            this.Continuous = false;
        }

        public LLVMTypeRef[] Emit()
        {
            List<LLVMTypeRef> args = new List<LLVMTypeRef>();

            // Emit all arguments.
            foreach (var arg in this.Values)
            {
                args.Add(arg.Emit());
            }

            return args.ToArray();
        }
    }
}
