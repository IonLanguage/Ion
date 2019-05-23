using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class Prototype : Named
    {
        public FormalArgs Args { get; set; }

        public ITypeEmitter ReturnType { get; set; }

        public Prototype(string name, FormalArgs args, ITypeEmitter returnType)
        {
            this.SetName(name);
            this.Args = args;
            this.ReturnType = returnType;
        }
    }
}
