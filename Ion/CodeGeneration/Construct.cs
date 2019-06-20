namespace Ion.CodeGeneration
{
    public abstract class Construct : ICodeGenVisitable
    {
        public abstract ConstructType ConstructType { get; }

        public abstract Construct Accept(CodeGenVisitor visitor);
    }
}
