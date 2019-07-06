namespace Ion.Generation
{
    public class VarDeclare : Construct
    {
        public override ConstructType ConstructType => ConstructType.VariableDeclaration;

        public string Identifier { get; }

        public Type Type { get; }

        public Value Value { get; }

        public VarDeclare(string identifier, Type type, Value value)
        {
            this.Type = type;
            this.Value = value;
            this.Identifier = identifier;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitVarDeclare(this);
        }
    }
}
