using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class Prototype : Named
    {
        public Prototype(string name, FormalArgs args, Type returnType)
        {
            this.SetName(name);
            this.Args = args;
            this.ReturnType = returnType;
        }

        public FormalArgs Args { get; set; }

        public Type ReturnType { get; set; }
    }
}