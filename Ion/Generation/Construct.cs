namespace Ion.Generation
{
    public abstract class Construct : ICodeGenVisitable
    {
        public abstract ConstructType ConstructType { get; }

        public abstract Construct Accept(CodeGenVisitor visitor);
    }
}
