using System;
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
            // Creates a string represention of the path.
            string result = String.Join(".", nodes);

            // Return the resulting string.
            return result;
        }
    }
}
