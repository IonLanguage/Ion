using System.Collections.Generic;

namespace Ion.CodeGeneration
{
    public class StructPrototype
    {
        public List<StructProperty> Properties { get; }

        public StructPrototype()
        {
            this.Properties = new List<StructProperty>();
        }
    }
}
