using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class PathResult
    {
        public string FirstNode => this.nodes[0];

        public string LastNode => this.nodes[this.nodes.Count - 1];

        public readonly ReadOnlyCollection<string> nodes;

        public PathResult(List<string> nodes)
        {
            // Ensure node list contains at least one item.
            if (nodes.Count == 0)
            {
                throw new Exception("Node list parameter must contain at least one item");
            }

            this.nodes = nodes.AsReadOnly();
        }

        public PathResult(string node) : this(new List<string> { node })
        {
            //
        }

        public override string ToString()
        {
            // Creates a string represention of the path.
            string result = String.Join(Constants.PathDelimiter, nodes);

            // Return the resulting string.
            return result;
        }
    }
}
