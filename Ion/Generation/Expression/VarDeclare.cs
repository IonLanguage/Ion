namespace Ion.Generation
{
    public class VarDeclare : Construct
    {
        public override ConstructType ConstructType => ConstructType.VariableDeclaration;

        public Type ValueType { get; protected set; }

        public Value Value { get; set; }

        public VarDeclare(Type valueType, Value value)
        {
            this.ValueType = valueType;
            this.Value = value;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitVarDeclare(this);
        }
    }
}
