using System.Collections.Generic;
using Ion.Generation.Helpers;
using LLVMSharp;

namespace Ion.Generation
{
    public class Pipe : Construct
    {
        public override ConstructType ConstructType => ConstructType.Pipe;

        public Expr[] Arguments { get; }

        /// <summary>
        /// The name of the target method.
        /// </summary>
        public string TargetName { get; }

        public Pipe(Expr[] arguments, string targetName)
        {
            this.Arguments = arguments;
            this.TargetName = targetName;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
