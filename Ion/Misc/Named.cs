using System;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Core;

namespace Ion.Misc
{
    public interface INamed
    {
        string Identifier { get; }
    }

    public abstract class Named : INamed
    {
        public string Identifier { get; protected set; }

        protected Named()
        {
            // Set anonymous name as default.
            this.SetNameAnonymous();
        }

        /// <summary>
        /// Sets the name and validates it.
        /// </summary>
        public void SetName(string name)
        {
            // Ensure name is valid.
            if (Util.ValidateIdentifier(name))
            {
                this.Identifier = name;
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
            // Retrieve name from the name counter.
            string name = NameRegister.GetAnonymous();

            // Assign the name.
            this.SetName(name);
        }

        public void EnsureNameOrThrow()
        {
            // Ensure name is not null nor empty.
            if (String.IsNullOrEmpty(this.Identifier))
            {
                throw new Exception("Unexpected name property to be null or empty");
            }
        }
    }
}
