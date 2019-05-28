using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class StructProperty : Named
    {
        public Expr Value { get; }

        public int Index { get; }

        public StructProperty(string identifier, Expr value, int index)
        {
            this.SetName(identifier);
            this.Value = value;
            this.Index = index;
        }
    }
}
