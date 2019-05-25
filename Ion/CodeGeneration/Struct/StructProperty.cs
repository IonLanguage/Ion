using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class StructProperty : Named
    {
        public Expr Value { get; }

        public StructProperty(string identifier, Expr value)
        {
            this.SetName(identifier);
            this.Value = value;
        }
    }
}
