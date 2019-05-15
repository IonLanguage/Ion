using System.Collections.Generic;

namespace Ion.Core
{
    public class ReferenceMap
    {
        protected readonly Dictionary<string, List<string>> references;

        public ReferenceMap()
        {
            this.references = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Add a 
        /// </summary>
        public bool Add(string from, string to)
        {
            // Create a new reference list if applicable.
            if (!this.references.ContainsKey(from))
            {
                // Create the new reference counter list.
                List<string> referenceCounter = new List<string>();

                // Append the new, empty reference counter list to the references.
                this.references.Add(from, referenceCounter);
            }

            // Retrieve the reference counter list.
            List<string> references = this.references[from];

            // Fail if reference already exists.
            if (references.Contains(to))
            {
                return false;
            }

            // Append the new reference.
            references.Add(to);

            // Return successfully.
            return true;
        }

        public bool Exists(string from, string to)
        {
            // Fail immediatly if the reference counter list does not exist.
            if (this.references.ContainsKey(from))
            {
                return false;
            }

            // Retrieve the reference counter list.
            List<string> referenceCounter = this.references[from];

            // Determine whether the reference exists.
            return referenceCounter.Contains(to);
        }

        // TODO: Improve algorithm, as multiple loops (with nested loops) have inefficient complexity.
        public string[] ComputeUnreferenced()
        {
            // Create the unique references buffer list.
            List<string> uniqueReferences = new List<string>();

            // Loop through all reference counters.
            foreach (List<string> referenceCounter in this.references.Values)
            {
                // Loop through all the references in the counter.
                foreach (string reference in referenceCounter)
                {
                    // Mark reference as unique if not marked beforehand.
                    if (!uniqueReferences.Contains(reference))
                    {
                        uniqueReferences.Add(reference);
                    }
                }
            }

            // Create the resulting unused references.
            List<string> result = new List<string>();

            // Loop through all the references' keys.
            foreach (string referenceKey in this.references.Keys)
            {
                // Append to the result if not present in the unique reference buffer.
                if (!uniqueReferences.Contains(referenceKey))
                {
                    result.Add(referenceKey);
                }
            }

            // Return the result as an array.
            return result.ToArray();
        }
    }
}
