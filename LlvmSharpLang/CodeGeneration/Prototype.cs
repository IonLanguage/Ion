using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
{
    public class Prototype : Named
    {
        public FormalArgs Args { get; set; }

        public Prototype(string name, FormalArgs args)
        {
            this.SetName(name);
            this.Args = args;
        }
    }
}
