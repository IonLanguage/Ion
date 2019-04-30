using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
{
    public class Prototype : Named
    {
        public FormalArgs Args { get; set; }

        public Type ReturnType { get; set; }

        public Prototype(string name, FormalArgs args, Type returnType)
        {
            this.SetName(name);
            this.Args = args;
            this.ReturnType = returnType;
        }
    }
}
