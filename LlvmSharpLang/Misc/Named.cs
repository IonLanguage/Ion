using LlvmSharpLang.CodeGen.Structure;

namespace LlvmSharpLang.CodeGen
{
    public abstract class Named
    {
        public string Name { get; protected set; }

        public Named()
        {
            this.Name = SpecialName.Anonymous;
        }

        public void SetName(string name)
        {
            // TODO: Apply name validation here.

            this.Name = name;
        }
    }
}
