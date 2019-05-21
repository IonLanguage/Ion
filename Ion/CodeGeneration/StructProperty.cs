using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class StructProperty : Named
    {
        public Type Type { get; }

        public StructProperty(Type type, string name)
        {
            this.Type = type;
            this.SetName(name);
        }
    }
}
