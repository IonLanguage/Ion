using System.Collections.Generic;

namespace Ion.Generation
{
    public class FormalArgs : Construct
    {
        /// <summary>
        /// The list of arguments.
        /// </summary>
        public List<FormalArg> Values { get; set; }

        /// <summary>
        /// Whether the arguments are continous.
        /// </summary>
        public bool Continuous { get; set; }

        public override ConstructType ConstructType => ConstructType.FormalArguments;

        public FormalArgs()
        {
            this.Values = new List<FormalArg>();
            this.Continuous = false;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
