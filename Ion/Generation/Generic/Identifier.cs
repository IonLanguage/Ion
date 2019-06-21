using Ion.Engine.Constants;
using Ion.IR.Constructs;

namespace Ion.Generation.Generic
{
    public class Identifier
    {
        public string Value { get; }

        public bool IsValid => Pattern.Identifier.IsMatch(this.Value);

        public Identifier(string value)
        {
            this.Value = value;
        }

        public Reference AsIrReference()
        {
            return new Reference(this.Value);
        }

        public string AsIrReferenceString()
        {
            return this.AsIrReference().Emit();
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
