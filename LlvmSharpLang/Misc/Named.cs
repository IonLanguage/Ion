using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.Misc
{
    public abstract class Named
    {
        public string Name { get; protected set; }

        public Named()
        {
            this.Name = SpecialName.Anonymous;
        }

        /// <summary>
        /// Sets the name and validates it.
        /// </summary>
        public void SetName(string name)
        {
            // TODO: Apply name validation here.

            this.Name = name;
        }

        /// <summary>
        /// Sets the name to the special name of
        /// anonymous.
        /// </summary>
        public void SetNameAnonymous()
        {
            this.SetName(SpecialName.Anonymous);
        }
    }
}
