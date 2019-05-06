using System.Collections.Generic;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FormalArgs : IUncontextedEntity<LLVMTypeRef[]>
    {
        /// <summary>
        /// The list of arguments.
        /// </summary>
        public List<FormalArg> Values { get; set; }

        /// <summary>
        /// Whether the arguments are continous.
        /// </summary>
        public bool Continuous { get; set; }

        public FormalArgs()
        {
            this.Values = new List<FormalArg>();
            this.Continuous = false;
        }

        public LLVMTypeRef[] Emit()
        {
            // Create the resulting argument list.
            List<LLVMTypeRef> args = new List<LLVMTypeRef>();

            // Emit all arguments.
            foreach (FormalArg arg in this.Values)
            {
                args.Add(arg.Emit());
            }

            return args.ToArray();
        }
    }
}
