#nullable enable

namespace Ion.Generation
{
    public enum BlockType
    {
        Default,

        Short
    }

    public class Block : Construct
    {
        public override ConstructType ConstructType => ConstructType.Block;

        public bool HasReturnExpr => this.ReturnConstruct != null;

        public Construct? ReturnConstruct { get; }

        public Construct[] Statements { get; }

        public string Identifier { get; }

        public Block(string identifier, Construct[] statements, Construct? returnConstruct = null)
        {
            this.Identifier = identifier;
            this.Statements = statements;
            this.ReturnConstruct = returnConstruct;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitBlock(this);
        }
    }
}
