using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using System;
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
            // Ensure name is not null nor empty.
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception("Unexpected name to be null or empty");
            }
            // Ensure identifier pattern matches provided name.
            else if (Pattern.Identifier.IsMatch(name))
            {
                this.Name = name;
            }
            // Otherwise, throw an error.
            else
            {
                throw new Exception($"Invalid name: {name}");
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
