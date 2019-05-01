using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using System.Text.RegularExpressions;

namespace Ion.Misc
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
            if (new Regex(@"[a-z_][A-Z0-9]").IsMatch(name))
            {
                this.Name = name;
            }
            else
            {
                throw new System.Exception("Invalid name: " + name);
            }
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
