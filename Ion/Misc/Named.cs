using System;
using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;

namespace Ion.Misc
{
    public abstract class Named
    {
        protected Named()
        {
            Name = SpecialName.Anonymous;
        }

        public string Name { get; protected set; }

        /// <summary>
        ///     Sets the name and validates it.
        /// </summary>
        public void SetName(string name)
        {
            // Ensure name is not null nor empty.
            if (string.IsNullOrEmpty(name))
                throw new Exception("Unexpected name to be null or empty");
            // Ensure identifier pattern matches provided name.
            else if (Pattern.identifier.IsMatch(name))
                Name = name;
            // Otherwise, throw an error.
            else
                throw new Exception($"Invalid name: {name}");
        }

        /// <summary>
        ///     Sets the name to the special name of
        ///     anonymous.
        /// </summary>
        public void SetNameAnonymous()
        {
            SetName(SpecialName.Anonymous);
        }
    }
}