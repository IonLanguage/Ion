using System.Collections.Generic;

namespace Ion.Generation
{
    public class StructExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.Struct;

        public string TargetIdentifier { get; }

        public List<StructProperty> Body { get; }

        public StructExpr(string targetIdentifier, List<StructProperty> body)
        {
            this.TargetIdentifier = targetIdentifier;
            this.Body = body;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitStruct(this);
        }
    }
}
