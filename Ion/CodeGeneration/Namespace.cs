using System;
using Ion.Engine.CodeGeneration.Helpers;
using System.Collections.Generic;
using Ion.Misc;
using Ion.Parsing;

namespace Ion.CodeGeneration
{
    public class Namespace : IReaction<Module>
    {
        public readonly PathResult path;

        public Namespace(PathResult path)
        {
            this.path = path;
        }

        public void Invoke(Module context)
        {
            // Retrieve the corresponding path name.
            string name = this.path.ToString();

            // Apply the name.
            context.Identifier = name;
        }
    }
}
