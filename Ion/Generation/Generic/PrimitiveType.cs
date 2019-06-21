using System;
using Ion.CognitiveServices;

namespace Ion.Generation
{
    public class PrimitiveType : Construct
    {
        public bool IsVoid => this.TokenValue == TypeName.Void;

        public string TokenValue { get; }

        public override ConstructType ConstructType => ConstructType.PrimitiveType;

        public PrimitiveType(string tokenValue)
        {
            this.TokenValue = tokenValue;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
