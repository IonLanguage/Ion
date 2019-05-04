using System.Collections.Generic;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FormalArgs : IUncontextedEntity<LLVMTypeRef[]>
    {
        public FormalArgs()
        {
            this.Values = new List<FormalArg>();
            this.Continuous = false;
        }

        public Type Type { get; set; }

        public List<FormalArg> Values { get; set; }

        public bool Continuous { get; set; }

        public LLVMTypeRef[] Emit()
        {
            var args = new List<LLVMTypeRef>();

            // Emit all arguments.
            foreach (FormalArg arg in this.Values) args.Add(arg.Emit());

            return args.ToArray();
        }
    }
}