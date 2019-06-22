using System.Collections.Generic;
using Ion.IR.Constructs;

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

        public readonly List<Expr> Expressions;

        public Expr ReturnExpr { get; set; }

        public bool HasReturnExpr => this.ReturnExpr != null;

        public BlockType Type { get; set; }

        // TODO: Find a better way to cache emitted values.
        public Section Current { get; protected set; }

        public Block()
        {
            this.Expressions = new List<Expr>();
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
