namespace Ion.Generation
{
    public class Pipe : Construct
    {
        public override ConstructType ConstructType => ConstructType.Pipe;

        public Construct[] Arguments { get; }

        /// <summary>
        /// The name of the target method.
        /// </summary>
        public string TargetName { get; }

        public Pipe(Construct[] arguments, string targetName)
        {
            this.Arguments = arguments;
            this.TargetName = targetName;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitPipe(this);
        }
    }
}
