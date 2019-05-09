using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Ion.Parsing
{
    public class PathResult
    {
        public readonly ReadOnlyCollection<string> nodes;

        public PathResult(List<string> nodes)
        {
            this.nodes = nodes.AsReadOnly();
        }

        public override string ToString()
        {
            // Create the string builder instance.
            StringBuilder builder = new StringBuilder();

            foreach (string node in this.nodes)
            {
                builder.Append($"{node}.");
            }

            // Retrieve the string builder's result.
            string result = builder.ToString();

            // Trim the ending, extra dot character.
            result = result.TrimEnd('.');

            // Return the resulting string.
            return result;
        }
    }
}
