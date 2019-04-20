using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
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
