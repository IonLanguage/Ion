using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class Prototype : Named
    {
        public FormalArgs Args { get; set; }

        public ITypeEmitter ReturnType { get; }

        public bool ReturnsVoid { get; }

        public Prototype(string name, FormalArgs args, ITypeEmitter returnType, bool returnsVoid = false)
        {
            this.SetName(name);
            this.Args = args;
            this.ReturnType = returnType;
            this.ReturnsVoid = returnsVoid;
        }
    }
}
